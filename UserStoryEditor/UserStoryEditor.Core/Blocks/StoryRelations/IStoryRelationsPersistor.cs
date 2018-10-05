namespace UserStoryEditor.Core.Blocks.StoryRelations
{
    using System;
    using System.Collections.Generic;

    public interface IStoryRelationsPersistor
    {
        void Add(
            Guid parent,
            Guid child);

        IReadOnlyCollection<(Guid parent, Guid child)> GetAll();
    }
}