using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arrow : MonoBehaviour
{

    private bool Grab = true;
    Vector3 beforePosition;

    // Start is called before the first frame update
    void Start()
    {
        beforePosition = new Vector3(-9999, -9999, -9999);
    }

    // Update is called once per frame
    void Update()
    {
        if(!Grab)
        {
            if(beforePosition.z != -9999)
            {
                Vector3 diff = this.transform.position - beforePosition;
                this.transform.LookAt(this.transform.position + diff);
            }
            beforePosition = this.transform.position;
        }
    }

    public void Shot(float Power)
    {
        this.GetComponent<Rigidbody>().AddForce(this.transform.forward * Power, ForceMode.Impulse);
        Destroy( this.gameObject, 5.0f);
        Grab = false;
    }
}
