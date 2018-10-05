namespace UserStoryEditor.Core.Operation.Backlogs
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Blocks.Audits;
    using UserStoryEditor.Core.Blocks.Estimations;
    using UserStoryEditor.Core.Blocks.StoryRelations;
    using UserStoryEditor.Core.Blocks.UserStoryData;
    using UserStoryEditor.Core.Persistency;

    public class Backlog : IBacklog
    {
        private readonly UserStories userStories = new UserStories();
        private readonly Estimates estimates = new Estimates();
        private readonly StoryRelations relations;
        private readonly AuditTrail auditTrail;

        public Backlog(AuditTrail auditTrail, IStoryRelationsPersistor storyRelationsPersistor)
        {
            this.auditTrail = auditTrail;
            this.relations = new StoryRelations(storyRelationsPersistor);
        }

        public int GetEstimation(Strategy strategy = Strategy.Sum)
        {
            var userStoryIds = this.userStories
                .GetAll()
                .Select(x => x.Id)
                .ToArray();

            var mappedEstimates = this.estimates.GetEstimatesNew(
                userStoryIds)
                .ToArray();

            //filter

            return StoryEstimationCalculator
                .Calculate(
                    this.relations.GetAllLeafs(userStoryIds).ToArray(),
                    mappedEstimates,
                    this.relations.GetAll().ToArray(),
                    strategy);
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

            this.auditTrail.AddedUserStory(id);
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
            var childIds = this.relations.GetChildIds(userStoryId);
            foreach (var childId in childIds)
            {
                this.userStories.DeleteUserStory(childId);
            }

            this.auditTrail.DeletedUserStory(userStoryId);
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
            this.auditTrail.AddedUserStory(childId);
        }

        public IReadOnlyCollection<UserStory> GetAllUserStories()
        {
            return this.userStories.GetAll();
        }
    }
}
