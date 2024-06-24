using UnityEngine;
using UnityEngine.Events;

public class Health : MonoBehaviour
{
    public event UnityAction<Component, float> OnHit;
    public event UnityAction<Component, float> OnFill;
    public event UnityAction<Component> OnDeath;

    public float MaxHealth { get => _maxHealth; }
    public float CurrentHealth { get => _currentHealth; }
    public float PercentFilled => _currentHealth / _maxHealth;
    public bool Dead => _currentHealth <= 0.0f;

    [SerializeField] private float _maxHealth;
    [SerializeField] private float _currentHealth;

    public void TakeDamage(Component instigator, float amount)
    {
        if (amount <= 0 || CurrentHealth <= 0) return;

        _currentHealth -= amount;
        OnHit?.Invoke(instigator, amount);
        

        if (CurrentHealth <= 0) OnDeath?.Invoke(instigator);
    }

    public void FillHeatlh(Component instigator, float amount)
    {
        if (amount <= 0) return;

        float lastHealth = _currentHealth;
        _currentHealth += amount;

        if (CurrentHealth > MaxHealth)
        {
            _currentHealth = Mathf.Clamp(CurrentHealth, 0, MaxHealth);
            OnFill?.Invoke(instigator, CurrentHealth - lastHealth);
        }
        else OnFill?.Invoke(instigator, amount);
    }
}
