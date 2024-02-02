using Domain.Repository;
using Domain.Rules;
using Infracstructure.Repository;
using UnityEngine;

namespace Domain.Primitive
{
    public class Character : MonoBehaviour
    {
        public ICombatRepository combat;
        public IMovementRepository movement;
        public Rigidbody2D rigbody2D;
        public int level;
        public int healthPoint;
        public int damage;
        public float movementSpeed;
        public float jumpForce;
        public bool isLive;
        public bool isAttacking;
        public bool isGrounded;
        public bool canJump;

        protected virtual void Start()
        {
            rigbody2D = gameObject.GetComponent<Rigidbody2D>();
            combat = new CombatRepository();
            movement = new MovementRepository();
        }

        protected virtual void Update()
        {
            if (isLive && healthPoint <= 0)
            {
                healthPoint = 0;
                isLive = false;
                Debug.Log("HP " + gameObject.name + ": " + healthPoint);
                Debug.Log(gameObject.name + " is live: " + isLive);
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = true;
                canJump = true;
            }
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            if (collision.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                isGrounded = false;
                canJump = false;
            }
        }

        public void DoDamage(Character target)
        {
            if (target != null && target.isLive)
            {
                target.healthPoint = target.combat.TakeDamage(target.healthPoint, damage);
                Debug.Log(target.gameObject.name + " recivied DMG: " + damage);
            }
        }
    }
}
