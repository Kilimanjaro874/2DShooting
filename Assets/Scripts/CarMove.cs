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

    private void FixedUpdate()
    {
        // -- Playerを左右移動させる -- //
        if(Mathf.Abs(_rb2D.velocity.x) < _topSpeed)
        {
            // 入力：加速
            if (_inputHor >= 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _frontTorqueCoff, 0));

            }
            // 入力：減速(ブレーキ)
            if (_inputHor < 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _backTorqueCoff, 0));
            }
        }
    }

    public void SetHorInput(float inputHor)
    {
        _inputHor = inputHor;   // ユーザ入力（水平移動)を取得：マネージャーから
    }
   
}
