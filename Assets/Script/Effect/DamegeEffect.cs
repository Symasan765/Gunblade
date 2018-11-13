using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DamegeEffect : MonoBehaviour
{

    Image img;

    private float damege_num;       // ダメージ合計量

    [SerializeField]
    private float damege_plus;      // 1回で受けるダメージ量

    [SerializeField]
    private float life_up;          // 回復量

	// Use this for initialization
	void Start ()
    {
        img = GetComponent<Image>();
        img.color = Color.clear;
	}
	
	// Update is called once per frame
	void Update ()
    {
        Player_Hit_Effect();
	}

    void Player_Hit_Effect()
    {
        // HPでエフェクトを調整するのか画面のRedの明るさで調整するか現時点では未定。
        if(img.color.r < 0.9f)
        {
            // ダメージを受けた時のエフェクト(現在はスペースキーで起動)←変更部分
            if (Input.GetKeyDown("space"))
            {
                // ダメージ量の加算
                damege_num += damege_plus;
                this.img.color = new Color(damege_num, 0f, 0.11764f, 0.7f);
                Debug.Log(img.color.r);
            }
            else
            {
                this.img.color = Color.Lerp(this.img.color, Color.clear, 0.008f);
                if(damege_num > 0)
                {
                    // 回復する時間
                    damege_num -= life_up;
                }
            }
        }
        else
        {
            // 死亡となりシーンが切り替わるまでずっと赤色になる。
            this.img.color = new Color(damege_num, 0f, 0.11764f, 0.7f);
        }
    }
}
