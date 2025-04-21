using TMPro;
using UnityEngine;

public class HealthDisplay : HealthUIBase
{
    [SerializeField] private TMP_Text _displayText;

    protected override void Initialize()
    {
        UpdateText();
    }

    protected override void OnValueChangedUpdate()
    {
        UpdateText();
    }

    private void UpdateText()
    {
        _displayText.text = $"{_health.CurrentValue}/{_health.MaxValue}";
    }
}