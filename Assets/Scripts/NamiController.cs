using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NamiController : MonoBehaviour
{
    public static NamiController Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public void Ready()
    {
        GetComponent<Animator>().SetTrigger("Ready");
    }
}
