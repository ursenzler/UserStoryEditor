namespace UserStoryEditor.Core.Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StoryEstimationCalculator
    {
        public static int Calculate(IEnumerable<int> estimates)
        {
            return estimates.Sum();
        }

        public static int Calculate(
            Guid[] prunedStoryIds,
            (Guid userStoryId, int? estimate)[] estimates)
        {
            return prunedStoryIds
                .Sum(id => estimates.Single(e => e.userStoryId == id).estimate ?? 0);
        }
    }
}