using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GoldenFreddyManagement : MonoBehaviour
{

    private CameraManagement cm;
    //private int goldenFreddySpawnChance;
    public bool attemptToSpawnGoldenFreddy;
    private GameObject goldenFreddy;
    public bool goldenFreddyIsSpawned;
    public float timeToPutOnMaskForGoldenFreddy;
    private FreddyMaskManagement fmm;
    private GameObject goldenFreddyHeadJumpscare;
    private bool spawnGoldenFreddy;
    public bool goldenFreddyCamJumpscare;

    public GameObject goldenFreddyIdle;
    public GameObject goldenFreddyJump;

    // Start is called before the first frame update
    void Start()
    {
        cm = GameObject.Find("CameraManager").GetComponent<CameraManagement>();
        attemptToSpawnGoldenFreddy = true;
        goldenFreddy = GameObject.Find("golden freddy");
        DespawnGoldenFreddy();
        goldenFreddyIsSpawned = false;
        fmm = GameObject.Find("FreddyMaskManager").GetComponent<FreddyMaskManagement>();
        goldenFreddyHeadJumpscare = GameObject.Find("golden freddy head jumpscare");
        goldenFreddyHeadJumpscare.transform.localScale = new Vector3(0, 0, 0);
        goldenFreddyCamJumpscare = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (cm.camIsOpen && attemptToSpawnGoldenFreddy)
        {
            //goldenFreddySpawnChance = Random.Range(1, 3);
            attemptToSpawnGoldenFreddy = false;
        }

        if (goldenFreddyIsSpawned && cm.camIsOpen)
        {
            goldenFreddyIsSpawned = false;
            StartCoroutine(GoldenFreddyJumpscare());
        }
    }

    public void CheckIfGoldenFreddyIsSpawning()
    {
        goldenFreddyIsSpawned = false;
        int random = Random.Range(1, 23);
        if (random <= PlayerPrefs.GetInt("Golden Freddy Level") && spawnGoldenFreddy)
        {
            SpawnGoldenFreddy();
            spawnGoldenFreddy = false;
        }
        /*
        if (goldenFreddySpawnChance == 1)
        {
            SpawnGoldenFreddy();
        }*/
    }

    private void SpawnGoldenFreddy()
    {
        goldenFreddy.transform.localScale = new Vector3(1, 1.3906f, 1);
        goldenFreddyIsSpawned = true;
        goldenFreddyIdle.GetComponent<Animator>().Play("Freddy_Golden--CPU_Lurch");
    }

    public void DespawnGoldenFreddy()
    {
        goldenFreddy.transform.localScale = new Vector3(0, 0, 0);
        spawnGoldenFreddy = true;
        goldenFreddyIsSpawned = false;
    }

    public IEnumerator CheckIfMaskIsOnForGoldenFreddy()
    {
        if (PlayerPrefs.GetInt("Golden Freddy Level", 0) < 20)
        {
            for (float i = 0; i < 3.0; i += 0.1f)
            {
                if (i >= 2.9)
                {
                    StartCoroutine(GoldenFreddyJumpscare());
                }
                if (fmm.maskIsOn)
                {
                    DespawnGoldenFreddy();
                    attemptToSpawnGoldenFreddy = true;
                    break;
                }
                yield return new WaitForSeconds(0.1f);
            }
            /*
            yield return new WaitForSeconds(0.5f);
            timeToPutOnMaskForGoldenFreddy += 1;

            if (fmm.maskIsOn)
            {
                DespawnGoldenFreddy();
                attemptToSpawnGoldenFreddy = true;
                timeToPutOnMaskForGoldenFreddy = 0;
            }

            yield return new WaitForSeconds(0.5f);

            if (timeToPutOnMaskForGoldenFreddy < 2 && fmm.maskIsOn)
            {
                DespawnGoldenFreddy();
                attemptToSpawnGoldenFreddy = true;
            }

            timeToPutOnMaskForGoldenFreddy += 1;

            if (timeToPutOnMaskForGoldenFreddy == 2 && !fmm.maskIsOn && !attemptToSpawnGoldenFreddy && goldenFreddyIsSpawned)
            {
                StartCoroutine(GoldenFreddyJumpscare());
            }
            timeToPutOnMaskForGoldenFreddy = 0;*/
        }
        else if (PlayerPrefs.GetInt("Golden Freddy Level", 0) == 20)
        {
            yield return new WaitForSeconds(0.5f);
            timeToPutOnMaskForGoldenFreddy += 1;

            if (fmm.maskIsOn)
            {
                DespawnGoldenFreddy();
                attemptToSpawnGoldenFreddy = true;
                timeToPutOnMaskForGoldenFreddy = 0;
            }

            yield return new WaitForSeconds(0.5f);

            if (timeToPutOnMaskForGoldenFreddy < 2 && fmm.maskIsOn)
            {
                DespawnGoldenFreddy();
                attemptToSpawnGoldenFreddy = true;
            }

            timeToPutOnMaskForGoldenFreddy += 1;

            if (timeToPutOnMaskForGoldenFreddy == 2 && !fmm.maskIsOn && !attemptToSpawnGoldenFreddy && goldenFreddyIsSpawned)
            {
                StartCoroutine(GoldenFreddyJumpscare());
            }
            timeToPutOnMaskForGoldenFreddy = 0;
        }
        
    }

    IEnumerator GoldenFreddyJumpscare()
    {
        DespawnGoldenFreddy();
        FindObjectOfType<AudioManagement>().Play("golden freddy scream");
        goldenFreddyHeadJumpscare.transform.localScale = new Vector3(1724.263f, 1152.485f, 430.1922f);
        goldenFreddyJump.GetComponent<Animator>().Play("Freddy_Golden--Jumpscare_Tuned");
        /*
        while (true)
        {
            goldenFreddyHeadJumpscare.transform.localPosition += new Vector3(0, 200, 0);
            yield return new WaitForSeconds(0.0001f);

            if (goldenFreddyHeadJumpscare.transform.localPosition.y >= -350)
            {
                break;
            }
        }*/

        yield return new WaitForSeconds(1);
        SceneManager.LoadScene("GameOverScene");
    }
}
