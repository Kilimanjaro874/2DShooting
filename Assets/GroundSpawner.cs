using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ground;
    [SerializeField]
    private int _spawnNum = 10;
    [SerializeField]
    private float _spawnSpan = 8f;
    [SerializeField]
    private float _groundSpeed = -1f;
    [SerializeField]
    private float _ground_delete_x = -25;


    private void Start()
    {
        // 初期の地形を用意する
        for(int i = 0; i < _spawnNum; i++)
        {
            var ground = Instantiate(_ground);
            ground.transform.SetParent(transform, false);
            ground.transform.position = new Vector3(ground.transform.position.x + i * _spawnSpan, transform.position.y, 0);

        }
    }

    private void FixedUpdate()
    {
        // groundの削除を検知＆インスタンス化
        if(transform.childCount < _spawnNum)
        {
            var ground = Instantiate(_ground);
            ground.transform.SetParent(transform, false);
            
        }

        // groundを速度に応じて移動させる
        for(int i = 0; i < transform.childCount; i++)
        {
            var x = transform.GetChild(i).position.x;
            transform.GetChild(i).transform.position = new Vector3(x + _groundSpeed, transform.position.y, 0);

            // groundが基準値(_ground_delete_x)以下となったとき、ground生成＆ground削除
            if(transform.GetChild(i).transform.position.x <= _ground_delete_x)
            {
                // 生成(最後に生成された子オブジェクトの位置を参照し、新たなgroundの相対位置を決定)
                var ground = Instantiate(_ground);
                ground.transform.SetParent(transform);
                var diff_x = transform.GetChild(_spawnNum - 1).position.x + _spawnSpan;
                ground.transform.position = new Vector3(diff_x, transform.position.y, 0);
                // 削除
                Destroy(transform.GetChild(i).gameObject);

            }
        }

        // groundが
    }

}
