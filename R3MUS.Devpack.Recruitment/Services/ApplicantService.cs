using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Character;
using R3MUS.Devpack.ESI.Models.Mail;
using R3MUS.Devpack.ESI.Models.Shared;
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

        public ApplicantViewModel GetCharacterViewModel(long id)
        {
            var character = new ESI.Models.Character.Detail(id);
            var endpoint = _esiRepository.GetByName("Applicant");
            var accessToken = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(id));
            
            var result = new ApplicantViewModel
            {
                Applicant = _mapper.Map<CharacterModel>(character),
                Contacts = _mapper.Map<List<CharacterContactModel>>(character.GetContacts(accessToken.AccessToken))
            };
            result.Applicant.EmploymentHistory = _mapper.Map<List<CorporationModel>>(character.GetEmploymentHistory());
            result.Applicant.AccountStatus = _accountStatusHelper.GetAccountStatus(character.GetTrainingQueue(accessToken.AccessToken));
            
            var idList = new IdList { Ids = result.Applicant.EmploymentHistory.Select(s => s.Id).Distinct().ToList() };
            var corporationsSummary = idList.GetCorporationNames();

            result.Applicant.EmploymentHistory.ForEach(corporation => 
                corporation.Name = corporationsSummary.CorporationDetail.First(w => w.Id == corporation.Id).Name
            );
            result.Applicant.Corporation = result.Applicant.EmploymentHistory.First(w => w.Id == character.CorporationId);
            result.Applicant.Alliance = AllianceModel.GetAllianceInfo(character.AllianceId);

            return result;
        }
    }
}