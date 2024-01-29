using Domain.Enum;
using Domain.Primitive;
using UnityEngine;

namespace Domain.Entities
{
    public class Enemy : Character
    {
        private SubjectA subjectA;
        private Transform transform;
        public EnemyType enemyType;

        protected override void Start()
        {
            base.Start();
            transform = GetComponent<Transform>();
            subjectA = GameObject.FindGameObjectWithTag("Player").GetComponent<SubjectA>();
        }

        protected override void Update()
        {
            base.Update();

            Vector2 directionToPlayer = subjectA.transform.position - transform.position;
            RaycastHit2D hit = Physics2D.Raycast(transform.position, directionToPlayer, Mathf.Infinity, subjectA.gameObject.layer);

            if (hit.collider != null && hit.collider.gameObject == subjectA)
            {
                Debug.Log("O inimigo pode ver o jogador");
            }
            else
            {
                Debug.Log("O inimigo não pode ver o jogador");
            }

        }
    }
}
