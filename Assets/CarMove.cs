using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb2D;              // rigidbody 2d
    [SerializeField]
    private float _topSpeed = 10.0f;        // Car最高速度限界
    [SerializeField]
    private float _frontTorqueCoff = 0.5f;  // 前方進行タイヤトルク係数
    [SerializeField]
    private float _backTorqueCoff = 1.0f;   // 後方進行タイヤトルク係数(ブレーキ力)
 
    private float _inputHor = 0;            // ユーザー入力水平方向記録用

    [SerializeField]
    private TextMeshProUGUI _scoreText;     // ゲームスコアテキスト参照用
    private int _score = 0;                 // ゲームスコア

    private void Update()
    {
        // ユーザー入力受付
        _inputHor = Input.GetAxisRaw("Horizontal");
        //this.transform.position = _rb2D.transform.position;
        //this.transform.rotation = _rb2D.transform.rotation;

        // スコアをUIへ反映
        _scoreText.text = _score.ToString();
    }


    private void FixedUpdate()
    {
        if(Mathf.Abs(_rb2D.velocity.x) < _topSpeed)
        {
            // 入力：加速
            if (_inputHor >= 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _frontTorqueCoff, 0));

            }

            // 入力：減速
            if (_inputHor < 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _backTorqueCoff, 0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // アイテムゲット用コライダ(is Trigger)にゴールドが当たった時の処理
        var gold = other.gameObject.GetComponent<GoldScript>();
        if (gold)
        {
            _score += gold.GetScore();      // スコア加算
            Destroy(other.gameObject);      // ゴールド消去

        }
    }


}
