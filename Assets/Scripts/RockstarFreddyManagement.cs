using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RockstarFreddyManagement : MonoBehaviour
{
    private GameObject payCoinsText;

    private bool rfIsActive;
    private bool rfIsActivating;
    //private float randomNum;
    private bool allowHeater;
    private bool heaterIsActive;
    private bool playVoice;
    private bool spawnRockstarFreddy;
    private bool rockstarFreddyEnabled;

    private PowerManagement pm;

    private CameraManagement cm;

    public GameObject rockstarFreddyShutOff;
    public GameObject rockstarFreddyJump;

    private GameObject rfjumpscarehead;

    // Start is called before the first frame update
    void Start()
    {
        payCoinsText = GameObject.Find("pay 5 coins");
        DisableHead();
        rfIsActive = false;
        rfIsActivating = false;
        allowHeater = false;
        pm = GameObject.Find("PowerManager").GetComponent<PowerManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();

        rfjumpscarehead = GameObject.Find("rockstar freddy jumpscare head");
        rfjumpscarehead.transform.localScale = new Vector3(0, 0, 0);

        rockstarFreddyShutOff.GetComponent<Animator>().Play("Toy_Freddy--CPU_Lurch");

        if (PlayerPrefs.GetInt("Rockstar Freddy Level") == 0)
        {
            rockstarFreddyEnabled = false;
        }
        else
        {
            rockstarFreddyEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (!rfIsActivating && !rfIsActive && rockstarFreddyEnabled)
        {
            if (Time.timeSinceLevelLoad % 15.0f >= 0.001f && Time.timeSinceLevelLoad % 15.0f <= 0.05f && Time.timeSinceLevelLoad >= 15.0f)
            {
                rfIsActivating = true;
                RockstarFreddySpawning();
                //StartCoroutine(ActivateRF());
            }
        }

        if (Input.GetKeyDown(KeyCode.Alpha3) && rfIsActive && allowHeater)
        {
            allowHeater = false;
            heaterIsActive = true;
            pm.powerLeft -= 2;
            //rockstarFreddyShutOff.GetComponent<Animator>().Play("Toy_Freddy--Haywire");
            StartCoroutine(TamperWithHead());
        }
    }

    private void RockstarFreddySpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Rockstar Freddy Level") && spawnRockstarFreddy)
        {
            EnableHead();
            rfIsActivating = false;
            spawnRockstarFreddy = false;
        }
        rfIsActivating = false;
    }

    private void DisableHead()
    {
        rfIsActive = false;
        payCoinsText.transform.localScale = new Vector3(0, 0, 0);
        spawnRockstarFreddy = true;
    }

    private void EnableHead()
    {
        rfIsActive = true;
        allowHeater = true;
        rockstarFreddyShutOff.GetComponent<Animator>().Play("Toy_Freddy--Idle");
        payCoinsText.transform.localScale = new Vector3(1, 1, 0.089201f);
        StartCoroutine(CheckIfDealtWith());
        playVoice = true;
        StartCoroutine(PlayVoice());
    }
    /*
    IEnumerator ActivateRF()
    {
        randomNum = Random.Range(10.0f, 25.0f);

        yield return new WaitForSeconds(randomNum);

        EnableHead();
        rfIsActivating = false;
    }*/

    IEnumerator TamperWithHead()
    {
        playVoice = false;
        FindObjectOfType<AudioManagement>().Play("tamper");

        yield return new WaitForSeconds(2.5f);
        DisableHead();
        heaterIsActive = false;
        rockstarFreddyShutOff.GetComponent<Animator>().Play("Toy_Freddy--CPU_Lurch");
        FindObjectOfType<AudioManagement>().Stop("tamper");
        FindObjectOfType<AudioManagement>().Play("thank you");
        
    }

    IEnumerator CheckIfDealtWith()
    {
        for (int i = 0; i < 70; i++)
        {
            if (heaterIsActive)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
            if (i == 49 && !heaterIsActive)
            {
                i = 50;
                StartCoroutine(RockstarFreddyJumpscare());
            }
        }
    }

    private IEnumerator RockstarFreddyJumpscare()
    {
        rfjumpscarehead.transform.localScale = new Vector3(1, 1, 1);
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        rockstarFreddyJump.GetComponent<Animator>().Play("Toy_Freddy--Jumpscare");
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }

    IEnumerator PlayVoice()
    {
        while (true)
        {
            if (!playVoice)
            {
                break;
            }
            FindObjectOfType<AudioManagement>().Play("please deposit 5 coins");
            yield return new WaitForSeconds(3);
            if (!playVoice)
            {
                break;
            }
        }
    }

    public void PayClicked()
    {
        if (cm.coinCount >= 5)
        {
            FindObjectOfType<AudioManagement>().Play("kaching");
            heaterIsActive = true;
            DisableHead();
            playVoice = false;
            rockstarFreddyShutOff.GetComponent<Animator>().Play("Toy_Freddy--CPU_Lurch");
            FindObjectOfType<AudioManagement>().Play("thank you");
            cm.coinCount -= 5;
        }
    }
}
