using System;
using System.Linq;

namespace UserStoryEditor.Core.Blocks
{
    public static class StoryTreeGenerator
    {
        public static Guid[] GetLeaves(
            Guid[] userStoryIds,
            (Guid parent, Guid child)[] relations)
        {
            return userStoryIds
                .Where(s => relations.All(r => r.parent != s))
                .ToArray();
        }
    }
}