using Infracstructure.Repository;
using Misc.Experimental;
using UnityEngine;

public class SubjectA : SubjectFather
{
    void Start()
    {
        combat = new CombatRepository();
        healthPoint = 25;
        isLive = true;
        damage = 25;
    }

    void Update()
    {
        if (isLive && healthPoint <= 0)
        {
            healthPoint = 0;
            isLive = false;
            Debug.Log("HP A: " + healthPoint);
            Debug.Log("Subject A is live: " + isLive);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var enemy = collision.gameObject.GetComponent<SubjectFather>() as SubjectB;

        if (enemy && enemy.isLive && isAttacking)
        {
            enemy.healthPoint = enemy.combat.TakeDamage(enemy.healthPoint, damage);
            isAttacking = !isAttacking;
        }
    }
}
