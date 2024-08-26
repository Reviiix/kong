using Player;
using UnityEngine;

[RequireComponent(typeof(EdgeCollider2D))]
public class Floor : MonoBehaviour
{
    public static bool PlayerOnGround { get; private set; }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag(PlayerManager.PlayerTag))
        {
            PlayerOnGround = true;
        }
    }
    
    private void OnTriggerStay2D(Collider2D other)
    {
        if (PlayerOnGround) return;
        
        if (other.CompareTag(PlayerManager.PlayerTag))
        {
            PlayerOnGround = true;
        }
        PlayerOnGround = false;
    }
    
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag(PlayerManager.PlayerTag))
        {
            PlayerOnGround = false;
        }
    }
}
