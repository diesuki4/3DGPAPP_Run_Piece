using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;

    void Awake()
    {
        if (Instance == null)
            Instance = this;
    }

    public enum GameState
    {
        Intro = 1,
        Idle = 2,
        Ready = 4,
        Play = 8,
        End = 16
    }
    public GameState gState;

    public float idleDuration;

    public GameObject Camera1, Camera2, Camera3;
    public GameObject Parallax1, Parallax2;

    public Transform[] characters;
    public Transform[] originPoss;

    public Text Timer;
    public Image EndScreen;
    public GameObject ocean;

    GameObject winObject;

    AudioSource introBgm, mainBgm, readyBgm;

    public GameObject zoro, usopp;

    public GameObject manualPage;

    void Start()
    {
        gState = GameState.Intro;

        introBgm = GetComponent<AudioSource>();

        mainBgm = GetComponents<AudioSource>()[1];
        
        readyBgm = GetComponents<AudioSource>()[2];
    }

    void Update()
    {
        if (gState == GameState.End)
        {
            Color c = EndScreen.color;
            c.a += Time.deltaTime;
            EndScreen.color = c;

            Camera3.transform.forward = (winObject.transform.position - Camera3.transform.position + Vector3.up).normalized;
            Camera3.GetComponent<Camera>().fieldOfView -= Time.deltaTime * 50;
        }
    }

    public void Initialize()
    {
        Parallax1.SetActive(true);
        Parallax2.SetActive(true);
        Camera1.SetActive(true);
        Camera2.SetActive(true);

        for (int i=0; i<characters.Length; i++)
        {
            characters[i].transform.position = originPoss[i].transform.position;
            characters[i].transform.rotation = originPoss[i].transform.rotation;
        }

        StartCoroutine(AudioFadeOut(introBgm, 1.5f));
        StartGm(idleDuration);
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
    }

    void StartGm(float delay)
    {
        manualPage.SetActive(true);
        StartCoroutine(StartGame(delay));
    }

    public bool manualOK;
    IEnumerator StartGame(float delay)
    {
        while (!manualOK)
            yield return null;

        yield return new WaitForSeconds(delay);

        gState = GameState.Ready;

        readyBgm.Play();

        zoro.GetComponent<CPUController>().Ready();
        usopp.GetComponent<CPUController>().Ready();
        ChopperController.Instance.Ready();
        NamiController.Instance.Ready();
        PlayerController.Instance.Ready();
    }

    public void Play()
    {
        mainBgm.Play();

        GameManager.Instance.gState = GameManager.GameState.Play;
    }

    public void End(int winner, GameObject winObject)
    {
        GameManager.Instance.gState = GameManager.GameState.End;
        EndScreen.gameObject.SetActive(true);

        StartCoroutine(AudioFadeOut(mainBgm, 5f));

        int second, third;
        second = third = 0;

        if (winner == 0)
        {
            if (usopp.transform.position.x < zoro.transform.position.x)
            {
                second = 2;
                third = 1;
            }
            else
            {
                second = 1;
                third = 2;
            }
        }
        else if (winner == 1)
        {
            if (PlayerController.Instance.transform.position.x < zoro.transform.position.x)
            {
                second = 2;
                third = 0;
            }
            else
            {
                second = 0;
                third = 2;
            }
        }
        else if (winner == 2)
        {
            if (PlayerController.Instance.transform.position.x < usopp.transform.position.x)
            {
                second = 1;
                third = 0;
            }
            else
            {
                second = 0;
                third = 1;
            }
        }

        PlayerPrefs.SetInt("Winner", winner);
        PlayerPrefs.SetInt("Second", second);
        PlayerPrefs.SetInt("Third", third);
        PlayerPrefs.SetString("Score", Timer.text);

        this.winObject = winObject;

        Parallax1.SetActive(false);
        Parallax2.SetActive(false);
        Camera1.SetActive(false);
        Camera2.SetActive(false);
        Camera3.SetActive(true);

        ocean.GetComponent<Ocean>().speed = 2f;

        Time.timeScale = 0.1f;

        zoro.GetComponent<CPUController>().End();
        usopp.GetComponent<CPUController>().End();
        PlayerController.Instance.End();

        StartCoroutine(EndGame(1.0f));
    }

    IEnumerator EndGame(float delay)
    {
        yield return new WaitForSeconds(delay);

        Time.timeScale = 1.0f;

        SceneManager.LoadScene(1);
    }
}
