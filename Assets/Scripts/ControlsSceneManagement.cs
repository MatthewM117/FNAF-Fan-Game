using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ControlsSceneManagement : MonoBehaviour
{
    public TextMeshProUGUI keyboardControlsText;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("SetupNightScene");
        }

        if (PlayerPrefs.GetInt("EnableKeyboard", 0) == 0)
        {
            keyboardControlsText.text = "Keyboard Controls Disabled";
        }
        else
        {
            keyboardControlsText.text = "Keyboard Controls Enabled";
        }
    }

    public void EnableKeyboardControls()
    {
        PlayerPrefs.SetInt("EnableKeyboard", 1);
    }

    public void DisableKeyboardControls()
    {
        PlayerPrefs.SetInt("EnableKeyboard", 0);
    }
}
