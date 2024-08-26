using Player;
using UnityEngine;

namespace Pickups
{
    public class Pickup : MonoBehaviour
    {
        [SerializeField] protected PickupType type;
        private const string PlayerTag = PlayerManager.PlayerTag;

        private void OnPlayerEnter()
        {
            PickupManager.OnCollected(type);
            Destroy(gameObject);
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag(PlayerTag))
            {
                OnPlayerEnter();
            }
        }
    }

    public enum PickupType
    {
        Hammer = 0,
    }
}
