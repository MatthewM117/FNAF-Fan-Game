using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ToyChicaManagement : MonoBehaviour
{
    private GameObject toyChica;
    private float randomNum;
    private bool toyChicaIsHere;
    private bool currentlySpawningToyChica;
    private bool moveRight;
    private FreddyMaskManagement fmm;
    private GameObject toyChicaJumpscareHead;
    private bool doJumpscare;
    private bool spawnToyChica;
    private bool toyChicaEnabled;

    public GameObject toyChicaJump;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        toyChica = GameObject.Find("toy chica");
        HideToyChica();
        toyChicaIsHere = false;
        currentlySpawningToyChica = false;
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        toyChicaJumpscareHead = GameObject.Find("toy chica jumpscare head");
        toyChicaJumpscareHead.transform.localScale = new Vector3(0, 0, 0);
        doJumpscare = true;
        if (PlayerPrefs.GetInt("Toy Chica Level") == 0)
        {
            toyChicaEnabled = false;
        }
        else
        {
            toyChicaEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!currentlySpawningToyChica && !toyChicaIsHere)
        {
            if (Time.timeSinceLevelLoad % 17.2f >= 0.001f && Time.timeSinceLevelLoad % 17.2f <= 0.05f && Time.timeSinceLevelLoad >= 17.2f && !currentlySpawningToyChica && toyChicaEnabled)
            {
                //Debug.Log("sfs " + Time.timeSinceLevelLoad);
                currentlySpawningToyChica = true;
                spawnToyChica = true;
                ToyChicaSpawning();
                //StartCoroutine(SpawnToyChica());
            }
        }

        if (toyChicaIsHere)
        {
            if (toyChica.transform.localPosition.x <= 0 && moveRight)
            {
                toyChica.transform.localPosition += new Vector3(10, 0, 0);
            }
            else
            {
                moveRight = false;
                toyChica.transform.localPosition -= new Vector3(10, 0, 0);

                if (toyChica.transform.localPosition.x <= -920)
                {
                    toyChicaIsHere = false;
                }
            }

            if (toyChica.transform.localPosition.x >= -174f && !fmm.maskIsOn)
            {
                if (doJumpscare)
                {
                    doJumpscare = false;
                    StartCoroutine(ToyChicaJumpscare());
                }
                
            }
        }
    }

    private void ToyChicaSpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Toy Chica Level") && spawnToyChica)
        {
            ShowToyChica();
            currentlySpawningToyChica = false;
            spawnToyChica = false;
        }
        currentlySpawningToyChica = false;
    }
    /*
    IEnumerator SpawnToyChica()
    {
        randomNum = Random.Range(10.0f, 30.0f);

        yield return new WaitForSeconds(randomNum);

        ShowToyChica();
        currentlySpawningToyChica = false;
    }*/

    public void HideToyChica()
    {
        toyChica.transform.localScale = new Vector3(0, 0, 0);
        toyChicaIsHere = false;
        spawnToyChica = true;
    }

    private void ShowToyChica()
    {
        moveRight = true;
        toyChica.transform.localScale = new Vector3(651.5254f, 721.687f, 452.0715f);
        toyChicaIsHere = true;
    }

    IEnumerator ToyChicaJumpscare()
    {
        if (cm.camIsOpen)
        {
            cm.forceCamDown = true;
        }
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        toyChicaJumpscareHead.transform.localScale = new Vector3(1234.411f, 1132.903f, 313.1035f);
        toyChicaJump.GetComponent<Animator>().Play("Toy_Chica--Jumpscare");
        /*
        while (true)
        {
            toyChicaJumpscareHead.transform.localPosition += new Vector3(0, 200, 0);
            yield return new WaitForSeconds(0.0001f);

            if (toyChicaJumpscareHead.transform.localPosition.y >= -100)
            {
                break;
            }
        }*/
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
