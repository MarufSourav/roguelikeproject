using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDamageZone : MonoBehaviour
{
    public EnemyHP EHP;
    public void TakeDamage(float damage)
    {
        EHP.HP(damage);
    }
}
