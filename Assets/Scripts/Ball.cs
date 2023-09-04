using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public float speed;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x > 100)
            Destroy(gameObject);
    }

    void OnTriggerEnter(Collider other)
    {
        int layer = other.gameObject.layer;

        if (layer == LayerMask.NameToLayer("Player"))
        {
            if (PlayerController.Instance.state == PlayerController.State.Run)
            {
                PlayerController.Instance.FallDown();
                Destroy(gameObject);
            }
        }
        else if (layer == LayerMask.NameToLayer("Enemy2"))
        {
            CPUController ccon = other.GetComponent<CPUController>();

            if (ccon.state == CPUController.State.Run)
            {
                ccon.FallDown();
                Destroy(gameObject);
            }
        }
    }
}
