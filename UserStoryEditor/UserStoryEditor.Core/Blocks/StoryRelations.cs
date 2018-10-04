namespace UserStoryEditor.Core.Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StoryRelations
    {
        private readonly List<(Guid parent, Guid child)> relations = new List<(Guid, Guid)>();

        public void AddRelation(
            Guid parentId,
            Guid childId)
        {
            this.relations.Add((parentId, childId));
        }

        public IReadOnlyCollection<Guid> GetAllLeafs(IEnumerable<Guid> storyIds)
        {
            return storyIds.Where(s => this.relations.All(r => r.parent != s)).ToArray();
        }

        public List<(Guid parent, Guid child)> GetAll()
        {
            return this.relations;
        }
    }
}