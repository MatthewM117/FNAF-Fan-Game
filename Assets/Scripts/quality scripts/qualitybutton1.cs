using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class qualitybutton1 : MonoBehaviour
{
    public TextMeshProUGUI arrow;
    public TextMeshProUGUI qualityTitle;

    private void Start()
    {
        arrow.text = "";
    }

    private void Update()
    {
        if (QualitySettings.GetQualityLevel() == 0)
        {
            qualityTitle.text = "Quality Level: Very Low";
        }
        else if (QualitySettings.GetQualityLevel() == 1)
        {
            qualityTitle.text = "Quality Level: Low";
        }
        else if (QualitySettings.GetQualityLevel() == 2)
        {
            qualityTitle.text = "Quality Level: Medium";
        }
        else if (QualitySettings.GetQualityLevel() == 3)
        {
            qualityTitle.text = "Quality Level: High";
        }
        else if (QualitySettings.GetQualityLevel() == 4)
        {
            qualityTitle.text = "Quality Level: Very High";
        }
        else if (QualitySettings.GetQualityLevel() == 5)
        {
            qualityTitle.text = "Quality Level: Ultra";
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
    }

    private void OnMouseEnter()
    {
        FindObjectOfType<AudioManagement>().Play("title button sound");
        arrow.rectTransform.localPosition = new Vector3(gameObject.transform.localPosition.x, gameObject.transform.localPosition.y, 0);
        arrow.text = ">>";
    }

    private void OnMouseOver()
    {
        if (Input.GetMouseButtonDown(0))
        {
            if (gameObject.tag == "very low") //Fastest Quality
            {
                QualitySettings.SetQualityLevel(0, true);
            }

            if (gameObject.tag == "low") //Fast Quality
            {
                QualitySettings.SetQualityLevel(1, true);
            }

            if (gameObject.tag == "medium") //Simple Graphics
            {
                QualitySettings.SetQualityLevel(2, true);
            }

            if (gameObject.tag == "high") //Good Graphics
            {
                QualitySettings.SetQualityLevel(3, true);
            }

            if (gameObject.tag == "very high") //Beautiful Graphics
            {
                QualitySettings.SetQualityLevel(4, true);
            }

            if (gameObject.tag == "ultra") //Fantastic Graphics
            {
                QualitySettings.SetQualityLevel(5, true);
            }
        }
    }

    private void OnMouseExit()
    {
        arrow.text = "";
    }
}
