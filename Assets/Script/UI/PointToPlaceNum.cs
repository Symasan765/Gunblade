using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPlaceNum : MonoBehaviour
{
    public DrawNumber[] drawNumbers;
    private int placeNum;
    private int nowPlace;
    private int maxPlace;
    private int m_point;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        placeNum = drawNumbers.Length;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void DrawPoint(int point)
    {
        int place;

        for (int i = 0; i < placeNum; ++i)
        {
            place =( point % (10 ^ placeNum-i)) / (10 ^( placeNum -(i+1)));
            drawNumbers[i].Draw(place);
        }
    }

    public int DrawPointPlace(int point)
    {
        int num , length;
        
        num = (point % 10);
        length = num.ToString().Length;

        return num;
    }

    public void SetColor(Color color)
    {
        for(int i = 0; i < placeNum; ++i)
        {
            drawNumbers[i].image.color = color;
        }
    }

    public void StartDraw(int point)
    {
        nowPlace = 0;
        maxPlace = point.ToString().Length;
        m_point = point;
        StartCoroutine("DrawPlace");
    }

    private IEnumerator DrawPlace()
    {
        for (int i = 0; i < maxPlace; ++i)
        {
            int num = DrawPointPlace(m_point);
            drawNumbers[placeNum - (i + 1)].Draw(num);
            m_point /= 10;
            audioSource.PlayOneShot(audioSource.clip);
            if (i == maxPlace-1) yield break;
            yield return new WaitForSeconds(0.6f);
        }
    }
}
