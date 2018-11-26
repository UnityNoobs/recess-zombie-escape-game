using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoad : MonoBehaviour {

    [SerializeField] float levelLoadDelay = 2f; //Amount of time in seconds to load the scene.
    
    void OnTriggerEnter2D (Collider2D collider2D)
    {

        switch (collider2D.gameObject.tag)
        {
            case "Finish": //Unity Collider asset with "Finish" tag will load the next scene.
                StartSuccessSequence();
                break;
            case "Unfriendly": //Unity Collider asset with "Finish" tag will load the first scene.
                StartDeathSequence();
                break;                
            default:
                break;
        }
    }

    private void StartDeathSequence()
    {
        Invoke("LoadFirstLevel", levelLoadDelay); //Invokes the LoadFirstLevel fuction below, however, the call is delayed by the amount declared for the levelLoadDelay variable above.
    }

    private void StartSuccessSequence()
    {
        Invoke("LoadNextLevel", levelLoadDelay); //Invokes the LoadNextLevel fuction below, however, the call is delayed by the amount declared for the levelLoadDelay variable above.
    }

    private void LoadFirstLevel()
    {
        SceneManager.LoadScene(0); //Loads the first scene indicated in the project build settings
    }

    private void LoadNextLevel()
    {
        int currentSceneIndex = SceneManager.GetActiveScene().buildIndex; //Gets the index of the current/active scene from the build settings.
        int nextSceneIndex = currentSceneIndex + 1; //Gets the index of the following scene based on the current scene.
        int LastSceneIndex = SceneManager.sceneCountInBuildSettings; //Gets the index of the last scene from the build settings based on the total number of scenes in the build settings.
        
        if (nextSceneIndex == LastSceneIndex) //Will load the first scene if player is currently on the last scene 
        {
            Invoke("LoadFirstLevel", levelLoadDelay);
        }
        else //Loads the next scene
        {
            
            SceneManager.LoadScene(nextSceneIndex);
        }

    }
}
