using Player;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Floor : MonoBehaviour
{
    /// <summary>
    /// false = left
    /// right = left
    /// </summary>
    public bool direction = true;
    public static bool PlayerGrounded { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerManager.PlayerTag))
        {
            PlayerGrounded = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (PlayerGrounded) return;
        
        if (other.CompareTag(PlayerManager.PlayerTag))
        {
            PlayerGrounded = true;
        }
        PlayerGrounded = false;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerManager.PlayerTag))
        {
            PlayerGrounded = false;
        }
    }
}
