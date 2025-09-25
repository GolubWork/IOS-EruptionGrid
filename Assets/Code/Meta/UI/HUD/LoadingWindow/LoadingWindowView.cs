using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace Code.Meta.UI.HUD.LoadingWindow
{
    public class LoadingWindowView
    {
        private readonly Image _progressSlider;
        private readonly Image _rotateImage;
        private Tween _rotateTween;
        
        public LoadingWindowView(Image progressSlider, Image rotateImage)
        {
            _progressSlider = progressSlider;
            _rotateImage = rotateImage;
        }

        public void SetProgress(float progress)
        {
            _progressSlider.fillAmount = progress;
            Debug.Log(_progressSlider.fillAmount);
        }

        public void RotateImage(float speed)
        {
            _rotateTween = _rotateImage.transform
                .DORotate(new Vector3(0, 0, 360f), speed, RotateMode.FastBeyond360)
                .SetEase(Ease.Linear)
                .SetLoops(-1, LoopType.Incremental);
        }

        public void CleanUp()
        {
            _rotateTween.Kill();
        }
        
    }
}