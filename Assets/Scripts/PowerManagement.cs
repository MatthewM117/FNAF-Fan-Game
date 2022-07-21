using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class PowerManagement : MonoBehaviour
{

    public TextMeshProUGUI powerText;
    public int powerLeft;
    private bool changePower;
    public int powerUsage;

    private GameObject usageBar2;
    private GameObject usageBar3;
    private GameObject usageBar4;
    private GameObject usageBar5;
    private GameObject usageBar6;

    private FadeHandler fadeToBlack;

    private PhantomFreddyManagement pfm;

    // Start is called before the first frame update
    void Start()
    {
        powerLeft = 100;
        powerText.text = "Power: " + powerLeft + "%";
        changePower = true;

        usageBar2 = GameObject.Find("pubar2");
        usageBar3 = GameObject.Find("pubar3");
        usageBar4 = GameObject.Find("pubar4");
        usageBar5 = GameObject.Find("pubar5");
        usageBar6 = GameObject.Find("pubar6");

        powerUsage = 1;

        fadeToBlack = GameObject.Find("fade to black").GetComponent<FadeHandler>();

        pfm = GameObject.Find("PhantomFreddyManager").GetComponent<PhantomFreddyManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad % 0.5f >= 0.001f && Time.timeSinceLevelLoad % 0.5f <= 0.05f && changePower && powerUsage >= 6)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (Time.timeSinceLevelLoad % 1.0f >= 0.001f && Time.timeSinceLevelLoad % 1.0f <= 0.05f && changePower && powerUsage == 5)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (Time.timeSinceLevelLoad % 1.0f >= 0.001f && Time.timeSinceLevelLoad % 1.0f <= 0.05f && changePower && pfm.flashlightIsOn)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (Time.timeSinceLevelLoad % 2.0f >= 0.001f && Time.timeSinceLevelLoad % 2.0f <= 0.05f && changePower && powerUsage == 4)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (Time.timeSinceLevelLoad % 3.0f >= 0.001f && Time.timeSinceLevelLoad % 3.0f <= 0.05f && changePower && powerUsage == 3)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (Time.timeSinceLevelLoad % 4.0f >= 0.001f && Time.timeSinceLevelLoad % 4.0f <= 0.05f && changePower && powerUsage == 2)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (Time.timeSinceLevelLoad % 5.0f >= 0.001f && Time.timeSinceLevelLoad % 5.0f <= 0.05f && Time.timeSinceLevelLoad >= 5 && changePower && powerUsage == 1)
        {
            changePower = false;
            powerLeft -= 1;
            powerText.text = "Power: " + powerLeft + "%";
            StartCoroutine(ChangePowerCooldown());
        }

        if (powerUsage == 1)
        {
            HidePubar2();
            HidePubar3();
            HidePubar4();
            HidePubar5();
            HidePubar6();
        }
        else if (powerUsage == 2)
        {
            ShowPubar2();
            HidePubar3();
            HidePubar4();
            HidePubar5();
            HidePubar6();
        }
        else if (powerUsage == 3)
        {
            ShowPubar2();
            ShowPubar3();
            HidePubar4();
            HidePubar5();
            HidePubar6();
        }
        else if (powerUsage == 4)
        {
            ShowPubar2();
            ShowPubar3();
            ShowPubar4();
            HidePubar5();
            HidePubar6();
        }
        else if (powerUsage == 5)
        {
            ShowPubar2();
            ShowPubar3();
            ShowPubar4();
            ShowPubar5();
            HidePubar6();
        }
        else if (powerUsage == 6)
        {
            ShowPubar2();
            ShowPubar3();
            ShowPubar4();
            ShowPubar5();
            ShowPubar6();
        }

        if (powerLeft <= 0)
        {
            fadeToBlack.FadeOut();
            StartCoroutine(WaitASecond());
        }
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }

    IEnumerator ChangePowerCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        changePower = true;
    }

    private void HidePubar2()
    {
        usageBar2.transform.localScale = new Vector3(0, 0, 0);
    }

    private void HidePubar3()
    {
        usageBar3.transform.localScale = new Vector3(0, 0, 0);
    }

    private void HidePubar4()
    {
        usageBar4.transform.localScale = new Vector3(0, 0, 0);
    }

    private void HidePubar5()
    {
        usageBar5.transform.localScale = new Vector3(0, 0, 0);
    }

    private void HidePubar6()
    {
        usageBar6.transform.localScale = new Vector3(0, 0, 0);
    }

    private void ShowPubar2()
    {
        usageBar2.transform.localScale = new Vector3(0.100955f, 0.04739559f, 0.033412f);
    }

    private void ShowPubar3()
    {
        usageBar3.transform.localScale = new Vector3(0.100955f, 0.04739559f, 0.033412f);
    }

    private void ShowPubar4()
    {
        usageBar4.transform.localScale = new Vector3(0.100955f, 0.04739559f, 0.033412f);
    }

    private void ShowPubar5()
    {
        usageBar5.transform.localScale = new Vector3(0.100955f, 0.04739559f, 0.033412f);
    }

    private void ShowPubar6()
    {
        usageBar6.transform.localScale = new Vector3(0.100955f, 0.04739559f, 0.033412f);
    }
}
