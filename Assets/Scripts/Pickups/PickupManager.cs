using System;

namespace Pickups
{
    public static class PickupManager
    {
        public static void OnCollected(PickupType type)
        {
            switch (type)
            {
                case PickupType.Hammer:
                    //GameManager.Instance.HammerPickup();
                    break;
                default:
                    UnityEngine.Debug.LogError("NOt a valid collectable type, RETURN POINTS INSTEAD OF  BREAKING");
                    throw new ArgumentOutOfRangeException(nameof(type), type, null);
            }
        }
    }
}
