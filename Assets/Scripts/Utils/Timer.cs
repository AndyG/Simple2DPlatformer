using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public Text text;
    public Text bestTimeText;
    public Text lastTimeText;

    public float timePassed = 0f;

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        timePassed += Time.deltaTime;
        text.text = "Time: " + timePassed.ToString("0.00");
        string t1 = "Best: " + AppState.bestTime.ToString("0.00").ToString();
        bestTimeText.text = t1;
        t1 = "Last: " + AppState.lastTime.ToString("0.00").ToString();
        lastTimeText.text = t1;
    }
}
