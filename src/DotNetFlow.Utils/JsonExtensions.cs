using Newtonsoft.Json;

namespace DotNetFlow.Utils
{
    internal static class JsonExtensions
    {
        public static string ToJson(this object obj)
        {
            return JsonConvert.SerializeObject(obj, Formatting.Indented);
        }
    }
}