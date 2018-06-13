using System;
using System.Collections.Generic;
using System.Linq;

namespace R3MUS.Devpack.Recruitment.Helpers
{
    public class AccountStatusHelper : IAccountStatusHelper
    {
        private const string _queueEmpty = "Training Queue is empty - cannot determine account status";
        private const string _queuePaused = "Training Queue is paused - cannot determine account status";
        private const string _alpha = "Alpha (based on training queue Sp / min)";
        private const string _omega = "Omega (based on training queue Sp / min)";

        public string GetAccountStatus(List<ESI.Models.Skills.SkillQueueItem> trainingQueue)
        {
            if (!trainingQueue.Any())
            {
                return _queueEmpty;
            }
            else if (trainingQueue.First().StartDate == DateTime.MinValue)
            {
                return _queuePaused;
            }

            var correlation = new List<KeyValuePair<long, TimeSpan>>();

            trainingQueue.ForEach(item =>
                correlation.Add(new KeyValuePair<long, TimeSpan>(
                    item.LevelEndSp - item.LevelStartSp,
                    item.EndDate - item.StartDate
                    ))
            );
            var totalSp = correlation.Sum(s => s.Key);
            var totalTime = correlation.Sum(s => s.Value.TotalMinutes);
            if (totalSp / totalTime < 20)
            {
                return _alpha;
            }
            return _omega;
        }
    }
}