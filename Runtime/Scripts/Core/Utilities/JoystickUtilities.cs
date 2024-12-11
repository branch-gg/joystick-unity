using System;
using System.Text;
using JoystickRemoteConfig.Core.Data;
using UnityEngine;

namespace JoystickRemoteConfig.Core
{
    public static class JoystickUtilities
    {
        public static EnvironmentsDataDefinition GetEnvironmentsDataDefinition()
        {
            return Resources.Load<EnvironmentsDataDefinition>("EnvironmentsDataDefinition");
        }

        public static JoystickGeneralDefinition GetJoystickGeneralDefinition()
        {
            return Resources.Load<JoystickGeneralDefinition>("JoystickGeneralDefinition");
        }

        public static GlobalExtendedRequestDefinition GlobalExtendedRequestDefinition()
        {
            return Resources.Load<GlobalExtendedRequestDefinition>("GlobalExtendedRequestDefinition");
        }

        public static string GetConfigContentAPIUrl(string[] contentIds)
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < contentIds.Length; i++)
            {
                // Ensure each content ID is properly encoded
                stringBuilder.Append("\"");
                stringBuilder.Append(Uri.EscapeDataString(contentIds[i]));
                stringBuilder.Append("\"");

                if (i < contentIds.Length - 1)
                {
                    stringBuilder.Append(",");
                }
            }

            var generalConfig = GetJoystickGeneralDefinition();
            bool shouldSerialized = generalConfig.IsSerializedResponseEnabled;

            string responseTypeParam = "&responseType=serialized";
            string appendParam = shouldSerialized ? responseTypeParam : string.Empty;

            // Properly encode the entire 'c=[...]' query parameter
            string encodedCParameter = Uri.EscapeDataString($"[{stringBuilder}]");

            return $"https://api.getjoystick.com/api/v1/combine/?c={encodedCParameter}&dynamic=true{appendParam}";
        }

        public static string GetCatalogAPIUrl()
        {
            return $"https://api.getjoystick.com/api/v1/env/catalog";
        }
    }
}