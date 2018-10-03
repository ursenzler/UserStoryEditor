namespace UserStoryEditor.WebApi.Specs
{
    using System;
    using System.Net;
    using System.Net.Http;
    using FluentAssertions;
    using FluentAssertions.Json;
    using FluentAssertions.Primitives;
    using Newtonsoft.Json.Linq;

    public static class FluentAssertionExtensionMethods
    {
        public static void HaveStatusCode(
            this ObjectAssertions resultingResponseAssertion,
            HttpStatusCode expectedStatusCode)
        {
            var response = resultingResponseAssertion.Subject as HttpResponseMessage;
            if (response == null)
            {
                throw new ArgumentException($"actual object was no HttpResponseMessage");
            }

            var content = response.Content.ReadAsStringAsync().Result;
            response.StatusCode.Should().Be(expectedStatusCode, "Error: " + content.Replace("{", "(").Replace("}", ")"));
        }

        public static void HaveStatusCodeOk(
            this ObjectAssertions resultingResponseAssertion)
        {
            resultingResponseAssertion.HaveStatusCode(
                HttpStatusCode.OK);
        }

        public static void HaveStatusCodeUnauthorized(
            this ObjectAssertions resultingResponseAssertion)
        {
            resultingResponseAssertion.HaveStatusCode(
                HttpStatusCode.Unauthorized);
        }

        public static AndConstraint<JTokenAssertions> BeJsonEquivalentTo(
            this StringAssertions resultingJsonAssertion,
            string expectedJson)
        {
            var resultingString = resultingJsonAssertion.Subject;
            var resultingToken = JToken.Parse(resultingString);
            var expectedToken = JToken.Parse(expectedJson);

            var assertions = new JTokenAssertions(resultingToken);
            return assertions.BeEquivalentTo(expectedToken);
        }

        public static AndConstraint<JTokenAssertions> BeJsonEquivalentToEmptyArray(
            this StringAssertions resultingJsonAssertion)
        {
            return resultingJsonAssertion.BeJsonEquivalentTo("[]");
        }
    }
}