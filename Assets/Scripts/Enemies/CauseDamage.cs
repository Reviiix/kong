using System;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CauseDamage : MonoBehaviour
{
    public static Action OnPlayerEnter;

    private void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag(PlayerManager.PlayerTag)) return;
        
        OnPlayerEnter?.Invoke();
    }
}
