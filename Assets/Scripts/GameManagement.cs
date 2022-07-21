using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class GameManagement : MonoBehaviour
{

    private GameObject leftDoor;
    private Vector3 scaleChangeDoorDown;
    private Vector3 scaleChangeDoorUp;
    public bool doorIsClosed;
    private float doorSpeed;

    private GameObject rightVent;
    public bool rightVentIsClosed;

    private GameObject middleVent;
    public bool middleVentIsClosed;

    private FreddyManagement freddyManager;

    private SpringTrapManagement stm;

    private ToyBonnieManagement tbm;

    public TextMeshProUGUI nightTimeText;
    private int timeInNight;
    private bool switchFrom12to1;
    private bool changeTimeCooldown;

    public TextMeshProUGUI accurateTimeText;
    private float timeNow;
    //string minutesString;
    string secondsString;
    string msString;

    private PowerManagement pm;

    // Start is called before the first frame update
    void Start()
    {
        //Debug.Log(Screen.width + " " + Screen.height);

        leftDoor = GameObject.Find("left door");
        scaleChangeDoorDown = new Vector3(0f, 0.1f, 0f);
        scaleChangeDoorUp = new Vector3(0f, -0.1f, 0f);
        doorSpeed = 0.01f;
        doorIsClosed = false;

        rightVent = GameObject.Find("right vent");
        rightVentIsClosed = false;

        middleVent = GameObject.Find("middle vent");
        middleVentIsClosed = false;

        freddyManager = GameObject.Find("FreddyManager").GetComponent<FreddyManagement>();

        stm = GameObject.Find("SpringTrapManager").GetComponent<SpringTrapManagement>();

        tbm = GameObject.Find("ToyBonnieManager").GetComponent<ToyBonnieManagement>();

        pm = GameObject.Find("PowerManager").GetComponent<PowerManagement>();

        timeInNight = 12;
        switchFrom12to1 = true;
        changeTimeCooldown = true;
        nightTimeText.text = timeInNight + " AM";
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.A))
        {
            if (doorIsClosed)
            {
                pm.powerUsage -= 1;
                doorIsClosed = false;
                StartCoroutine(OpenDoor());
            }
            else
            {
                pm.powerUsage += 1;
                doorIsClosed = true;
                StartCoroutine(CloseDoor());
            }
            FindObjectOfType<AudioManagement>().Play("door slam");
            
        }

        if (Input.GetKeyDown(KeyCode.D))
        {
            if (rightVentIsClosed)
            {
                pm.powerUsage -= 1;
                rightVentIsClosed = false;
                StartCoroutine(OpenRightVent());
            }
            else
            {
                pm.powerUsage += 1;
                rightVentIsClosed = true;
                StartCoroutine(CloseRightVent());
            }
            FindObjectOfType<AudioManagement>().Play("door slam");
        }

        if (Input.GetKeyDown(KeyCode.W))
        {
            if (middleVentIsClosed)
            {
                pm.powerUsage -= 1;
                middleVentIsClosed = false;
                StartCoroutine(OpenMiddleVent());
            }
            else
            {
                pm.powerUsage += 1;
                middleVentIsClosed = true;
                StartCoroutine(CloseMiddleVent());
            }
            FindObjectOfType<AudioManagement>().Play("door slam");
        }

        if (Time.timeSinceLevelLoad % 50.0f >= 0.001f && Time.timeSinceLevelLoad % 50.0f <= 0.05f && Time.timeSinceLevelLoad >= 50.0f && changeTimeCooldown)
        {
            changeTimeCooldown = false;
            if (switchFrom12to1)
            {
                switchFrom12to1 = false;
                timeInNight = 1;
                nightTimeText.text = timeInNight + " AM";
            }
            else
            {
                timeInNight += 1;
                nightTimeText.text = timeInNight + " AM";
            }
            StartCoroutine(ChangeTimeCooldown());
        }

        if (Time.timeSinceLevelLoad >= 300)
        {
            SceneManager.LoadScene("YouWinScene");
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SetupNightScene");
        }

        Timer();

        if (Input.GetKeyDown(KeyCode.Alpha5)) //Fastest Quality
        {
            QualitySettings.SetQualityLevel(0, true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha6)) //Fast Quality
        {
            QualitySettings.SetQualityLevel(1, true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha7)) //Simple Graphics
        {
            QualitySettings.SetQualityLevel(2, true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha8)) //Good Graphics
        {
            QualitySettings.SetQualityLevel(3, true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha9)) //Beautiful Graphics
        {
            QualitySettings.SetQualityLevel(4, true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha0)) //Fantastic Graphics
        {
            QualitySettings.SetQualityLevel(5, true);
        }
    }

    private void Timer()
    {
        timeNow = Time.timeSinceLevelLoad;
        System.TimeSpan t = System.TimeSpan.FromSeconds(timeNow);
        if (t.Seconds < 10)
        {
            secondsString = "0" + t.Seconds.ToString();
        }
        else
        {
            secondsString = t.Seconds.ToString();
        }
        msString = t.Milliseconds.ToString();
        accurateTimeText.text = t.Minutes + ":" + secondsString + "." + msString[0];
    }

    IEnumerator ChangeTimeCooldown()
    {
        yield return new WaitForSeconds(1);
        changeTimeCooldown = true;
    }

    // for left door
    IEnumerator CloseDoor()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorSpeed);
            leftDoor.transform.localScale += new Vector3(0f, 0.3f, 0f);
        }

        if (freddyManager.freddyIsHere)
        {
            freddyManager.HideFreddyEyes();
            freddyManager.freddyIsHere = false;
        }
    }

    // for left door
    IEnumerator OpenDoor()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorSpeed);
            leftDoor.transform.localScale += new Vector3(0f, -0.3f, 0f);
        }
    }

    IEnumerator CloseRightVent()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorSpeed);
            rightVent.transform.localScale += scaleChangeDoorDown;
        }

        if (tbm.toyBonnieIsHere)
        {
            tbm.HideToyBonnie();
            tbm.toyBonnieIsHere = false;
        }
    }

    IEnumerator OpenRightVent()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorSpeed);
            rightVent.transform.localScale += scaleChangeDoorUp;
        }
    }

    IEnumerator CloseMiddleVent()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorSpeed);
            middleVent.transform.localScale += scaleChangeDoorDown;
        }

        if (stm.springtrapIsHere)
        {
            stm.HideSpringtrapEyes();
            stm.springtrapIsHere = false;
        }
    }

    IEnumerator OpenMiddleVent()
    {
        for (int i = 0; i < 20; i++)
        {
            yield return new WaitForSeconds(doorSpeed);
            middleVent.transform.localScale += scaleChangeDoorUp;
        }
    }
}
