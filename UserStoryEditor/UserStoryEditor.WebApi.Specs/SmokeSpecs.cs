namespace UserStoryEditor.WebApi.Specs
{
    using System.Net.Http;
    using FakeItEasy;
    using FluentAssertions;
    using UserStoryEditor.Core.Blocks.Estimations;
    using UserStoryEditor.Core.Operation;
    using Xbehave;

    public class SmokeSpecs : WebsiteApiSpec
    {
        [Scenario]
        public void WebApiSpecsWork(
            HttpResponseMessage response)
        {
            "setup query data".x(()
                => A.CallTo(
                        () => this.RootFactory
                            .CreateBacklogOperations()
                            .GetEstimation(Strategy.Sum))
                    .Returns(5));

            "when sending a request".x(async ()
                => response = await this.Client.GetFromApi("userstoryeditor/getestimation"));

            "it should have status code OK".x(()
                => response.Should().HaveStatusCodeOk());

            "it should return estimation".x(()
                => response.ToJson().Should().BeJsonEquivalentTo("5"));
        }

        [Scenario]
        public void WebApiSpecsWork2(
            HttpResponseMessage response)
        {
            "when sending a request".x(async ()
                => response = await this.Client.GetFromApi("userstoryeditor/getestimation"));

            "it should have status code OK".x(()
                => response.Should().HaveStatusCodeOk());

            "it should return estimation".x(()
                => response.ToJson().Should().BeJsonEquivalentTo("0"));
        }
    }
}
