using System;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private Slider _feverGage;                  // フィーバーゲージ参照
    [SerializeField]
    private GameObject _feverEffect;            // フィーバータイムエフェクトオブジェクト参照
    [SerializeField]
    private TextMeshProUGUI _feverLvText;       // フィーバーレベルテキスト

    private float _gameOverWindowCount = 0f;    // タイムアップ後のカウンタ用
    private int[] _feverDamageThreshold = new int[30];      // フィーバータイム突入ダメージ累計閾値
    private float[] _coinNumExpectedValue = new float[30];  // コイン出現数期待値
    private int _feverCount = 0;                            // フィーバータイム回数カウント
    private int _damageTempCount;                           // フィーバータイム用ダメージ一時カウンタ
    [SerializeField]
    private float _feverTimeSpan = 3f;                      // フィーバータイム継続時間
    [SerializeField]
    private float _maxRankScore = 450000;                   // Sランクスコアを入力
    private float _feverTimeCount = 0f;                     // フィーバータイム経過時間カウント 
    private bool _feverFlag = false;                        // フィーバータイム発動フラグ

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
        // エフェクト非表示
        _feverEffect.SetActive(false);
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
        FeverController(Time.deltaTime);        // フィーバータイム管理
        // - ダメージ変化検知
        _preEnemyTotalDamage = _enemyTotalDamage;
    }

    private void FixedUpdate()
    {
        // - プレイヤーを邪魔する爆弾スポーン
        if (UnityEngine.Random.Range(0, 1000) > (995 - _feverCount))
        {
            _enemyObj.gameObject.GetComponent<EnemyManager>().PopItem(EnemyManager._Item.bomb, 1);
        }
    }

    private bool GameOver(float delta_time, float viewTime)
    {
        // -- ゲームオーバーならばtrueを返す関数：タイム0を数えた後、viewTime後にゲームオーバー画面を表示 -- //
        if(_countdownSec <= 0)
        {
            // プレイヤー操作無効化
            _playerGObj.gameObject.GetComponent<PlayerManager>().SetGameEnd();
            // 敵の爆弾投下無効か
            _enemyObj.gameObject.GetComponent<EnemyManager>().SetGameEnd();
            
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
        float moneyNum = _playerGObj.GetComponent<PlayerManager>().GetMoneyNum();
        float damageNum = _enemyObj.GetComponent<EnemyManager>().GetDamage();
        _g_oMoneyNum.text = moneyNum.ToString();       // 稼いだお金
        _g_oDamageNum.text = damageNum.ToString();     // 稼いだダメージ
        _g_oScoreNum.text = (moneyNum * damageNum/100).ToString();
        _g_oRankChar.text = GetRank(moneyNum * damageNum / 100);

    }

    private void BoardRender(float delta_time, bool render)
    {
        // -- 画面左上のUI表示管理 -- //
        // - レンダー停止の場合
        if (!render)
        {
            _timeText.text = "";
            _scoreText.text = "";
            _feverLvText.text = "";
            _feverGage.gameObject.SetActive(false);
            return;
        }
        // - レンダー表示の場合
        // 時間管理
        _timeText.text = "Time Limit : " + CountDown(Time.deltaTime);
        // スコア表示
        _scoreText.text = "Money : " + _playerGObj.GetComponent<PlayerManager>().GetMoneyNum().ToString();
        // フィーバーレベル表示
        _feverLvText.text = "Lv : " + _feverCount.ToString();

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

    }

    private void FeverController(float delta_time)
    {

        // - フィーバータイム発動チェック
        if (!_feverFlag)
        {
            // フィーバータイム発動無しの時だけゲージを貯める
            if (_enemyTotalDamage != _preEnemyTotalDamage)
            {
                _damageTempCount += 10;
                // ゲージの値を反映
                float gagenNum = Mathf.Clamp((float)_damageTempCount / _feverDamageThreshold[_feverCount], 0, 1f); // %表示
                _feverGage.value = gagenNum;
            }

            // 100%であればフィーバータイム開始
            if (_damageTempCount >= _feverDamageThreshold[_feverCount])
            {
                _feverFlag = true;          // フィーバータイム開始
                _feverCount++;              // 次のレベルへ進める
                _damageTempCount = 0;       // リセット
                // プレイヤーマネージャーにフィーバータイムである事を伝える
                _playerGObj.gameObject.GetComponent<PlayerManager>().ToggleFeverFlag();
                // エフェクト表示
                _feverEffect.SetActive(true);
            }
        }
        else
        {
            // - フィーバータイム発動時の処理 - //
            _feverGage.value = 1;               // ゲージ表示はMAXのまま。

            _feverTimeCount += delta_time;
            if(_feverTimeCount >= _feverTimeSpan)
            {
                // フィーバータイム終了処理
                _feverTimeCount = 0;
                _feverFlag = false;
                // プレイヤーマネージャーにフィーバータイム終了である事を伝える
                _playerGObj.gameObject.GetComponent<PlayerManager>().ToggleFeverFlag();
                // エフェクト非表示
                _feverEffect.SetActive(false);
            }
        }
        
    }

    string GetRank(float score)
    {
        string rank = "D";
        if (score > _maxRankScore * 0.5)  { rank = "C"; }
        if (score > _maxRankScore * 0.7) { rank = "B"; }
        if (score > _maxRankScore * 0.8) { rank = "A"; }
        if (score > _maxRankScore * 1.0) { rank = "S"; }
        if (score > _maxRankScore * 1.1) { rank = "S+"; }
        if (score > _maxRankScore * 1.2) { rank = "S++"; }

        return rank;
    }


}
