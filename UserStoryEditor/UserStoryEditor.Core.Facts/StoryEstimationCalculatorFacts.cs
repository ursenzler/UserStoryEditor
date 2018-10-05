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
            var userStoryId3 = GuidGenerator.Create("3");

            StoryEstimationCalculator
                .Calculate(
                    new[] { userStoryId2, userStoryId3 },
                    new[]
                    {
                        (userStoryId1, (int?)5),
                        (userStoryId2, (int?)8),
                        (userStoryId3, (int?)3)
                    },
                    new[]
                    {
                        (userStoryId1, userStoryId2)
                    })
                .Should().Be(11);
        }

        [Fact]
        public void ReturnsEmptyTree_WhenPruningEmptyTree()
        {
            StoryEstimationCalculator
                .Prune(new Guid[0], new (Guid, int?)[0], new (Guid, Guid)[0])
                .Should().BeEmpty();
        }

        [Fact]
        public void ReturnsUnaltered_WhenPruningSingleLeafTree()
        {
            var userStoryId = GuidGenerator.Create("1");

            StoryEstimationCalculator
                .Prune(new[] {userStoryId}, new (Guid, int?)[0], new (Guid, Guid)[0])
                .Should().BeEquivalentTo(new [] {userStoryId});
        }

        [Fact]
        public void ReturnsUnaltered_WhenPruningFlatTree()
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");

            StoryEstimationCalculator
                .Prune(
                    new[]
                    {
                        userStoryId1,
                        userStoryId2
                    },
                    new (Guid, int?)[0], 
                    new (Guid, Guid)[0])
                .Should().BeEquivalentTo(new[]
                {
                    userStoryId1,
                    userStoryId2
                });
        }

        [Theory]
        [InlineData("1", "A5;13", "A1", "1")]
        [InlineData("1;2", "A5;13;23", "A1;A2", "1;2")]
        [InlineData("1;2", "A5;1-;23", "A1;A2", "A")]
        [InlineData("8;9;2", "A5;13;28;84;96", "A1;A2;18;19", "8;9;2")]
        [InlineData("8;9;2", "A5;13;28;8-;96", "A1;A2;18;19", "1;2")]
        [InlineData("8;9;2", "A5;1-;28;8-;96", "A1;A2;18;19", "A")]
        [InlineData("8;9;2;B", "A-;1-;28;8-;96;B4", "A1;A2;18;19", "A;B")]
        public void Rindsplätzli2(
            string stringishLeaves,
            string stringishEstimates,
            string stringishRelations,
            string stringishExpectedLeaves)
        {
            var leafIds = GenerateStoryIds(stringishLeaves);
            var estimates = GenerateEstimates(stringishEstimates);
            var relations = GenerateRelations(stringishRelations);
            var expectedLeafIds = GenerateStoryIds(stringishExpectedLeaves);

            StoryEstimationCalculator
                .Prune(leafIds, estimates, relations)
                .Should().BeEquivalentTo(expectedLeafIds);
        }

        [Theory]
        [InlineData("A", "A5;1-;28", "A1;A2", 5)]
        [InlineData("1;2", "A5;13;28", "A1;A2", 11)]
        [InlineData("8;9;2", "A5;13;28;84;96", "A1;A2;18;19", 18)]
        [InlineData("1;2", "A5;13;28;8-;96", "A1;A2;18;19", 11)]
        public void Rindsplätzli(
            string stringishStories,
            string stringishEstimates,
            string stringishRelations,
            int expectedEstimate)
        {
            var initialLeaves = GenerateStoryIds(stringishStories);
            var estimates = GenerateEstimates(stringishEstimates);
            var relations = GenerateRelations(stringishRelations);

            StoryEstimationCalculator
                .Calculate(
                    initialLeaves,
                    estimates,
                    relations)
                .Should().Be(expectedEstimate);
        }

        private static Guid[] GenerateStoryIds(string idString)
        {
            return idString
                .Split(';')
                .Select(GuidGenerator.Create)
                .ToArray();
        }

        private static (Guid, Guid)[] GenerateRelations(string relationString)
        {
            return relationString
                .Split(';')
                .Select(x =>
                    (
                        GuidGenerator.Create(x[0].ToString()),
                        GuidGenerator.Create(x[1].ToString())
                    ))
                .ToArray();
        }

        private static (Guid, int?)[] GenerateEstimates(string estimateString)
        {
            return estimateString
                .Split(';')
                .Select(x =>
                    (
                        GuidGenerator.Create(x[0].ToString()),
                        x[1] == '-' ? default(int?) : int.Parse(x[1].ToString())))
                .ToArray();
        }
    }
}