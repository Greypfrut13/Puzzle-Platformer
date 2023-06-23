using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChangeHealthComponent : MonoBehaviour
{
    [SerializeField] [Min(0.0f)] private int _value;

    public void ApplyDamage(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.ApplyDamage(_value);
        }
    }

    public void ApplyHeal(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();
        if(healthComponent!= null)
        {
            healthComponent.ApplyHeal(_value);
        }
    }
}
