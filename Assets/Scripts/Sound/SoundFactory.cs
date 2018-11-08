using UnityEngine;
using System.Collections.Generic;
public class SoundFactory : MonoBehaviour // create and play all sound effects using this class
{                                         // used in conjunction with SoundStruct.cs

    public static SoundStruct AddSound(string file_location, Transform transformation, Rigidbody2D rigidbody) // This function is used in other scripts to play the sound diagetically
    {                                                                                                           
        SoundStruct sound = CreateSound(file_location); // calls the below function
        FMODUnity.RuntimeManager.AttachInstanceToGameObject(sound.instance, transformation, rigidbody); //attaches the sound to a gameobject
        return sound; //outputs the sound
    }
    public static SoundStruct AddSound2D(string file_location) // This function is used in other scripts to play the sound nondiagetically
    {
        SoundStruct sound = CreateSound(file_location); // calls the below function
        return sound; //outputs the sound
    }
    private static SoundStruct CreateSound(string file_location) // creates a sound from FMOD Event and then plays it
                                                                 // addendum: this also works with snapshots, which wasn't intentional, but it works!
    {
        FMOD.Studio.EventInstance instance = FMODUnity.RuntimeManager.CreateInstance(file_location);  //"file_location" is the event path in FMOD, this is used to reference the path
        SoundStruct sound = new SoundStruct(instance, file_location); //the referenced path is then generated in this class
        instance.start(); // plays the sound whenever it is called
        return sound; //outputs the sound
    }
    public static void DeleteSound(ref List<SoundStruct> sounds, string file_location) // stops a specific sound based on string
    {
        foreach (SoundStruct sound in sounds)
        {
            if (sound.name.Equals(file_location))
            {
                sound.instance.stop(FMOD.Studio.STOP_MODE.ALLOWFADEOUT); //stop fmod sound
                sound.instance.release(); //removes container from memory
                sounds.Remove(sound); //deletes the sound from list
                break; //breaks out of the foreach loop after deleting the sound
            }
        }
    }
}