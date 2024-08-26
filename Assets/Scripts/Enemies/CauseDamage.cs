using System;
using Player;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public abstract class CauseDamage : MonoBehaviour
{
    public static Action OnPlayerEnter;

    protected virtual void OnTriggerEnter2D(Collider2D c)
    {
        if (!c.CompareTag(PlayerManager.PlayerTag)) return;
        
        OnPlayerEnter?.Invoke();
    }
}
