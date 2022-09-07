using System;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _countdownSec = 3 * 60f;      // ゲームタイムアップ時間を設定(sec)
    [SerializeField]
    private GameObject _playerGObj;              // プレイヤーオブジェクト参照
    [SerializeField]
    private GameObject _enemyObj;               // エネミーオブジェクト参照       
    [SerializeField]
    private GameObject _gameOverWindow;         // ゲームオーバーウィンドウ参照
    [SerializeField]
    private TextMeshProUGUI _scoreText;         // 画面左上のUI:moneyを参照
    [SerializeField]
    private TextMeshProUGUI _timeText;          // 画面左上のUI:TimeLimitを参照
    [SerializeField]
    private TextMeshProUGUI _g_oMoneyNum;       // ゲームオーバー画面：Money数表示用(※GameOverオブジェクトから、子のコレらを参照するようにしたい)
    [SerializeField]
    private TextMeshProUGUI _g_oDamageNum;      // ゲームオーバー画面：Damage数表示用
    [SerializeField]
    private TextMeshProUGUI _g_oScoreNum;       // ゲームオーバー画面：Score数表示用
    [SerializeField]
    private TextMeshProUGUI _g_oRankChar;       // ゲームオーバー画面：ランク(S-D)表示用

    private float _gameOverWindowCount = 0f;    // タイムアップ後のカウンタ用
    private int[] _feverDamageThreshold = new int[30];      // フィーバータイム突入ダメージ累計閾値
    private float[] _coinNumExpectedValue = new float[30];  // コイン出現数期待値
    private int _feverCount = 0;                            // フィーバータイム回数カウント
    private int _damageTempCount;                           // フィーバータイム用ダメージ一時カウンタ

    // Enemy関連変数
    private int _enemyTotalDamage = 0;          // 敵ダメージ総量
    private int _preEnemyTotalDamage = 0;       // 1フレーム前の敵ダメージ総量(被弾検知のために使用)

    void Start()
    {
        for(int i = 0; i < _feverDamageThreshold.Length; i++)
        {
            // log10(0):-infinit,log10(1):0の解を除いた計算式を格納していく
            // 計算式の詳細はプロジェクトフォルダ同梱のCoinSpawnExam.xlsxに記載
            _feverDamageThreshold[i] = (int)(250F * Mathf.Log10(i+2));
            _coinNumExpectedValue[i] = (float)(Mathf.Log10(i+2) * (1.0 / 0.3));

        }
    }

    // Update is called once per frame
    void Update()
    {
        // --- ゲームオーバー後の処理 --- //
        if(GameOver(Time.deltaTime, 3f)) {
            BoardRender(0f, false);             // 左上表示のUI無効化
            GameOverTask();                     // ゲームオーバータスク実行                                    
            return;                             // ゲームオーバー有効化後、以降の処理無し
        }   

        // --- ゲーム中の通常処理 --- //
        BoardRender(Time.deltaTime, true);      // 左上表示のUI管理
        DropItemController();                   // ドロップアイテム管理

        
    }

 
    private bool GameOver(float delta_time, float viewTime)
    {
        // -- ゲームオーバーならばtrueを返す関数：タイム0を数えた後、viewTime後にゲームオーバー画面を表示 -- //
        if(_countdownSec <= 0)
        {
            // カウント終了後、ゲームオーバー画面を有効化
            _gameOverWindowCount += delta_time;
            if (_gameOverWindowCount < viewTime) return false;
            if (!_gameOverWindow.activeSelf)
            {
                _gameOverWindow.SetActive(true);
            }
            return true;
        }
        return false;
    }

    private void GameOverTask()
    {
        // -- ゲームオーバー中の処理を実行 -- //
        // - リザルトの表示
        _g_oMoneyNum.text = _playerGObj.GetComponent<PlayerManager>().GetMoneyNum().ToString();       // 稼いだお金
        _g_oDamageNum.text = _enemyObj.GetComponent<EnemyManager>().GetDamage().ToString();     // 稼いだダメージ

    }

    private void BoardRender(float delta_time, bool render)
    {
        // -- 画面左上のUI表示管理 -- //
        // - レンダー停止の場合
        if (!render)
        {
            _timeText.text = "";
            _scoreText.text = "";
            return;
        }
        // - レンダー表示の場合
        // 時間管理
        _timeText.text = "Time Limit : " + CountDown(Time.deltaTime);
        // スコア表示
        _scoreText.text = "Money : " + _playerGObj.GetComponent<PlayerManager>().GetMoneyNum().ToString();
    }

    private string CountDown(float delta_time)
    {
        // -- 刻み時間を与えるとカウントダウン＆分秒のテキストを返す -- //
        _countdownSec -= Time.deltaTime;
        if (_countdownSec <= 0)
            _countdownSec = 0;
        var timeSpan = new TimeSpan(0, 0, (int)_countdownSec);
        return timeSpan.ToString(@"mm\:ss");
    }

    private void DropItemController()
    {
        // -- 敵がドロップするアイテムの管理を実施 -- //
        // - ダメージを調べる
        _enemyTotalDamage = _enemyObj.gameObject.GetComponent<EnemyManager>().GetDamage();
        if(_enemyTotalDamage != _preEnemyTotalDamage)   // ダメージ総量の変化検知
        {
            // 期待値からドロップするコイン数を計算
            float expectedValue = _coinNumExpectedValue[_feverCount];   // 期待値格納
            int tempDropNum = (int)expectedValue;                       // 暫定のドロップ数格納
            expectedValue = expectedValue - (int)expectedValue;         // 小数点のみ取得
            if(expectedValue > UnityEngine.Random.Range(0f, 1f))
            {
                // 期待値が乱数0~100%を上回ったら追加のコインをドロップする
                tempDropNum++;
            }
            // ゴールドドロップ実行
            _enemyObj.gameObject.GetComponent<EnemyManager>().PopItem(EnemyManager._Item.gold, tempDropNum);
        }

        // - 1フレーム前のダメージ総量格納
        _preEnemyTotalDamage = _enemyTotalDamage;
    }

    private void FeverController()
    {
        // -- フィーバータイムをコントロールする関数 -- //
        if(_enemyTotalDamage >= _feverDamageThreshold[_feverCount])
        {
            _feverCount++;
        }
    }
}
