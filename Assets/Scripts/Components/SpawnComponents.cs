using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnComponents : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private GameObject _prefab;

    [ContextMenu("Spawn")]
    public void Spawn()
    {
        Instantiate(_prefab, _target.position, Quaternion.identity);
    }
}
