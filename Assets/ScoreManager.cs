﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    static public int TotalScore = 0; 

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ScoreReset()
    {
        TotalScore = 0;
    }

    public void AddScore(int add)
    {
        TotalScore += add;
    }

    public int GetScore()
    {
        return TotalScore;
    }
}