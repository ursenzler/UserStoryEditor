using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStoryEditor.Core
{
    public class MaximaSummer
    {
        public Estimate GetMaximaEstimate(
            IReadOnlyCollection<UserStory> roots,
            IReadOnlyDictionary<Guid, Estimate> estimates)
        {
            var result = roots
                .Select(root => GetEstimateMaxima(root, estimates))
                .Where(x => x.HasValue) // not tested! booh!
                .Sum(x => x!.Value);

            return new Estimate(result);
        }
        private int? GetEstimateMaxima(UserStory userStory,
            IReadOnlyDictionary<Guid, Estimate> estimates)
        {
            var childEstimates = userStory
                .Children
                .Select(
                    child => GetEstimateMaxima(child, estimates))
                .ToArray();

            return
                Math.Max(
                    estimates.ContainsKey(userStory.Id) ? estimates[userStory.Id].Value : 0,
                    childEstimates
                        .Sum(x => x ?? 0));
        }
    }
}