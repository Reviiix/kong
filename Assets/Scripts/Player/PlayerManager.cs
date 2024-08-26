﻿using System;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerManager : MonoBehaviour
    {
        public const string PlayerTag = "Player";
        public static PlayerManager Instance;
        private PlayerAnimations animations;
        private PlayerMovement movement;
        private PlayerLives lives;
        [SerializeField] private PlayerHammer hammer;
        public bool Climbing => movement.Climbing;
        public bool InAir => movement.InAir;

        private void Awake()
        {
            Initialise();
        }
        
        private void Initialise()
        {
            Instance = this;
            movement = new PlayerMovement(new []{transform}, GetComponent<Rigidbody2D>());
            animations = new PlayerAnimations(GetComponent<Animator>(), transform);
            lives = new PlayerLives();
        }

        private void OnEnable()
        {
            Ladder.OnPlayerEnter += OnLadderEnter;
            CauseDamage.OnPlayerEnter += ReceiveDamage;
        }
        
        private void OnDisable()
        {
            Ladder.OnPlayerEnter -= OnLadderEnter;
            CauseDamage.OnPlayerEnter -= ReceiveDamage;
        }

        public void FixedUpdate()
        {
            movement.OnFixedUpdate();
        }
        
        public void Update()
        {
            movement.OnUpdate();
            animations.OnUpdate();
        }
        
        private void OnLadderEnter(bool enter)
        {
            movement.OnLadderEnter(enter);
        }
        
        public void EnableMovement(bool state = true)
        {
            movement.Enable(state);
        }
        
        public void TransportPlayer(Vector3 position)
        {
            transform.localPosition = position;
        }

        private void AddLife()
        {
            lives.AddLife();
        }

        public void HammerPickup(int time = 10)
        {
            animations.HammerPickup(time);
        }

        private void ReceiveDamage()
        {
            lives.RemoveLife((livesRemaining) =>
            {
                EnableMovement(false);
                animations.Death();
                GameManager.Instance.EndRound(livesRemaining);
            });
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
            TransportPlayer(point);
            EnableMovement();
            ResetDeadAnimation();
        }
        
    }
}