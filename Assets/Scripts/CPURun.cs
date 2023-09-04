using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPURun : MonoBehaviour
{
    public float speed;
    public float speedScale;
    public float sensitivity;

    Vector3 oPos;

    Animator anim;
    CharacterController cc;
    CPUController ccon;

    void Start()
    {
        anim = GetComponent<Animator>();

        cc = GetComponent<CharacterController>();

        ccon = GetComponent<CPUController>();

        oPos = transform.position;
    }

    void Update()
    {
        if (GameManager.Instance.gState != GameManager.GameState.Play)
            return;

        if (ccon.state == CPUController.State.Idle || ccon.state == CPUController.State.Throw)
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * sensitivity);
        else if (ccon.state == CPUController.State.Run)
            speed = Mathf.Lerp(speed, 1, Time.deltaTime * sensitivity * 2);
        else if (ccon.state == CPUController.State.FallDown)
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * sensitivity * 3);

        anim.SetFloat("Run", speed);

        cc.Move(transform.forward * speed * speedScale * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.y = oPos.y;
        pos.z = oPos.z;
        transform.position = pos;
    }
}
