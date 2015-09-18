using UnityEngine;
using System.Collections;

public class SceneManager : MonoBehaviour {

    public static SceneManager instance;

    bool switchingScenes = false;

    void Awake()
    {
        instance = this;
    }

    public void SwitchScenes(string room)
    {
        if (!switchingScenes)
        {
            switchingScenes = true;
            Fade.instance.fadeOut();
            StartCoroutine(WaitThenExit(room));
        }
        
    }

    IEnumerator WaitThenExit(string room)
    {
        yield return new WaitForSeconds(0.6f);
        Application.LoadLevel(room);
    }
}
