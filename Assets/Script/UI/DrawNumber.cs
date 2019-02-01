using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawNumber : MonoBehaviour
{
    public Image image;
    public NumberSprite numberSprite;
    // Start is called before the first frame update
    void Start()
    {
        image = GetComponent<Image>();   
    }
    

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Draw(int num)
    {
        if (num < 0 || num > 9) return;
        image.sprite = numberSprite.numbers[num];
    }
}
