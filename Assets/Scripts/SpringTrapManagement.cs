using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// springtrap ai, every 10 seconds, generate a random number to see when he should spawn at middle vent
public class SpringTrapManagement : MonoBehaviour
{
    private GameObject springtrapEyes;
    //private float randomNum;
    public bool springtrapIsHere;
    private bool currentlySpawningSpringtrap;
    private GameManagement gm;
    private GameObject springtrapJumpscareHead;
    //private float springtrapRangeMin;
    //private float springtrapRangeMax;
    private bool springtrapEnabled;
    private bool spawnSpringtrap;

    public GameObject springtrapIdle;
    public GameObject springtrapJump;

    private FreddyMaskManagement fmm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        springtrapEyes = GameObject.Find("springtrap eyes");
        HideSpringtrapEyes();
        springtrapIsHere = false;
        currentlySpawningSpringtrap = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManagement>();
        springtrapJumpscareHead = GameObject.Find("springtrap jumpscare head");
        springtrapJumpscareHead.transform.localScale = new Vector3(0, 0, 0);
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        //springtrapRangeMin = 22.0f;
        //springtrapRangeMax = 29.0f;
        if (PlayerPrefs.GetInt("Springtrap Level") == 0)
        {
            springtrapEnabled = false;
        }
        else
        {
            springtrapEnabled = true;
        }
        //DetermineAI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlySpawningSpringtrap && !springtrapIsHere)
        {
            if (Time.timeSinceLevelLoad % 4.3f >= 0.001f && Time.timeSinceLevelLoad % 4.3f <= 0.05f && Time.timeSinceLevelLoad >= 4.3f && springtrapEnabled)
            {
                //Debug.Log("sfs " + Time.timeSinceLevelLoad);
                currentlySpawningSpringtrap = true;
                SpringtrapSpawning();
                //StartCoroutine(SpawnSpringtrap());
            }
        }
    }

    private void SpringtrapSpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Springtrap Level") && spawnSpringtrap)
        {
            ShowSpringtrapEyes();
            currentlySpawningSpringtrap = false;
            spawnSpringtrap = false;
        }
        currentlySpawningSpringtrap = false;
    }

    /*
    private void DetermineAI()
    {
        if (PlayerPrefs.GetInt("Springtrap Level") == 0)
        {
            springtrapEnabled = false;
            return;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("Springtrap Level"); i++)
        {
            if (springtrapRangeMin > 0)
            {
                springtrapRangeMin -= 1.0f;
            }

            if (springtrapRangeMax > 0)
            {
                springtrapRangeMax -= 1.0f;
            }
        }
    }*/
    /*
    IEnumerator SpawnSpringtrap()
    {
        randomNum = Random.Range(springtrapRangeMin, springtrapRangeMax);

        yield return new WaitForSeconds(randomNum);

        ShowSpringtrapEyes();
        currentlySpawningSpringtrap = false;
    }*/

    public void HideSpringtrapEyes()
    {
        springtrapEyes.transform.localScale = new Vector3(0, 0, 0);
        spawnSpringtrap = true;
    }

    private void ShowSpringtrapEyes()
    {
        springtrapEyes.transform.localScale = new Vector3(1, 1, 1);
        springtrapIsHere = true;
        springtrapIdle.GetComponent<Animator>().Play("SpringTrap--Decloaked");
        StartCoroutine(CheckIfDoorIsClosed());
    }

    IEnumerator CheckIfDoorIsClosed()
    {
        for (int i = 0; i < 50; i++)
        {
            if (gm.middleVentIsClosed)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!gm.middleVentIsClosed)
                    {
                        gm.middleVentIsClosed = true;
                        StartCoroutine(SpringtrapJumpscare());
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                FindObjectOfType<AudioManagement>().Play("thud");
                break;
            }
            yield return new WaitForSeconds(0.1f);
            if (i == 49 && !gm.middleVentIsClosed)
            {
                StartCoroutine(SpringtrapJumpscare());
            }
        }
    }

    IEnumerator SpringtrapJumpscare()
    {
        if (fmm.maskIsOn)
        {
            StartCoroutine(fmm.TakeMaskOff());
        }
        if (cm.camIsOpen)
        {
            cm.forceCamDown = true;
        }
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        springtrapJumpscareHead.transform.localScale = new Vector3(2045.199f, 2045.199f, 68.89946f);
        springtrapJump.GetComponent<Animator>().Play("SpringTrap--Jumpscare");
        /*
        while (true)
        {
            springtrapJumpscareHead.transform.localPosition += new Vector3(0, 200, 0);
            yield return new WaitForSeconds(0.0001f);

            if (springtrapJumpscareHead.transform.localPosition.y >= -2333)
            {
                break;
            }
        }*/
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
