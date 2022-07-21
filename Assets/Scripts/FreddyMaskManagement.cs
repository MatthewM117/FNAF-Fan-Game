using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FreddyMaskManagement : MonoBehaviour
{
    public bool maskIsOn;
    private GameObject freddyMask;
    private bool cooldown;
    private CameraManagement cm;
    private bool allowMaskOff;
    private bool allowMaskOn;
    private bool doOnce;
    //private GoldenFreddyManagement gfm;

    // Start is called before the first frame update
    void Start()
    {
        maskIsOn = false;
        freddyMask = GameObject.Find("Mask_Geo");
        cooldown = false;
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        freddyMask.transform.localScale = new Vector3(0, 0, 0);
        doOnce = true;
        //gfm = GameObject.Find("GoldenFreddyManager").GetComponent<GoldenFreddyManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 mousePos = Input.mousePosition;
        //Debug.Log(mousePos.x);
        //Debug.Log(mousePos.y);

        if (PlayerPrefs.GetInt("EnableKeyboard", 0) == 0)
        {
            if (mousePos.x <= (Screen.width / 2) && mousePos.y <= 100 && !maskIsOn && !cm.camIsOpen && allowMaskOn)
            {
                maskIsOn = true;
                allowMaskOff = false;
                FindObjectOfType<AudioManagement>().Play("freddy mask on");
                StartCoroutine(PutMaskOn());
                StartCoroutine(MaskOnCooldown());
            }

            if (mousePos.x <= (Screen.width / 2) && mousePos.y <= 100 && maskIsOn && allowMaskOff)
            {
                maskIsOn = false;
                allowMaskOn = false;
                FindObjectOfType<AudioManagement>().Play("freddy mask off");
                StartCoroutine(TakeMaskOff());
                StartCoroutine(MaskOffCooldown());
            }

            if (!allowMaskOff && mousePos.y > 100)
            {
                allowMaskOff = true;
            }

            if (!allowMaskOn && mousePos.y > 100)
            {
                allowMaskOn = true;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.X) && !maskIsOn && !cooldown && !cm.camIsOpen && allowMaskOn)
            {
                maskIsOn = true;
                allowMaskOff = false;
                FindObjectOfType<AudioManagement>().Play("freddy mask on");
                StartCoroutine(PutMaskOn());
                StartCoroutine(MaskOnCooldown());
            }

            if (Input.GetKeyDown(KeyCode.X) && maskIsOn && cooldown && allowMaskOff)
            {
                maskIsOn = false;
                allowMaskOn = false;
                FindObjectOfType<AudioManagement>().Play("freddy mask on");
                StartCoroutine(TakeMaskOff());
                StartCoroutine(MaskOffCooldown());
            }

            if (!allowMaskOff && mousePos.y > 100)
            {
                allowMaskOff = true;
            }

            if (!allowMaskOn && mousePos.y > 100)
            {
                allowMaskOn = true;
            }
        }
        
    }

    IEnumerator MaskOnCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        cooldown = true;
    }

    IEnumerator MaskOffCooldown()
    {
        yield return new WaitForSeconds(0.5f);
        cooldown = false;
    }

    IEnumerator PutMaskOn()
    {
        float rotationNum = 0;
        freddyMask.transform.localScale = new Vector3(539013.4f, 539013.4f, 539013.4f);
        //freddyMask.transform.localScale = new Vector3(655763.6f, 655763.6f, 655763.6f);
        // y -500
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            rotationNum += 0.5f;
            if (doOnce)
            {
                freddyMask.transform.localPosition += new Vector3(0, 0, 200);
            }
            freddyMask.transform.Rotate(rotationNum, 0, 0);
            if (freddyMask.transform.localRotation.x >= 0)
            {
                doOnce = false;
                break;
            }
            /*
            if (freddyMask.transform.localPosition.y <= 78)
            {
                break;
            }*/
        }
        
    }

    // ORIGINAL Z POSITION: -566

    public IEnumerator TakeMaskOff()
    {
        float rotationNum = 0;
        while (true)
        {
            yield return new WaitForSeconds(0.001f);
            rotationNum -= 0.5f;
            freddyMask.transform.Rotate(rotationNum, 0, 0);
            if (freddyMask.transform.localRotation.x <= -0.9f)
            {
                freddyMask.transform.localScale = new Vector3(0, 0, 0);
                //freddyMask.transform.localPosition += new Vector3(0, -500, -1824);
                break;
            }
            //freddyMask.transform.position += new Vector3(0f, 6f, 0f);
            /*
            if (freddyMask.transform.localPosition.y >= 1050)
            {
                break;
            }*/
        }
        
    }
}
