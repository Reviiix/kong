﻿using System;
using System.Linq;
using PureFunctions.Movement;
using UnityEngine;

namespace Player
{
    [Serializable]
    public class PlayerMovement
    {
        private bool enabled;
        private Transform[] playerTransform; //Using an array because the movement system handles arrays better than single transforms (job system).
        private Rigidbody2D playerRigidBody;
        
        private const int MovementSpeed = 100;
        private bool moveLeft;
        private bool moveRight;
        
        private const int JumpHeight = 250;
        private bool jump;
        public bool InAir => !Floor.PlayerOnGround;
        
        private const int ClimbingSpeed = 10;
        private bool climbUp;
        private bool climbDown;
        private bool canClimb;
        public bool Climbing { get; private set; }

        private float defaultGravityScaleCache;

        public PlayerMovement(Transform[] transform, Rigidbody2D rigidBody)
        {
            playerTransform = transform;
            playerRigidBody = rigidBody;
            defaultGravityScaleCache = playerRigidBody.gravityScale;
        }

        public void OnUpdate()
        {
            if (!enabled) return;
            CheckForInput();
        }
        
        public void OnFixedUpdate()
        {
            if (!enabled) return;
            MoveObjects();
        }

        private void MoveObjects()
        {
            if (climbUp)
            {
                ClimbUp();
                return;
            }
            if (climbDown)
            {
                ClimbDown();
                return;
            }
            if (jump)
            {
                Jump();
            }
        
            if (moveRight)
            {
                MovePlayerRight();
                return;
            }
        
            if (moveLeft)
            {
                MovePlayerLeft();
            }
        }

        private void CheckForInput()
        {
            if (canClimb)
            {
                if (Input.GetKey(KeyCode.UpArrow))
                {
                    climbUp = true;
                    return;
                }
                climbUp = false;
                
                if (Input.GetKey(KeyCode.DownArrow))
                {
                    climbDown = true;
                    return;
                }
                climbDown = false;
            }
            

            if (Input.GetKeyDown(KeyCode.Space))
            {
                jump = !InAir;
            }
        
            if (Input.GetKey(KeyCode.RightArrow))
            {
                moveRight = true;
                moveLeft = false;
                return;
            }
        
            if (Input.GetKey(KeyCode.LeftArrow))
            {
                moveLeft = true;
                moveRight = false;
                return;
            }

            SetPlayerStationary();
        }

        private void SetPlayerStationary()
        {
            moveLeft = false;
            moveRight = false;
            
            if (jump) return;
            
            jump = false;
        }

        private void MovePlayerRight()
        {
            if (playerTransform.First().position.x > 150) return;
        
            MovePlayer(new[] {new Vector3(playerTransform.First().localPosition.x+1, playerTransform.First().localPosition.y)}, MovementSpeed);
        }
    
        private void MovePlayerLeft()
        {
            if (playerTransform.First().position.x < -150) return;
        
            MovePlayer(new[] {new Vector3(playerTransform.First().localPosition.x-1, playerTransform.First().localPosition.y)}, MovementSpeed);
        }
        
        private void MovePlayer(Vector3[] newPosition, int movementSpeed)
        {
            MovementJobs.MoveObjects(playerTransform, newPosition, movementSpeed);
        }

        private void Jump()
        {
            if (InAir) return;

            //MovePlayer(new[] {new Vector3(_player.First().localPosition.x, _player.First().localPosition.y+JumpHeight)}, JumpSpeed);

            playerRigidBody.velocity = Vector2.up * JumpHeight;

            jump = false;
        }

        private void ClimbUp()
        {
            if (!canClimb) return;
            Climbing = true;
            playerRigidBody.gravityScale = 0;
            MovePlayer(new[] {new Vector3(playerTransform.First().localPosition.x, playerTransform.First().localPosition.y+1)}, ClimbingSpeed);
        }
        
        private void ClimbDown()
        {
            if (!canClimb) return;
            Climbing = true;
            playerRigidBody.gravityScale = 0;
            MovePlayer(new[] {new Vector3(playerTransform.First().localPosition.x, playerTransform.First().localPosition.y-1)}, ClimbingSpeed);
        }
        
        private void StopClimbing()
        {
            climbUp = false;
            climbDown = false;
            Climbing = false;
            playerRigidBody.gravityScale = defaultGravityScaleCache;
        }
        
        public void OnLadderEnter(bool enter)
        {
            canClimb = enter;
            if (!enter)
            {
                StopClimbing();
            }
        }

        public void Enable(bool state = true)
        {
            enabled = state;
        }
    }
}