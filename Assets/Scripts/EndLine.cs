using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndLine : MonoBehaviour
{
    void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Player"))
        {
            GameManager.Instance.End(0, other.gameObject);
            Destroy(gameObject);
        }
        else if (layer == LayerMask.NameToLayer("Enemy"))
        {
            GameManager.Instance.End(1, other.gameObject);
            Destroy(gameObject);
        }
        else if (layer == LayerMask.NameToLayer("Enemy2"))
        {
            GameManager.Instance.End(2, other.gameObject);
            Destroy(gameObject);
        }
    }
}
