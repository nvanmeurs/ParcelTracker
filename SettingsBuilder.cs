using ParcelTracker.destination;
using ParcelTracker.source;
using System;

namespace ParcelTracker
{
    static class SettingsBuilder
    {
        public static Settings Build()
        {
            var settings = new Settings()
            {
                ParcelSource = GetEnvVarEnum<ParcelSourceImplementation>("ParcelSource", true),
                ParcelDestination = GetEnvVarEnum<ParcelDestinationImplementation>("ParcelDestination", true),
                TrelloBoardId = GetEnvVar("TrelloBoardId"),
                TrelloAppKey = GetEnvVar("TrelloAppKey"),
                TrelloUserToken = GetEnvVar("TrelloUserToken"),
                DefaultDestinationCountry = GetEnvVar("DefaultDestinationCountry"),
                DefaultPostalCode = GetEnvVar("DefaultPostalCode")
            };

            return settings;
        }

        private static TEnum GetEnvVarEnum<TEnum>(string variableName, bool required) where TEnum : struct, IConvertible
        {
            var value = GetEnvVar(variableName, required);

            try
            {
                return Enum.Parse<TEnum>(value, true);
            }
            catch (ArgumentException)
            {
                throw new Exception($"'{value}' is not a valid value for setting '{variableName}'");
            }
        }

        private static string GetEnvVar(string variableName, bool required = false)
        {
            var value = Environment.GetEnvironmentVariable(variableName)?.Trim();

            if (required && (value == null || value == ""))
                throw new Exception($"Required setting '{variableName}' has not been set.");

            return value;
        }
    }

}
