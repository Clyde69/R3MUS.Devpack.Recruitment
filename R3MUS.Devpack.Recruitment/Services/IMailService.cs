using System.Collections.Generic;
using R3MUS.Devpack.Recruitment.Models;

namespace R3MUS.Devpack.Recruitment.Services
{
    public interface IMailService
    {
        List<MailSummaryModel> GetMailHeaders(long characterId);
        MailModel GetMailDetail(MailModel request);
    }
}