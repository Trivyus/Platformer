using UnityEngine;
using UnityEngine.UI;

public class HealthSlider : HealthUIBase
{
    [SerializeField] private Slider _slider;

    protected override void Initialize()
    {
        _slider.maxValue = _health.MaxValue;
        UpdateSliderValue();
    }

    protected override void OnValueChangedUpdate()
    {
        UpdateSliderValue();
    }

    private void UpdateSliderValue()
    {
        _slider.value = _health.CurrentValue;
    }
}
