using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointToRank : MonoBehaviour
{
    public Sprite[] sprites;
    private int spriteNum;
    public int[] rankComparison;
    private Image image;
    private RectTransform rectTransform;
    public Color[] rankColor;
    public AudioSource audioSource;
    // Start is called before the first frame update
    void Start()
    {
        spriteNum = sprites.Length;
        image = GetComponent<Image>();
        rectTransform = GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DrawRank(int point)
    {
        image.sprite = sprites[sprites.Length - 1];
        image.color = rankColor[sprites.Length - 1];
        for (int i = 0; i < rankComparison.Length; ++i)
        {
            if (rankComparison[i] <= point)
            {
                image.sprite = sprites[i];
                image.color = rankColor[i];
                break;
            }
        }
    }

    public void StartDraw(int point)
    {
        DrawRank(point);
        StartCoroutine("DrawRankAnimation");
    }

    private IEnumerator DrawRankAnimation()
    {
        while (rectTransform.localScale.x < 1.0f)
        {
            rectTransform.localScale += new Vector3(0.04f, 0.04f, 0);
            yield return null;
        }
        audioSource.PlayOneShot(audioSource.clip);
    }
}
