using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CPUController : MonoBehaviour
{
    public enum State
    {
        Idle = 1,
        Ready = 2,
        Run = 4,
        End = 8,
        FallDown = 16,
        Throw = 32
    }
    public State state;

    public Transform firePosition;

    public GameObject ball;

    public Transform otherEnemy;

    Animator anim;

    float currentTime, coolTime;

    public Camera playerCam;

    void Start()
    {
        anim = GetComponent<Animator>();

        state = State.Idle;

        coolTime = UnityEngine.Random.Range(3, 5);
    }

    void Update()
    {
        if (state != State.Run)
            return;
        else if (state != State.Throw)
            currentTime += Time.deltaTime;

        if (coolTime < currentTime && (transform.position.x < PlayerController.Instance.transform.position.x
            || transform.position.x < otherEnemy.position.x))
        {
            Throw();
        }
    }

    public void Ready()
    {
        state = State.Ready;
        
        anim.SetTrigger("Ready");
    }

    public void Play()
    {
        state = State.Run;

        anim.SetTrigger("Play");
    }

    public void End()
    {
        state = State.End;

        GetComponent<Animator>().SetTrigger("RunToIdle");
    }
    
    public void FallDown()
    {
        if (state != State.Run)
            return;

        state = State.FallDown;

        StartCoroutine(FallDownProcess());
    }

    IEnumerator FallDownProcess()
    {
        anim.SetTrigger("FallDown");

        playerCam.cullingMask += 1 << gameObject.layer;

        yield return new WaitForSeconds(1.267f);

        playerCam.cullingMask -= 1 << gameObject.layer;

        state = State.Run;
    }

    void Throw()
    {
        state = State.Throw;

        StartCoroutine(ThrowProcess());
    }

    IEnumerator ThrowProcess()
    {
        GameObject go = Instantiate(ball);

        go.transform.position = firePosition.position;
        go.transform.forward = firePosition.forward;

        anim.SetTrigger("RunToThrow");
        yield return new WaitForSeconds(0.24f);

        state = State.Run;
        anim.SetTrigger("ThrowToRun");

        currentTime = 0;
        coolTime = UnityEngine.Random.Range(3, 5);
    }
}
