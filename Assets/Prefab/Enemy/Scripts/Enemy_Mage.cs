using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mage : EnemyBase
{

    float SpawnHeight;
    float FloatingHeight = 0;
    float FloatVector = 1;
    float FloatMaxHeight = 1;

    // Start is called before the first frame update
    void Start()
    {
        SpawnHeight = this.transform.position.y;
        Destroy(this.gameObject, 30.0f);

    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
        this.transform.position = new Vector3(this.transform.position.x, SpawnHeight + FloatingHeight, this.transform.position.z);

        FloatingHeight += Time.deltaTime * FloatVector;
        if(FloatingHeight > FloatMaxHeight || FloatingHeight < -FloatMaxHeight)
        {
            FloatVector *= -1;
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        //base.OnCollisionEnter(collision);
        // 死
        if (collision.gameObject.CompareTag("Arrow"))
        {
            var go = Instantiate(HitEffect, this.transform.position, Quaternion.identity);
            Destroy(go.gameObject, 5.0f);
            HitLife -= 1;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            var go = Instantiate(HitEffect, other.ClosestPointOnBounds(this.transform.position), Quaternion.identity);
            Destroy(go.gameObject, 5.0f);
            HitLife -= 1;
        }
    }
}
