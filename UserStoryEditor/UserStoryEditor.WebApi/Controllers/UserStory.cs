namespace UserStoryEditor.WebApi.Controllers
{
    public class UserStory
    {
        public UserStory(string title)
        {
            Title = title;
        }

        public string Title { get; }
    }
}