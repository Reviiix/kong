using System;
using PureFunctions;
using UnityEngine;
using UnityEngine.UI;

namespace Player
{
    [Serializable]
    public class PlayerAnimations
    {
        private Animator _playerAnimator;
        private Transform _playerTransform;
        
        private static bool _facingRight = true;
        
        private static readonly int InAirHash = Animator.StringToHash("InAir");
        private static readonly int LeftHash = Animator.StringToHash("Left");
        private static readonly int RightHash = Animator.StringToHash("Right");
        private static readonly int ClimbHash = Animator.StringToHash("Climb");
        private static readonly int HammerHash = Animator.StringToHash("Hammer");
        private static readonly int DeathHash = Animator.StringToHash("Death");
        private static readonly int DeadHash = Animator.StringToHash("Dead");

        public PlayerAnimations(Animator playerAnimator, Transform playerTransform)
        {
            _playerAnimator = playerAnimator;
            _playerTransform = playerTransform;
        }

        public void OnUpdate()
        {
            CheckClimbingState();
            
            CheckInAirState();
            
            CheckForInput();
        }

        private void CheckForInput()
        {
            if (Input.GetKey(KeyCode.RightArrow))
            {
                MovePlayerRight();
                return;
            }
        
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                MovePlayerLeft();
                return;
            }
            
            SetPlayerStationary();
        }

        private void MovePlayerRight()
        {
            if (!_facingRight) FlipCharacterTransform();
            
            _facingRight = true;
            
            _playerAnimator.SetBool(LeftHash, false);
            _playerAnimator.SetBool(RightHash, true);
        }
        
        private void MovePlayerLeft()
        {
            if (_facingRight) FlipCharacterTransform();
            
            _facingRight = false;

            _playerAnimator.SetBool(LeftHash, true);
            _playerAnimator.SetBool(RightHash, false);
        }

        private void FlipCharacterTransform()
        {
            _playerTransform.localScale = new Vector2(-_playerTransform.localScale.x, 1);
        }
        
        private void CheckInAirState()
        {
            _playerAnimator.SetBool(InAirHash, PlayerManager.Instance.InAir);
        }
        
        private void CheckClimbingState()
        {
            _playerAnimator.SetBool(ClimbHash, PlayerManager.Instance.Climbing);
        }

        private void SetPlayerStationary()
        {
            _playerAnimator.SetBool(LeftHash, false);
            _playerAnimator.SetBool(RightHash, false);
        }

        public void HammerPickup(int time)
        {
            _playerAnimator.SetBool(HammerHash, true);
        }
        
        public void HammerPickupOver()
        {
            _playerAnimator.SetBool(HammerHash, false);
        }

        public void Death()
        {
            _playerAnimator.SetBool(DeathHash, true);
        }
        
        public void Dead()
        {
            _playerAnimator.SetBool(DeadHash, true);
        }
        
        public void UnDead()
        {
            _playerAnimator.SetBool(DeadHash, false);
            _playerAnimator.SetBool(DeathHash, false);
        }
    }
}
