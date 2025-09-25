using UnityEngine;

namespace Code.Common.Helpers
{
    public class CustomDebug
    {
        private static bool EnableLogs = true;
        
        public static void Log(object message, string tag = "LOG", bool shouldLog = true, string color = "green")
        {
            if (!EnableLogs || !shouldLog) return;
            Debug.Log(Format(message, tag, color));
        }

        public static void LogWarning(object message, string tag = "WARNING", bool shouldLog = true, string color = "yellow")
        {
            if (!EnableLogs || !shouldLog) return;
            Debug.LogWarning(Format(message, tag, color));
        }

        public static void LogError(object message, string tag = "ERROR", bool shouldLog = true, string color = "red")
        {
            if (!EnableLogs || !shouldLog) return;
            Debug.LogError(Format(message, tag, color));
        }

        private static string Format(object message, string tag, string color)
        {
            return $"<color={color}>[{tag}]</color> {message}";
        }
        
        
    }
}