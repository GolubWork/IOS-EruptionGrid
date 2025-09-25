#if UNITY_EDITOR
using System;
using System.IO;
using UnityEditor;
using UnityEditor.Build;
using UnityEditor.Build.Reporting;
using UnityEngine;

namespace Code.Common.Helpers
{
    public class BuildVersionIncrementor : IPreprocessBuildWithReport
    {
        public int callbackOrder => 0;

        public void OnPreprocessBuild(BuildReport report)
        {
            try
            {
                IncrementVersion();
            }
            catch (Exception ex)
            {
                Debug.LogError($"[BuildVersionIncrementor] Failed to increment version: {ex}");
                throw;
            }
        }

        private void IncrementVersion()
        {
            int buildNumber = PlayerSettings.Android.bundleVersionCode;
            buildNumber++;
            PlayerSettings.Android.bundleVersionCode = buildNumber;

            string currentVersion = PlayerSettings.bundleVersion;
            Version version;

            if (!Version.TryParse(currentVersion, out version))
            {
                Debug.LogWarning($"[BuildVersionIncrementor] Invalid version '{currentVersion}', defaulting to 1.0.0");
                version = new Version(1, 0, 0);
            }
            version = new Version(version.Major, version.Minor, buildNumber);
            PlayerSettings.bundleVersion = version.ToString();

            Debug.Log($"[BuildVersionIncrementor] Version updated: {version} (Build {buildNumber})");
        }
    }

    [InitializeOnLoad]
    public static class CustomBuildHandler
    {
        static CustomBuildHandler()
        {
            BuildPlayerWindow.RegisterBuildPlayerHandler(OnBuild);
        }

        private static void OnBuild(BuildPlayerOptions options)
        {
            try
            {
                string version = PlayerSettings.bundleVersion;
                int buildNumber = PlayerSettings.Android.bundleVersionCode;

                var versionParts = version.Split('.');
                int major = int.Parse(versionParts[0]);
                int minor = int.Parse(versionParts[1]);

                int displayPatch = buildNumber;
                string versionForFilename = $"{major}.{minor}.{displayPatch + 1}";

                string projectName = SanitizeName(Application.productName);
                int apiVersion = GetTargetApiVersion();

                string buildDir = GetBuildDirectory(options);

                bool originalBuildAppBundle = EditorUserBuildSettings.buildAppBundle;

                string apkPath = Path.Combine(buildDir, $"{projectName}_{versionForFilename}_API{apiVersion}.apk");
                string aabPath = Path.Combine(buildDir, $"{projectName}_{versionForFilename}_API{apiVersion}.aab");

                var apkOptions = options;
                apkOptions.locationPathName = apkPath;
                EditorUserBuildSettings.buildAppBundle = false;
                Debug.Log($"[CustomBuildHandler] Building APK: {apkPath}");
                BuildReport apkReport = BuildPipeline.BuildPlayer(apkOptions);
                LogBuildResult(apkReport, "APK");

                var aabOptions = options;
                aabOptions.locationPathName = aabPath;
                EditorUserBuildSettings.buildAppBundle = true;
                Debug.Log($"[CustomBuildHandler] Building AAB: {aabPath}");
                BuildReport aabReport = BuildPipeline.BuildPlayer(aabOptions);
                LogBuildResult(aabReport, "AAB");

                EditorUserBuildSettings.buildAppBundle = originalBuildAppBundle;
            }
            catch (Exception ex)
            {
                Debug.LogError($"[CustomBuildHandler] Build process failed: {ex}");
            }
        }


        private static void LogBuildResult(BuildReport report, string buildType)
        {
            if (report == null)
            {
                Debug.LogError($"[CustomBuildHandler] {buildType} BuildReport is null");
                return;
            }

            if (report.summary.result == BuildResult.Succeeded)
                Debug.Log($"{buildType} Build succeeded: {report.summary.outputPath}");
            else
                Debug.LogError($"{buildType} Build failed: {report.summary.result}. Errors: {report.summary.totalErrors}");
        }

        private static int GetTargetApiVersion()
        {
            int apiVersion = (int)PlayerSettings.Android.targetSdkVersion;
            if (apiVersion == 0)
            {
                apiVersion = (int)PlayerSettings.Android.minSdkVersion;
                Debug.LogWarning($"[CustomBuildHandler] Target API was set to 'Auto', using minSdkVersion {apiVersion}");
            }
            return apiVersion;
        }

        private static string GetBuildDirectory(BuildPlayerOptions options)
        {
            string dir = "Builds";
            if (!string.IsNullOrEmpty(options.locationPathName))
                dir = Path.GetDirectoryName(options.locationPathName);

            if (!Directory.Exists(dir))
            {
                try
                {
                    Directory.CreateDirectory(dir);
                    Debug.Log($"[CustomBuildHandler] Created build directory: {dir}");
                }
                catch (Exception ex)
                {
                    Debug.LogError($"[CustomBuildHandler] Failed to create build directory '{dir}': {ex}");
                }
            }
            return dir;
        }

        private static string SanitizeName(string name)
        {
            foreach (var c in Path.GetInvalidFileNameChars())
                name = name.Replace(c.ToString(), "");
            return name.Replace(" ", "_");
        }
    }
#endif
}