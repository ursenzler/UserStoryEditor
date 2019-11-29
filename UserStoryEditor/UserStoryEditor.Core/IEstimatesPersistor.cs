using System;
using System.Collections.Generic;

namespace UserStoryEditor.Core
{
    public interface IEstimatesPersistor
    {
        Dictionary<Guid, Estimate> GetAll();

        void Store(Dictionary<Guid, Estimate> estimates);
    }
}