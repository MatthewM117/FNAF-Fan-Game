using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinManagement : MonoBehaviour
{
    private CameraManagement cm;

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    
    private void OnMouseEnter()
    {
        gameObject.transform.localScale = new Vector3(0, 0, 0);
        FindObjectOfType<AudioManagement>().Play("coin collect");
        cm.coinCount += 1;
    }
}
