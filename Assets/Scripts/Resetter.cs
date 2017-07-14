using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Resetter : MonoBehaviour
{

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Resetting");
            SceneManager.LoadScene("ControlsPlayground");

            AppState.bestTime = 1000f;
            AppState.lastTime = 1000f;
        }
    }
}
