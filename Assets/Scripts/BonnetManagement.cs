using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BonnetManagement : MonoBehaviour
{
    private GameObject bonnet;
    private Vector3 bonnetMovement;
    private bool startRotateBonnet;
    public bool moveBonnet;
    public bool moveBonnetDown;
    private float randomNum;
    private bool bonnetIsHere;
    private GameObject bonnetJumpscareHead;
    private bool spawnBonnet;
    private bool bonnetEnabled;
    private FreddyMaskManagement fmm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        bonnet = GameObject.Find("BonBonHW");
        bonnetMovement = new Vector3(5, 0, 0);
        //bonnet.transform.localScale = new Vector3(0, 0, 0);
        startRotateBonnet = true;
        moveBonnet = false;
        moveBonnetDown = false;
        bonnetIsHere = false;
        bonnetJumpscareHead = GameObject.Find("bonnet jumpscare head");
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();

        if (PlayerPrefs.GetInt("Bonnet Level") == 0)
        {
            bonnetEnabled = false;
        }
        else
        {
            bonnetEnabled = true;
        }
    }

    // Update is called once per frame
    void Update()
    {
        //30.3
        if (Time.timeSinceLevelLoad % 19.7f >= 0.001f && Time.timeSinceLevelLoad % 19.7f <= 0.05f && Time.timeSinceLevelLoad >= 19.7f && !bonnetIsHere && bonnetEnabled)
        {
            //StartCoroutine(StartBonnetSpawn());
            spawnBonnet = true;
            BonnetSpawning();
        }

        if (moveBonnet)
        {
            bonnetIsHere = true;
            MoveBonnet();
        }

        if (moveBonnetDown)
        {
            MoveBonnetDown();
            moveBonnet = false;
            if (bonnet.transform.localPosition.y <= -1153)
            {
                ResetBonnet();
            }
        }
        else if (bonnet.transform.localPosition.x <= -500)
        {
            MoveBonnetDown();
            if (bonnet.transform.localPosition.y <= -1153)
            {
                ResetBonnet();
                StartCoroutine(BonnetJumpscare());
            }
        }
    }

    private void BonnetSpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Bonnet Level") && spawnBonnet)
        {
            moveBonnet = true;
            startRotateBonnet = true;
            spawnBonnet = false;
        }
    }

    IEnumerator BonnetJumpscare()
    {
        if (cm.camIsOpen)
        {
            cm.forceCamDown = true;
        }
        if (fmm.maskIsOn)
        {
            StartCoroutine(fmm.TakeMaskOff());
        }
        FindObjectOfType<AudioManagement>().Play("jumpscare");
        while (true)
        {
            bonnetJumpscareHead.transform.localPosition += new Vector3(0, 200, 0);
            yield return new WaitForSeconds(0.0001f);

            if (bonnetJumpscareHead.transform.localPosition.y >= -650)
            {
                break;
            }
        }
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }

    private void ResetBonnet()
    {
        bonnetIsHere = false;
        moveBonnetDown = false;
        startRotateBonnet = false;
        moveBonnet = false;
        bonnet.transform.localPosition = new Vector3(896, -561, 432);
        //bonnet.transform.localScale = new Vector3(0, 0, 0);
        spawnBonnet = true;
    }

    private void MoveBonnet()
    {
        if (!moveBonnetDown)
        {
            //bonnet.transform.localScale = new Vector3(1.4321f, 1.5986f, 1);
            bonnet.transform.localPosition -= bonnetMovement;
            if (startRotateBonnet)
            {
                startRotateBonnet = false;
                StartCoroutine(RotateBonnet());
            }
        }
    }

    private void MoveBonnetDown()
    {
        bonnet.transform.localPosition -= new Vector3(0, 15f, 0);
    }

    IEnumerator RotateBonnet()
    {
        while (true)
        {
            yield return new WaitForSeconds(0.2f);
            bonnet.transform.Rotate(0, 0, 12);
            yield return new WaitForSeconds(0.2f);
            bonnet.transform.Rotate(0, 0, -12);
            if (!moveBonnet)
            {
                break;
            }
        }
    }
    /*
    IEnumerator StartBonnetSpawn()
    {
        randomNum = Random.Range(7.0f, 15.0f);

        yield return new WaitForSeconds(randomNum);

        moveBonnet = true;
        startRotateBonnet = true;
    }*/
}
