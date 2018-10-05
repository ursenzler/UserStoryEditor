namespace UserStoryEditor.Core.Specs.Operations
{
    using System;
    using FluentAssertions;
    using UserStoryEditor.Core.Blocks.Estimations;
    using UserStoryEditor.Core.Operation;
    using UserStoryEditor.Core.Operation.Backlogs;
    using Xbehave;

    public class UserStories
    {
        private RootFactory rootFactory;

        [Background]
        public void Background()
        {
            "establish a factory".x(()
                => this.rootFactory = new RootFactory());
        }

        [Scenario]
        public void AddUserStory(
            IBacklog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "establish a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "when I add a user story with an estimate".x(()
                => backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    5));

            "then the list of all user stories contains the added user story".x(()
                => backlog.GetAllUserStories().Should().BeEquivalentTo(
                    new UserStory(
                        userStoryId,
                        "some title")));

            Check
                .Using(() => this.rootFactory)
                .AuditTrailHasAddedUserStory(
                    userStoryId);
        }

        [Scenario]
        public void ChangUserStoryEstimation(
            IBacklog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "Given a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "And an existing UserStory".x(() =>
            {
                backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    5);
            });

            "When the UserStory is changed".x(()
                => backlog.ChangeEstimate(userStoryId, 10));

            "Then the some of estimates should change".x(()
                => backlog.GetEstimation(Strategy.Sum).Should().Be(10));
        }

        [Scenario]
        public void DeleteUserStory(
            IBacklog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "Given a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "And an existing  UserStory".x(() =>
            {
                backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    5);
            });

            "When delete UserStory".x(()
                => backlog.DeleteUserStory(userStoryId));

            "Then estimation should be decreased".x(()
                => backlog.GetEstimation(Strategy.Sum).Should().Be(0));

            Check
                .Using(() => this.rootFactory)
                .AuditTrailHasDeletedUserStory(
                    userStoryId);
        }

        [Scenario]
        public void AddStoryAsChild(
            IBacklog backlog
        )
        {
            Guid parentId = GuidGenerator.Create("1");
            Guid childId = GuidGenerator.Create("2");

            "Given a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

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
                => backlog.GetEstimation(Strategy.Sum).Should().Be(3));

            Check
                .Using(() => this.rootFactory)
                .AuditTrailHasAddedUserStory(
                    childId);
        }

        [Scenario]
        public void AddStoryAsChildWithoutEstimation(
            IBacklog backlog
        )
        {
            Guid parentId = GuidGenerator.Create("1");
            Guid childId = GuidGenerator.Create("2");

            "Given a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

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
                => backlog.GetEstimation(Strategy.Sum).Should().Be(5));
        }

        [Scenario]
        public void DeleteParentUserStory(
            IBacklog backlog)
        {
            Guid parentId = GuidGenerator.Create("1");
            Guid childId = GuidGenerator.Create("2");

            "Given a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "And an existing  UserStory".x(() =>
            {
                backlog.AddUserStory(
                    parentId,
                    "parent Story",
                    5);
            });

            "And Child is added to UserStory".x(()
                => backlog.AddChildStory(parentId, childId, "ChildStory", 3));

            "When parent is deleted".x(()
                => backlog.DeleteUserStory(parentId));

            "Then the total estimation is correct".x(()
                => backlog.GetEstimation(Strategy.Sum).Should().Be(0));
        }

        [Scenario]
        public void AddUserStoryStragegyMax(
            IBacklog backlog)
        {
            Guid userStoryId = Guid.NewGuid();

            "establish a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "when I add a user story with an estimate".x(()
                => backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    3));

            "then the sum of all estimates should be 5".x(()
                => backlog.GetEstimation(Strategy.Max).Should().Be(5));

            Check
                .Using(() => this.rootFactory)
                .AuditTrailHasAddedUserStory(
                    userStoryId);
        }
    }
}