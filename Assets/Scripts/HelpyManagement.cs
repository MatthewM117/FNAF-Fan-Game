using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HelpyManagement : MonoBehaviour
{
    private CameraManagement cm;
    //private int helpySpawnChance;
    public bool attemptToSpawnHelpy;
    private GameObject helpy;
    public bool helpyIsSpawned;
    public int timeToClickHelpy;
    public bool helpyIsClicked;
    private bool spawnHelpy;
    public bool allowClick;

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        attemptToSpawnHelpy = true;
        helpy = GameObject.Find("helpy");
        DespawnHelpy();
        helpyIsSpawned = false;
        helpyIsClicked = false;
        allowClick = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (cm.camIsOpen && attemptToSpawnHelpy)
        {
            //helpySpawnChance = Random.Range(1, 3);
            attemptToSpawnHelpy = false;
        }

        if (helpyIsSpawned)
        {
            StartCoroutine(CheckIfHelpyIsClicked());
            helpyIsSpawned = false;
        }

    }

    public void CheckIfhelpyIsSpawning()
    {
        helpyIsClicked = false;
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Helpy Level") && spawnHelpy)
        {
            SpawnHelpy();
            spawnHelpy = false;
        }
        /*
        if (helpySpawnChance == 1)
        {
            SpawnHelpy();
        }*/
    }

    private void SpawnHelpy()
    {
        helpy.transform.localScale = new Vector3(1, 1, 1);
        helpyIsSpawned = true;
    }

    public void DespawnHelpy()
    {
        timeToClickHelpy = 0;
        helpy.transform.localScale = new Vector3(0, 0, 0);
        spawnHelpy = true;
    }

    IEnumerator CheckIfHelpyIsClicked()
    {
        timeToClickHelpy = 0;
        for(int i = 0; i < 7; i++)
        {
            if (helpyIsClicked)
            {
                break;
            }

            yield return new WaitForSeconds(1);
            timeToClickHelpy += 1;

            if (helpyIsClicked)
            {
                break;
            }
            if (timeToClickHelpy == 6)
            {
                HelpyDistraction();
            }
        }
        
    }

    private void HelpyDistraction()
    {
        allowClick = false;
        helpy.transform.localPosition = new Vector3(-3.5f, 1, 1f);
        helpy.transform.localScale = new Vector3(15, 9, 0);
        FindObjectOfType<AudioManagement>().Play("pfff");
    }
}
