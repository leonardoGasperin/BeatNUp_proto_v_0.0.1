using Domain.Enum;
using Domain.Primitive;
using Unity.Burst.CompilerServices;
using UnityEngine;

namespace Domain.Entities
{
    public class Enemy : Character
    {
        private SubjectA subjectA;
        public EnemyType enemyType;

        protected override void Start()
        {
            base.Start();
            subjectA = GameObject.FindGameObjectWithTag("Player").GetComponent<SubjectA>();
        }

        protected override void Update()
        {
            base.Update();
        }

        private bool CanSeePlayer()
        {
            Vector2 directionToPlayer = subjectA.transform.position - transform.position;
            Debug.DrawLine(transform.position, (Vector2)transform.position + directionToPlayer.normalized * 5f, Color.red);
            int layerMask = 1 << subjectA.gameObject.layer;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, 5f, layerMask);

            return hit.collider != null && hit.collider.gameObject.layer == subjectA.gameObject.layer;
        }
    }
}
