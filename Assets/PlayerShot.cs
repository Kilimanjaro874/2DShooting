using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzlePos;
    [SerializeField]
    private Transform _targetPos;
    [SerializeField]
    private GameObject _bullet;
    [SerializeField]
    private float _reloadTime = 10.2f;
    private float _reloadCount = 0f;
    [SerializeField]
    private float _bulletSpeed = 100.0f;    // 

    [SerializeField]


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Shot();

    }

    void Shot()
    {
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
        if (reset) {_reloadCount = 0f; }
        _reloadCount += Time.deltaTime;
        if (_reloadCount <= _reloadTime) return false;
        return true;
    }
}
