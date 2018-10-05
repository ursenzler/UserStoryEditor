namespace UserStoryEditor.Core.Blocks.Audits
{
    using System;
    using System.Collections.Generic;

    public class AuditTrail
    {
        private readonly List<string> auditTrail = new List<string>();

        public void AddedUserStory(Guid userStoryId)
        {
            this.auditTrail.Add($"added user story {userStoryId}");
        }

        public void DeletedUserStory(Guid userStoryId)
        {
            this.auditTrail.Add($"deleted user story {userStoryId}");
        }

        public IReadOnlyCollection<string> GetAuditTrail()
        {
            return this.auditTrail;
        }
    }
}