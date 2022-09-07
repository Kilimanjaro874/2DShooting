using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverManager : MonoBehaviour
{
    // 
    [SerializeField]
    private GameObject _gameOver;

    [SerializeField]
    private TextMeshProUGUI _moneyNum;
    [SerializeField]
    private TextMeshProUGUI _totalDamageNum;
    [SerializeField]
    private TextMeshProUGUI _scoreNum;
    [SerializeField]
    private TextMeshProUGUI _rankChar;

    public void Start()
    {
        // ゲームオーバー画面を非表示化
        this.gameObject.SetActive(false);
    }

    public void GameOverInit()
    {
        // ゲームオーバー時、画面表示＆スコアを計算＆描画
        this.gameObject.SetActive(true);
        
    }

}
