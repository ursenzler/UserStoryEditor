using System.Collections.Generic;

namespace UserStoryEditor.Core
{
    public interface IRelationsPersistor
    {
        IReadOnlyCollection<UserStory> GetAll();

        void Store(IReadOnlyCollection<UserStory> userStories);
    }
}