using Code.Infrastructure.AssetManagement;
using Code.Infrastructure.DependencyInjection;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.LoadingWindow
{
    public class LoadingController: MonoBehaviour, IInitializable
    {
        [SerializeField] private Canvas canvas;
        [SerializeField] private Image progressImage;
        [SerializeField] private Image rotateImage;
        [SerializeField] private CanvasGroup canvasGroup;
        
        private LoadingWindowView _view;
        private IAssetDownloadReporter _downloadReporter;

        [Inject]
        private void Construct(IAssetDownloadReporter downloadReporter)
        {
            _downloadReporter = downloadReporter;
        }
        public void Initialize()
        {
            _downloadReporter.ProgressUpdated += DisplayDownloadProgress;
        }
        private void Awake()
        {
            if (canvas == null) canvas = GetComponentInChildren<Canvas>(true);
            _view = new LoadingWindowView(progressImage, rotateImage);
        }

        public void Show()
        {
            if (canvas == null)
            {
                canvas = GetComponentInChildren<Canvas>(true);
                if (canvas == null)
                {
                    Debug.LogWarning("[LoadingController] Canvas is missing, cannot Show()");
                    return;
                }
            }

            canvas.enabled = true;
            _view.RotateImage(2);
        }

        public void Hide()
        {
            if (canvasGroup == null || canvas == null) return;
            canvasGroup.DOFade(0f, 0.5f).OnComplete(() =>
            {
                canvas.enabled = false;
                canvasGroup.alpha = 1;
            });
        }
        
        private void OnDestroy()
        {
            _view?.CleanUp();
            if (_downloadReporter != null)
                _downloadReporter.ProgressUpdated -= DisplayDownloadProgress;
        }
        
        private void DisplayDownloadProgress()
        {
            _view.SetProgress(_downloadReporter.Progress);
        }


    }
}