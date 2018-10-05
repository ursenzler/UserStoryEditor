using System;
using System.Linq;
using FluentAssertions;
using UserStoryEditor.Core.Blocks;
using Xbehave;

namespace UserStoryEditor.Core.Specs.Blocks
{
    using UserStoryEditor.Core.Blocks.StoryRelations;
    using UserStoryEditor.Core.Persistency;

    public class StoryRelationManagement
    {
        StoryRelations relations;

        [Background]
        public void Background()
        {
            this.relations = new StoryRelations(new StoryRelationsPersistor());
        }

        [Scenario]
        public void FindLeafs(Guid[] leaves)
        {
            var userStoryIdA = GuidGenerator.Create("A");
            var userStoryId1 = GuidGenerator.Create("1");
            var userStoryId2 = GuidGenerator.Create("2");
            var userStoryId8 = GuidGenerator.Create("8");
            var userStoryId9 = GuidGenerator.Create("9");
            var userStoryIdB = GuidGenerator.Create("B");

            var stories = new[]
            {
                userStoryIdA,
                userStoryId1,
                userStoryId2,
                userStoryId8,
                userStoryId9,
                userStoryIdB
            };

            "establish user story relations".x(() =>
            {
                this.relations.AddRelation(userStoryIdA, userStoryId1);
                this.relations.AddRelation(userStoryIdA, userStoryId2);
                this.relations.AddRelation(userStoryId1, userStoryId8);
                this.relations.AddRelation(userStoryId1, userStoryId9);
            });

            "when I request all leaves".x(() =>
                leaves = this.relations.GetAllLeafs(stories).ToArray());

            "then I get all stories without children".x(() =>
                leaves.Should().BeEquivalentTo(new []
                {
                    userStoryId8,
                    userStoryId9,
                    userStoryId2,
                    userStoryIdB
                }));
        }

        [Scenario]
        public void GetChildIds()
        {
            var parentUserStoryId = GuidGenerator.Create("A");
            var childUserStoryId1 = GuidGenerator.Create("1");
            var childUserStoryId2 = GuidGenerator.Create("2");

            "establish user story relations".x(() =>
            {
                this.relations.AddRelation(parentUserStoryId, childUserStoryId1);
                this.relations.AddRelation(parentUserStoryId, childUserStoryId2);
            });

            "then I get all child IDs".x(()
                => this.relations.GetChildIds(parentUserStoryId)
                    .Should()
                    .BeEquivalentTo(childUserStoryId1, childUserStoryId2));
        }

        [Scenario]
        public void GetChildIdsMultipleLevels()
        {
            var parentUserStoryId = GuidGenerator.Create("A");
            var childUserStoryId1 = GuidGenerator.Create("1");
            var childUserStoryId2 = GuidGenerator.Create("2");
            var subChildUserStoryId = GuidGenerator.Create("3");
            var subSubChildUserStoryId = GuidGenerator.Create("4");

            "establish user story relations".x(() =>
            {
                this.relations.AddRelation(parentUserStoryId, childUserStoryId1);
                this.relations.AddRelation(parentUserStoryId, childUserStoryId2);
                this.relations.AddRelation(childUserStoryId2, subChildUserStoryId);
                this.relations.AddRelation(subChildUserStoryId, subSubChildUserStoryId);
            });

            "then I get all child IDs".x(()
                => this.relations.GetChildIds(parentUserStoryId)
                    .Should()
                    .BeEquivalentTo(
                        childUserStoryId1,
                        childUserStoryId2,
                        subChildUserStoryId,
                        subSubChildUserStoryId));
        }
    }
}