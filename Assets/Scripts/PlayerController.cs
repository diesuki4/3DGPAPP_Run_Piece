using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;

    Animator anim;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public enum State
    {
        Idle = 1,
        Run = 2,
        End = 4,
        FallDown = 8,
        Throw = 16
    }
    public State state;

    public Camera enemyCam;

    void Start()
    {
        anim = GetComponent<Animator>();

        state = State.Idle;
    }

    public void Ready()
    {
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

        anim.SetTrigger("RunToIdle");
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
        enemyCam.cullingMask += 1 << gameObject.layer;

        yield return new WaitForSeconds(1.267f);

        enemyCam.cullingMask -= 1 << gameObject.layer;
        state = State.Run;
    }
    
    public GameObject gomuhand;
    public GameObject gomugomu;
    public Transform firePosition;
    public void GomuSkill()
    {
        GameObject go = Instantiate(gomuhand);
        
        go.GetComponent<GomuGomuHand>().gomugomu = gomugomu;
        gomugomu.GetComponent<GomuGomu>().Gomuhand = go.transform;

        go.transform.position = firePosition.position;

        gomugomu.SetActive(true);
    }

    public void IdleToPoint()
    {
        anim.SetTrigger("IdleToPoint");
    }
}
