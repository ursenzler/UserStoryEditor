namespace UserStoryEditor.Core.Operation.Audits
{
    using System.Collections.Generic;
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Blocks.Audits;

    public class AuditTrailOperations
    {
        private readonly AuditTrail auditTrail;

        public AuditTrailOperations(AuditTrail auditTrail)
        {
            this.auditTrail = auditTrail;
        }

        public IReadOnlyCollection<string> GetAuditEntries()
        {
            return this.auditTrail.GetAuditTrail();
        }
    }
}