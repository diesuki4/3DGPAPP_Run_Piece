using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public struct BGParallax
{
    public MeshRenderer rend;
    public float speed;
}

public class ParallaxScroller : MonoBehaviour
{
    public BGParallax[] arrBGParallax;
    public GameObject target;

    public float speedScale;

    void Start()
    {

    }

    void Update()
    {
        float speed = 1;
        float scale = speedScale;
        CPURun cpu = target.GetComponent<CPURun>();

        if (cpu)
            speed = cpu.speed;
        else
            speed = target.GetComponent<PlayerRun>().speed;

        for (int i=0; i<arrBGParallax.Length; i++)
        {
            BGParallax bgPrx = arrBGParallax[i];

            bgPrx.rend.material.mainTextureOffset += (bgPrx.speed * speed) * scale * Vector2.right * Time.deltaTime;
        }
    }
}
