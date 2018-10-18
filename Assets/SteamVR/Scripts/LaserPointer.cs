using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserPointer : MonoBehaviour {
    /// <summary>
    /// レーザー用変数
    /// </summary>
    private SteamVR_TrackedObject trackedObj;
    // コントローラと当たっているオブジェクト
    private GameObject collidingObject;
    // 手に持っているオブジェクト
    private GameObject objectInHand;

    /// <summary>
    /// テレポート用変数
    /// </summary>
    // [CameraRig]のTransformコンポーネント
    public Transform cameraRigTransform;
    // 的のプレハブへの参照
    public GameObject teleportReticlePrefab;
    // 的のインスタンス
    private GameObject reticle;
    // 的のTransformコンポーネント
    private Transform teleportReticleTransform;
    // プレイヤーの頭（カメラ）のTransformコンポーネント
    public Transform headTransform;
    // 的と床が重ならないようにOffsetを設定
    public Vector3 teleportReticleOffset;
    // テレポート可能なエリアを区別するためのマスク
    public LayerMask teleportMask;
    // テレポート先がテレポート可能かの判断用
    private bool shouldTeleport;

    private SteamVR_Controller.Device Controller
    {
        get { return SteamVR_Controller.Input((int)trackedObj.index); }
    }

    void Awake()
    {
        trackedObj = GetComponent<SteamVR_TrackedObject>();
    }


    // ==================================
    // レーザー用変数
    // ==================================
    // レーザーのプレハブへの参照
    public GameObject laserPrefab;
    // レーザーのインスタンス
    public GameObject laser;
    // レーザーのTransform情報
    private Transform laserTransform;
    // レーザーが当たる点のベクトル情報
    private Vector3 hitPoint;

    // レーザー表示関数
    private void Showlaser(RaycastHit hit)
    {
        // レーザーオブジェクトを表示する。
        laser.SetActive(true);
        // レーザーと当たっているオブジェクトの位置とコントローラの
        // 位置の中心点を求めて、レーザーオブジェクトの位置にする。
        laserTransform.position =
            Vector3.Lerp(trackedObj.transform.position, hit.point, .5f);
        // レーザーオブジェクトを当たっているオブジェクトに向かわせる
        laserTransform.LookAt(hit.point);
        // レーザーのZ軸の長さをコントローラから当たるオブジェクトまでの
        // 距離にする
        laserTransform.localScale = new Vector3(laserTransform.localScale.x,
    laserTransform.localScale.y, hit.distance);

    }
    //  プレイヤーの入力でレーザーを表示する
    private void Update () {
        // Touchpadを押されている間、レーザーを表示する
        if (Controller.GetPress(SteamVR_Controller.ButtonMask.Touchpad))
        {
            RaycastHit hit;
            // コントローラから光線を飛ばす
            // 100m以内にオブジェクトと当たったらレーザーを表示する
            if (Physics.Raycast(trackedObj.transform.position, transform.forward, out hit, 100, teleportMask))
            {
                hitPoint = hit.point;
                Showlaser(hit);
                // 的を表示する
                reticle.SetActive(true);
                // Offsetを当たっている位置に加える
                teleportReticleTransform.position = hitPoint + teleportReticleOffset;
                // テレポート可能にする。
                shouldTeleport = true;
            }


        }
        else // Touchpadが放されたら、レーザーを非表示にする
        {
            laser.SetActive(false);
            // 的を非表示にする
            reticle.SetActive(false);
        }
        if (Controller.GetPressUp(SteamVR_Controller.ButtonMask.Touchpad) &&
         shouldTeleport)
        {
            Teleport();
        }

    }

    // テレポート関数を追加する。
    private void Teleport()
    {
        // テレポート中に再テレポートできないようにする
        shouldTeleport = false;
        // 的を消す
        reticle.SetActive(false);
        // CameraRigの位置とプレイヤーの頭の位置の差を求める
        Vector3 difference = cameraRigTransform.position - headTransform.position;
        //高さの差を消す
        difference.y = 0;
        // テレポート先の位置に差を加える
        // （これがないと微妙に違う位置にテレポートすることがある）
        cameraRigTransform.position = hitPoint + difference;
    }


    // レーザーの初期化をStart関数に追加
    private void Start()
    {
        // レーザーのオブジェクトをプレハブから生成する
        laser = Instantiate(laserPrefab);

        // Transformのコンポーネントを最初から取得する
        // （アクセスしやすくするため）
        laserTransform = laser.transform;
        // 的のプレハブから的のオブジェクトを生成する
        reticle = Instantiate(teleportReticlePrefab);
        // 的のTransformコンポーネントを取得する
        teleportReticleTransform = reticle.transform;

    }
}
