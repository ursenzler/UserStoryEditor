namespace UserStoryEditor.Core.Specs
{
    using System;

    public static class GuidGenerator
    {
        public static Guid Create(string index)
        {
            var firstChar = index[0];
            var guidString = new string(firstChar, 32);
            return Guid.Parse(guidString);
        }
    }
}