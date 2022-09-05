using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _Gold;
    private bool _spawnFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // アイテムスポーン受付
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spawnFlag = true;
        }
    }

    private void FixedUpdate()
    {
        if (_spawnFlag)
        {
            SpawnGold();
            _spawnFlag = false;
        }
    }

    public void SetSpawnFlag()
    {
        _spawnFlag = true;
    }

    public void SpawnGold()
    {
        // ゴールドインスタンス化
        var gold = Instantiate(_Gold);
        gold.transform.position = transform.position;
        var rg2D = gold.GetComponent<Rigidbody2D>();    // リジッドボディ取得

        // ゴールド射出方向を定める力ベクトル作成＆作用
        Vector2 forceDir = new Vector2(Random.Range(-10, 2), Random.Range(-3, 5));
        rg2D.AddForce(forceDir, ForceMode2D.Impulse);

    }
}
