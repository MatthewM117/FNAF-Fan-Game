using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// freddy AI, every five seconds, generate a random number (done in update function) to see when he should spawn
// at ur left door
public class FreddyManagement : MonoBehaviour
{

    private GameObject freddyEyes;
    //private float randomNum;
    public bool freddyIsHere;
    private bool currentlySpawningFreddy;
    private GameManagement gm;
    private GameObject freddyJumpscareHead;
    //private float freddyRangeMin;
    //private float freddyRangeMax;
    private bool freddyEnabled;
    private bool spawnFreddy;

    public GameObject freddyIdle;
    public GameObject freddyJump;

    private FreddyMaskManagement fmm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        freddyEyes = GameObject.Find("freddy eyes");
        HideFreddyEyes();
        freddyIsHere = false;
        currentlySpawningFreddy = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManagement>();
        freddyJumpscareHead = GameObject.Find("freddy jumpscare head");
        freddyJumpscareHead.transform.localScale = new Vector3(0, 0, 0);
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        //freddyRangeMin = 25.0f;
        //freddyRangeMax = 40.0f;
        if (PlayerPrefs.GetInt("Freddy Level") == 0)
        {
            freddyEnabled = false;
        }
        else
        {
            freddyEnabled = true;
        }
        //DetermineAI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlySpawningFreddy && !freddyIsHere)
        {
            if (Time.timeSinceLevelLoad % 9.2f >= 0.001f && Time.timeSinceLevelLoad % 9.2f <= 0.05f && Time.timeSinceLevelLoad >= 9.2f && freddyEnabled)
            {
                //Debug.Log("sfs " + Time.timeSinceLevelLoad);
                currentlySpawningFreddy = true;
                FreddySpawning();
                //StartCoroutine(SpawnFreddy());
            }
        }
    }

    private void FreddySpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Freddy Level") && spawnFreddy)
        {
            ShowFreddyEyes();
            currentlySpawningFreddy = false;
            spawnFreddy = false;
        }
        currentlySpawningFreddy = false;
    }

    /*
    private void DetermineAI()
    {
        if (PlayerPrefs.GetInt("Freddy Level") == 0)
        {
            freddyEnabled = false;
            return;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("Freddy Level"); i++)
        {
            if (freddyRangeMin > 0)
            {
                freddyRangeMin -= 1.0f;
            }

            if (freddyRangeMax > 0)
            {
                freddyRangeMax -= 1.0f;
            }
        }
    }*/
    /*
    IEnumerator SpawnFreddy()
    {
        randomNum = Random.Range(freddyRangeMin, freddyRangeMax);

        yield return new WaitForSeconds(randomNum);

        ShowFreddyEyes();
        currentlySpawningFreddy = false;
    }*/

    public void HideFreddyEyes()
    {
        freddyEyes.transform.localScale = new Vector3(0, 0, 0);
        spawnFreddy = true;
    }

    private void ShowFreddyEyes()
    {
        freddyEyes.transform.localScale = new Vector3(1, 1, 1);
        freddyIsHere = true;
        freddyIdle.GetComponent<Animator>().Play("Freddy--Idle");
        StartCoroutine(CheckIfDoorIsClosed());
    }

    IEnumerator CheckIfDoorIsClosed()
    {
        for (int i = 0; i < 50; i++)
        {
            if (gm.doorIsClosed)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!gm.doorIsClosed)
                    {
                        gm.doorIsClosed = true;
                        StartCoroutine(FreddyJumpscare());
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                FindObjectOfType<AudioManagement>().Play("thud");
                break;
            }
            yield return new WaitForSeconds(0.1f);
            if (i == 49 && !gm.doorIsClosed)
            {
                StartCoroutine(FreddyJumpscare());
            }
        }
    }

    private IEnumerator FreddyJumpscare()
    {
        if (cm.camIsOpen)
        {
            cm.forceCamDown = true;
        }
        if (fmm.maskIsOn)
        {
            StartCoroutine(fmm.TakeMaskOff());
        }
        freddyJumpscareHead.transform.localScale = new Vector3(26358.27f, 26358.27f, 887.9675f);
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        freddyJump.GetComponent<Animator>().Play("Freddy--Jumpscare");
        /*
        while (true)
        {
            freddyJumpscareHead.transform.localPosition += new Vector3(0, 200f, 0);
            yield return new WaitForSeconds(0.0001f);

            if (freddyJumpscareHead.transform.localPosition.y >= -1951)
            {
                break;
            }
        }*/
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
