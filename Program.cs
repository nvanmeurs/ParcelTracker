using System;
using System.Linq;

namespace ParcelTracker
{
    class Program
    {
        static void Main()
        {
            var settings = SettingsBuilder.Build();
            PrintSettings(settings);
        }

        static void PrintSettings(Settings settings)
        {
            Console.WriteLine(string.Join(",\n", settings.GetType().GetProperties().Select(propertyInfo => string.Join(" = ", new object[] { propertyInfo.Name, propertyInfo.GetValue(settings)?.ToString() }))));
        }
    }
}