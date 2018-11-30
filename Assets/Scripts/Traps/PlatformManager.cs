using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformManager : MonoBehaviour {

    public static PlatformManager Instance = null; //Allows other game objects to call on the PlatformManager

    [SerializeField] GameObject FallingPlatform; //Reference to the falling platform prefab which must be assigned in the inspector

    public float createNew = 5; //amount of time to create platform from the time the fallingPlatform script detects the player

    void Awake() //initializes the PlatformManager
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
        
    void Start () //Creates the platforms below at the positions indicated [new Vector2(x position, y position)]
    {
        Instantiate(FallingPlatform, new Vector2(-2.2f, -2.46f), FallingPlatform.transform.rotation);
        Instantiate(FallingPlatform, new Vector2(3.42f, -2.46f), FallingPlatform.transform.rotation);
    }

    /// <summary>
    /// This is called on by the "FallingPlatform" script after it detects the player. 
    /// The "FallingPlatform" script will pass on it's own transform so that PlatformManager creates a new prefab in that same transform location.
    /// </summary>
    /// <param name="spawnPosition"></param>
    /// <returns></returns>

    IEnumerator spawnPlatform(Vector2 spawnPosition) 
    {
        yield return new WaitForSeconds(createNew);
        Instantiate(FallingPlatform, spawnPosition, FallingPlatform.transform.rotation);
    }
}
