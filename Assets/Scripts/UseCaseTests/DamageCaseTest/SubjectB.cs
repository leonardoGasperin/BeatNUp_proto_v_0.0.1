using Infracstructure.Repository;
using Domain.Primitive;
using UnityEngine;

public class SubjectB : Character
{
    void Start()
    {
        combat = new CombatRepository();
        healthPoint = 100;
        isLive = true;
        damage = 25;
    }

    void Update()
    {
        if (isLive && healthPoint <= 0)
        {
            healthPoint = 0;
            isLive = false;
            Debug.Log("HP B: " + healthPoint);
            Debug.Log("Subject B is live: " + isLive);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<Character>() as SubjectA;

        if (enemy && enemy.isLive && isAttacking)
        {
            enemy.healthPoint = enemy.combat.TakeDamage(enemy.healthPoint, damage);
            isAttacking = !isAttacking;
        }
    }
}
