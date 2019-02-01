using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdA : EnemyBase
{
    // Start is called before the first frame update
    public override void Start()
    {
        base.Start();
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
                ScoreManager.GetComponent<ScoreManager>().AddScore(Score);

                var go = Instantiate(HitEffect, point.point, Quaternion.identity);
                Destroy(go.gameObject, 5.0f);
                break;
            }
            HitLife -= 1;
            Audio.PlayOneShot(Clip);

        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.CompareTag("Arrow"))
        {
            Audio.PlayOneShot(Clip);

            ScoreManager.GetComponent<ScoreManager>().AddScore(Score);

            var go = Instantiate(HitEffect, other.ClosestPointOnBounds(this.transform.position), Quaternion.identity);
            Destroy(go.gameObject, 5.0f);

            HitLife -= 1;

        }
    }
}
