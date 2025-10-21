using DG.Tweening;
using UnityEngine;

[DisallowMultipleComponent]
public class SlightlyShake : MonoBehaviour
{
    [Header("Shake settings")]
    [SerializeField] private float _shakeStrength = 0.05f;     // амплитуда по X/Y в юнитах
    [SerializeField] private float _shakeDuration = 0.3f;      // длительность одного цикла
    [SerializeField] private int _vibrato = 10;                // количество "вздрагиваний"
    [Tooltip("Случайность направления в градусах (0..180)")]
    [SerializeField] private float _randomness = 90f;          // 0..180
    [SerializeField] private bool _snapping = false;           // приглаживать к пиксельной сетке
    [SerializeField] private bool _autoStart = true;           // запускать при старте

    private Tween _shakeTween;
    private Vector3 _originalLocalPos;
    private bool _isShaking;

    private void Awake()
    {
        _originalLocalPos = transform.localPosition;
    }

    private void Start()
    {
        if (_autoStart)
            StartShake();
    }

    public void StartShake()
    {
        if (_isShaking) return;

        var strength = new Vector3(_shakeStrength, _shakeStrength, 0f);

        _shakeTween = transform
            .DOShakePosition(_shakeDuration, strength, _vibrato, _randomness, _snapping)
            .SetLoops(-1, LoopType.Restart)
            .SetEase(Ease.Linear);

        _isShaking = true;
    }

    public void StopShake(bool restorePosition = true)
    {
        if (!_isShaking) return;

        _shakeTween?.Kill();
        _shakeTween = null;
        _isShaking = false;

        if (restorePosition)
            transform.localPosition = _originalLocalPos;
    }

    private void OnDestroy()
    {
        _shakeTween?.Kill();
        transform.localPosition = _originalLocalPos;
    }
}