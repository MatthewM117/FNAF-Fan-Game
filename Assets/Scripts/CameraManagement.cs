using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CameraManagement : MonoBehaviour
{

    public Camera cam;
    private Vector3 camRotateRight;
    private Vector3 camRotateLeft;
    private bool stopCamOnRight;
    private bool stopCamOnLeft;
    public bool camIsOpen;
    private bool cooldown;
    private float currentCamRotY;
    private FreddyMaskManagement fmm;
    private GameObject fhb;
    private GoldenFreddyManagement gfm;
    private HelpyManagement hm;
    private OMCManagement omcm;
    private bool helpyEnabled;
    private bool goldenFreddyEnabled;
    private bool allowCamClose;
    private bool allowCamOpen;
    private GameObject coin1;
    private GameObject coin2;
    float randomPosX;
    float randomPosY;
    public TextMeshProUGUI coinText;
    public int coinCount;
    private PhantomFreddyManagement pfm;
    private PowerManagement pm;
    public bool forceCamDown;

    // Start is called before the first frame update
    void Start()
    {
        camRotateRight = new Vector3(0f, 0.5f, 0f);
        camRotateLeft = new Vector3(0f, -0.5f, 0f);
        stopCamOnRight = false;
        camIsOpen = false;
        cooldown = false;
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        fhb = GameObject.Find("freddy head button");
        gfm = GameObject.Find("GoldenFreddyManager").GetComponent<GoldenFreddyManagement>();
        hm = GameObject.Find("HelpyManager").GetComponent<HelpyManagement>();
        omcm = GameObject.Find("OMCManager").GetComponent<OMCManagement>();
        coin1 = GameObject.Find("coin1");
        coin2 = GameObject.Find("coin2");
        coin1.transform.localScale = new Vector3(0, 0, 0);
        coin2.transform.localScale = new Vector3(0, 0, 0);
        coinCount = 0;
        coinText.text = coinCount.ToString();
        pfm = GameObject.Find("PhantomFreddyManager").GetComponent<PhantomFreddyManagement>();
        pm = GameObject.Find("PowerManager").GetComponent<PowerManagement>();
        forceCamDown = false;

        if (PlayerPrefs.GetInt("Helpy Level") == 0)
        {
            helpyEnabled = false;
        }
        else
        {
            helpyEnabled = true;
        }

        if (PlayerPrefs.GetInt("Golden Freddy Level") == 0)
        {
            goldenFreddyEnabled = false;
        }
        else
        {
            goldenFreddyEnabled = true;
        }
    }
    
    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.x);
        //Debug.Log(mousePos.y);
        //Debug.Log("right: " + stopCamOnRight);
        //Debug.Log("left: " + stopCamOnLeft);
        
        if (cam.transform.rotation.y >= 0.2f)
        {
            stopCamOnRight = true;
        }

        if (cam.transform.rotation.y <= -0.2f)
        {
            stopCamOnLeft = true;
        }

        if (!camIsOpen)
        {

            if (PlayerPrefs.GetInt("EnableKeyboard", 0) == 0)
            {
                if (mousePos.x >= (Screen.width / 2) + 200 && !stopCamOnRight)
                {
                    if (mousePos.x >= (Screen.width / 2) + 500)
                    {
                        cam.transform.Rotate(0, 1.5f, 0);
                    }
                    else
                    {
                        cam.transform.Rotate(camRotateRight);
                    }
                    stopCamOnLeft = false;
                }
                else if (mousePos.x < (Screen.width / 2) - 200 && !stopCamOnLeft)
                {
                    if (mousePos.x <= (Screen.width / 2) - 500)
                    {
                        cam.transform.Rotate(0, -1.5f, 0);
                    }
                    else
                    {
                        cam.transform.Rotate(camRotateLeft);
                    }
                    stopCamOnRight = false;
                }
            }
            else
            {
                if (Input.GetKey(KeyCode.RightArrow) && !stopCamOnRight)
                {
                    cam.transform.Rotate(0, 1, 0);
                    stopCamOnLeft = false;
                }
                else if (Input.GetKey(KeyCode.LeftArrow) && !stopCamOnLeft)
                {
                    cam.transform.Rotate(0, -1, 0);
                    stopCamOnRight = false;
                }
            }
            
        }

        if ((mousePos.x >= (Screen.width/2) && mousePos.y <= 100 && !camIsOpen && !cooldown && !fmm.maskIsOn && !omcm.OMCLockMonitor && allowCamOpen) || (Input.GetKeyDown(KeyCode.S) && !camIsOpen && !fmm.maskIsOn && !omcm.OMCLockMonitor))
        {
            pm.powerUsage += 1;
            cam.transform.position += new Vector3(0f, 8.095f, 0f);
            currentCamRotY = cam.transform.rotation.eulerAngles.y;
            //currentCamRotY = cam.transform.rotation.y;
            //Debug.Log("before " + currentCamRotY);
            cam.transform.rotation = Quaternion.Euler(0, 0, 0);
            fhb.transform.localScale = new Vector3(0, 0, 0);
            camIsOpen = true;
            allowCamClose = false;
            SpawnCoins();

            StartCoroutine(CamOpenCooldown());

            // helpy stuff
            if (helpyEnabled)
            {
                hm.CheckIfhelpyIsSpawning();
            }

            FindObjectOfType<AudioManagement>().Play("monitor up");

        }
        else if ((mousePos.x >= (Screen.width / 2) && mousePos.y <= 100 && camIsOpen && cooldown && allowCamClose) || (Input.GetKeyDown(KeyCode.S) && camIsOpen) || forceCamDown)
        {
            if (forceCamDown)
            {
                forceCamDown = false;
            }
            pm.powerUsage -= 1;
            cam.transform.position -= new Vector3(0f, 8.095f, 0f);
            //cam.transform.rotation = Quaternion.Euler(0, currentCamRotY, 0);
            cam.transform.Rotate(0, 0, 0);
            fhb.transform.localScale = new Vector3(157.8189f, 176.1609f, 0.2206791f);
            //Debug.Log("after " + currentCamRotY);
            //cam.transform.Rotate(0f, currentCamRotY, 0f);
            camIsOpen = false;
            stopCamOnLeft = false;
            stopCamOnRight = false;
            allowCamOpen = false;
            DespawnCoins();

            // golden freddy stuff
            if (goldenFreddyEnabled)
            {
                gfm.CheckIfGoldenFreddyIsSpawning();
            }

            if (!gfm.goldenFreddyIsSpawned)
            {
                gfm.attemptToSpawnGoldenFreddy = true;
            }
            
            if (gfm.goldenFreddyIsSpawned)
            {
                StartCoroutine(gfm.CheckIfMaskIsOnForGoldenFreddy());
            }

            // helpy stuff
            if (!hm.helpyIsSpawned)
            {
                hm.attemptToSpawnHelpy = true;
            }

            FindObjectOfType<AudioManagement>().Play("monitor up");
            StartCoroutine(CamCloseCooldown());
        }

        if (mousePos.x >= (Screen.width / 2) && mousePos.y <= 100 && omcm.OMCLockMonitor || Input.GetKeyDown(KeyCode.S) && omcm.OMCLockMonitor)
        {
            FindObjectOfType<AudioManagement>().Play("button not working");
        }

        if (!allowCamClose && mousePos.y > 100)
        {
            allowCamClose = true;
        }

        if (!allowCamOpen && mousePos.y > 100)
        {
            allowCamOpen = true;
        }

        coinText.text = coinCount.ToString();
    }

    IEnumerator CamOpenCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        cooldown = true;
    }

    IEnumerator CamCloseCooldown()
    {
        yield return new WaitForSeconds(0.2f);
        cooldown = false;
    }

    private void SpawnCoins()
    {
        int randNum = Random.Range(1, 5);
        if (randNum == 1)
        {
            int randNum2 = Random.Range(1, 3);
            if (randNum2 == 1)
            {
                SpawnCoin1();
            }
            else if (randNum2 == 2)
            {
                SpawnCoin1();
                SpawnCoin2();
            }
        }
    }

    private void SpawnCoin1()
    {
        randomPosX = Random.Range(-0.9f, 0.4f);
        randomPosY = Random.Range(-0.0222f, 0.0262f);
        coin1.transform.localPosition = new Vector3(Mathf.Round(randomPosX * 10000f) / 10000f, Mathf.Round(randomPosY * 10000f) / 10000f, -2.5f);
        coin1.transform.localScale = new Vector3(0.1497379f, 0.007239751f, 1.203074f);
    }

    private void SpawnCoin2()
    {
        randomPosX = Random.Range(-0.9f, 0.4f);
        randomPosY = Random.Range(-0.0222f, 0.0262f);
        coin2.transform.localPosition = new Vector3(randomPosX, randomPosY, -2.5f);
        coin2.transform.localScale = new Vector3(0.1497379f, 0.007239751f, 1.203074f);
    }

    private void DespawnCoins()
    {
        coin1.transform.localScale = new Vector3(0, 0, 0);
        coin2.transform.localScale = new Vector3(0, 0, 0);
    }
}
