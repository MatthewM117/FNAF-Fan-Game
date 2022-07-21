using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class QualityButtonClick : MonoBehaviour
{
    public TextMeshProUGUI qualityArrows;

    // Start is called before the first frame update
    void Start()
    {
        qualityArrows.text = "";
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnMouseEnter()
    {
        FindObjectOfType<AudioManagement>().Play("title button sound");
        qualityArrows.text = ">>";
    }

    private void OnMouseExit()
    {
        qualityArrows.text = "";
    }

    private void OnMouseDown()
    {
        SceneManager.LoadScene("QualityScene");
    }
}
