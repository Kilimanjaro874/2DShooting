using System;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _countdownSec = 3 * 60f;      // �Q�[���^�C���A�b�v���Ԃ�ݒ�(sec)
    [SerializeField]
    private GameObject _playerGObj;             // �v���C���[�I�u�W�F�N�g�Q��
    [SerializeField]
    private GameObject _enemyObj;               // �G�l�~�[�I�u�W�F�N�g�Q��       
    [SerializeField]
    private GameObject _gameOverWindow;         // �Q�[���I�[�o�[�E�B���h�E�Q��
    [SerializeField]
    private TextMeshProUGUI _scoreText;         // ��ʍ����UI:money���Q��
    [SerializeField]
    private TextMeshProUGUI _timeText;          // ��ʍ����UI:TimeLimit���Q��
    [SerializeField]
    private TextMeshProUGUI _g_oMoneyNum;       // �Q�[���I�[�o�[��ʁFMoney���\���p(��GameOver�I�u�W�F�N�g����A�q�̃R������Q�Ƃ���悤�ɂ�����)
    [SerializeField]
    private TextMeshProUGUI _g_oDamageNum;      // �Q�[���I�[�o�[��ʁFDamage���\���p
    [SerializeField]
    private TextMeshProUGUI _g_oScoreNum;       // �Q�[���I�[�o�[��ʁFScore���\���p
    [SerializeField]
    private TextMeshProUGUI _g_oRankChar;       // �Q�[���I�[�o�[��ʁF�����N(S-D)�\���p



    private float _gameOverWindowCount = 0f;    // �^�C���A�b�v��̃J�E���^�p


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        // --- �Q�[���I�[�o�[��̏��� --- //
        if(GameOver(Time.deltaTime, 3f)) {
            BoardRender(0f, false);            // ����\����UI������
            GameOverTask();                    // �Q�[���I�[�o�[�^�X�N���s                                    
            return;                            // �Q�[���I�[�o�[�L������A�ȍ~�̏�������
        }   

        // --- �Q�[�����̒ʏ폈�� --- //
        BoardRender(Time.deltaTime, true);    // ����\����UI�Ǘ�

        
    }

   
    private bool GameOver(float delta_time, float viewTime)
    {
        // -- �Q�[���I�[�o�[�Ȃ��true��Ԃ��֐��F�^�C��0�𐔂�����AviewTime��ɃQ�[���I�[�o�[��ʂ�\�� -- //
        if(_countdownSec <= 0)
        {
            // �J�E���g�I����A�Q�[���I�[�o�[��ʂ�L����
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
        // -- �Q�[���I�[�o�[���̏��������s -- //
        // - ���U���g�̕\��
        _g_oMoneyNum.text = _playerGObj.GetComponent<CarMove>().GetMoneyNum().ToString();       // �҂�������
        _g_oDamageNum.text = _enemyObj.GetComponent<EnemyManager>().GetDamege().ToString();     // �҂����_���[�W

    }

    private void BoardRender(float delta_time, bool render)
    {
        // -- ��ʍ����UI�\���Ǘ� -- //
        // - �����_�[��~�̏ꍇ
        if (!render)
        {
            _timeText.text = "";
            _scoreText.text = "";
            return;
        }
        // - �����_�[�\���̏ꍇ
        // ���ԊǗ�
        _timeText.text = "Time Limit : " + CountDown(Time.deltaTime);
        // �X�R�A�\��
        _scoreText.text = "Money : " + _playerGObj.GetComponent<CarMove>().GetMoneyNum().ToString();
    }

    private string CountDown(float delta_time)
    {
        // -- ���ݎ��Ԃ�^����ƃJ�E���g�_�E�������b�̃e�L�X�g��Ԃ� -- //
        _countdownSec -= Time.deltaTime;
        if (_countdownSec <= 0)
            _countdownSec = 0;
        var timeSpan = new TimeSpan(0, 0, (int)_countdownSec);
        return timeSpan.ToString(@"mm\:ss");
    }

}
