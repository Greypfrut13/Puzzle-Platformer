using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine;

public class HealthComponent : MonoBehaviour
{
    [SerializeField] private int _health;
    [SerializeField] private UnityEvent _onDamage;
    [SerializeField] private UnityEvent _onDie;

    private int _maxHealth;

    private void Start() 
    {
        _maxHealth = _health;
    }

    public void ApplyDamage(int damageValue)
    {
        _health -= damageValue;
        _onDamage?.Invoke();
        if(_health <= 0)
        {
            _onDie?.Invoke();
        }
    }

    public void ApplyHeal(int healthValue)
    {
        _health += healthValue;
        if(_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
