using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzlePos;       // 銃口位置
    [SerializeField]
    private Transform _targetPos;       // 照準位置
    [SerializeField]
    private GameObject _bullet;         // 弾丸オブジェクト：インスタンスする用
    [SerializeField]
    private float _reloadTime = 10.2f;  // 弾丸リロード時間
    private float _reloadCount = 0f;    // リロード時間カウント用

    void Update()
    {
        // リロードが済んでいれば弾丸発射
        Shot();

    }

    void Shot()
    {
        // リロード時間完了＆Fire1コマンド実行時、弾丸インスタンス化＆向き補正
        if (Count(false) && Input.GetAxis("Fire1") > 0)
        {
            // 射撃処理
            Vector3 dir = _targetPos.position - _muzzlePos.position;
            dir.Normalize();    // 射撃方向単位ベクトル
            float angle = Mathf.Atan2(dir.x, dir.y);

            // 弾の生成
            var bullet = Instantiate(_bullet, _muzzlePos.position, Quaternion.Euler( 0, 0, -1*Mathf.Rad2Deg * angle ));
           
            Count(true);    // リロード時間開始
        }
    }

    bool Count(bool reset)
    {
        // リロード時間の管理
        if (reset) {_reloadCount = 0f; }
        _reloadCount += Time.deltaTime;
        if (_reloadCount <= _reloadTime) return false;
        return true;
    }

}
