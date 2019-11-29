using System;
using System.Collections.Generic;
using UserStoryEditor.Core;

namespace UserStoryEditor
{
    public class RealFakeEstimatesPersistor : IEstimatesPersistor
    {
        private Dictionary<Guid, Estimate> estimates = new Dictionary<Guid, Estimate>();

        public Dictionary<Guid, Estimate> GetAll()
        {
            return this.estimates;
        }

        public void Store(Dictionary<Guid, Estimate> estimates)
        {
            this.estimates = estimates;
        }
    }
}