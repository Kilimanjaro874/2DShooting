using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _Gold;
    private bool _spawnFlag;

    public void SpawnGold(int num)
    {
        // ゴールドインスタンス化
        for (int i = 0; i < num; i++)
        {
            var gold = Instantiate(_Gold);
            gold.transform.position = transform.position;
            var rg2D = gold.GetComponent<Rigidbody2D>();    // リジッドボディ取得
            // ゴールド射出方向を定める力ベクトル作成＆作用
            Vector2 forceDir = new Vector2(Random.Range(-8, -2), Random.Range(-3, 2));
            rg2D.AddForce(forceDir, ForceMode2D.Impulse);
        }
    }
}
