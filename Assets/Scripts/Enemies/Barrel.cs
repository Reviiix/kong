using Environment;
using UnityEngine;

namespace Enemies
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Barrel : CauseDamage
    {
        private Rigidbody2D barrelRigidBody;
        private int PushForce = 5;
        private void Awake()
        {
            barrelRigidBody = GetComponent<Rigidbody2D>();
        }
    
        protected override void OnTriggerEnter2D(Collider2D other)
        {
            base.OnTriggerEnter2D(other);
            if (other.CompareTag("Floor"))
            {
                PushObject(other.GetComponent<Floor>().direction);
            }
        }

        private void PushObject(bool direction)
        {
            if (direction) //right
            {
                barrelRigidBody.velocity = Vector2.right * PushForce;
            }
            else //left
            {
                barrelRigidBody.velocity = Vector2.left * PushForce;
            }
        }
    }
}
