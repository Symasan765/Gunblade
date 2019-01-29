using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private bool Grab;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Shot(float Power)
    {
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * Power, ForceMode.Impulse);
        Destroy(this.gameObject, 3.0f);
    }
}
