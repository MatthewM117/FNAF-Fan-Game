using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class AftonManagement : MonoBehaviour
{
    private GameObject aftonJumpscareHead;

    [SerializeField]
    private Image black;
    private bool ceilingVentClosed;

    private PowerManagement pm;
    private float darknessOfScene;

    private FreddyMaskManagement fmm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        aftonJumpscareHead = GameObject.Find("afton jumpscare head");
        pm = GameObject.Find("PowerManager").GetComponent<PowerManagement>();
        ceilingVentClosed = false;
        darknessOfScene = 0.6f;
        var tempColor = black.color;
        tempColor.a = darknessOfScene;
        black.color = tempColor;
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad % 20.7f >= 0.001f && Time.timeSinceLevelLoad % 20.7f <= 0.05f && Time.timeSinceLevelLoad >= 20.7f)
        {
            int randomNum = Random.Range(1, 23);
            if (randomNum <= PlayerPrefs.GetInt("Afton Level"))
            {
                StartCoroutine(FlickerLights());
                StartCoroutine(CheckIfVentClosed());
            }
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            ceilingVentClosed = !ceilingVentClosed;
            if (ceilingVentClosed)
            {
                pm.powerUsage += 1;
            }
            else
            {
                pm.powerUsage -= 1;
            }
            FindObjectOfType<AudioManagement>().Play("door slam");
        }
    }

    private IEnumerator FlickerLights()
    {
        var tempColor = black.color;
        for (int i = 0; i < 5; i++)
        {
            if (i < 3)
            {
                tempColor.a = 1;
                black.color = tempColor;
                yield return new WaitForSeconds(0.2f);
                tempColor.a = darknessOfScene;
                black.color = tempColor;
                yield return new WaitForSeconds(0.2f);
            }
            else
            {
                tempColor.a = 1;
                black.color = tempColor;
                yield return new WaitForSeconds(0.1f);
                tempColor.a = darknessOfScene;
                black.color = tempColor;
                yield return new WaitForSeconds(0.1f);
            }
        }
    }

    private IEnumerator CheckIfVentClosed()
    {
        for (int i = 0; i < 15; i++)
        {
            if (ceilingVentClosed)
            {
                break;
            }
            yield return new WaitForSeconds(0.1f);
            if (i == 14)
            {
                i = 15;
                StartCoroutine(AftonJumpscare());
            }
        }
    }

    private IEnumerator AftonJumpscare()
    {
        if (cm.camIsOpen)
        {
            cm.forceCamDown = true;
        }
        if (fmm.maskIsOn)
        {
            StartCoroutine(fmm.TakeMaskOff());
        }
        int random = Random.Range(5, 11);
        yield return new WaitForSeconds(random);
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        while (true)
        {
            aftonJumpscareHead.transform.localPosition += new Vector3(0, 200, 0);
            yield return new WaitForSeconds(0.0001f);

            if (aftonJumpscareHead.transform.localPosition.y >= -1241)
            {
                break;
            }
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
