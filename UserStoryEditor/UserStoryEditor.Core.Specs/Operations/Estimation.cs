namespace UserStoryEditor.Core.Specs.Operations
{
    using System;
    using FluentAssertions;
    using UserStoryEditor.Core.Blocks.Estimations;
    using UserStoryEditor.Core.Operation;
    using UserStoryEditor.Core.Operation.Backlogs;
    using Xbehave;

    public class Estimation
    {
        private RootFactory rootFactory;

        [Background]
        public void Background()
        {
            "establish a factory".x(()
                => this.rootFactory = new RootFactory());
        }

        [Scenario]
        public void EstimateWithSumStrategy(
            IBacklog backlog,
            int estimate)
        {
            Guid userStoryId = Guid.NewGuid();

            "establish a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "establish a user story with an estimate".x(()
                => backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    3));

            "when the sum of all estimates is calculated".x(()
                => estimate = backlog.GetEstimation(Strategy.Sum));

            "it should be calculated by using the estimates of non-null children".x(()
                => estimate.Should().Be(3));
        }

        [Scenario]
        public void EstimateWithMaxStrategy(
            IBacklog backlog,
            int estimate)
        {
            Guid userStoryId = Guid.NewGuid();

            "establish a Backlog".x(()
                => backlog = this.rootFactory.CreateBacklogOperations());

            "establish a user story with an estimate".x(()
                => backlog.AddUserStory(
                    userStoryId,
                    "some title",
                    3));

            "when the sum of all estimates is calculated".x(()
                => estimate = backlog.GetEstimation(Strategy.Max));

            "it should be calculated by using the max estimates of parent and children".x(()
                => estimate.Should().Be(5));
        }
    }
}