using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStoryEditor.Core.Algorithms
{
    public static class EstimationAggregator
    {
        public static int Calculate(IEnumerable<int> estimates)
        {
            return estimates.Sum();
        }

        public static int Calculate(Guid[] storyIds,
            (Guid userStoryId, int? estimate)[] estimates,
            (Guid, Guid)[] relations)
        {
            return storyIds
                .GetLeaves(relations)
                .Prune(estimates, relations)
                .Sum(id => estimates.Single(e => e.userStoryId == id).estimate ?? 0);
        }
    }
}