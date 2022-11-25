using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;


public class GameOverManager : MonoBehaviour
{
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
