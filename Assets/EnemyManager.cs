using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _heliTargetPos;      // ヘリの目的位置
    [SerializeField]
    private List<Vector3> _enemyRandPos;    // 敵の移動位置を格納
    [SerializeField]
    private int _enemyRandPosNum = 10;           // 敵移動位置個数指定
    [SerializeField]
    private float _moveTime = 3F;           // 敵の移動スパン(s)

   
    private float _moveTimeCount = 0F;      // 敵の移動スパンカウント用
    private int _damageTotal = 0;           // ダメージ総量

    private void Start()
    {
        for(int i = 0; i < _enemyRandPosNum; i++)
        {
            int x = Random.Range(-4, 14);
            int y = Random.Range(8, 12);
            _enemyRandPos.Add(new Vector3(x, y, 0));
        }
        // 次の4つの隅位置は欲しいので追加する
        _enemyRandPos.Add(new Vector3(-4, 14, 0));
        _enemyRandPos.Add(new Vector3(14, 14, 0));
        _enemyRandPos.Add(new Vector3(-4, 8, 0));
        _enemyRandPos.Add(new Vector3(14, 8, 0));

    }

    private void Update()
    {
        // ヘリの目標位置更新
        if (TimeCounter(Time.deltaTime))
        {
            int no = Random.Range(0, _enemyRandPosNum + 4);
            _heliTargetPos.transform.position = _enemyRandPos[no];
        }
    }

    private bool TimeCounter(float delta_time)
    {
        _moveTimeCount += delta_time;
        if (_moveTimeCount < _moveTime) return false;
        _moveTimeCount = 0;
        return true;
    }

}
