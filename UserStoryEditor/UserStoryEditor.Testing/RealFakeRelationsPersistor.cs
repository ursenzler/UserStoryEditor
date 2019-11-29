using System.Collections.Generic;
using System.Linq;
using UserStoryEditor.Core;

namespace UserStoryEditor
{
    public class RealFakeRelationsPersistor : IRelationsPersistor
    {
        private List<UserStory> roots = new List<UserStory>();

        public IReadOnlyCollection<UserStory> GetAll()
        {
            return this.roots;
        }

        public void Store(IReadOnlyCollection<UserStory> userStories)
        {
            this.roots = userStories.ToList();
        }
    }
}