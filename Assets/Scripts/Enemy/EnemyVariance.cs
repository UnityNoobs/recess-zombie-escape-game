using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyVariance : MonoBehaviour {
    public float variance = 0.0f;
    public float varianceIncrement = 0.01f;
    public float varianceFrequency = 10f;
    // Use this for initialization
    void Start () {
        StartCoroutine(UpdateVariance(varianceFrequency));
    }
    private IEnumerator UpdateVariance(float waitTime)
    {
        yield return new WaitForSeconds(waitTime);
        variance += varianceIncrement;
        Debug.Log("The variance is now: " + variance);
        StartCoroutine(UpdateVariance(varianceFrequency));
        yield return null;
    }

}
