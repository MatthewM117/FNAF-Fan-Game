using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleHelpyClick : MonoBehaviour
{
    private HelpyManagement hm;
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        hm = GameObject.Find("HelpyManager").GetComponent<HelpyManagement>();
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0) && hm.allowClick)
        {
            hm.helpyIsClicked = true;
            hm.DespawnHelpy();
            hm.attemptToSpawnHelpy = true;
            cm.coinCount += 1;
            FindObjectOfType<AudioManagement>().Play("nose honk");
        }
    }
}
