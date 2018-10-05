using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStoryEditor.Core.Algorithms
{
    public static class StoryTreePruner
    {
        public static IReadOnlyCollection<Guid> Prune(
            this Guid[] leafs,
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
                var unwantedChildren = FindDeepChildren(offenderParents, relations);
                leafs = leafs
                    .Concat(offenderParents)
                    .Except(unwantedChildren)
                    .ToArray();
                done = !offenderParents.Any();
            } while (!done);
            return leafs;
        }

        private static IEnumerable<Guid> FindDeepChildren(
            Guid[] offenderParents,
            (Guid parent, Guid child)[] relations)
        {
            var offenderChildren = relations
                .Where(r => offenderParents.Contains(r.parent))
                .Select(r => r.child)
                .ToArray();
            return offenderChildren.Any()
                ? offenderChildren.Concat(FindDeepChildren(offenderChildren, relations))
                : offenderChildren;
        }
    }
}