namespace UserStoryEditor.Core.Blocks.StoryRelations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class StoryRelations
    {
        private IStoryRelationsPersistor persistor;

        public StoryRelations(
            IStoryRelationsPersistor persistor)
        {
            this.persistor = persistor;
        }

        public void AddRelation(
            Guid parentId,
            Guid childId)
        {
            this.persistor.Add(parentId, childId);
        }

        public IReadOnlyCollection<Guid> GetAllLeafs(IEnumerable<Guid> storyIds)
        {
            return storyIds
                .Where(s => this.persistor.GetAll().All(r => r.parent != s))
                .ToArray();
        }

        public IReadOnlyCollection<(Guid parent, Guid child)> GetAll()
        {
            return this.persistor.GetAll();
        }

        public IReadOnlyCollection<Guid> GetChildIds(
            Guid userStoryId)
        {
            Guid[] childIds = this.persistor
                .GetAll()
                .Where(x => x.parent == userStoryId)
                .Select(x => x.child).ToArray();

            return childIds
                .SelectMany(this.GetChildIds)
                .Union(childIds)
                .ToArray();
        }
    }
}