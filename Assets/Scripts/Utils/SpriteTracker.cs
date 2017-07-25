using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class SpriteTracker : MonoBehaviour
{

    public int numSprites = 10;
    public float timePassed = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
    }

    public void handleSpriteCollected()
    {
        numSprites--;
        if (numSprites <= 0)
        {
            AppState.lastTime = timePassed;
            if (timePassed < AppState.bestTime)
            {
                AppState.bestTime = timePassed;
            }
            SceneManager.LoadScene("ControlsPlayground");
        }
    }
}
