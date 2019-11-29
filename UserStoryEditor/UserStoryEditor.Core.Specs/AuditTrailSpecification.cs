using System;
using System.Collections.Generic;
using FluentAssertions;
using UserStoryEditor.Testing;
using Xbehave;

namespace UserStoryEditor.Core.Specs
{
    public class AuditTrailSpecification
    {
        private ProductBacklog backlog = default!;
        private readonly List<string> auditTrail = new List<string>();

        [Background]
        public void EstablishScenario()
        {
            "establish a PBL".x(
                () => this.backlog = new ProductBacklogBuilder()
                .WithAuditTrail(auditTrail)
                .Build());
        }

        [Scenario]
        public void AddUserStory()
        {
            "when adding user story".x(
                () => backlog.AddUserStory(
                    Guid.NewGuid(),
                    1.ToEstimate()));

            "the new story is reported to the audit trail".x(() =>
                auditTrail.Should().Contain("added user story"));
        }

        [Scenario]
        public void RemoveUserStory()
        {
            "when deleting user story".x(
                () => backlog.RemoveUserStory(
                    Guid.NewGuid()));

            auditTrail.CheckAudit(
                "deleted user story");

            "the story is deleted this is reported to the audit trail".x(() =>
                auditTrail.Should().Contain("deleted user story"));
        }
    }

    public static class AuditVerificator
    {
        public static void CheckAudit(
            this List<string> auditTrail,
            string value)
        {
            "the ... the audit trail".x(() =>
                auditTrail.Should().Contain(value));
        }
    }
}