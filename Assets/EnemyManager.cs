using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _goldSpawner;        // ゴールドスポナー参照
    [SerializeField]
    private GameObject _heliMoover;         // ヘリゲームオブジェクト参照
    [SerializeField]
    private Transform _heliTargetPos;       // ヘリ制御目標位置
    private int[] _feverLimDamage = new int[100];   // フィーバータイムに必要なダメージリスト作成
    // Start is called before the first frame update
    void Start()
    {
        // フィーバータイムのダメージ積算値配列作成
        for(int i = 0; i < _feverLimDamage.Length; i++)
        {
            _feverLimDamage[i] = (int)(50 + i * 50 / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ヘリのダメージに応じてアイテムをドロップする
        float heliDamage = _heliMoover.GetComponent<EnemyMove>().GetDamage();   // ダメージ積算値取得

        // 妨害アイテムをドロップする
    }

    private void FixedUpdate()
    {
        // ヘリの目標位置を変更し、ヘリ動作を実現する

    }
}
