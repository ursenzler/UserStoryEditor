using UserStoryEditor.Core;

namespace UserStoryEditor.Testing
{
    public static class Parsers
    {
        public static Estimate ToEstimate(
            this int value)
        {
            return new Estimate(value);
        }
    }
}