using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UserStoryEditor.Testing;
using Xunit;

namespace UserStoryEditor.Core.Facts
{
    public class SummerFacts
    {
        private Summer testee;

        public SummerFacts()
        {
            this.testee = new Summer();
        }

        [Fact]
        public void SumsEstimates()
        {
            var roots = new UserStory[]
            {
            };

            var estimates = new Dictionary<Guid, Estimate>();

            var result = this.testee
                .GetEstimate(
                    roots,
                    estimates);

            result.Should()
                .Be(0.ToEstimate());
        }

        [Fact]
        public void SumsRoots()
        {
            var roots = new UserStory[]
            {
                new UserStory(
                    Guid.NewGuid()),
                new UserStory(
                    Guid.NewGuid()),
            };

            var estimates = new Dictionary<Guid, Estimate>
            {
                { roots[0].Id, 5.ToEstimate() },
                { roots[1].Id, 3.ToEstimate() }
            };

            var result = this.testee
                .GetEstimate(
                    roots,
                    estimates);

            result.Should()
                .Be(8.ToEstimate());
        }

        [Fact]
        public void SumsChildren()
        {
            var roots = new UserStory[]
            {
                new UserStory(
                    Guid.NewGuid()),
                new UserStory(
                    Guid.NewGuid(),
                    new[]
                    {
                        new UserStory(
                            Guid.NewGuid()),
                        new UserStory(
                            Guid.NewGuid()),
                    }),
            };

            var estimates = new Dictionary<Guid, Estimate>
            {
                { roots[0].Id, 5.ToEstimate() },
                { roots[1].Id, 3.ToEstimate() },
                { roots[1].Children.First().Id, 4.ToEstimate() },
                { roots[1].Children.Last().Id, 7.ToEstimate() },
            };

            var result = this.testee
                .GetEstimate(
                    roots,
                    estimates);

            result.Should()
                .Be(16.ToEstimate());
        }

        [Fact]
        public void UsesEstimateOfParentIfAChildHasNoEstimate()
        {
            var roots = new UserStory[]
            {
                new UserStory(
                    Guid.NewGuid()),
                new UserStory(
                    Guid.NewGuid(),
                    new[]
                    {
                        new UserStory(
                            Guid.NewGuid()),
                        new UserStory(
                            Guid.NewGuid()),
                    }),
            };

            var estimates = new Dictionary<Guid, Estimate>
            {
                { roots[0].Id, new Estimate(5) },
                { roots[1].Id, 3.ToEstimate() },
                { roots[1].Children.First().Id, 4.ToEstimate() },
            };

            var result = this.testee
                .GetEstimate(
                    roots,
                    estimates);

            result.Should()
                .Be(8.ToEstimate());
        }


    }
}