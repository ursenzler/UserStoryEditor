using System;
using FluentAssertions;
using UserStoryEditor.Core.Algorithms;
using UserStoryEditor.Core.Specs;
using Xunit;

namespace UserStoryEditor.Core.Facts.Algorithms
{
    public class StoryTreePrunerFacts
    {
        [Fact]
        public void ReturnsEmptyTree_WhenPruningEmptyTree()
        {
            new Guid[0]
                .Prune(new (Guid, int?)[0], new (Guid, Guid)[0])
                .Should().BeEmpty();
        }

        [Fact]
        public void ReturnsUnaltered_WhenPruningSingleLeafTree()
        {
            var userStoryId = GuidGenerator.Create("1");

            new[] {userStoryId}
                .Prune(new (Guid, int?)[0], new (Guid, Guid)[0])
                .Should().BeEquivalentTo(new [] {userStoryId});
        }

        [Fact]
        public void ReturnsUnaltered_WhenPruningFlatTree()
        {
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");

            new[]
                {
                    userStoryId1,
                    userStoryId2
                }
                .Prune(new (Guid, int?)[0], 
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
        [InlineData("1;2;B", "A5;1-;2-", "A1;A2", "A;B")]
        [InlineData("8;9;2", "A5;13;28;84;96", "A1;A2;18;19", "8;9;2")]
        [InlineData("8;9;2", "A5;13;28;8-;96", "A1;A2;18;19", "1;2")]
        [InlineData("8;9;2", "A5;1-;28;8-;96", "A1;A2;18;19", "A")]
        [InlineData("8;9;2;B", "A-;1-;28;8-;96;B4", "A1;A2;18;19", "A;B")]
        [InlineData("1;8;9", "A8;1-;25;83;93", "A1;A2;28;29", "A")]
        public void Rindsplätzli(
            string stringishLeaves,
            string stringishEstimates,
            string stringishRelations,
            string stringishExpectedLeaves)
        {
            var leafIds = TestArrayParser.GenerateStoryIds(stringishLeaves);
            var estimates = TestArrayParser.GenerateEstimates(stringishEstimates);
            var relations = TestArrayParser.GenerateRelations(stringishRelations);
            var expectedLeafIds = TestArrayParser.GenerateStoryIds(stringishExpectedLeaves);

            leafIds
                .Prune(estimates, relations)
                .Should().BeEquivalentTo(expectedLeafIds);
        }
    }
}