using System.Collections.Generic;

namespace UserStoryEditor.Core
{
    using System;

    public class ProductBacklog
    {
        private readonly List<string> auditTrail;
        private readonly Relations relations;
        private readonly Estimates estimates;
        private readonly Summer summer = new Summer();
        private readonly MaximaSummer maximumSummer = new MaximaSummer();

        public ProductBacklog(
            List<string> auditTrail,
            IRelationsPersistor relationsPersistor,
            IEstimatesPersistor estimatesPersistor)
        {
            this.auditTrail = auditTrail;
            this.estimates= new Estimates(estimatesPersistor);
            this.relations = new Relations(relationsPersistor);
        }

        public void AddUserStory(Guid id, Estimate estimate)
        {
            relations.AddUserStory(
                id);

            estimates.Set(
                id,
                estimate);

            this.auditTrail.Add("added user story");
        }

        public Estimate GetEstimation()
        {
            var roots = this.relations
                .GetRoots();
            var allEstimates = this.estimates
                .GetAll();

            return this.summer
                .GetEstimate(
                    roots,
                    allEstimates);
        }

        public Estimate GetEstimation2()
        {
            var roots = this.relations
                .GetRoots2();
            var allEstimates = this.estimates
                .GetAll();

            return this.summer
                .GetEstimate(
                    roots,
                    allEstimates);
        }

        public Estimate GetMaximaEstimation()
        {
            var roots = this.relations
                .GetRoots();
            var allEstimates = this.estimates
                .GetAll();

            return this.maximumSummer
                .GetMaximaEstimate(
                    roots,
                    allEstimates);
        }

        public void ChangeUserStoryEstimate(Guid id, Estimate estimate)
        {
            this.estimates.Set(id, estimate);
        }

        public void RemoveUserStory(Guid id)
        {
            this.relations
                .Remove(id);

            this.auditTrail
                .Add("deleted user story");
        }

        public void AddChildUserStory(Guid parentId, Guid id, Estimate estimate)
        {
            this.relations.AddChildrenToUserStory(
                parentId,
                id);

            this.estimates
                .Set(
                    id,
                    estimate);
        }

        public void AddChildUserStory(Guid parentId, Guid id)
        {
            this.relations.AddChildrenToUserStory(
                parentId,
                id);
        }


    }
}