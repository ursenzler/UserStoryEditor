namespace UserStoryEditor.WebApi.Specs
{
    using FakeItEasy;
    using Microsoft.CodeAnalysis;
    using UserStoryEditor.Core;
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Blocks.Audits;
    using UserStoryEditor.Core.Operation;
    using UserStoryEditor.Core.Operation.Audits;
    using UserStoryEditor.Core.Operation.Backlogs;

    public class FakeRootFactory : IRootFactory
    {
        private readonly IBacklog backlog = A.Fake<IBacklog>();

        public IBacklog CreateBacklogOperations()
        {
            return this.backlog;
        }

        public AuditTrailOperations CreateAuditTrailOperations()
        {
            return new AuditTrailOperations(new AuditTrail());
        }
    }
}