using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ResultSystem : MonoBehaviour
{
    public GameObject[] uiObj;
    public GameObject[] rankObj;
    private int index;
    private bool nextFlag = false;
    private ViveCtrl inputMng;
    public FadeScript fadeScript;
    public AudioSource audioSource;
    public string nextScene;
    private int score;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ResultPointDisplay");
        index = 0;
        inputMng = GameObject.FindObjectOfType<ViveCtrl>();
        score = GameObject.FindGameObjectWithTag("ScoreSystem").GetComponent<ScoreManager>().GetScore();
    }

    // Update is called once per frame
    void Update()
    {

        if (fadeScript.SceneTrans())
        {
            SceneManager.LoadScene(nextScene);
        }
        if (!nextFlag) return;
        // 入力で遷移
        
        if (inputMng.Trigger(ViveCtrl.ViveDeviceType.RightHand, ViveCtrl.ViveKey.Trigger))
        {
            fadeScript.ChangeFlag(false);
            audioSource.PlayOneShot(audioSource.clip);
            nextFlag = false;
        }
        /*
        if (Input.GetKeyDown(KeyCode.Space))
        {
            fadeScript.ChangeFlag(false);
            audioSource.PlayOneShot(audioSource.clip);
            nextFlag = false;
        }
        */


    }

    private IEnumerator ResultPointDisplay()
    {
        for (int i = 0; i < uiObj.Length; ++i)
        {
            Vector4 color; ;
            color = uiObj[i].GetComponent<Image>().color;

            while (color.w < 1.0f)
            {
                color.w += 0.01f;
                uiObj[i].GetComponent<Image>().color = color;
                uiObj[i].GetComponent<PointToPlaceNum>().SetColor(color);
                yield return null;
            }
            yield return new WaitForSeconds(0.5f);

            uiObj[i].GetComponent<PointToPlaceNum>().StartDraw(score);
        }
        yield return new WaitForSeconds(2.0f);
        rankObj[0].GetComponent<PointToRank>().StartDraw(score);

        yield return new WaitForSeconds(2.0f);

        nextFlag = true;
    }
}
