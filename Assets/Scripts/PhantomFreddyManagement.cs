using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PhantomFreddyManagement : MonoBehaviour
{
    public Material phantomFreddyMaterial;
    public Material phantomFreddyFaceMaterial; // eyes and mouth
    private GameObject phantomFreddy;
    private float globalAlpha;
    private bool zIsPressed;
    private GameObject flashlight;
    public bool flashlightIsOn;
    private bool playJumpscare;
    private CameraManagement cm;
    private PowerManagement pm;
    private FreddyMaskManagement fmm;

    // Start is called before the first frame update
    void Start()
    {
        globalAlpha = 0f;
        ChangeAlphaOfBody(phantomFreddyMaterial, globalAlpha);
        ChangeAlphaOfFace(phantomFreddyFaceMaterial, globalAlpha);
        phantomFreddy = GameObject.Find("phantom freddy");
        flashlight = GameObject.Find("flashlight");
        flashlight.transform.localScale = new Vector3(0, 0, 0);
        flashlightIsOn = false;
        playJumpscare = true;
        //flashlight.transform.localPosition = new Vector3(-0.152f, 20f, -7.901f);
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        pm = GameObject.Find("PowerManager").GetComponent<PowerManagement>();
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.timeSinceLevelLoad % 7.0f >= 0.001f && Time.timeSinceLevelLoad % 7.0f <= 0.05f && Time.timeSinceLevelLoad >= 7.0f)
        {
            PhantomFreddySpawning();
        }

        if (Input.GetKey(KeyCode.Z))
        {
            FadeOut(globalAlpha);
            flashlight.transform.localScale = new Vector3(0.05001513f, 0.05001513f, 0.05001513f);
            zIsPressed = true;
            flashlightIsOn = true;
        }
        else
        {
            flashlight.transform.localScale = new Vector3(0, 0, 0);
            zIsPressed = false;
            flashlightIsOn = false;
        }

        if (Input.GetKeyDown(KeyCode.Z))
        {
            pm.powerUsage += 1;
            //flashlight.transform.localPosition = new Vector3(-0.152f, 4.822f, -7.901f); 
            FindObjectOfType<AudioManagement>().Play("flashlight on");
        }

        if (Input.GetKeyUp(KeyCode.Z))
        {
            pm.powerUsage -= 1;
            //flashlight.transform.localPosition = new Vector3(-0.152f, 20f, -7.901f);
            FindObjectOfType<AudioManagement>().Play("flashlight off");
        }

        if (globalAlpha < 0)
        {
            globalAlpha = 0f;
            phantomFreddy.transform.localScale = new Vector3(0, 0, 0);
        }
        
        if (globalAlpha >= 1.01f && globalAlpha <= 1.03f)
        {
            if (playJumpscare)
            {
                playJumpscare = false;
                if (cm.camIsOpen)
                {
                    cm.forceCamDown = true;
                }
                StartCoroutine(PhantomFreddyJumpscare());
            }
        }
    }

    private void PhantomFreddySpawning()
    {
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Phantom Freddy Level"))
        {
            StartCoroutine(FadeIn(globalAlpha));
        }
    }

    private void ChangeAlphaOfBody(Material mat, float alphaValue)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
        mat.SetColor("_Color", newColor);
    }

    private void ChangeAlphaOfFace(Material mat, float alphaValue)
    {
        Color oldColor = mat.color;
        Color newColor = new Color(oldColor.r, oldColor.g, oldColor.b, alphaValue);
        mat.SetColor("_Color", newColor);
    }

    private IEnumerator FadeIn(float originalAlpha)
    {
        phantomFreddy.transform.localScale = new Vector3(1.1599f, 1.1599f, 1.1599f);
        while (!zIsPressed)
        {
            if (originalAlpha >= 1 || zIsPressed)
            {
                break;
            }
            originalAlpha += 0.02f;
            globalAlpha = originalAlpha;
            ChangeAlphaOfBody(phantomFreddyMaterial, originalAlpha);
            ChangeAlphaOfFace(phantomFreddyFaceMaterial, originalAlpha);
            yield return new WaitForSeconds(0.1f);
        }
    }

    private void FadeOut(float originalAlpha)
    {
        if (originalAlpha > 0)
        {
            originalAlpha -= 0.05f;
            globalAlpha = originalAlpha;
            ChangeAlphaOfBody(phantomFreddyMaterial, originalAlpha);
            ChangeAlphaOfFace(phantomFreddyFaceMaterial, originalAlpha);
        }
        else
        {
            globalAlpha = originalAlpha;
        }
        
    }

    private IEnumerator PhantomFreddyJumpscare()
    {
        if (fmm.maskIsOn)
        {
            StartCoroutine(fmm.TakeMaskOff());
        }
        FindObjectOfType<AudioManagement>().Play("golden freddy scream");
        for (int i = 0; i < 20; i++)
        {
            phantomFreddy.transform.localScale += new Vector3(0.5f, 0.5f, 0);
            yield return new WaitForSeconds(0.01f);
        }
        SceneManager.LoadScene("GameOverScene");
    }
}
