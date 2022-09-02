using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb2D;              // ヘリのリジッドボディ2D取得
    [SerializeField]
    private Transform _heliTargetPos;       // ヘリの位置PID制御目標位置
    // 位置制御用パラメータ
    Vector3 _preErrorPos = new Vector3(0, 0, 0);
    Vector3 _integralError = new Vector3(0, 0, 0);
    [SerializeField]
    float _kp = 16f;        // P制御係数  
    [SerializeField]
    float _ki = 0.01f;      // I制御係数
    [SerializeField]
    float _kd = 3.5f;       // D制御係数
    float _velMax = 1.0f;   // 速度上限
    float _accMax = 0.5f;   // 加速度上限

    private int _damage = 0;

    private void Start()
    {
        // 制御用パラメータ初期化
        _preErrorPos = transform.position;   // Vector3D(x,y,z) -> Vector2D(x,y);
    }

    private void FixedUpdate()
    {
        // ヘリを位置制御（ふわふわ飛んでいる感じを再現＆制御係数で感じの調整したい)
        HeliController(Time.deltaTime, _heliTargetPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damage += bullet.GetBulletDamage();    //  ダメージ加算
            Destroy(other.gameObject);              //  弾丸消去
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bullet = collision.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damage += bullet.GetBulletDamage();
            Destroy(collision.gameObject);              //  弾丸消去
        }
 
    }

    private void HeliController(float delta_time, Transform targetPos)
    {
        // --- ヘリコプターのPID制御 --- //
        // -- 誤差計算 -- //
        Vector3 posError = targetPos.position - transform.position;
        // -- 制御入力生成 -- //
        _integralError = (posError + _preErrorPos) / 2 * delta_time;
        Vector3 uf =    _kp * posError + 
                        _ki * _integralError + 
                        _kd * (posError - _preErrorPos) / delta_time;
        // -- 制御入力をリジッドボディへ反映 -- //
        Vector2 uf2D = uf;  // 3D -> 2D(x, y)落とし込み
        _rb2D.AddForce(uf);

        _preErrorPos = posError;    // 今フレームの誤差を格納しておく

        // --- ヘリコプターの水平制御 --- //
    }
}
