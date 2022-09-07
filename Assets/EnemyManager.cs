using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _heliTargetPos;      // ヘリの目的位置
    [SerializeField]
    private GameObject _itemSpawner;        // アイテムスポナー
    [SerializeField]
    private List<Vector3> _enemyRandPos;    // 敵の移動位置を格納
    [SerializeField]
    private int _enemyRandPosNum = 10;      // 敵移動位置個数指定
    [SerializeField]
    private float _moveTime = 3F;           // 敵の移動スパン(s)
    [SerializeField]
    private List<AudioClip> _audioClips;     // オーディオクリップリスト
    [SerializeField]
    private AudioSource _audioSource;       // 敵のオーディオソース
   
    private float _moveTimeCount = 0F;      // 敵の移動スパンカウント用
    private int _damageTotal = 0;           // ダメージ総数
    private int _predamageTotal = 0;        // 1フレーム前のダメージ総量(変化検知に使用)

    private float _heliMoveSoundCount = 0;      // ヘリ効果音ループ再生用カウント
    private float _heliLoopSec = 3f;            // ヘリ効果音再生時間
    private bool _heliHitFlag_1f;               // ヘリ弾丸ヒットフラグ(1フレーム有効)

    public enum _Item{                     // アイテム列挙型
        gold,
        bomb
    }
    _Item _item;

    private void Start()
    {
        // ヘリのランダム移動位置を取得する
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
        // -- ヘリの目標位置更新(一定時間間隔で、リストのランダム位置をチョイス) -- //
        if (TimeCounter(Time.deltaTime))
        {
            int no = Random.Range(0, _enemyRandPosNum + 4);
            _heliTargetPos.transform.position = _enemyRandPos[no];
        }
        // -- オーディオ管理 -- //
        AudioController();

        // ダメージ変化検知用
        _predamageTotal = _damageTotal;
    }

    private bool TimeCounter(float delta_time)
    {
        // -- ヘリが移動を開始する時間間隔カウント -- //
        _moveTimeCount += delta_time;
        if (_moveTimeCount < _moveTime) return false;
        _moveTimeCount = 0;
        return true;        // カウント完了した時, trueを返す
    }

    void AudioController()
    {
        // -- エネミーの効果音を管理する -- //
        // ヘリ動作音
        if(_heliMoveSoundCount == 0) { _audioSource.PlayOneShot(_audioClips[0], 0.5f); }
        _heliMoveSoundCount += Time.deltaTime;
        if(_heliMoveSoundCount > _heliLoopSec) { _heliMoveSoundCount = 0; }
        // ダメージ音(軽)
        if(_damageTotal != _predamageTotal) { _audioSource.PlayOneShot(_audioClips[1], 0.5f); }


        
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // -- ダメージ判定：物理的な当たり判定無しver -- //
        var bullet = other.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damageTotal += bullet.GetBulletDamage();    //  ダメージ加算
            Destroy(other.gameObject);              //  弾丸消去
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- ダメージ判定: 物理的な当たり判定有りver -- //
        var bullet = collision.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damageTotal += bullet.GetBulletDamage();
            Destroy(collision.gameObject);              //  弾丸消去

        }
    }

    public int GetDamage()
    {
        return _damageTotal;
    }

    public void PopItem(_Item item, int num)
    {
        // -- 指定アイテムを指定数スポーン -- //
        switch (item)
        {
            case _Item.gold:    // ゴールド
                _itemSpawner.GetComponent<SpawnItem>().SpawnGold(num);
                break;
            case _Item.bomb:    // グレネード

                break;
        }
    }

}
