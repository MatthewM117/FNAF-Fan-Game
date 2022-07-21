using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ExitButtonHoverHandler : MonoBehaviour
{
    public TextMeshProUGUI exitArrow;

    private void OnMouseEnter()
    {
        FindObjectOfType<AudioManagement>().Play("title button sound");
        exitArrow.text = ">>";
    }

    private void OnMouseExit()
    {
        exitArrow.text = "";
    }
}
