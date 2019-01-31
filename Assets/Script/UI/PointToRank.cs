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
    // Start is called before the first frame update
    void Start()
    {
        spriteNum = sprites.Length;
        image = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void DrawRank(int point)
    {
        image.sprite = sprites[sprites.Length - 1];

        for(int i = 0; i < rankComparison.Length; ++i)
        {
            if (rankComparison[i] >= point)
            {
                image.sprite = sprites[i];
                break;
            }
        }
    }
}
