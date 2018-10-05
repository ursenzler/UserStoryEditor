namespace UserStoryEditor.Core.Specs.Operations
{
    using System;
    using FluentAssertions;
    using UserStoryEditor.Core.Operation;
    using Xbehave;

    public static class Check
    {
        public static Context Using(
            Func<RootFactory> factory)
        {
            return new Context(factory);
        }

        public static void AuditTrailHasAddedUserStory(
            this Context context,
            Guid userStoryId)
        {
            "then in AuditTrail added entry should logged".x(()
                => context.Factory()
                    .CreateAuditTrailOperations()
                    .GetAuditEntries()
                    .Should()
                    .Contain($"added user story {userStoryId}"));
        }

        public static void AuditTrailHasDeletedUserStory(
            this Context context,
            Guid userStoryId)
        {
            "then in AuditTrail delete entry should logged".x(()
                => context.Factory()
                    .CreateAuditTrailOperations()
                    .GetAuditEntries()
                    .Should()
                    .Contain($"deleted user story {userStoryId}"));
        }

        public class Context
        {
            public Context(
                Func<RootFactory> factory)
            {
                this.Factory = factory;
            }

            public Func<RootFactory> Factory { get; }
        }
    }
}