using System;
using UnityEngine.Profiling;

namespace Code.Common.Helpers
{
    public static class ProfilerHelper
    {
        public static void ProfileAction(string actionName, Action action)
        {
            Profiler.BeginSample(actionName);
            action();
            Profiler.EndSample();
        }
    }
}