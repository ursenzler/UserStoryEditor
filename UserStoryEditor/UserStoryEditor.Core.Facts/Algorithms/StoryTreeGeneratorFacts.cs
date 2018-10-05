using System;
using FluentAssertions;
using UserStoryEditor.Core.Algorithms;
using UserStoryEditor.Core.Specs;
using Xunit;

namespace UserStoryEditor.Core.Facts.Algorithms
{
    public class StoryTreeGeneratorFacts
    {
        [Fact]
        public void ReturnsEmptyList_WhenGettingLeavesFromEmptyList()
        {
            new Guid[0]
                .GetLeaves(new (Guid, Guid)[0])
                .Should().BeEmpty();
        }

        [Fact]
        public void ReturnsUnaltered_WhenGettingLeavesFromSingleLeafTree()
        {
            var singleLeafTree = new[]{GuidGenerator.Create("1")};

            singleLeafTree
                .GetLeaves(new (Guid, Guid)[0])
                .Should().BeEquivalentTo(singleLeafTree);
        }

        [Fact]
        public void ReturnsUnaltered_WhenGettingLeavesFromFlatTree()
        {
            var flatTree = new[]
            {
                GuidGenerator.Create("A"),
                GuidGenerator.Create("B")
            };

            flatTree
                .GetLeaves(new (Guid, Guid)[0])
                .Should().BeEquivalentTo(flatTree);
        }

        [Theory]
        [InlineData("A;1;2;B", "A1;A2", "1;2;B")]
        [InlineData("A;1;2;B;8;9", "A1;A2;18;19", "8;9;2;B")]
        public void ReturnsLeaves_WhenGettingTree(
            string stringishStories,
            string stringishRelations,
            string stringishExpectedLeaves)
        {
            var userStoryIds = TestArrayParser.GenerateStoryIds(stringishStories);
            var relations = TestArrayParser.GenerateRelations(stringishRelations);
            var leaves = TestArrayParser.GenerateStoryIds(stringishExpectedLeaves);

            userStoryIds
                .GetLeaves(relations)
                .Should().BeEquivalentTo(leaves);
        }
    }
}