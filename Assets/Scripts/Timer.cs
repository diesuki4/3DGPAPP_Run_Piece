using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    Text timer;
    float startTime;
    bool isStarted;

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance.gState != GameManager.GameState.Play)
            return;
        else if (!isStarted)
        {
            startTime = Time.time;
            isStarted = true;
        }

        float passedTime = Time.time - startTime;

        int ms = (int)(passedTime % 1.0f * 1000);
        int s = (int)passedTime % 60;
        int m = (int)passedTime / 60;

        timer.text = string.Format("{0:00}:{1:00}:{2:000}", m, s, ms);
    }
}
