using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class TitleScreenManagement : MonoBehaviour
{
    public TextMeshProUGUI playArrow;
    public TextMeshProUGUI exitArrow;
    public TextMeshProUGUI versionText;

    // Start is called before the first frame update
    void Start()
    {
        playArrow.text = "";
        exitArrow.text = "";
        versionText.text = "v" + Application.version;
        FindObjectOfType<AudioManagement>().Play("title music");
        PlayerPrefs.SetInt("EnableKeyboard", 0);
    }

    public void LoadSetupScene()
    {
        FindObjectOfType<AudioManagement>().Stop("title music");
        SceneManager.LoadScene("SetupNightScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
