using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public List<GameObject> players;
    public ObjectPool objectPool;
    public EnemyVariance enemyVariance;
    public static GameManager instance;

    private void Awake()
    {
        instance = this;
        objectPool = gameObject.GetComponent<ObjectPool>();
        enemyVariance = gameObject.GetComponent<EnemyVariance>();
        players = new List<GameObject>();
    }

}
