namespace UserStoryEditor.WebApi.Controllers
{
    public class UserStory
    {
        public UserStory(
            string title,
            int estimate)
        {
            Title = title;
            Estimate = estimate;
        }

        public string Title { get; }

        public int Estimate { get; }
    }
}