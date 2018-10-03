namespace UserStoryEditor.Core.Specs
{
    using FluentAssertions;
    using Xbehave;

    public class SmokeSpecs
    {
        [Scenario]
        public void XBehaveWorks()
        {
            "it should work".x(()
                => 1.Should().NotBe(1));
        }
    }
}
