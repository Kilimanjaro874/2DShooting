using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;             // プレイヤーゲームオブジェクト参照
    [SerializeField]
    private List<AudioClip> _audioClips;    // オーディオクリップリスト
    [SerializeField]
    private AudioSource _audioSource;       // プレイヤーのオーディオソース
    [SerializeField]
    private float _reloadTime = 0.5f;       // 通常のリロード時間
    [SerializeField]
    private float _reloadTimeInFever = 0.15f;   // フィーバータイム中のリロード時間

    private int _totalGold = 0;         // ゴールド総数
    private int _preTotalGold = 0;      // 1フレーム前のゴールド総数(変化検知に使用)
    private bool _feverFlag = false;    // フィーバータイムフラグ

    private float _waitCount = 0;           // グレネード爆発で待機する時間カウント
    [SerializeField]
    private float _waitTime = 1.5f;          // グレネード爆発ウェイト時間設定

    private bool _gameEnd = false;          // ゲーム終了時、true

    void Update()
    {
        // ユーザ入力受付
        InputReflection(WaitCount(false));
        AudioController();          // プレイヤーのオーディオ再生管理
        ChangePlayerReloadTime();   // リロード時間変更(フィーバータイム時リロード時間短縮)
        // ゴールドの変化検知用
        _preTotalGold = _totalGold;
        
    }

    void InputReflection(bool flag)
    {
        // -- ユーザ入力を反映 -- //
        float inputHor = Input.GetAxis("Horizontal");  // 左右移動
        float fire = Input.GetAxis("Fire1");           // 射撃
        if (!flag || _gameEnd)
        {
            inputHor = 0;
            fire = 0;
        }
        // - オブジェクトに反映
        GetComponent<CarMove>().SetHorInput(inputHor);
        _player.GetComponent<PlayerShot>().GetShotInput(fire);
    }

    void AudioController()
    {
        // -- プレイヤーの効果音を管理する -- //
        // 射撃音
        if (_player.gameObject.GetComponent<PlayerShot>().ShotedFlag_1f()) _audioSource.PlayOneShot(_audioClips[0]);  
        // ゴールド取得音
        if(_totalGold != _preTotalGold) _audioSource.PlayOneShot(_audioClips[1]);              

    }

    private void ChangePlayerReloadTime()
    {
        // - プレイヤーのリロード時間変更 - //
        if (_feverFlag)
        {   // フィーバータイム
            _player.gameObject.GetComponent<PlayerShot>().SetReloadTime(_reloadTimeInFever);
        }
        else
        {   // 通常
            _player.gameObject.GetComponent<PlayerShot>().SetReloadTime(_reloadTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // アイテムゲット用コライダ(is Trigger)にゴールドが当たった時の処理
        var gold = other.gameObject.GetComponent<GoldScript>();
        if (gold)
        {
            _totalGold += gold.GetScore();      // スコア加算
            Destroy(other.gameObject);      // ゴールド消去
        }

        // 爆弾の処理
        if (other.gameObject.CompareTag("bomb"))
        {
            WaitCount(true);    // ウェイト時間開始
            _audioSource.PlayOneShot(_audioClips[2]);   // 被ダメージ音再生

        }
    }

    bool WaitCount(bool reset)
    {
        // 爆弾被弾中など、射撃を無効化＆エフェクトを発生させたい時に実行
        if (reset) { _waitCount = _waitTime; }
        _waitCount -= Time.deltaTime;
        if(_waitCount <= 0) { _waitCount = 0; }
        if (_waitCount > 0) return false;
        return true;
    }
    public int GetMoneyNum()
    {
        // ゴールド量を返す
        return _totalGold;
    }
    public void ToggleFeverFlag()
    {
        _feverFlag = !_feverFlag;
    }
    public void SetGameEnd()
    {
        _gameEnd = true;
    }
}
