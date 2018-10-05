using System;
using FluentAssertions;
using UserStoryEditor.Core.Blocks;
using Xbehave;

namespace UserStoryEditor.Core.Specs.Blocks
{
    public class UserStoriesBlock
    {
        [Scenario]
        public void AddUserStory(
            UserStories userStories)
        {
            var userStoryId = GuidGenerator.Create("1");

            "establish user stories management".x(()
                => userStories = new UserStories());

            "when I add a user story".x(()
                => userStories.AddUserStory(
                    userStoryId,
                    "some title"));

            "then the list of all user stories should contain the added user story".x(()
                => userStories.GetAll()
                    .Should().BeEquivalentTo(
                        new UserStory(
                            userStoryId,
                            "some title")));
        }

        [Scenario]
        public void DeleteUserStory(
            UserStories userStories)
        {
            var userStoryId = GuidGenerator.Create("1");

            "establish user stories management".x(()
                => userStories = new UserStories());

            "there is a user story in the backlog".x(()
                => userStories.AddUserStory(
                    userStoryId,
                    "some title"));

            "when I delete the user story".x(()
                => userStories.DeleteUserStory(
                    userStoryId));

            "then the list of all user stories should not contain the added user story anymore".x(()
                => userStories.GetAll()
                    .Should().BeEmpty());
        }

        [Scenario]
        public void DeleteNonExistingUserStory(
            UserStories userStories)
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");

            "establish user stories management".x(()
                => userStories = new UserStories());

            "there is a user story in the backlog".x(()
                => userStories.AddUserStory(
                    userStoryId1,
                    "some title"));

            "when I delete the non-existing user story".x(()
                => userStories.DeleteUserStory(
                    userStoryId2));

            "then the list of all user stories should not contain the same user stories as before".x(()
                => userStories.GetAll()
                    .Should().BeEquivalentTo(
                        new UserStory(
                            userStoryId1,
                            "some title")));
        }

        [Scenario]
        public void ReplaceAUserStory(
            UserStories userStories)
        {
            Guid userStoryId = Guid.NewGuid();

            "establish user stories management".x(()
                => userStories = new UserStories());

            "there is a user story in the backlog".x(()
                => userStories.AddUserStory(
                    userStoryId,
                    "some title"));

            "when I replace the user story".x(()
                => userStories.AddUserStory(
                    userStoryId,
                    "some new title"));

            "then the list of all user stories should contain the updated user story".x(()
                => userStories.GetAll()
                    .Should().BeEquivalentTo(
                        new UserStory(
                            userStoryId,
                            "some new title")));
        }
    }
}