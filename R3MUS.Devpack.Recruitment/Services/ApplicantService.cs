using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Character;
using R3MUS.Devpack.ESI.Models.Mail;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.ESI.Models.Universe;
using R3MUS.Devpack.Recruitment.Extensions;
using R3MUS.Devpack.Recruitment.Helpers;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories;
using R3MUS.Devpack.Recruitment.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Services
{
    public class ApplicantService : IApplicantService
    {
        private readonly IMapper _mapper;
        private readonly IRecruitRepository _recruitRepository;
        private readonly IESIEndpointRepository _esiRepository;
        private readonly IAccountStatusHelper _accountStatusHelper;
        private readonly IMailService _mailService;
        
        public ApplicantService(IMapper mapper, IRecruitRepository recruitRepository, IESIEndpointRepository esiRepository,
            IAccountStatusHelper accountStatusHelper, IMailService mailService)
        {
            _mapper = mapper;
            _recruitRepository = recruitRepository;
            _esiRepository = esiRepository;
            _accountStatusHelper = accountStatusHelper;
            _mailService = mailService;
        }

        public List<ContactViewModel> GetContacts(long id)
        {
            var character = new ESI.Models.Character.Detail(id);

            var endpoint = _esiRepository.GetByName("Applicant");
            var accessToken = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(id));
            var contacts = character.GetContacts(accessToken.AccessToken);

            var characterContacts = _mapper.Map<List<CharacterContactModel>>(contacts.Where(w => w.ContactType == "character"));
            var corporationContacts = _mapper.Map<List<CorporationContactModel>>(contacts.Where(w => w.ContactType == "corporation"));
            var allianceContacts = _mapper.Map<List<AllianceContactModel>>(contacts.Where(w => w.ContactType == "alliance"));

            return _mapper.Map<List<ContactViewModel>>(characterContacts)
                .Concat(_mapper.Map<List<ContactViewModel>>(corporationContacts))
                .Concat(_mapper.Map<List<ContactViewModel>>(allianceContacts)).OrderByDescending(o => o.Standing).ToList();
        }
        
        public ApplicantViewModel GetCharacterViewModel(long id)
        {
            var character = new ESI.Models.Character.Detail(id);
            var endpoint = _esiRepository.GetByName("Applicant");
            var accessToken = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(id));
            var result = new ApplicantViewModel
            {
                Applicant = _mapper.Map<CharacterModel>(character)
            };

            result.Applicant.HomeStation = GetHomeStation(character.Id);
            result.Applicant.EmploymentHistory = _mapper.Map<List<CorporationModel>>(character.GetEmploymentHistory());
            result.Applicant.AccountStatus = _accountStatusHelper.GetAccountStatus(character.GetTrainingQueue(accessToken.AccessToken));
            result.Applicant.SkillPoints = character.GetTrainedSkills(accessToken.AccessToken).TotalSkillPoints;
            
            var idList = new IdList { Ids = result.Applicant.EmploymentHistory.Select(s => s.Id).Distinct().ToList() };
            var corporationsSummary = idList.GetCorporationNames();

            result.Applicant.EmploymentHistory.ForEach(corporation => 
                corporation.Name = corporationsSummary.CorporationDetail.First(w => w.Id == corporation.Id).Name
            );
            result.Applicant.Corporation = result.Applicant.EmploymentHistory.First(w => w.Id == character.CorporationId);
            result.Applicant.Alliance = AllianceModel.GetAllianceInfo(character.AllianceId);

            if(id != SSOUserManager.SiteUser.Character.Id)
            {
                result.Applicant.CurrentStatus = _recruitRepository.GetCurrentStatus(new CorporationAuthorisationModel() { CorporationId = SSOUserManager.SiteUser.CorporationId, RecruitId = result.Applicant.Id });
            }

            return result;
        }

        public string GetHomeStation(long id)
        {
            var character = new ESI.Models.Character.Detail(id);
            var endpoint = _esiRepository.GetByName("Applicant");
            var accessToken = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(id));
            var homeLocation = character.GetCloneInformation(accessToken.AccessToken).HomeStation;
            if(homeLocation.Type == "structure")
            {
                return new Structure(homeLocation.Id, accessToken.AccessToken).Name;
            }
            else
            {
                return new Station(homeLocation.Id).Name;
            }
        }

        public List<WalletJournalViewModal> GetWalletJournal(long id)
        {
            var character = new ESI.Models.Character.Detail(id);
            var endpoint = _esiRepository.GetByName("Applicant");
            var accessToken = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(id));

            return _mapper.Map<List<WalletJournalViewModal>>(character.GetWalletJournal(accessToken.AccessToken)
                .Where(w => w.ReferenceType == "player_donation" || w.ReferenceType == "corporation_account_withdrawal").ToList());
        }

        public List<WalletTransactionViewModel> GetWalletTransactions(long id)
        {
            var character = new ESI.Models.Character.Detail(id);
            var endpoint = _esiRepository.GetByName("Applicant");
            var accessToken = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(id));

            var results = _mapper.Map<List<WalletTransactionViewModel>>(character.GetWalletTransactions(accessToken.AccessToken));

            var clientIdList = new IdList() { Ids = results.Select(s => s.ClientId).Distinct().ToList() };
            var itemIdList = new IdList() { Ids = results.Select(s => (long)s.ItemTypeId).Distinct().ToList() };

            var clientInfo = clientIdList.GetEntityNames();
            var itemInfo = new List<ItemType>();
            itemIdList.Ids.ForEach(f => itemInfo.Add(new ItemType(f)));

            results.ForEach(f => {
                f.ClientName = clientInfo.First(w => w.Id == f.ClientId).Name;
                f.ItemTypeName = itemInfo.First(w => w.Id == f.ItemTypeId).Name;
            });

            return results;
        }
    }
}