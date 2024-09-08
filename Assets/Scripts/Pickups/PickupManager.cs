using System.Collections.Generic;
using UnityEngine;

namespace Pickups
{
    public class PickupManager : MonoBehaviour
    {
        [SerializeField] private Transform spawnPoints;
        private List<Pickup> items;
        //[SerializeField] itemPrefab

        private void Spawn(PickupType item)
        {
            
        }
        
        public static void OnCollected(PickupType type)
        {
            switch (type)
            {
                case PickupType.Hammer:
                    GameManager.Instance.HammerPickup();
                    break;
                default:
                    Debug.LogError($"Not a valid {typeof(PickupType)}, {type}.");
                    break;
            }
        }
    }
}
