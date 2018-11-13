using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface EnemyStats
{
    int Experience { get; set; }
    void TakeDamage(int amount);
    void AttackDamage();
    void Die();
}
