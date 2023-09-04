using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerRun : MonoBehaviour
{

    public float speed;
    public float maxSpeed;
    public float sensitivity, gravity;

    Vector3 oPos;

    Animator anim;
    CharacterController cc;
    AudioSource asource;

    public Slider slCooltime;
    public float cooltime;
    float currentCooltime;
    bool isSkillable;
    SkillController skCon;

    void Start()
    {
        anim = GetComponent<Animator>();

        cc = GetComponent<CharacterController>();

        oPos = transform.position;

        asource = GetComponent<AudioSource>();

        skCon = GetComponent<SkillController>();
    }

    void Update()
    {
        if (PlayerController.Instance.state != PlayerController.State.Run)
        {
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * gravity);
            return;
        }

        if (Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.N))
        {
            speed += sensitivity;
            asource.Stop();
            asource.Play();
        }
        else
            speed = Mathf.Lerp(speed, 0, Time.deltaTime * gravity);

        speed = Mathf.Clamp(speed, 0, maxSpeed);

        if (Input.GetKey(KeyCode.M))
            anim.SetTrigger("LeftLeg");
        else if (Input.GetKey(KeyCode.N))
            anim.SetTrigger("RightLeg");
        else
            anim.SetFloat("Run", speed / maxSpeed * gravity);

        cc.Move(transform.forward * speed * Time.deltaTime);

        Vector3 pos = transform.position;
        pos.y = oPos.y;
        pos.z = oPos.z;
        transform.position = pos;

        if (Input.GetKeyDown(KeyCode.K) & isSkillable & skCon.isFront)
            Throw();

        currentCooltime += Time.deltaTime;
        currentCooltime = Mathf.Clamp(currentCooltime, 0, cooltime);
        slCooltime.value = currentCooltime / cooltime;

        if (cooltime <= currentCooltime)
            isSkillable = true;
    }

    void Throw()
    {
        PlayerController.Instance.state = PlayerController.State.Throw;

        StartCoroutine(ThrowProcess());
    }

    IEnumerator ThrowProcess()
    {
        PlayerController.Instance.GomuSkill();

        anim.SetTrigger("RunToThrow");
        yield return new WaitForSeconds(0.24f);

        PlayerController.Instance.state = PlayerController.State.Run;
        anim.SetTrigger("ThrowToRun");

        currentCooltime = 0;
        isSkillable = false;
    }
}
