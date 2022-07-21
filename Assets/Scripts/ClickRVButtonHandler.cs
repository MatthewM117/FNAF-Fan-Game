using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClickRVButtonHandler : MonoBehaviour
{
    private VentFailManagement vfm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        vfm = GameObject.Find("VentilationFailureManager").GetComponent<VentFailManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) && PlayerPrefs.GetInt("EnableKeyboard", 0) == 1 && cm.camIsOpen)
        {
            vfm.stopFlashing = true;
        }
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            vfm.stopFlashing = true;
        }
    }
}
