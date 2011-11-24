using System;
using System.IO;
using System.Reflection;

namespace DotNetFlow.Features.Extensions
{
    internal static class AssemblyExtensions
    {
        public static string GetResource(string name)
        {
            using (var fileStream = Assembly.GetExecutingAssembly().GetManifestResourceStream(name))
            {
                if (fileStream == null)
                    throw new Exception(string.Format(
                        "Could not find resource named '{0}'. Please ensure template file exists and 'Build Action' is set to 'Embedded Resource'.",
                        name));

                using (var reader = new StreamReader(fileStream))
                {
                    return reader.ReadToEnd();
                }
            }
        }
    }
}