using DG.Tweening;
using UnityEngine;

public class CoinFlipper : MonoBehaviour
{
    private Tween _flipTween;
    private Tween _floatTween;

    private void Awake()
    {
        StartFlip();
    }

    public void StartFlip()
    {
        _flipTween?.Kill();
        _floatTween?.Kill();

        _flipTween = transform
            .DOLocalRotate(new Vector3(0, 360, 0), 2f, RotateMode.FastBeyond360)
            .SetEase(Ease.Linear)
            .SetLoops(-1, LoopType.Restart);

        float startY = transform.localPosition.y;
        _floatTween = transform
            .DOLocalMoveY(startY + 0.5f, 1f)
            .SetEase(Ease.InOutSine)
            .SetLoops(-1, LoopType.Yoyo);
    }

    private void OnDestroy()
    {
        _flipTween?.Kill();
        _floatTween?.Kill();
    }
}