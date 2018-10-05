using System;
using System.Linq;
using UserStoryEditor.Core.Specs;

namespace UserStoryEditor.Core.Facts
{
    public class TestArrayParser
    {
        /// <param name="idString">parsing: "A;B;C" => [Guid a, Guid b, Guid c]</param>
        public static Guid[] GenerateStoryIds(string idString)
        {
            return idString
                .Split(';')
                .Select(GuidGenerator.Create)
                .ToArray();
        }

        /// <param name="relationString">parsing: "A1;A2" => [(Guid a, Guid 1), (Guid a, Guid 2)]</param>
        public static (Guid, Guid)[] GenerateRelations(string relationString)
        {
            return relationString
                .Split(';')
                .Select(x =>
                    (
                        GuidGenerator.Create(x[0].ToString()),
                        GuidGenerator.Create(x[1].ToString())
                    ))
                .ToArray();
        }

        /// <param name="estimateString">parsing: "A3;B-" => [(Guid a, 3), (Guid b, null)]</param>
        public static (Guid, int?)[] GenerateEstimates(string estimateString)
        {
            return estimateString
                .Split(';')
                .Select(x =>
                    (
                        GuidGenerator.Create(x[0].ToString()),
                        x[1] == '-' ? default(int?) : int.Parse(x[1].ToString())))
                .ToArray();
        }
    }
}