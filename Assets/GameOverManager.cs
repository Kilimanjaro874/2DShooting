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
        // �Q�[���I�[�o�[��ʂ��\����
        this.gameObject.SetActive(false);
    }

    public void GameOverInit()
    {
        // �Q�[���I�[�o�[���A��ʕ\�����X�R�A���v�Z���`��
        this.gameObject.SetActive(true);
        
    }

}
