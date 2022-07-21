using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class PlayButtonHoverHandler : MonoBehaviour
{
    public TextMeshProUGUI playArrow;

    private void OnMouseEnter()
    {
        FindObjectOfType<AudioManagement>().Play("title button sound");
        playArrow.text = ">>";
    }

    private void OnMouseExit()
    {
        playArrow.text = "";
    }
}
