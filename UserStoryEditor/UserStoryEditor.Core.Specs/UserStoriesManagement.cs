using System;
using FluentAssertions;
using UserStoryEditor.Testing;
using Xbehave;

namespace UserStoryEditor.Core.Specs
{
    public class UserStoriesManagement
    {
        private ProductBacklog backlog = default!;

        [Background]
        public void EstablishScenario()
        {
            "establish a PBL".x(() =>
                this.backlog = new ProductBacklogBuilder().Build());
        }

        [Scenario]
        public void AddUserStory()
        {
            "when adding a user story".x(() =>
                backlog.AddUserStory(Guid.NewGuid(), 17.ToEstimate()));

            "the sum of all estimates should be increased by the user story estimate".x(() =>
                backlog.GetEstimation()
                    .Should().Be(17.ToEstimate()));
        }

        [Scenario]
        public void ChangeUserStoryEstimate()
        {
            var id = Guid.NewGuid();

            "when adding a user story".x(() =>
                backlog.AddUserStory(
                    id,
                    17.ToEstimate()));

            "and then changing the stories estimate".x(() =>
                backlog.ChangeUserStoryEstimate(
                    id,
                    14.ToEstimate()));

            "the sum of all estimates should be adapted by the user story updated estimate".x(() =>
                backlog.GetEstimation()
                    .Should().Be(14.ToEstimate()));
        }

        [Scenario]
        public void DeleteUserStoryEstimate()
        {
            var id = Guid.NewGuid();

            "add 2 User Stories".x(() =>
                {
                    backlog.AddUserStory(
                        id,
                        17.ToEstimate());
                    backlog.AddUserStory(
                        new Guid(),
                        42.ToEstimate());
                }
            );

            "when removing the first story".x(() =>
                backlog.RemoveUserStory(
                    id));

            "the sum of all estimates should be equal to the leftover stories".x(() =>
                backlog.GetEstimation()
                    .Should().Be(42.ToEstimate()));
        }

        [Scenario]
        public void AStoryWithAChildHasEstimateOfSumOfChildren()
        {
            var id = Guid.NewGuid();

            "when adding a user story".x(() =>
                backlog.AddUserStory(
                    id,
                    17.ToEstimate()));

            "when adding a child user story".x(() =>
                backlog.AddChildUserStory(
                    id,
                    Guid.NewGuid(),
                    42.ToEstimate()));

            "then the estimate of all user stories should be the sum of children estimates".x(() =>
                backlog.GetEstimation()
                    .Should().Be(42.ToEstimate()));
        }

        [Scenario]
        public void AStoryWithAChildWhichHasNoEstimateTakeEstimateFromParent()
        {
            var id = Guid.NewGuid();

            "when adding a user story".x(() =>
                backlog.AddUserStory(
                    id,
                    17.ToEstimate()));

            "when adding a child user story".x(() =>
                backlog.AddChildUserStory(
                    id,
                    Guid.NewGuid()));

            "then estimate of all stories is parent".x(() =>
                backlog.GetEstimation()
                    .Should()
                    .Be(17.ToEstimate()));
        }

        [Scenario]
        public void TestRemovingAParentStoryWithChildren()
        {
            var id = Guid.NewGuid();

            "when adding a user story".x(() =>
                backlog.AddUserStory(
                    id,
                    17.ToEstimate()));

            "when adding a child user story".x(() =>
                backlog.AddChildUserStory(
                    id,
                    Guid.NewGuid(),
                    3.ToEstimate()));

            "when removing the parent story".x(() =>
                backlog.RemoveUserStory(id));

            "then estimate of all stories should be zero".x(() =>
                backlog.GetEstimation()
                    .Should()
                    .Be(0.ToEstimate()));
        }

        [Scenario]
        public void CalculateEstimatesUsingMaxima()
        {
            var id = Guid.NewGuid();

            "when adding a user story with children".x(() =>
            {
                backlog.AddUserStory(
                    id,
                    17.ToEstimate());

                backlog.AddChildUserStory(
                    id,
                    Guid.NewGuid(),
                    3.ToEstimate());

                backlog.AddChildUserStory(
                    id,
                    Guid.NewGuid(),
                    5.ToEstimate());
            });

            "using maxima sum the estimate of all stories should be 17".x(() =>
                backlog.GetMaximaEstimation()
                    .Should()
                    .Be(17.ToEstimate()));
        }
    }
}