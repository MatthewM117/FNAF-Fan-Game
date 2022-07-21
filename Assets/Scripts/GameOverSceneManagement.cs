using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOverSceneManagement : MonoBehaviour
{
    private bool canChangeScene;

    // Start is called before the first frame update
    void Start()
    {
        FindObjectOfType<AudioManagement>().Play("game over static");
        canChangeScene = false;
        StartCoroutine(AllowSceneChange());
    }

    // Update is called once per frame
    void Update()
    {
        if (canChangeScene)
        {
            if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Return))
            {
                FindObjectOfType<AudioManagement>().Stop("game over static");
                SceneManager.LoadScene("SetupNightScene");
            }
        }
    }

    IEnumerator AllowSceneChange()
    {
        yield return new WaitForSeconds(2);
        canChangeScene = true;
    }
}
