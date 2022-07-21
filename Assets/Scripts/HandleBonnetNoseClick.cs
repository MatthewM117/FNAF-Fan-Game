using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandleBonnetNoseClick : MonoBehaviour
{

    private BonnetManagement bm;

    // Start is called before the first frame update
    void Start()
    {
        bm = GameObject.Find("BonnetManager").GetComponent<BonnetManagement>();
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            bm.moveBonnet = false;
            bm.moveBonnetDown = true;
            FindObjectOfType<AudioManagement>().Play("click");
        }
    }
}
