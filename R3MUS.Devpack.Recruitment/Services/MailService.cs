using AutoMapper;
using R3MUS.Devpack.ESI.Extensions;
using R3MUS.Devpack.Recruitment.Models;
using R3MUS.Devpack.Recruitment.Properties;
using R3MUS.Devpack.Recruitment.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace R3MUS.Devpack.Recruitment.Services
{
    public class MailService : IMailService
    {
        private readonly IMapper _mapper;
        private readonly IRecruitRepository _recruitRepository;
        private readonly IESIEndpointRepository _esiRepository;

        public MailService(IMapper mapper, IRecruitRepository recruitRepository, IESIEndpointRepository esiRepository)
        {
            _mapper = mapper;
            _recruitRepository = recruitRepository;
            _esiRepository = esiRepository;
        }

        public List<MailSummaryModel> GetMailHeaders(long characterId)
        {
            var character = new ESI.Models.Character.Detail() { Id = characterId };
            var endpoint = _esiRepository.GetByName(Resources.ApplicantEndpointName);

            var token = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey,
                _recruitRepository.GetRefreshTokenForApplicant(characterId));

            return _mapper.Map<List<MailSummaryModel>>(character.GetMails(token.AccessToken));
        }
        
        public MailModel GetMailDetail(MailModel request)
        {
            var character = new ESI.Models.Character.Detail() { Id = request.OwnerId };
            var endpoint = _esiRepository.GetByName(Resources.ApplicantEndpointName);

            var token = ESI.SingleSignOn.GetTokensFromRefreshToken(endpoint.ClientId, endpoint.SecretKey, 
                _recruitRepository.GetRefreshTokenForApplicant(request.OwnerId));
            request.Body = character.GetMail(request.Id, token.AccessToken).Body;

            return request;
        }
    }
}