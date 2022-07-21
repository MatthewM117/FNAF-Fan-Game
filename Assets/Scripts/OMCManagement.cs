using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ai for old man consequences, every 7 seconds generate random number to see when he'll spawn.
public class OMCManagement : MonoBehaviour
{

    private GameObject omcFish;
    private GameObject omc;
    private Vector3 changePos;
    private bool moveRight;
    private bool omcIsActive;
    private float randomNum;
    public bool OMCLockMonitor;
    private bool omcEnabled;
    private bool spawnOMC;

    // Start is called before the first frame update
    void Start()
    {
        omcFish = GameObject.Find("omc fish");
        omc = GameObject.Find("omc");
        HideOMC();
        changePos = new Vector3(0.03f, 0f, 0f);
        moveRight = true;
        omcIsActive = false;
        OMCLockMonitor = false;
        if (PlayerPrefs.GetInt("OMC Level") == 0)
        {
            omcEnabled = false;
        }
        else
        {
            omcEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad % 8.5f >= 0.001f && Time.timeSinceLevelLoad % 8.5f <= 0.05f && Time.timeSinceLevelLoad >= 8.5f && !omcIsActive && omcEnabled && spawnOMC)
        {
            //StartCoroutine(SpawnOMC());
            OMCSpawning();
        }

        if (omcIsActive)
        {
            // if player catches fish
            if (omcFish.transform.localPosition.x >= -0.3f && omcFish.transform.localPosition.x <= 0.1f && Input.GetKeyDown(KeyCode.C))
            {
                HideOMC();
                FindObjectOfType<AudioManagement>().Play("omc grab");
                omcIsActive = false;
            }

            if (omcFish.transform.localPosition.x <= -0.532f)
            {
                moveRight = true;
            }
            else if (omcFish.transform.localPosition.x >= 0.37f)
            {
                moveRight = false;
            }

            if (moveRight)
            {
                omcFish.transform.localPosition += changePos;
            }
            else
            {
                omcFish.transform.localPosition -= changePos;
            }
        }
        
    }

    private void OMCSpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("OMC Level") && spawnOMC)
        {
            ShowOMC();
            omcIsActive = true;
            spawnOMC = false;
        }
    }
    /*
    IEnumerator SpawnOMC()
    {
        randomNum = Random.Range(7.0f, 28.0f);

        yield return new WaitForSeconds(randomNum);
        
        ShowOMC();
    }*/

    void HideOMCFish()
    {
        omcFish.transform.localScale = new Vector3(0, 0, 0);
    }

    void ShowOMCFish()
    {
        //omcFish.transform.localScale = new Vector3(0.4878438f, 0.2129764f, 0.1f);
        omcFish.transform.localScale = new Vector3(0.1158362f, 0.0564314f, 0.1f);
    }

    void HideOMC()
    {
        omc.transform.localScale = new Vector3(0, 0, 0);
        spawnOMC = true;
        FindObjectOfType<AudioManagement>().Stop("omc sound");
        HideOMCFish();
    }

    void ShowOMC()
    {
        omc.transform.localScale = new Vector3(282.4353f, 261.1361f, 1f);
        ShowOMCFish();
        FindObjectOfType<AudioManagement>().Play("omc sound");
        StartCoroutine(CheckIfFishCaughtInTime());
    }

    IEnumerator CheckIfFishCaughtInTime()
    {
        omcIsActive = true;
        for (int i = 0; i < 30; i++)
        {
            if (!omcIsActive)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
            if (i == 29)
            {
                StartCoroutine(LockMonitor());
                HideOMC();
            }
        }
    }

    IEnumerator LockMonitor()
    {
        OMCLockMonitor = true;
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(1);
        }
        OMCLockMonitor = false;
    }
}
