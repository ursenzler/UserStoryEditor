namespace UserStoryEditor.Core.Blocks.Estimations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class Estimates
    {
        private readonly Dictionary<Guid, int?> estimates = new Dictionary<Guid, int?>();

        public void SetEstimate(
            Guid id,
            int? estimate)
        {
            this.estimates[id] = estimate;
        }

        public IReadOnlyCollection<int?> GetEstimates(
            IReadOnlyCollection<Guid> userStoryIds)
        {
            return userStoryIds
                .Select(
                    x => this.estimates[x])
                .ToArray();
        }

        public IReadOnlyCollection<(Guid UserStoryId, int? Estimate)> GetEstimatesNew(
            IReadOnlyCollection<Guid> userStoryIds)
        {
            return userStoryIds
                .Select(
                    x => (x, this.estimates[x]))
                .ToArray();
        }
    }
}