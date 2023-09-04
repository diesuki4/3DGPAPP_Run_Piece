using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WinnerInitialize : MonoBehaviour
{
    public GameObject player;
    public GameObject enemy, enemy2;
    public GameObject chopper, nami;

    public Transform firstPos, secondPos, thirdPos;

    public Text score;

    // Start is called before the first frame update
    void Start()
    {
        GameObject first, second, third;
        first = second = third = null;

        switch (PlayerPrefs.GetInt("Winner"))
        {
            case 0 : first = player; break;
            case 1 : first = enemy; break;
            case 2 : first = enemy2; break;
        }

        switch (PlayerPrefs.GetInt("Second"))
        {
            case 0 : second = player; break;
            case 1 : second = enemy; break;
            case 2 : second = enemy2; break;
        }

        switch (PlayerPrefs.GetInt("Third"))
        {
            case 0 : third = player; break;
            case 1 : third = enemy; break;
            case 2 : third = enemy2; break;
        }

        first.transform.position = firstPos.position;
        second.transform.position = secondPos.position;
        third.transform.position = thirdPos.position;

        first.GetComponent<Animator>().SetTrigger("first");
        second.GetComponent<Animator>().SetTrigger("second");
        third.GetComponent<Animator>().SetTrigger("third");

        chopper.GetComponent<Animator>().SetTrigger("hello");
        nami.GetComponent<Animator>().SetTrigger("hello");

        score.text = PlayerPrefs.GetString("Score");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
