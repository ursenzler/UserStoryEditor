using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Versioning;

namespace UserStoryEditor.Core
{
    public class FakePersistor : IRelationsPersistor
    {
        public IReadOnlyCollection<UserStory> GetAll()
        {
            throw new NotImplementedException();
        }

        public void Store(IReadOnlyCollection<UserStory> userStories)
        {
            throw new NotImplementedException();
        }
    }

    public class Relations
    {
        private readonly IRelationsPersistor persistor;

        public Relations(
            IRelationsPersistor? persistor = null)
        {
            this.persistor = persistor ?? new FakePersistor();
        }

        private readonly List<UserStory> roots = new List<UserStory>();

        public void AddUserStory(Guid id)
        {
            if (this.persistor is FakePersistor)
            {
                if (this.roots.Any(x => x.Id == id))
                {
                    throw new ArgumentException($"user story with id {id} already present.");
                }

                this.roots.Add(new UserStory(id));
            }
            else
            {
                var roots = this.persistor.GetAll().ToList();

                if (roots.Any(x => x.Id == id))
                {
                    throw new ArgumentException($"user story with id {id} already present.");
                }

                roots.Add(new UserStory(id));

                this.persistor.Store(roots);
            }
        }

        public IReadOnlyCollection<UserStory> GetRoots()
        {
            return this.roots;
        }

        public IReadOnlyCollection<UserStory> GetRoots2()
        {
            return persistor.GetAll();
        }

        public void Remove(Guid id)
        {
            var queue = new Queue<(UserStory Node, UserStory? Parent)>(
                roots.Select(x => (x, default(UserStory?))).ToArray());

            while (queue.Any())
            {
                var item = queue.Dequeue();

                if (item.Node.Id == id)
                {
                    if (item.Parent != null)
                    {
                        item.Parent.RemoveChild(item.Node);
                    }
                    else
                    {
                        roots.Remove(item.Node);
                    }
                }

                foreach (var child in item.Node.Children)
                {
                    queue.Enqueue((child, item.Node));
                }
            }
        }

        public void AddChildrenToUserStory(Guid parentId, Guid childId)
        {
            this.FindUserStory(parentId)
                .AddChild(new UserStory(childId));
        }

        private UserStory FindUserStory(Guid id)
        {
            var queue = new Queue<UserStory>(
                roots);

            while (queue.Any())
            {
                var userStory = queue.Dequeue();

                if (userStory.Id == id)
                {
                    return userStory;
                }

                foreach (var child in userStory.Children)
                {
                    queue.Append(child);
                }
            }

            throw new Exception("blöd");
        }
    }
}