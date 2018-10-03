namespace UserStoryEditor.Core
{
    using System;

    public class UserStoryEditor
    {
        private int numberOfStories = 0;

        public void AddUserStory(
            string title)
        {
            this.numberOfStories++;
        }

        public int GetEstimation()
        {
            return this.numberOfStories;
        }
    }
}
