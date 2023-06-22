using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    [SerializeField] [Min(0.0f)] private int _damage;

    public void ApplyDamage(GameObject target)
    {
        var healthComponent = target.GetComponent<HealthComponent>();
        if(healthComponent != null)
        {
            healthComponent.ApplyDamage(_damage);
        }
    }
}
