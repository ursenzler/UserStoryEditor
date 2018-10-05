namespace UserStoryEditor.Core
{
    using UserStoryEditor.Core.Operation;
    using UserStoryEditor.Core.Operation.Audits;
    using UserStoryEditor.Core.Operation.Backlogs;

    public interface IRootFactory
    {
        IBacklog CreateBacklogOperations();

        AuditTrailOperations CreateAuditTrailOperations();
    }
}