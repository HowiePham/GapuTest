using System;
using UnityEngine;

public class SelfDestructEffect : MonoBehaviour
{
    [SerializeField] private float destructTime;

    private void OnEnable()
    {
        Invoke(nameof(SelfDestruct), destructTime);
    }

    private void SelfDestruct()
    {
        Destroy(gameObject);
    }
}