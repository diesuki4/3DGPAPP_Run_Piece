using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChopperController : MonoBehaviour
{
    public static ChopperController Instance;

    public GameObject zoro, usopp;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public GameObject fireEff;
    public Transform firePosition;

    public void GameStart()
    {
        GameObject go = Instantiate(fireEff);

        go.transform.position = firePosition.position;

        go.transform.forward = firePosition.forward;

        go.GetComponent<ParticleSystem>().Play();

        GameManager.Instance.Play();
        zoro.GetComponent<CPUController>().Play();
        usopp.GetComponent<CPUController>().Play();
        PlayerController.Instance.Play();
    }

    public void Ready()
    {
        GetComponent<Animator>().SetTrigger("Ready");
    }
}
