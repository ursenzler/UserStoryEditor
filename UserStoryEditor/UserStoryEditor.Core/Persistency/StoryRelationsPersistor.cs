namespace UserStoryEditor.Core.Persistency
{
    using System;
    using System.Collections.Generic;
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Blocks.StoryRelations;

    public class StoryRelationsPersistor : IStoryRelationsPersistor
    {
        private readonly List<(Guid parent, Guid child)> relations = new List<(Guid, Guid)>();

        public void Add(
            Guid parent,
            Guid child)
        {
            this.relations.Add((parent, child));
        }

        public IReadOnlyCollection<(Guid parent, Guid child)> GetAll()
        {
            return this.relations;
        }
    }
}