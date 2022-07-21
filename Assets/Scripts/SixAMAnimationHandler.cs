using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class SixAMAnimationHandler : MonoBehaviour
{
    public TextMeshProUGUI six;
    public TextMeshProUGUI zero1;
    public TextMeshProUGUI zero2;
    private int randomNum;
    private bool canChangeScene;

    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Animate6());
        StartCoroutine(AnimateZero1());
        StartCoroutine(AnimateZero2());
        FindObjectOfType<AudioManagement>().Stop("office ambience");
        FindObjectOfType<AudioManagement>().Play("6am sound");
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
                SceneManager.LoadScene("SetupNightScene");
            }
        }
    }

    IEnumerator AllowSceneChange()
    {
        yield return new WaitForSeconds(10);
        canChangeScene = true;
    }

    IEnumerator Animate6()
    {
        for (int i = 0; i < 50; i++)
        {
            randomNum = Random.Range(0, 10);
            six.text = randomNum.ToString();
            yield return new WaitForSeconds(0.01f);
        }
        six.text = "6";
    }

    IEnumerator AnimateZero1()
    {
        for (int i = 0; i < 100; i++)
        {
            randomNum = Random.Range(0, 10);
            zero1.text = randomNum.ToString();
            yield return new WaitForSeconds(0.01f);
        }
        zero1.text = "0";
    }

    IEnumerator AnimateZero2()
    {
        for (int i = 0; i < 150; i++)
        {
            randomNum = Random.Range(0, 10);
            zero2.text = randomNum.ToString();
            yield return new WaitForSeconds(0.01f);
        }
        zero2.text = "0";
    }
}
