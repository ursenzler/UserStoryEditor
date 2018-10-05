namespace UserStoryEditor.Core
{
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Blocks.Audits;
    using UserStoryEditor.Core.Blocks.StoryRelations;
    using UserStoryEditor.Core.Operation;
    using UserStoryEditor.Core.Operation.Audits;
    using UserStoryEditor.Core.Operation.Backlogs;
    using UserStoryEditor.Core.Persistency;

    public class RootFactory : IRootFactory
    {
        private readonly Backlog backlog;
        private readonly AuditTrailOperations auditTrailOperations;

        public RootFactory()
        {
            var auditTrail = new AuditTrail();
            var storyRelationsPersistor = new StoryRelationsPersistor();

            this.backlog = new Backlog(
                auditTrail,
                storyRelationsPersistor);

            this.auditTrailOperations = new AuditTrailOperations(
                auditTrail);
        }

        public IBacklog CreateBacklogOperations()
        {
            return this.backlog;
        }

        public AuditTrailOperations CreateAuditTrailOperations()
        {
            return this.auditTrailOperations;
        }
    }
}