using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GomuGomu : MonoBehaviour
{
    public Transform firePosition;
    public Transform Gomuhand;

    void Update()
    {
        Vector3 pos = transform.position;
        pos.x =  firePosition.position.x + (Gomuhand.position.x - firePosition.position.x) / 2f;
        transform.position = pos;
        
        Vector3 scale = transform.localScale;
        scale.x = Gomuhand.position.x - firePosition.position.x;
        transform.localScale = scale;
    }
}
