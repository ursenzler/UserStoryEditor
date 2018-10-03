namespace UserStoryEditor.WebApi.Specs
{
    using System.Net.Http;
    using FluentAssertions;
    using Xbehave;

    public class SmokeSpecs : WebsiteApiSpec
    {
        [Scenario]
        public void WebApiSpecsWork(
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
