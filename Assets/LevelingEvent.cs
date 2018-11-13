using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelingEvent : MonoBehaviour {

    public delegate void PlayerLevelHandler(EnemyStats enemy);
    public static event PlayerLevelHandler onEnemyDeath;

    public static void EnemyDied(EnemyStats enemy)
    {
        if(onEnemyDeath != null)
        onEnemyDeath(enemy);
    }
}
