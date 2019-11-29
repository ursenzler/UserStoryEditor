using System;
using System.Collections.Generic;
using System.Linq;

namespace UserStoryEditor.Core
{
    public class UserStory
    {
        public Guid Id { get; }

        public UserStory(Guid id, IReadOnlyCollection<UserStory>? children = null)
        {
            this.Id = id;
            Children = children ?? new UserStory[0];
        }

        public void AddChild(UserStory userStory)
        {
            this.Children = this.Children
                .Union(new [] {userStory})
                .ToList();
        }

        public IReadOnlyCollection<UserStory> Children { get; private set; }

        public void RemoveChild(UserStory child)
        {
            var children = this.Children.ToList();
            children.Remove(child);

            this.Children = children;
        }
    }
}