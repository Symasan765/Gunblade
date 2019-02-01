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

    public GameObject ScoreManager;
    public AudioSource Audio;
    public AudioClip Clip;


    // Start is called before the first frame update
    public virtual void Start()
    {
        ScoreManager = GameObject.FindGameObjectWithTag("ScoreSystem");
        Audio = ScoreManager.GetComponent<AudioSource>();
        Destroy( this.gameObject, 30.0f );
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
        Destroy(this.gameObject,0.1f);
    }

    void OnCollisionEnter(Collision collision)
    {
        // 死
        if (collision.gameObject.CompareTag("Arrow"))
        {
            ScoreManager.GetComponent<ScoreManager>().AddScore(Score);

            var go = Instantiate(HitEffect, this.transform.position, Quaternion.identity);
            Destroy(go.gameObject, 5.0f);
            HitLife -= 1;

            Audio.PlayOneShot(Clip);
        }
    }
}
