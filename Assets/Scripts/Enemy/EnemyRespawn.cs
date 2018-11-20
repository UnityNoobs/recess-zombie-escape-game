using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRespawn : MonoBehaviour
{
    public int quantity = 5;
    public string poolName = "Enemies";
    public bool autoFirstSpawn = true;
    public List<Transform> spwanPoints;
    private int spawnCount = 0;
    private bool isEmpty = true;
    private bool firstSpawn = true;


    // Use this for initialization
    void Start()
    {
        isEmpty = spwanPoints.Count == 0;
        Debug.Log(isEmpty);

    }

    // Update is called once per frame
    void Update()
    {

        // Initial spwan
        if (firstSpawn && autoFirstSpawn)
        {
            Spawn();
            firstSpawn = false;
        }
    }

    private Transform GetRandomLocation()
    {
        int index = Random.Range(0, spwanPoints.Count);
        Debug.Log("Enemy has spwaned at index: " + index);
        return spwanPoints[index];
    }

    public void Spawn()
    {   
        if (spawnCount < quantity && !isEmpty)
        {
            Transform location = GetRandomLocation();
            GameObject enemy = ObjectPool.instance.SpawnFromPool(poolName, location.position, location.rotation);
            enemy.GetComponent<EnemyBehavior>().Restart();
            spawnCount++;
        }
    }
}
