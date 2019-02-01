using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    // スコア
    public int Score = 500;
    // スコアの扱いをタイム加算にする
    public bool ScoreIsTime = false;
    public float MoveSpeed = 1;
    // 死ぬまでの必要ヒット数
    public int HitLife = 1;
    // 死亡時のエフェクト
    public GameObject ExplosionEffect;
    public GameObject HitEffect;


    // Start is called before the first frame update
    void Start()
    {
        Destroy(this.gameObject, 30.0f);
    }

    // Update is called once per frame
    public virtual void Update()
    {
        this.transform.position += this.transform.forward * MoveSpeed * Time.deltaTime;
        if(HitLife <= 0)
        {
            Death();
        }
    }

    public void Death()
    {
        var go = Instantiate(ExplosionEffect, this.transform.position, Quaternion.identity);
        Destroy(go.gameObject, 5.0f);
        Destroy(this.gameObject);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 死
        if (collision.gameObject.CompareTag("Arrow"))
        {
            var go = Instantiate(HitEffect, this.transform.position, Quaternion.identity);
            Destroy(go.gameObject, 5.0f);
            HitLife -= 1;
        }
    }
}
