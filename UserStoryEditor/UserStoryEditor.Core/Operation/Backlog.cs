namespace UserStoryEditor.Core.Operation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UserStoryEditor.Core.Blocks;

    public class Backlog
    {
        private readonly UserStories userStories = new UserStories();
        private readonly Estimates estimates = new Estimates();
        private readonly StoryRelations relations = new StoryRelations();

        public int GetEstimation()
        {
            var userStoryIds = this.userStories
                .GetAll()
                .Select(x => x.Id)
                .ToArray();

            var e = this.estimates.GetEstimatesNew(
                userStoryIds)
                .ToArray();

            return StoryEstimationCalculator
                .Calculate(
                    userStoryIds,
                    e,
                    this.relations.GetAll().ToArray());
        }

        public void AddUserStory(
            Guid id,
            string title,
            int estimate)
        {
            this.userStories.AddUserStory(
                id,
                title);

            this.estimates
                .SetEstimate(
                    id,
                    estimate);
        }

        public void ChangeEstimate(
            Guid userStoryId,
            int newEstimate)
        {
            this.estimates
                .SetEstimate(
                    userStoryId,
                    newEstimate);
        }

        public void DeleteUserStory(
            Guid userStoryId)
        {
            this.userStories.DeleteUserStory(
                userStoryId);
        }

        public void AddChildStory(
            Guid parentId,
            Guid childId,
            string title,
            int? estimate)
        {
            this.userStories.AddUserStory(
                childId,
                title);
            this.relations.AddRelation(parentId, childId);
            this.estimates.SetEstimate(childId, estimate);
        }
    }
}
