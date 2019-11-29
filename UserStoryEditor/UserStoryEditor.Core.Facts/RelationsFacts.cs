using System;
using FluentAssertions;
using Xunit;

namespace UserStoryEditor.Core.Facts
{
    public class RelationsFacts
    {
        [Fact]
        public void CanAddStory()
        {
            var relations = new Relations();

            var id = Guid.NewGuid();
            relations.AddUserStory(id);

            relations.GetRoots()
                .Should()
                .BeEquivalentTo(new UserStory(id));
        }

        [Fact]
        public void CanRemoveStory()
        {
            var relations = new Relations();

            var id = Guid.NewGuid();
            relations.AddUserStory(id);

            relations.Remove(id);

            relations.GetRoots()
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void CanAddChildren()
        {
            var relations = new Relations();

            var parentId = Guid.NewGuid();
            var childId = Guid.NewGuid();
            relations.AddUserStory(parentId);

            relations.AddChildrenToUserStory(parentId, childId);

            relations.GetRoots()
                .Should()
                .BeEquivalentTo(
                    new UserStory(
                        parentId,
                        new[]
                        {
                            new UserStory(
                                childId),
                        }));
        }

        [Fact]
        public void CanRemoveChild()
        {
            var relations = new Relations();

            var parentId = Guid.NewGuid();
            var childId = Guid.NewGuid();
            relations.AddUserStory(parentId);

            relations.AddChildrenToUserStory(parentId, childId);

            relations.Remove(childId);

            relations.GetRoots()
                .Should()
                .BeEquivalentTo(
                    new UserStory(
                        parentId));
        }

        [Fact]
        public void CanRemoveParentWithChildren()
        {
            var relations = new Relations();

            var parentId = Guid.NewGuid();
            var childId = Guid.NewGuid();
            relations.AddUserStory(parentId);
            relations.AddChildrenToUserStory(parentId, childId);

            relations.Remove(parentId);

            relations.GetRoots()
                .Should()
                .BeEmpty();
        }

        [Fact]
        public void CannotAddAUserStoryTwice()
        {
            var relations = new Relations();

            var id = Guid.NewGuid();
            relations.AddUserStory(id);

            relations
                .Invoking(x => x.AddUserStory(id))
                .Should()
                .Throw<ArgumentException>();
        }
    }
}