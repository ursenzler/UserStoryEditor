namespace UserStoryEditor.Core.Facts
{
    using System;
    using System.Linq;
    using FluentAssertions;
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Specs;
    using Xunit;

    public class StoryEstimationCalculatorFacts
    {
        [Fact]
        public void ReturnsZero_WhenNoUserStories()
        {
            StoryEstimationCalculator.Calculate(
                new Guid[0],
                new (Guid UserStoryId, int? Estimate)[0],
                new (Guid Parent, Guid Child)[0])
                .Should().Be(0);
        }

        [Fact]
        public void ReturnsEstimateOfASingleUserStory()
        {
            var userStoryId = GuidGenerator.Create("1");

            StoryEstimationCalculator
                .Calculate(
                    new[] { userStoryId },
                    new[] { (userStoryId, (int?)5) },
                    new (Guid Parent, Guid Child)[0])
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
                    },
                    new (Guid Parent, Guid Child)[0])
                .Should().Be(13);
        }

        [Fact]
        public void ReturnsSumOfLeafUserStories()
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
                    },
                    new[]
                    {
                        (userStoryId1, userStoryId2)
                    })
                .Should().Be(8);
        }

        [Theory]
        [InlineData("A;1", "A5;1-", "A1", 5)]
        [InlineData("A;1;2", "A5;13;28", "A1;A2", 5)]
        [InlineData("A;1;2", "A5;13;28", "A1;A2", 11)]
        [InlineData("A;1;2;8;9", "A5;13;28;84;96", "A1;A2;18;19", 18)]
        [InlineData("A;1;2;8;9", "A5;13;28;8-;96", "A1;A2;18;19", 11)]
        public void Mist(
            string stringishStories,
            string stringishEstimates,
            string stringishRelations,
            int expectedEstimate)
        {
            var userStories = stringishStories
                .Split(';')
                .Select(GuidGenerator.Create)
                .ToArray();

            var estimates = stringishEstimates
                .Split(';')
                .Select(x =>
                    (
                        GuidGenerator.Create(x[0].ToString()),
                        x[1] == '-' ? default(int?) : int.Parse(x[1].ToString())))
                .ToArray();

            var relations = stringishRelations
                .Split(';')
                .Select(x =>
                    (
                        GuidGenerator.Create(x[0].ToString()),
                        GuidGenerator.Create(x[1].ToString())
                    ))
                .ToArray();

            StoryEstimationCalculator
                .Calculate(
                    userStories,
                    estimates,
                    relations)
                .Should().Be(expectedEstimate);
        }
    }
}