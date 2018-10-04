namespace UserStoryEditor.Core.Blocks
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class UserStories
    {
        private readonly List<UserStory> stories  = new List<UserStory>();

        public void AddUserStory(
            Guid id,
            string title)
        {
            this.stories.RemoveAll(x => x.Id == id);

            this.stories.Add(new UserStory(id, title));
        }

        public void DeleteUserStory(
            Guid userStoryId)
        {
            this.stories.RemoveAll(x => x.Id == userStoryId);
        }

        public IReadOnlyCollection<UserStory> GetAll()
        {
            return this.stories;
        }
    }
}