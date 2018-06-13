using System.Collections.Generic;
using R3MUS.Devpack.ESI.Models.Skills;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public interface IAccountStatusHelper
    {
        string GetAccountStatus(List<SkillQueueItem> trainingQueue);
    }
}