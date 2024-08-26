using UnityEngine;

namespace Player
{
    public class PlayerHammer : MonoBehaviour
    {
        [SerializeField] private GameObject up;
        [SerializeField] private GameObject down;
        private static bool _state = true;

        public void SwapHammerPosition()
        {
            up.SetActive(_state);
            down.SetActive(!_state);

            _state = !_state;
        }

        public void UpState()
        {
            up.SetActive(true);
            down.SetActive(false);
        }
        
        public void DownState()
        {
            down.SetActive(true);
            up.SetActive(false);
        }
    }
}
