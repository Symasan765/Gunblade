using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointToPlaceNum : MonoBehaviour
{
    public DrawNumber[] drawNumbers;
    private int placeNum;
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
            place = point % (10 ^ placeNum-i);
            drawNumbers[i].Draw(place);
        }
    }
}
