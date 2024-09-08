using System;
using Enemies;
using Environment;
using PureFunctions.UnitySpecific;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerManager : Singleton<PlayerManager>
    {
        public const string PlayerTag = "Player";
        private PlayerAnimations animations;
        private PlayerMovement movement;
        private PlayerLives lives;
        private bool Invincible;
        [SerializeField] private PlayerHammer hammer;
        public bool Climbing => movement.Climbing;
        public bool InAir => movement.InAir;

        private void Awake()
        {
            movement = new PlayerMovement(new []{transform}, GetComponent<Rigidbody2D>());
            animations = new PlayerAnimations(GetComponent<Animator>(), transform);
            lives = new PlayerLives();
        }

        protected override void OnEnable()
        {
            base.OnEnable();
            Ladder.OnPlayerEnter += OnLadderEnter;
            CauseDamage.OnPlayerEnter += ReceiveDamage;
        }
        
        protected override void OnDisable()
        {
            base.OnDisable();
            Ladder.OnPlayerEnter -= OnLadderEnter;
            CauseDamage.OnPlayerEnter -= ReceiveDamage;
        }

        public void Update()
        {
            movement.OnUpdate();
            animations.OnUpdate();
        }
        
        private void OnLadderEnter(bool enter, Vector3 ladderPosition)
        {
            movement.OnLadderEnter(enter);
            TransportPlayer(new[] {new Vector3(ladderPosition.x, transform.position.y)});
        }
        
        public void EnableMovement(bool state = true)
        {
            movement.Enable(state);
        }
        
        public void TransportPlayer(Vector3[] position)
        {
            movement.MovePlayer(position, 1);
        }

        private void AddLife()
        {
            lives.AddLife();
        }

        private void ReceiveDamage()
        {
            if (Invincible) return;
            lives.RemoveLife((livesRemaining) =>
            {
                EnableMovement(false);
                animations.Death();
                GameManager.Instance.EndRound(livesRemaining);
            });
        }
        
        public void StartHammerTime(int time = 10)
        {
            animations.HammerPickup(time);
            Invincible = true;

            StartCoroutine(Wait.WaitThenCallBack(time, StopHammerTime));
        }
        
        public void StopHammerTime()
        {
            animations.HammerPickupOver();
            Invincible = false;
        }

        public void Dead()
        {
            animations.Dead();
        }
        
        public void ResetDeadAnimation()
        {
            animations.UnDead();
        }
        
        public void Respawn(Vector3 point)
        {
            TransportPlayer(new [] { new Vector3(point.x, point.y, point.z)});
            EnableMovement();
            ResetDeadAnimation();
        }
        
    }
}
