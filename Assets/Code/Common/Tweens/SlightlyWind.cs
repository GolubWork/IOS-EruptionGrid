using System.Collections;
using DG.Tweening;
using UnityEngine;

public class SlightlyWind : MonoBehaviour
{

    [SerializeField] private float _angle = 3f;        // Максимальный угол отклонения (в градусах)
    [SerializeField] private float _duration = 2f;     // Время одного покачивания
    [SerializeField] private Ease _ease = Ease.InOutSine;

    private Tween _swingTween;

    private void Start()
    {
        float randomOffset = Random.Range(0f, 1f); // чтобы не все качались синхронно

        _swingTween = DOVirtual.DelayedCall(randomOffset, () =>
        {
            _swingTween = transform
                .DORotate(new Vector3(0, 0, _angle), _duration)
                .SetEase(_ease)
                .SetLoops(-1, LoopType.Yoyo);
        });
    }

    private void OnDestroy()
    {
        _swingTween?.Kill();
    }
}


