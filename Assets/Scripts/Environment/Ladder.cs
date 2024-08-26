using System;
using JetBrains.Annotations;
using Player;
using UnityEngine;

[RequireComponent(typeof(BoxCollider2D))]
public class Ladder : MonoBehaviour
{
    public static Action<bool, Vector3> OnPlayerEnter;
    [CanBeNull] [SerializeField] private BoxCollider2D floor;
    
    private void OnEnable()
    {
        OnPlayerEnter += EnableFloor;
    }
    
    private void OnDisable()
    {
        OnPlayerEnter -= EnableFloor;
    }

    private void OnTriggerStay2D(Collider2D c)
    {
        if (!c.CompareTag(PlayerManager.PlayerTag)) return;

        OnPlayerEnter?.Invoke(true, transform.position);
    }

    private void OnTriggerExit2D(Collider2D c)
    {
        if (!c.CompareTag(PlayerManager.PlayerTag)) return;
        
        OnPlayerEnter?.Invoke(false, transform.position);
    }

    private void EnableFloor(bool state, Vector3 ladderPosition)
    {
        if (floor == null) return;
        if (!PlayerManager.Instance.Climbing) return;

        floor.enabled = !state;
    }
}
