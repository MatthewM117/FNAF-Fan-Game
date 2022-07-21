using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VentFailManagement : MonoBehaviour
{

    private GameObject ventFailWarning;
    private float randomNum;
    private float timeUntilTenSeconds;
    private float timeUntil15Seconds;
    public bool stopFlashing;
    private FadeHandler fadeToBlack;

    // Start is called before the first frame update
    void Start()
    {
        ventFailWarning = GameObject.Find("ventilation warning");
        HideVentWarning();
        stopFlashing = false;
        fadeToBlack = GameObject.Find("fade to black").GetComponent<FadeHandler>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad % 30.0f >= 0.001f && Time.timeSinceLevelLoad % 30.0f <= 0.05f && Time.timeSinceLevelLoad >= 30.0f)
        {
            StartCoroutine(StartCalculatinVentWarning());
        }
    }

    private void HideVentWarning()
    {
        ventFailWarning.transform.localScale = new Vector3(0, 0, 0);
    }

    private void ShowVentWarning()
    {
        ventFailWarning.transform.localScale = new Vector3(439.6476f, 335.3862f, 72.53056f);
    }

    IEnumerator FlashVentWarning()
    {
        timeUntilTenSeconds = Time.timeSinceLevelLoad + 10;
        timeUntil15Seconds = Time.timeSinceLevelLoad + 15;
        while (true)
        {
            if (Time.timeSinceLevelLoad >= timeUntilTenSeconds)
            {
                yield return new WaitForSeconds(0.1f);
                ShowVentWarning();

                yield return new WaitForSeconds(0.1f);
                HideVentWarning();
            }
            else
            {
                yield return new WaitForSeconds(0.2f);
                ShowVentWarning();

                yield return new WaitForSeconds(0.2f);
                HideVentWarning();
            }

            if (stopFlashing)
            {
                break;
            }

            if (Time.timeSinceLevelLoad >= timeUntil15Seconds)
            {
                fadeToBlack.FadeOut();
                StartCoroutine(WaitASecond());
                break;
            }
        }
    }

    IEnumerator StartCalculatinVentWarning()
    {
        stopFlashing = false;
        randomNum = Random.Range(5.0f, 10.0f);

        yield return new WaitForSeconds(randomNum);

        StartCoroutine(FlashVentWarning());
    }

    IEnumerator WaitASecond()
    {
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
