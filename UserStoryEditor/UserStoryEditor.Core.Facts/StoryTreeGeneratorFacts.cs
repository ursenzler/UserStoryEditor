using System;
using FluentAssertions;
using UserStoryEditor.Core.Blocks;
using UserStoryEditor.Core.Specs;
using Xunit;

namespace UserStoryEditor.Core.Facts
{
    public class StoryTreeGeneratorFacts
    {
        [Fact]
        public void ReturnsEmptyList_WhenGettingLeavesFromEmptyList()
        {
            StoryTreeGenerator
                .GetLeaves(new Guid[0], new (Guid, Guid)[0])
                .Should().BeEmpty();
        }

        [Fact]
        public void ReturnsUnaltered_WhenGettingLeavesFromSingleLeafTree()
        {
            var singleLeafTree = new[]{GuidGenerator.Create("1")};

            StoryTreeGenerator
                .GetLeaves(singleLeafTree, new (Guid, Guid)[0])
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

            StoryTreeGenerator
                .GetLeaves(flatTree, new (Guid, Guid)[0])
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

            StoryTreeGenerator
                .GetLeaves(userStoryIds, relations)
                .Should().BeEquivalentTo(leaves);
        }
    }
}