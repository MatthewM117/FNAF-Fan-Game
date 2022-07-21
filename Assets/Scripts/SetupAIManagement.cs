using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class SetupAIManagement : MonoBehaviour
{
    public Image freddy;
    public Image springtrap;
    public Image toyBonnie;
    public Image toyChica;
    public Image omc;
    public Image helpy;
    public Image goldenFreddy;
    public Image bonnet;
    public Image rockstarFreddy;
    public Image phantomFreddy;
    public Image afton;

    public TextMeshProUGUI freddyLevel;
    public TextMeshProUGUI springtrapLevel;
    public TextMeshProUGUI toyBonnieLevel;
    public TextMeshProUGUI toyChicaLevel;
    public TextMeshProUGUI omcLevel;
    public TextMeshProUGUI helpyLevel;
    public TextMeshProUGUI goldenFreddyLevel;
    public TextMeshProUGUI bonnetLevel;
    public TextMeshProUGUI rockstarFreddyLevel;
    public TextMeshProUGUI phantomFreddyLevel;
    public TextMeshProUGUI aftonLevel;

    private bool toggleText;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManagement>().Play("toreador theme");
        FindObjectOfType<AudioManagement>().Stop("office ambience");
        DisplayLevels();
        toggleText = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            SceneManager.LoadScene("TitleScene");
        }
        
    }

    public void toggleFreddyText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            freddy.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            freddy.rectTransform.localScale = new Vector3(0, 0, 0);
        }
        
    }

    public void toggleSpringtrapText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            springtrap.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            springtrap.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleToyBonnieText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            toyBonnie.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            toyBonnie.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleToyChicaText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            toyChica.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            toyChica.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleOMCText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            omc.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            omc.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleHelpyText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            helpy.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            helpy.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleGoldenFreddyText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            goldenFreddy.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            goldenFreddy.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleBonnetText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            bonnet.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            bonnet.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleRockstarFreddyText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            rockstarFreddy.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            rockstarFreddy.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void togglePhantomFreddyText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            phantomFreddy.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            phantomFreddy.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    public void toggleAftonText()
    {
        toggleText = !toggleText;
        if (toggleText)
        {
            afton.rectTransform.localScale = new Vector3(0.37515f, 0.5336103f, 1);
        }
        else
        {
            afton.rectTransform.localScale = new Vector3(0, 0, 0);
        }

    }

    private void DisplayLevels()
    {
        freddyLevel.text = PlayerPrefs.GetInt("Freddy Level", 0).ToString();
        springtrapLevel.text = PlayerPrefs.GetInt("Springtrap Level", 0).ToString();
        toyBonnieLevel.text = PlayerPrefs.GetInt("Toy Bonnie Level", 0).ToString();
        toyChicaLevel.text = PlayerPrefs.GetInt("Toy Chica Level", 0).ToString();
        omcLevel.text = PlayerPrefs.GetInt("OMC Level", 0).ToString();
        helpyLevel.text = PlayerPrefs.GetInt("Helpy Level", 0).ToString();
        goldenFreddyLevel.text = PlayerPrefs.GetInt("Golden Freddy Level", 0).ToString();
        bonnetLevel.text = PlayerPrefs.GetInt("Bonnet Level", 0).ToString();
        rockstarFreddyLevel.text = PlayerPrefs.GetInt("Rockstar Freddy Level", 0).ToString();
        phantomFreddyLevel.text = PlayerPrefs.GetInt("Phantom Freddy Level", 0).ToString();
        aftonLevel.text = PlayerPrefs.GetInt("Afton Level", 0).ToString();
    }

    // actually adds all 5
    public void SetAll5()
    {
        if (PlayerPrefs.GetInt("Freddy Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Freddy Level", PlayerPrefs.GetInt("Freddy Level") + 5);
        }
        if (PlayerPrefs.GetInt("Springtrap Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Springtrap Level", PlayerPrefs.GetInt("Springtrap Level") + 5);
        }
        if (PlayerPrefs.GetInt("Toy Bonnie Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Toy Bonnie Level", PlayerPrefs.GetInt("Toy Bonnie Level") + 5);
        }
        if (PlayerPrefs.GetInt("Toy Chica Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Toy Chica Level", PlayerPrefs.GetInt("Toy Chica Level") + 5);
        }
        if (PlayerPrefs.GetInt("OMC Level", 0) < 16)
        {
            PlayerPrefs.SetInt("OMC Level", PlayerPrefs.GetInt("OMC Level") + 5);
        }
        if (PlayerPrefs.GetInt("Helpy Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Helpy Level", PlayerPrefs.GetInt("Helpy Level") + 5);
        }
        if (PlayerPrefs.GetInt("Golden Freddy Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Golden Freddy Level", PlayerPrefs.GetInt("Golden Freddy Level") + 5);
        }
        if (PlayerPrefs.GetInt("Bonnet Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Bonnet Level", PlayerPrefs.GetInt("Bonnet Level") + 5);
        }
        if (PlayerPrefs.GetInt("Rockstar Freddy Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Rockstar Freddy Level", PlayerPrefs.GetInt("Rockstar Freddy Level") + 5);
        }
        if (PlayerPrefs.GetInt("Phantom Freddy Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Phantom Freddy Level", PlayerPrefs.GetInt("Phantom Freddy Level") + 5);
        }
        if (PlayerPrefs.GetInt("Afton Level", 0) < 16)
        {
            PlayerPrefs.SetInt("Afton Level", PlayerPrefs.GetInt("Afton Level") + 5);
        }
        PlayerPrefs.Save();
        DisplayLevels();
    }

    public void AddAll1()
    {
        if (PlayerPrefs.GetInt("Freddy Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Freddy Level", PlayerPrefs.GetInt("Freddy Level") + 1);
        }
        if (PlayerPrefs.GetInt("Springtrap Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Springtrap Level", PlayerPrefs.GetInt("Springtrap Level") + 1);
        }
        if (PlayerPrefs.GetInt("Toy Bonnie Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Toy Bonnie Level", PlayerPrefs.GetInt("Toy Bonnie Level") + 1);
        }
        if (PlayerPrefs.GetInt("Toy Chica Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Toy Chica Level", PlayerPrefs.GetInt("Toy Chica Level") + 1);
        }
        if (PlayerPrefs.GetInt("OMC Level", 0) < 20)
        {
            PlayerPrefs.SetInt("OMC Level", PlayerPrefs.GetInt("OMC Level") + 1);
        }
        if (PlayerPrefs.GetInt("Helpy Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Helpy Level", PlayerPrefs.GetInt("Helpy Level") + 1);
        }
        if (PlayerPrefs.GetInt("Golden Freddy Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Golden Freddy Level", PlayerPrefs.GetInt("Golden Freddy Level") + 1);
        }
        if (PlayerPrefs.GetInt("Bonnet Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Bonnet Level", PlayerPrefs.GetInt("Bonnet Level") + 1);
        }
        if (PlayerPrefs.GetInt("Rockstar Freddy Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Rockstar Freddy Level", PlayerPrefs.GetInt("Rockstar Freddy Level") + 1);
        }
        if (PlayerPrefs.GetInt("Phantom Freddy Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Phantom Freddy Level", PlayerPrefs.GetInt("Phantom Freddy Level") + 1);
        }
        if (PlayerPrefs.GetInt("Afton Level", 0) < 20)
        {
            PlayerPrefs.SetInt("Afton Level", PlayerPrefs.GetInt("Afton Level") + 1);
        }
        PlayerPrefs.Save();
        DisplayLevels();
    }

    public void SetAll0()
    {
        PlayerPrefs.SetInt("Freddy Level", 0);
        PlayerPrefs.SetInt("Springtrap Level", 0);
        PlayerPrefs.SetInt("Toy Bonnie Level", 0);
        PlayerPrefs.SetInt("Toy Chica Level", 0);
        PlayerPrefs.SetInt("OMC Level", 0);
        PlayerPrefs.SetInt("Helpy Level", 0);
        PlayerPrefs.SetInt("Golden Freddy Level", 0);
        PlayerPrefs.SetInt("Bonnet Level", 0);
        PlayerPrefs.SetInt("Rockstar Freddy Level", 0);
        PlayerPrefs.SetInt("Phantom Freddy Level", 0);
        PlayerPrefs.SetInt("Afton Level", 0);
        PlayerPrefs.Save();
        DisplayLevels();
    }

    public void SetAll20()
    {
        PlayerPrefs.SetInt("Freddy Level", 20);
        PlayerPrefs.SetInt("Springtrap Level", 20);
        PlayerPrefs.SetInt("Toy Bonnie Level", 20);
        PlayerPrefs.SetInt("Toy Chica Level", 20);
        PlayerPrefs.SetInt("OMC Level", 20);
        PlayerPrefs.SetInt("Helpy Level", 20);
        PlayerPrefs.SetInt("Golden Freddy Level", 20);
        PlayerPrefs.SetInt("Bonnet Level", 20);
        PlayerPrefs.SetInt("Rockstar Freddy Level", 20);
        PlayerPrefs.SetInt("Phantom Freddy Level", 20);
        PlayerPrefs.SetInt("Afton Level", 20);
        PlayerPrefs.Save();
        DisplayLevels();
    }

    public void IncreaseFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Freddy Level") < 20)
        {
            PlayerPrefs.SetInt("Freddy Level", PlayerPrefs.GetInt("Freddy Level") + 1);
            PlayerPrefs.Save();
            freddyLevel.text = PlayerPrefs.GetInt("Freddy Level").ToString();
        }
    }

    public void DecreaseFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Freddy Level") > 0)
        {
            PlayerPrefs.SetInt("Freddy Level", PlayerPrefs.GetInt("Freddy Level") - 1);
            PlayerPrefs.Save();
            freddyLevel.text = PlayerPrefs.GetInt("Freddy Level").ToString();
        }
    }

    public void IncreaseSpringtrapLevel()
    {
        if (PlayerPrefs.GetInt("Springtrap Level") < 20)
        {
            PlayerPrefs.SetInt("Springtrap Level", PlayerPrefs.GetInt("Springtrap Level") + 1);
            PlayerPrefs.Save();
            springtrapLevel.text = PlayerPrefs.GetInt("Springtrap Level").ToString();
        }
    }

    public void DecreaseSpringtrapLevel()
    {
        if (PlayerPrefs.GetInt("Springtrap Level") > 0)
        {
            PlayerPrefs.SetInt("Springtrap Level", PlayerPrefs.GetInt("Springtrap Level") - 1);
            PlayerPrefs.Save();
            springtrapLevel.text = PlayerPrefs.GetInt("Springtrap Level").ToString();
        }
    }

    public void IncreaseToyBonnieLevel()
    {
        if (PlayerPrefs.GetInt("Toy Bonnie Level") < 20)
        {
            PlayerPrefs.SetInt("Toy Bonnie Level", PlayerPrefs.GetInt("Toy Bonnie Level") + 1);
            PlayerPrefs.Save();
            toyBonnieLevel.text = PlayerPrefs.GetInt("Toy Bonnie Level").ToString();
        }
    }

    public void DecreaseToyBonnieLevel()
    {
        if (PlayerPrefs.GetInt("Toy Bonnie Level") > 0)
        {
            PlayerPrefs.SetInt("Toy Bonnie Level", PlayerPrefs.GetInt("Toy Bonnie Level") - 1);
            PlayerPrefs.Save();
            toyBonnieLevel.text = PlayerPrefs.GetInt("Toy Bonnie Level").ToString();
        }
    }

    public void IncreaseToyChicaLevel()
    {
        if (PlayerPrefs.GetInt("Toy Chica Level") < 20)
        {
            PlayerPrefs.SetInt("Toy Chica Level", PlayerPrefs.GetInt("Toy Chica Level") + 1);
            PlayerPrefs.Save();
            toyChicaLevel.text = PlayerPrefs.GetInt("Toy Chica Level").ToString();
        }
    }

    public void DecreaseToyChicaLevel()
    {
        if (PlayerPrefs.GetInt("Toy Chica Level") > 0)
        {
            PlayerPrefs.SetInt("Toy Chica Level", PlayerPrefs.GetInt("Toy Chica Level") - 1);
            PlayerPrefs.Save();
            toyChicaLevel.text = PlayerPrefs.GetInt("Toy Chica Level").ToString();
        }
    }

    public void IncreaseOMCLevel()
    {
        if (PlayerPrefs.GetInt("OMC Level") < 20)
        {
            PlayerPrefs.SetInt("OMC Level", PlayerPrefs.GetInt("OMC Level") + 1);
            PlayerPrefs.Save();
            omcLevel.text = PlayerPrefs.GetInt("OMC Level").ToString();
        }
    }

    public void DecreaseOMCLevel()
    {
        if (PlayerPrefs.GetInt("OMC Level") > 0)
        {
            PlayerPrefs.SetInt("OMC Level", PlayerPrefs.GetInt("OMC Level") - 1);
            PlayerPrefs.Save();
            omcLevel.text = PlayerPrefs.GetInt("OMC Level").ToString();
        }
    }

    public void IncreaseHelpyLevel()
    {
        if (PlayerPrefs.GetInt("Helpy Level") < 20)
        {
            PlayerPrefs.SetInt("Helpy Level", PlayerPrefs.GetInt("Helpy Level") + 1);
            PlayerPrefs.Save();
            helpyLevel.text = PlayerPrefs.GetInt("Helpy Level").ToString();
        }
    }

    public void DecreaseHelpyLevel()
    {
        if (PlayerPrefs.GetInt("Helpy Level") > 0)
        {
            PlayerPrefs.SetInt("Helpy Level", PlayerPrefs.GetInt("Helpy Level") - 1);
            PlayerPrefs.Save();
            helpyLevel.text = PlayerPrefs.GetInt("Helpy Level").ToString();
        }
    }

    public void IncreaseGoldenFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Golden Freddy Level") < 20)
        {
            PlayerPrefs.SetInt("Golden Freddy Level", PlayerPrefs.GetInt("Golden Freddy Level") + 1);
            PlayerPrefs.Save();
            goldenFreddyLevel.text = PlayerPrefs.GetInt("Golden Freddy Level").ToString();
        }
    }

    public void DecreaseGoldenFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Golden Freddy Level") > 0)
        {
            PlayerPrefs.SetInt("Golden Freddy Level", PlayerPrefs.GetInt("Golden Freddy Level") - 1);
            PlayerPrefs.Save();
            goldenFreddyLevel.text = PlayerPrefs.GetInt("Golden Freddy Level").ToString();
        }
    }

    public void IncreaseBonnetLevel()
    {
        if (PlayerPrefs.GetInt("Bonnet Level") < 20)
        {
            PlayerPrefs.SetInt("Bonnet Level", PlayerPrefs.GetInt("Bonnet Level") + 1);
            PlayerPrefs.Save();
            bonnetLevel.text = PlayerPrefs.GetInt("Bonnet Level").ToString();
        }
    }

    public void DecreaseBonnetLevel()
    {
        if (PlayerPrefs.GetInt("Bonnet Level") > 0)
        {
            PlayerPrefs.SetInt("Bonnet Level", PlayerPrefs.GetInt("Bonnet Level") - 1);
            PlayerPrefs.Save();
            bonnetLevel.text = PlayerPrefs.GetInt("Bonnet Level").ToString();
        }
    }

    public void IncreaseRockstarFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Rockstar Freddy Level") < 20)
        {
            PlayerPrefs.SetInt("Rockstar Freddy Level", PlayerPrefs.GetInt("Rockstar Freddy Level") + 1);
            PlayerPrefs.Save();
            rockstarFreddyLevel.text = PlayerPrefs.GetInt("Rockstar Freddy Level").ToString();
        }
    }

    public void DecreaseRockstarFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Rockstar Freddy Level") > 0)
        {
            PlayerPrefs.SetInt("Rockstar Freddy Level", PlayerPrefs.GetInt("Rockstar Freddy Level") - 1);
            PlayerPrefs.Save();
            rockstarFreddyLevel.text = PlayerPrefs.GetInt("Rockstar Freddy Level").ToString();
        }
    }

    public void IncreasePhantomFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Phantom Freddy Level") < 20)
        {
            PlayerPrefs.SetInt("Phantom Freddy Level", PlayerPrefs.GetInt("Phantom Freddy Level") + 1);
            PlayerPrefs.Save();
            phantomFreddyLevel.text = PlayerPrefs.GetInt("Phantom Freddy Level").ToString();
        }
    }

    public void DecreasePhantomFreddyLevel()
    {
        if (PlayerPrefs.GetInt("Phantom Freddy Level") > 0)
        {
            PlayerPrefs.SetInt("Phantom Freddy Level", PlayerPrefs.GetInt("Phantom Freddy Level") - 1);
            PlayerPrefs.Save();
            phantomFreddyLevel.text = PlayerPrefs.GetInt("Phantom Freddy Level").ToString();
        }
    }

    public void IncreaseAftonLevel()
    {
        if (PlayerPrefs.GetInt("Afton Level") < 20)
        {
            PlayerPrefs.SetInt("Afton Level", PlayerPrefs.GetInt("Afton Level") + 1);
            PlayerPrefs.Save();
            aftonLevel.text = PlayerPrefs.GetInt("Afton Level").ToString();
        }
    }

    public void DecreaseAftonLevel()
    {
        if (PlayerPrefs.GetInt("Afton Level") > 0)
        {
            PlayerPrefs.SetInt("Afton Level", PlayerPrefs.GetInt("Afton Level") - 1);
            PlayerPrefs.Save();
            aftonLevel.text = PlayerPrefs.GetInt("Afton Level").ToString();
        }
    }

    public void StartGame()
    {
        FindObjectOfType<AudioManagement>().Stop("toreador theme");
        SceneManager.LoadScene("SampleScene");
    }

    public void ShowControlScene()
    {
        SceneManager.LoadScene("ControlsScene");
    }
}
