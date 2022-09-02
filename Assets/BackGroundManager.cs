using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _backGround;             // 背景プレハブ
    [SerializeField]
    private int _spawnNum = 2;                  // 背景総表示数
    [SerializeField]
    private float _spawnSpan = 92.26f;
    [SerializeField]
    private float _backGroundSpeed = -0.2f;      // 背景スクロールスピード
    [SerializeField]
    private float _backGround_delete_x = -66f;  // 背景消去位置

    void Start()
    {
        // 初期の背景を用意する
        for(int i = 0; i < _spawnNum; i++)
        {
            var backGround = Instantiate(_backGround);
            backGround.transform.SetParent(transform, false);
            backGround.transform.position = new Vector3(
                backGround.transform.position.x + i * _spawnSpan, 
                transform.position.y, 
                1);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // 背景


        // backGroundを速度に応じて移動させる
        for(int i = 0; i < transform.childCount; i++)
        {
            // 移動
            var x = transform.GetChild(i).transform.position.x;
            transform.GetChild(i).transform.position = new Vector3(x + _backGroundSpeed, transform.position.y, 1);

            // backGround位置が基準値(_background_delete_x)以下になったとき、進行方向最後方へbackground生成＆ground(i)削除
            //var backGround = Instantiate(_backGround);
            //backGround.transform.SetParent(transform, false);
            //var diff_x = transform.GetChild(_spawnNum - 1).position.x + _spawnSpan;
            //backGround.transform.position = new Vector3(diff_x, transform.position.y, 1);
            //// 削除
            //Destroy(transform.GetChild(i).gameObject);
        }
        
    }
}
