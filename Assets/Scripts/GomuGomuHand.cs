using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GomuGomuHand : MonoBehaviour
{
    public float speed;
    public GameObject gomugomu;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.position += Vector3.right * speed * Time.deltaTime;

        if (transform.position.x > 100)
        {
            gomugomu.SetActive(false);
            Destroy(gameObject);
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (LayerMask.LayerToName(other.gameObject.layer).Contains("Enemy"))
        {
            CPUController ccon = other.GetComponent<CPUController>();

            if (ccon.state == CPUController.State.Run)
            {
                ccon.FallDown();
                gomugomu.SetActive(false);
                Destroy(gameObject);
            }
        }
    }
}
