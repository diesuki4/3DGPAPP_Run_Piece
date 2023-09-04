using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EpilogueEvent : MonoBehaviour
{
    public GameObject luffy, usopp, zoro;
    public GameObject score, end;

    AudioSource endBgm, laughBgm, shoutBgm;

    void Start()
    {
        endBgm = GetComponent<AudioSource>();

        laughBgm = GetComponents<AudioSource>()[1];

        shoutBgm = GetComponents<AudioSource>()[2];

        score.SetActive(true);
        end.SetActive(false);
    }

    public void AnimEvent()
    {
        shoutBgm.Play();

        luffy.GetComponent<Animator>().SetTrigger("jump");
        usopp.GetComponent<Animator>().SetTrigger("jump");
        zoro.GetComponent<Animator>().SetTrigger("jump");
    }

    public void AnimEvent2()
    {
        score.SetActive(false);

        laughBgm.Play();
    }

    public void AnimEvent3()
    {
        end.SetActive(true);

        endBgm.Play();
    }

    float ttime;
    IEnumerator AudioFadeOut(AudioSource a, float delay)
    {
        while ((ttime += Time.deltaTime) < delay)
        {
            a.volume = Mathf.Lerp(a.volume, 0f, Time.deltaTime);
            yield return null;
        }

        ttime = 0;
        a.enabled = false;
        yield break;
    }
}
