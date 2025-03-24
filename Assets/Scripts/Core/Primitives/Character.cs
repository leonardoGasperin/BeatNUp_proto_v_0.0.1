using Configuration;
using Core.Contract;
using Infracstructure.Handler;
using UnityEngine;

namespace Core.Primitive
{
    public class Character : MonoBehaviour
    {
        public ICombatRepository combat;
        public IMovementRepository movement;
        public Rigidbody2D rigbody2D;
        public int level;
        public int healthPoint;
        public int damage;

        [Range(0f, 1f)]
        public float blockingRate;
        public float movementSpeed;
        public float jumpForce;
        public bool isLive;
        public bool isAttacking;
        public bool isBlocking;
        public bool isGrounded;
        public bool canJump;
        public bool isDebugRaycast;

        private CollisionHandler collisionHandler;

        protected virtual void Start()
        {
            rigbody2D = gameObject.GetComponent<Rigidbody2D>();
            collisionHandler = new CollisionHandler(this);
            combat = ServiceLocator.Resolve<ICombatRepository>();
            movement = ServiceLocator.Resolve<IMovementRepository>();
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
            collisionHandler.OnCollisionEnter2D(collision);
        }

        private void OnCollisionExit2D(Collision2D collision)
        {
            collisionHandler.OnCollisionExit2D(collision);
        }

        public void DoDamage(Character target)
        {
            if (target == null || !target.isLive || !this.isLive) return;

            var finalDamage = target.isBlocking
                ? combat.BlockingAbsorbDamage(damage, target.blockingRate)
                : damage;

            target.healthPoint = target.combat.TakeDamage(target.healthPoint, finalDamage);
            Debug.Log(target.gameObject.name + " recivied DMG: " + finalDamage);
        }
    }
}
