using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public static SceneManager instance;

    bool switchingScenes = false;

    //store a static instance for easy access of this class's public functions from other classes
    void Awake()
    {
        instance = this;
    }

    //tell the fader to fade the view out 
    //and start a coroutine that will change to the new scene in some amount of time
    public void SwitchScenes(string room)
    {
        if (!switchingScenes)
        {
            switchingScenes = true;
            Fade.instance.fadeOut();
            StartCoroutine(WaitThenExit(room));
        }
        
    }

    //load the specified after waiting some number of seconds
    IEnumerator WaitThenExit(string room)
    {
        yield return new WaitForSeconds(0.6f);
        Application.LoadLevel(room);
    }
}
