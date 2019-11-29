using System;
using FluentAssertions;
using UserStoryEditor.Testing;
using Xbehave;

namespace UserStoryEditor.Core.Specs
{
    public class UserStoryPersistence
    {
        [Scenario]
        public void PersistUserStoriesOnShutdown(ProductBacklog backlog,
            ProductBacklog newBacklog, Estimate estimation)
        {
            var fake = new RealFakeRelationsPersistor();
            var estimatesFake = new RealFakeEstimatesPersistor();

            "establish a PBL".x(() =>
                backlog = new ProductBacklogBuilder()
                    .WithRealRelationsFake(fake)
                    .WithRealEstimatesFake(estimatesFake)
                    .Build());

            "adding a UserStory with Estimate".x(() =>
            {
                backlog.AddUserStory(
                    Guid.NewGuid(),
                    42.ToEstimate());
            });

            "creating a new PBL".x(() =>
                newBacklog = new ProductBacklogBuilder()
                    .WithRealRelationsFake(fake)
                    .WithRealEstimatesFake(estimatesFake)
                    .Build());

            "when getting Estimate".x(() =>
                estimation = newBacklog.GetEstimation2());

            "then the Estimate should already be available".x(() =>
                estimation.Should().Be(42.ToEstimate()));
        }
    }
}