using System;
using FluentAssertions;
using UserStoryEditor.Core.Operation;
using Xbehave;

namespace UserStoryEditor.Core.Specs.Operations
{
    public class BacklogOperations
    {
        [Scenario]
        public void AddUserStory(
            Backlog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "establish a Backlog".x(()
                => backlog = new Backlog());

            "when I add a user story with an estimate".x(()
                => backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    5));

            "then the sum of all estimates should be 5".x(()
                => backlog.GetEstimation().Should().Be(5));
        }

        [Scenario]
        public void ChangUserStoryEstimation( Backlog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "Given a Backlog".x(()
                => backlog =  new Backlog());

            "And an existing UserStory".x(() =>
            {
                backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    5);
            } );

            "When the UserStory is changed".x(()
                => backlog.ChangeEstimate(userStoryId, 10));

            "Then the some of estimates should change".x(()
                => backlog.GetEstimation().Should().Be(10));
        }

        [Scenario]
        public void DeleteUserStory(
            Backlog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "Given a Backlog".x(()
                => backlog =  new Backlog());

            "And an existing  UserStory".x(() =>
            {
                backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    5);
            } );

            "When delete UserStory".x(()
                => backlog.DeleteUserStory(userStoryId));

            "Then estimation should be decreased".x(()
                => backlog.GetEstimation().Should().Be(0));
        }

        [Scenario]
        public void AddStoryAsChild(
            Backlog backlog
        )
        {
            Guid parentId = GuidGenerator.Create("1");
            Guid childId = GuidGenerator.Create("2");

            "Given a Backlog".x(()
                => backlog = new Backlog());

            "And an existing  UserStory".x(() =>
            {
                backlog.AddUserStory(
                    parentId,
                    "parent Story",
                    5);
            });

            "When Child is added to UserStory".x(()
                => backlog.AddChildStory(parentId, childId, "ChildStory", 3));

            "Then estimation is correct calculated".x(()
                => backlog.GetEstimation().Should().Be(3));
        }

        [Scenario]
        public void AddStoryAsChildWithoutEstimation(
            Backlog backlog
        )
        {
            Guid parentId = GuidGenerator.Create("1");
            Guid childId = GuidGenerator.Create("2");

            "Given a Backlog".x(()
                => backlog = new Backlog());

            "And an existing  UserStory".x(() =>
            {
                backlog.AddUserStory(
                    parentId,
                    "parent Story",
                    5);
            });

            "When Child is added to UserStory".x(()
                => backlog.AddChildStory(parentId, childId, "ChildStory", null));

            "Then estimation is correct calculated".x(()
                => backlog.GetEstimation().Should().Be(5));
        }
    }
}