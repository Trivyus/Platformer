using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CooldownVisualizer : MonoBehaviour
{
    [SerializeField] private Image _cooldownImage;
    [SerializeField] private Vampirism _vampirism;

    private float _currentDuration;
    private bool _isOnCooldawn;

    private void OnEnable()
    {
        _vampirism.AbilityStarted += StartAbility;
        _vampirism.AbilityStartedRecharge += StartCooldown;
    }

    private void OnDisable()
    {
        _vampirism.AbilityStarted -= StartAbility;
        _vampirism.AbilityStartedRecharge -= StartCooldown;
    }

    public void StartAbility(float duration)
    {
        _currentDuration = duration;
        _cooldownImage.fillAmount = 1f;
        StartCoroutine(AbilityImageRoutine());
        _isOnCooldawn = true;
    }

    public void StartCooldown(float cooldown)
    {
        _currentDuration = cooldown;
        _cooldownImage.fillAmount = 0f;
        StartCoroutine(AbilityImageRoutine());
        _isOnCooldawn = false;
    }

    private IEnumerator AbilityImageRoutine()
    {
        float timer = _currentDuration;

        while (timer > 0)
        {
            timer -= Time.deltaTime;

            if(_isOnCooldawn)
                _cooldownImage.fillAmount -= Time.deltaTime / _currentDuration;
            else
                _cooldownImage.fillAmount += Time.deltaTime / _currentDuration;

            yield return null;
        }
    }
}
