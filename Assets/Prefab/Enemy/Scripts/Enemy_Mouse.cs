using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Mouse : EnemyBase
{
    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 30.0f);
    }

    // Update is called once per frame
    public override void Update()
    {
        base.Update();
    }

    void OnCollisionEnter(Collision collision)
    {
        //base.OnCollisionEnter(collision);
        // 死
        if (collision.gameObject.CompareTag("Arrow"))
        {
            foreach (ContactPoint point in collision.contacts)
            {
                var go = Instantiate(HitEffect, point.point, Quaternion.identity);
                Destroy(go.gameObject, 5.0f);
                break;
            }
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
