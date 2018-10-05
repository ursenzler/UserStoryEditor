﻿namespace UserStoryEditor.Core.Specs.Blocks
{
    using FluentAssertions;
    using Core.Blocks;
    using Xbehave;

    public class EstimatesBlock
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