using Code.Infrastructure.AssetManagement;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Code.Meta.UI.HUD.LoadingWindow
{
    public class LoadingController: MonoBehaviour
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image progressImage;
        [SerializeField] private Image rotateImage;
        
        private LoadingWindowView _view;
        private IAssetDownloadReporter _downloadReporter;

        [Inject]
        private void Construct(IAssetDownloadReporter downloadReporter)
        {
            _downloadReporter = downloadReporter;
        }
        private void Awake()
        {
            _view = new LoadingWindowView(progressImage, rotateImage);
            _downloadReporter.ProgressUpdated += DisplayDownloadProgress;
        }

        public void Show()
        {
            canvas.enabled = true;
            _view.RotateImage(2);
        }

        public void Hide()
        {
            canvas.enabled = false;
        }
        
        private void OnDestroy()
        {
            _view.CleanUp();
            _downloadReporter.ProgressUpdated -= DisplayDownloadProgress;
        }
        
        private void DisplayDownloadProgress()
        {
            _view.SetProgress(_downloadReporter.Progress);
        }
        
    }
}