using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Follow : MonoBehaviour
{
    public Transform target;

    void Start()
    {
        
    }

    void Update()
    {
        Vector3 pos = transform.position;

        pos.x = target.position.x;

        transform.position = pos;
    }
}
