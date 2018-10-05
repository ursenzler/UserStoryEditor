namespace UserStoryEditor.Core.Specs.Blocks
{
    using FluentAssertions;
    using UserStoryEditor.Core.Blocks;
    using UserStoryEditor.Core.Blocks.Estimations;
    using Xbehave;

    public class EstimatesManagement
    {
        [Scenario]
        public void SetEstimate(
            Estimates estimates)
        {
            var userStoryId = GuidGenerator.Create("1");

            "".x(()
                => estimates = new Estimates());

            "".x(()
                => estimates.SetEstimate(
                    userStoryId,
                    5));

            "".x(()
                => estimates.GetEstimates(
                    new[]
                    {
                        userStoryId
                    })
                    .Should().BeEquivalentTo(
                        5));
        }
    }
}