using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public int attackDamage;
    public Vector3 attackOffset;
    public float attackRange = 2f;
    public LayerMask attackMask;
    public Boss boss;
    // Animation Event
    public void Attack()
    {
        attackDamage = boss.attackDamage;
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Collider2D colInfo = Physics2D.OverlapCircle(pos, attackRange, attackMask);
        if (colInfo != null)
        {
            // colInfo.GetComponent<Health>().CurHealth -= attackDamage;
            // GlobalAnalysis.boss_damage += attackDamage;
            // if  (colInfo.GetComponent<Energy>().CurEnergy < attackDamage) {
            //     GlobalAnalysis.player_status = "boss_attack_dead";
            //     Debug.Log("lose by: boss");
            // }
            colInfo.GetComponent<Player>().TakeDamage(attackDamage, new Object[]{gameObject});
            Debug.Log("Boss Attack");
        }
    }
    // help to see the attack circle range
    // The white circle of the object
    void OnDrawGizmosSelected()
    {
        Vector3 pos = transform.position;
        pos += transform.right * attackOffset.x;
        pos += transform.up * attackOffset.y;

        Gizmos.DrawWireSphere(pos, attackRange);
    }
}