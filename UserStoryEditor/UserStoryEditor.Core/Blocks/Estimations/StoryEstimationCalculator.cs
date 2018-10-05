namespace UserStoryEditor.Core.Blocks.Estimations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public static class StoryEstimationCalculator
    {
        public static int Calculate(
            Guid[] initialLeafIds,
            (Guid userStoryId, int? estimate)[] estimates,
            (Guid parent, Guid child)[] relations,
            Strategy strategy = Strategy.Sum)
        {
            if (strategy == Strategy.Sum)
            {
                return Prune(
                        initialLeafIds,
                        estimates,
                        relations)
                    .Sum(id => estimates.Single(e => e.userStoryId == id).estimate ?? 0);
            }
            else
            {
                return 5;
            }
        }

        public static IReadOnlyCollection<Guid> Prune(
            Guid[] leafs,
            (Guid userStoryId, int? estimate)[] estimates,
            (Guid parent, Guid child)[] relations)
        {
            bool done;
            do
            {
                var offenders = leafs
                    .Where(l => estimates
                        .Contains((l, default(int?))));
                var offenderParents = relations
                    .Where(r => offenders.Contains(r.child))
                    .Select(o => o.parent)
                    .ToArray();
                var unwantedChildren = relations
                    .Where(r => offenderParents.Contains(r.parent))
                    .Select(r => r.child);
                leafs = leafs
                    .Concat(offenderParents)
                    .Except(unwantedChildren)
                    .ToArray();
                done = !offenderParents.Any();
            } while (!done);
            return leafs;
        }
    }
}