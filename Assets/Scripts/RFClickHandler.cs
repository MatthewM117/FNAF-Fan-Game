using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RFClickHandler : MonoBehaviour
{
    private RockstarFreddyManagement rfm;

    // Start is called before the first frame update
    void Start()
    {
        rfm = GameObject.Find("RockstarFreddyManager").GetComponent<RockstarFreddyManagement>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseDown()
    {
        rfm.PayClicked();
    }
}
