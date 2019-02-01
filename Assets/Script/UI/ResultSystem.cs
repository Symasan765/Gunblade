using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResultSystem : MonoBehaviour
{
    public GameObject[] uiObj;
    public GameObject[] rankObj;
    private int index;
    public AudioSource audioSource;
    private bool nextFlag = false;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine("ResultPointDisplay");
        index = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (!nextFlag) return;
        // 入力で遷移
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
            audioSource.PlayOneShot(audioSource.clip);
            yield return new WaitForSeconds(0.5f);

            uiObj[i].GetComponent<PointToPlaceNum>().StartDraw(567);
        }
        yield return new WaitForSeconds(2.0f);
        rankObj[0].GetComponent<PointToRank>().StartDraw(567);
        nextFlag = true;
    }
}
