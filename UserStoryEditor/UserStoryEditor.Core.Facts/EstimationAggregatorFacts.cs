using System;
using FluentAssertions;
using UserStoryEditor.Core.Algorithms;
using UserStoryEditor.Core.Blocks;
using UserStoryEditor.Core.Specs;
using Xunit;

namespace UserStoryEditor.Core.Facts
{
    public class EstimationAggregatorFacts
    {
        [Fact]
        public void ReturnsZero_WhenNoUserStories()
        {
            EstimationAggregator
                .Calculate(
                    new Guid[0],
                    new (Guid UserStoryId, int? Estimate)[0],
                    new (Guid, Guid)[0])
                .Should().Be(0);
        }

        [Fact]
        public void ReturnsEstimateOfASingleUserStory()
        {
            var userStoryId = GuidGenerator.Create("1");

            EstimationAggregator
                .Calculate(
                    new[] { userStoryId },
                    new[] { (userStoryId, (int?)5) },
                    new (Guid, Guid)[0])
                .Should().Be(5);
        }

        [Fact]
        public void ReturnsSumOfUserStories()
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");

            EstimationAggregator
                .Calculate(
                    new[] { userStoryId1, userStoryId2 },
                    new[]
                    {
                        (userStoryId1, (int?)5),
                        (userStoryId2, (int?)8)
                    },
                    new (Guid, Guid)[0])
                .Should().Be(13);
        }

        [Fact]
        public void ReturnsSumOfLeafUserStories()
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");
            var userStoryId3 = GuidGenerator.Create("3");

            EstimationAggregator
                .Calculate(
                    new[] { userStoryId1, userStoryId2, userStoryId3 },
                    new[]
                    {
                        (userStoryId1, (int?)5),
                        (userStoryId2, (int?)8),
                        (userStoryId3, (int?)3)
                    },
                    new []
                    {
                        (userStoryId1, userStoryId2),
                        (userStoryId1, userStoryId3)
                    })
                .Should().Be(11);
        }
    }
}