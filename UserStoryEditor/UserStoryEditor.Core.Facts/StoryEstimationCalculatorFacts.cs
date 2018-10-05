using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UserStoryEditor.Core.Blocks;
using UserStoryEditor.Core.Specs;
using Xunit;

namespace UserStoryEditor.Core.Facts
{
    public class StoryEstimationCalculatorFacts
    {
        [Fact]
        public void ReturnsZero_WhenNoUserStories()
        {
            StoryEstimationCalculator.Calculate(
                new Guid[0],
                new (Guid UserStoryId, int? Estimate)[0])
                .Should().Be(0);
        }

        [Fact]
        public void ReturnsEstimateOfASingleUserStory()
        {
            var userStoryId = GuidGenerator.Create("1");

            StoryEstimationCalculator
                .Calculate(
                    new[] { userStoryId },
                new[] { (userStoryId, (int?)5) })
                .Should().Be(5);
        }

        [Fact]
        public void ReturnsSumOfUserStories()
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");

            StoryEstimationCalculator
                .Calculate(
                    new[] { userStoryId1, userStoryId2 },
                new[]
                    {
                        (userStoryId1, (int?)5),
                        (userStoryId2, (int?)8)
                    })
                .Should().Be(13);
        }

        [Fact]
        public void ReturnsSumOfLeafUserStories()
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");
            var userStoryId3 = GuidGenerator.Create("3");

            StoryEstimationCalculator
                .Calculate(
                    new[] { userStoryId2, userStoryId3 },
                new[]
                    {
                        (userStoryId1, (int?)5),
                        (userStoryId2, (int?)8),
                        (userStoryId3, (int?)3)
                    })
                .Should().Be(11);
        }
    }
}