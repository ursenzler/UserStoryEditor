using System;
using System.Collections.Generic;

namespace UserStoryEditor.Core
{
    public class Estimates
    {
        private readonly IEstimatesPersistor estimatesPersistor;

        public Estimates (IEstimatesPersistor estimatesPersistor)
        {
            this.estimatesPersistor = estimatesPersistor;
        }

        public void Set(
            Guid id,
            Estimate estimate)
        {
            var estimates = estimatesPersistor.GetAll();

            if (estimates.ContainsKey(id))
            {
                estimates[id] = estimate;
            }
            else
            {
                estimates.Add(id, estimate);
            }
        }

        public IReadOnlyDictionary<Guid, Estimate> GetAll()
        {
            return estimatesPersistor.GetAll();
        }
    }
}