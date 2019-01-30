using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BirdA : MonoBehaviour
{

    public int Score;
    public float MoveSpeed;
    public float PopTimer;
    public GameObject ExplosionEffect;
    public float DeathTimer = 5.0f;

    private bool Moving = true;
    

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if(PopTimer > 0)
        {
            PopTimer -= Time.deltaTime;
        }
        else
        {
            Moving = true;
        }
    }

    void Move()
    {
        if (!Moving) return;
        this.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
        DeathTimer -= Time.deltaTime;
        if(DeathTimer < 0)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        // 死
        if(collision.gameObject.CompareTag("Arrow"))
        {
            Instantiate(ExplosionEffect,this.transform.position, Quaternion.identity);
            Destroy(this.gameObject);
        }
    }
}
