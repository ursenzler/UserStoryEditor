using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStoryEditor.Core
{
    public class Summer
    {
        public Estimate GetEstimate(
            IReadOnlyCollection<UserStory> roots,
            IReadOnlyDictionary<Guid, Estimate> estimates)
        {
            var result = roots
                .Select(root => GetEstimate(root, estimates))
                .Where(x => x.HasValue) // not tested! booh!
                .Sum(x => x!.Value);

            return new Estimate(result);
        }



        private int? GetEstimate(UserStory userStory,
            IReadOnlyDictionary<Guid, Estimate> estimates)
        {
            if (userStory.Children.Any())
            {
                var childEstimates = userStory
                    .Children
                    .Select(
                        child => GetEstimate(child, estimates))
                    .ToArray();

                if (childEstimates.All(x => x.HasValue))
                {
                    return childEstimates
                        .Sum(x => x!.Value);
                }
            }

            return
                estimates.ContainsKey(userStory.Id) ? estimates[userStory.Id].Value : default(int?);
        }


    }
}