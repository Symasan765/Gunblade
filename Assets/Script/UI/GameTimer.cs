using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameTimer : MonoBehaviour
{
    public DrawNumber[] drawNumbers;
    public Image[] images;
    public FadeScript fadeScript;
    public int timeMax;
    public string nextScene;
    private float timeNow;
    private bool flg;

    // Start is called before the first frame update
    void Start()
    {
        timeNow = timeMax;
        flg = false;
    }

    // Update is called once per frame
    void Update()
    {

        if (fadeScript.SceneTrans())
        {
            SceneManager.LoadScene(nextScene);
        }

        if (flg) return;
        timeNow -= Time.deltaTime;

        int num = ((int)timeNow % 10);
        drawNumbers[1].Draw(num);
        num = (int)timeNow / 10;
        num = num % 10;
        drawNumbers[0].Draw(num);


        if(timeNow < 0)
        {
            flg = true;
            fadeScript.ChangeFlag(false);
        }

        
    }

    public void AddTime(int add)
    {
        timeNow += add;
    }
}
