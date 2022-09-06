using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 1f;            // 弾速度
    [SerializeField]
    private float _deleteDistance = 100f;       // 飛翔距離限界
    [SerializeField]
    private int _bulletDamage = 10;             // 弾丸ダメージ
    private bool _init = false;                 // 初期化bool
    private Vector3 _startPos = Vector3.zero;   // インスタンス位置

    void FixedUpdate()
    {
        // --- 弾の移動：単にy軸直線上て等速直線運動させる --- 
        if (!_init)
        {
            _startPos = this.transform.position;
            _init = true;
        }

        this.transform.position += transform.up * _bulletSpeed;
        // オブジェクトの破壊
        if((transform.position - _startPos).magnitude > _deleteDistance)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetBulletDamage()
    {
        // - 弾丸ダメージ - 
        return _bulletDamage;
    }
}
