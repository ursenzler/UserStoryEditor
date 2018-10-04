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
            Guid[] userStoryIds,
            (Guid UserStoryId, int? Estimate)[] estimates,
            (Guid Parent, Guid Child)[] relations)
        {
            return userStoryIds
                .Where(
                    x => relations.All(r => x != r.Parent)
                         || relations
                             .Where(r => r.Parent == x)
                             .Select(r => r.Child)
                             .Select(child => estimates
                                 .Single(y => y.UserStoryId == child)
                                 .Estimate)
                             .Any(u => !u.HasValue))
                .Aggregate(
                    0,
                    (v, a) => v += estimates.Single(x => x.UserStoryId == a).Estimate ?? 0);
        }
    }
}