using System;
using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using UserStoryEditor.Testing;
using Xunit;

namespace UserStoryEditor.Core.Facts
{
    public class SummerMaximaFacts
    {
        private MaximaSummer  testee;

         public SummerMaximaFacts ()
        {
            this.testee = new MaximaSummer();
        }

         [Fact]
         public void MaximaEstimateFromParentChildren()
         {
             var roots = new UserStory[]
             {
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
                 { roots[0].Id, 10.ToEstimate() },
                 { roots[0].Children.First().Id, 4.ToEstimate()},
                 { roots[0].Children.Last().Id, 4.ToEstimate() }
             };

             var result = this.testee
                 .GetMaximaEstimate(
                     roots,
                     estimates);

             result.Should()
                 .Be(10.ToEstimate());
         }
    }
}