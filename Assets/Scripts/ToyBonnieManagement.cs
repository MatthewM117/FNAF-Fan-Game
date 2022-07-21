using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToyBonnieManagement : MonoBehaviour
{
    private GameObject toyBonnie;
    //private float randomNum;
    public bool toyBonnieIsHere;
    private bool currentlySpawningToyBonnie;
    private GameManagement gm;
    private GameObject toyBonnieJumpscareHead;
    //private float toyBonnieRangeMin;
    //private float toyBonnieRangeMax;
    private bool toyBonnieEnabled;
    private bool spawnToyBonnie;

    public GameObject toyBonnieJump;

    private FreddyMaskManagement fmm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        toyBonnie = GameObject.Find("toy bonnie");
        HideToyBonnie();
        toyBonnieIsHere = false;
        currentlySpawningToyBonnie = false;
        gm = GameObject.Find("GameManager").GetComponent<GameManagement>();
        toyBonnieJumpscareHead = GameObject.Find("toy bonnie jumpscare head");
        toyBonnieJumpscareHead.transform.localScale = new Vector3(0, 0, 0);
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        //toyBonnieRangeMin = 25.0f;
        //toyBonnieRangeMax = 40.0f;
        if (PlayerPrefs.GetInt("Toy Bonnie Level") == 0)
        {
            toyBonnieEnabled = false;
        }
        else
        {
            toyBonnieEnabled = true;
        }
        //DetermineAI();
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlySpawningToyBonnie && !toyBonnieIsHere)
        {
            if (Time.timeSinceLevelLoad % 12.6f >= 0.001f && Time.timeSinceLevelLoad % 12.6f <= 0.05f && Time.timeSinceLevelLoad >= 12.6f && toyBonnieEnabled)
            {
                //Debug.Log("sfs " + Time.timeSinceLevelLoad);
                currentlySpawningToyBonnie = true;
                ToyBonnieSpawning();
                //StartCoroutine(SpawnToyBonnie());
            }
        }
    }

    private void ToyBonnieSpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Toy Bonnie Level") && spawnToyBonnie)
        {
            ShowToyBonnie();
            currentlySpawningToyBonnie = false;
            spawnToyBonnie = false;
        }
        currentlySpawningToyBonnie = false;
    }

    /*
    private void DetermineAI()
    {
        if (PlayerPrefs.GetInt("Toy Bonnie Level") == 0)
        {
            toyBonnieEnabled = false;
            return;
        }
        for (int i = 0; i < PlayerPrefs.GetInt("Toy Bonnie Level"); i++)
        {
            if (toyBonnieRangeMin > 0)
            {
                toyBonnieRangeMin -= 1.0f;
            }

            if (toyBonnieRangeMax > 0)
            {
                toyBonnieRangeMax -= 1.0f;
            }
        }
    }*/
    /*
    IEnumerator SpawnToyBonnie()
    {
        randomNum = Random.Range(toyBonnieRangeMin, toyBonnieRangeMax);

        yield return new WaitForSeconds(randomNum);

        ShowToyBonnie();
        currentlySpawningToyBonnie = false;
    }*/

    public void HideToyBonnie()
    {
        toyBonnie.transform.localScale = new Vector3(0, 0, 0);
        spawnToyBonnie = true;
    }

    private void ShowToyBonnie()
    {
        toyBonnie.transform.localScale = new Vector3(1, 1, 1);
        toyBonnieIsHere = true;
        StartCoroutine(CheckIfDoorIsClosed());
    }

    IEnumerator CheckIfDoorIsClosed()
    {
        for (int i = 0; i < 50; i++)
        {
            if (gm.rightVentIsClosed)
            {
                for (int j = 0; j < 5; j++)
                {
                    if (!gm.rightVentIsClosed)
                    {
                        gm.rightVentIsClosed = true;
                        StartCoroutine(ToyBonnieJumpscare());
                    }
                    yield return new WaitForSeconds(0.1f);
                }
                FindObjectOfType<AudioManagement>().Play("thud");
                break;
            }
            yield return new WaitForSeconds(0.1f);
            if (i == 49 && !gm.rightVentIsClosed)
            {
                StartCoroutine(ToyBonnieJumpscare());
            }
        }
    }

    IEnumerator ToyBonnieJumpscare()
    {
        if (cm.camIsOpen)
        {
            cm.forceCamDown = true;
        }
        if (fmm.maskIsOn)
        {
            StartCoroutine(fmm.TakeMaskOff());
        }
        toyBonnieJumpscareHead.transform.localScale = new Vector3(3307.491f, 3307.491f, 111.424f);
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        toyBonnieJump.GetComponent<Animator>().Play("Toy_Bonnie--Jumpscare");
        /*
        while (true)
        {
            toyBonnieJumpscareHead.transform.localPosition += new Vector3(0, 200, 0);
            yield return new WaitForSeconds(0.0001f);

            if (toyBonnieJumpscareHead.transform.localPosition.y >= -1400)
            {
                break;
            }
        }*/
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
