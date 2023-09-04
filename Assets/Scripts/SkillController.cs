using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SkillController : MonoBehaviour
{
    public Transform usopp, zoro;
    public bool isFront;
    public GameObject imageBan;

    // Update is called once per frame
    void Update()
    {
        if (usopp.position.x < transform.position.x && zoro.position.x < transform.position.x)
        {
            isFront = false;
            imageBan.SetActive(true);
        }
        else if (usopp.GetComponent<CPUController>().state == CPUController.State.Run
                || zoro.GetComponent<CPUController>().state == CPUController.State.Run)
        {
            isFront = true;
            imageBan.SetActive(false);
        }
    }
}
