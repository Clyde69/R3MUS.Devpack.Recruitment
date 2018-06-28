using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.ESI.Models.Shared;
using R3MUS.Devpack.Recruitment.Enums;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Repositories;
using R3MUS.Devpack.Recruitment.ViewModels;
using System;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Services
{
    public class ScreeningService : IScreeningService
    {
        private readonly IScreenerRepository _screenerRepository;
        private readonly IApplicantService _applicantService;

        public ScreeningService(IScreenerRepository screenerRepository, IApplicantService applicantService)
        {
            _screenerRepository = screenerRepository;
            _applicantService = applicantService;
        }

        public ScreenerSummaryViewModel GetApplicantList(SSOApplicationUser currentUser)
        {
            var corpApplicantIds = _screenerRepository.GetCorporationApplicants(currentUser.CorporationId).Select(s => s.CharacterId);
            var allianceApplicantIds = _screenerRepository.GetAllianceApplicants(currentUser.AllianceId).Select(s => s.CharacterId);

            var idList = new IdList()
            {
                Ids = corpApplicantIds.Concat(allianceApplicantIds).Distinct().ToList()
            };
            if (idList.Ids.Count > 0)
            {
                var models = idList.GetCharacterNames();
                return new ScreenerSummaryViewModel()
                {
                    CorporateApplications = models.CharacterDetail.Where(w => corpApplicantIds.Contains(w.Id)
                        && !w.Id.Equals(currentUser.CorporationId)).ToList(),
                    AllianceApplications = models.CharacterDetail.Where(w => allianceApplicantIds.Contains(w.Id)
                        && !w.Id.Equals(currentUser.AllianceId)).ToList()
                };
            }
            return new ScreenerSummaryViewModel()
            {
                CorporateApplications = new System.Collections.Generic.List<ESI.Models.Character.Summary>(),
                AllianceApplications = new System.Collections.Generic.List<ESI.Models.Character.Summary>()
            };
        }

        public ApplicantViewModel GetApplicantDetails(SSOApplicationUser currentUser, long characterId)
        {
            if (!_screenerRepository.GetCorporationApplicants(currentUser.CorporationId).Any(w => w.CharacterId == characterId)
                && !_screenerRepository.GetAllianceApplicants(currentUser.AllianceId).Any(w => w.CharacterId == characterId))
            {
                throw new UnauthorizedAccessException();
            }
            return _applicantService.GetCharacterViewModel(characterId);
        }

        public bool ChangeStatus(CorporationStatusChangeModel request)
        {
            var status = (ApplicationStatus)Enum.Parse(typeof(ApplicationStatus), request.Status);
            _screenerRepository.ChangeApplicantStatus(request.RecruitId, request.CorporationId, (int)status);
            return true;
        }
    }
}