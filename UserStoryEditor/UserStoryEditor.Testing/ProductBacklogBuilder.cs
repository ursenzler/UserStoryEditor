using System.Collections.Generic;
using UserStoryEditor.Core;

namespace UserStoryEditor
{
    public class ProductBacklogBuilder
    {
        private List<string> auditTrail = new List<string>();

        private IRelationsPersistor RelationsPersistor { get; set; } = new FakePersistor();

        private IEstimatesPersistor EstimatesPersistor { get; set; } = new RealFakeEstimatesPersistor();

        public ProductBacklogBuilder WithAuditTrail(List<string> auditTrail)
        {
            this.auditTrail = auditTrail;
            return this;
        }

        public ProductBacklogBuilder WithRealRelationsFake(IRelationsPersistor fake)
        {
            this.RelationsPersistor = fake;
            return this;
        }

        public ProductBacklogBuilder WithRealEstimatesFake(IEstimatesPersistor fake)
        {
            this.EstimatesPersistor = fake;
            return this;
        }

        public ProductBacklog Build()
        {
            return new ProductBacklog(this.auditTrail,
                this.RelationsPersistor,
                this.EstimatesPersistor);
        }
    }
}