using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class SmoothHealthSlider : HealthUIBase
{
    [SerializeField] private Slider _smoothHealthSlider;
    [SerializeField] private float _smoothSpeed = 5f;
    [SerializeField] private float _smoothLimitTime = 1f;

    private Coroutine _smoothSliderCoroutine;

    protected override void Initialize()
    {
        _smoothHealthSlider.maxValue = _health.MaxValue;
        _smoothHealthSlider.value = _health.CurrentValue;
    }

    protected override void OnValueChangedUpdate()
    {
        if (_smoothSliderCoroutine != null)
            StopCoroutine(_smoothSliderCoroutine);

        _smoothSliderCoroutine = StartCoroutine(SmoothSliderUpdate());
    }

    private IEnumerator SmoothSliderUpdate()
    {
        float startValue = _smoothHealthSlider.value;
        float targetValue = _health.CurrentValue;
        float elapsedTime = 0f;

        while (elapsedTime < _smoothLimitTime)
        {
            elapsedTime += Time.deltaTime * _smoothSpeed;
            _smoothHealthSlider.value = Mathf.Lerp(startValue, targetValue, elapsedTime);
            yield return null;
        }

        _smoothHealthSlider.value = targetValue;
    }
}
