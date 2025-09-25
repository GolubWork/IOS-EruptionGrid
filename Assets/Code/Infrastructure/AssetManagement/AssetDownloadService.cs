using System;
using System.Collections.Generic;
using System.Linq;
using Code.Common.Extensions;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.AddressableAssets;
using UnityEngine.AddressableAssets.ResourceLocators;
using UnityEngine.ResourceManagement.AsyncOperations;
using UnityEngine.ResourceManagement.ResourceLocations;

namespace Code.Infrastructure.AssetManagement
{
    public class AssetDownloadService : IAssetDownloadService
    {
        private readonly IAssetDownloadReporter _downloadReporter;

        private List<IResourceLocator> _catalogLocators;
        private long _downloadSize;

        public AssetDownloadService(IAssetDownloadReporter downloadReporter)
        {
            _downloadReporter = downloadReporter;
        }

        public async UniTask InitializeDownloadDataAsync()
        {
            await Addressables.InitializeAsync().ToUniTask();
            await UpdateCatalogsAsync();
            await UpdateDownloadSizeAync();
        }

        public float GetDownloadSizeMb() => SizeToMb(_downloadSize);

        public async UniTask UpdateContentAsync()
        {
            if (_catalogLocators == null)
                await UpdateCatalogsAsync();

            IList<IResourceLocation> locations = await RefreshResourceLocations(_catalogLocators);
            if (locations.IsNullOrEmpty())
            {
                Debug.Log("location is null or empty");
                return;
            }

            await DownloadContentWithPreciseProgress(locations);
        }

        //TODO Try Catch
        // Might be problems with ReleaseHdnling
        private async UniTask DownloadContent(IList<IResourceLocation> locations)
        {
            UniTask downloadTask = Addressables
                .DownloadDependenciesAsync(locations, autoReleaseHandle: true)
                .ToUniTask(progress: _downloadReporter);

            await downloadTask;

            if (downloadTask.Status.IsFaulted())
                Debug.LogError("Error downloading content");

            _downloadReporter.Reset();
        }

        //TODO Try Catch
        private async UniTask DownloadContentWithPreciseProgress(IList<IResourceLocation> locations)
        {
            AsyncOperationHandle downloadHandle = Addressables.DownloadDependenciesAsync(locations);

            while (!downloadHandle.IsDone && downloadHandle.IsValid())
            {
                await UniTask.Yield();
                _downloadReporter.Report(downloadHandle.GetDownloadStatus().Percent);
            }

            _downloadReporter.Report(1);
            if (downloadHandle.Status == AsyncOperationStatus.Failed)
                Debug.LogError("Error downloading content");

            if (downloadHandle.IsValid())
                Addressables.Release(downloadHandle);

            _downloadReporter.Reset();
        }

        private async UniTask UpdateCatalogsAsync()
        {
            List<string> catalogsToUpdate = await Addressables.CheckForCatalogUpdates().ToUniTask();
            if (catalogsToUpdate.IsNullOrEmpty())
            {
                _catalogLocators = Addressables.ResourceLocators.ToList();
                return;
            }

            _catalogLocators = await Addressables.UpdateCatalogs(catalogsToUpdate).ToUniTask();
        }

        private async UniTask UpdateDownloadSizeAync()
        {
            IList<IResourceLocation> locations = await RefreshResourceLocations(_catalogLocators);

            if (locations.IsNullOrEmpty())
            {
                Debug.Log("locations is null or empty");
                return;
            }

            _downloadSize = await Addressables
                .GetDownloadSizeAsync(locations)
                .ToUniTask();
        }

        private async UniTask<IList<IResourceLocation>> RefreshResourceLocations(IEnumerable<IResourceLocator> locators)
        {
            IEnumerable<object> keysToCheck = locators.SelectMany(x => x.Keys);

            return await Addressables
                .LoadResourceLocationsAsync(keysToCheck, Addressables.MergeMode.Union)
                .ToUniTask();
        }

        private static float SizeToMb(long downloadSize) => downloadSize * 1f / 1048576;
    }
}