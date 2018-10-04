namespace UserStoryEditor.Core
{
    using System;

    public class UserStory
    {
        public Guid Id { get; }
        public string Title { get; }

        public UserStory(
            Guid id,
            string title)
        {
            this.Id = id;
            this.Title = title;
        }
    }
}