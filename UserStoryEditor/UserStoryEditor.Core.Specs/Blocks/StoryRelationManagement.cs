using System;
using System.Linq;
using FluentAssertions;
using UserStoryEditor.Core.Blocks;
using Xbehave;

namespace UserStoryEditor.Core.Specs.Blocks
{
    public class StoryRelationManagement
    {
        [Scenario]
        public void FindLeafs(StoryRelations relations, Guid[] leaves)
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
                relations = new StoryRelations();
                relations.AddRelation(userStoryIdA, userStoryId1);
                relations.AddRelation(userStoryIdA, userStoryId2);
                relations.AddRelation(userStoryId1, userStoryId8);
                relations.AddRelation(userStoryId1, userStoryId9);
            });

            "when I request all leaves".x(() =>
                leaves = relations.GetAllLeafs(stories).ToArray());

            "then I get all stories without children".x(() => 
                leaves.Should().BeEquivalentTo(new []
                {
                    userStoryId8,
                    userStoryId9,
                    userStoryId2,
                    userStoryIdB
                }));
        }
    }
}