namespace UserStoryEditor.Core.Operation.Backlogs
{
    using System;
    using System.Collections.Generic;
    using UserStoryEditor.Core.Blocks.Estimations;

    public interface IBacklog
    {
        int GetEstimation(Strategy strategy = Strategy.Sum);

        void AddUserStory(
            Guid id,
            string title,
            int estimate);

        void ChangeEstimate(
            Guid userStoryId,
            int newEstimate);

        void DeleteUserStory(
            Guid userStoryId);

        void AddChildStory(
            Guid parentId,
            Guid childId,
            string title,
            int? estimate);

        IReadOnlyCollection<UserStory> GetAllUserStories();
    }
}