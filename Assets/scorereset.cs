using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class scorereset : MonoBehaviour
{

    public ScoreManager ScoreManagerScript;
    // Start is called before the first frame update
    void Start()
    {
        ScoreManagerScript = this.GetComponent<ScoreManager>();
        ScoreManagerScript.ScoreReset();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
