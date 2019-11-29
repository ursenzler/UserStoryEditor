using System;

namespace UserStoryEditor.Core
{
    public struct Estimate
    {
        public Estimate(
            int value)
        {
            if (value < 0)
            {
                throw new ArgumentOutOfRangeException("so nicht!");
            }
            Value = value;
        }

        public int Value { get; }
    }
}