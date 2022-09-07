using System;
using UnityEngine;
using TMPro;


public class GameManager : MonoBehaviour
{
    [SerializeField]
    private float _countdownSec = 3 * 60f;      // �Q�[���^�C���A�b�v���Ԃ�ݒ�(sec)
    [SerializeField]
    private GameObject _playerGObj;              // �v���C���[�I�u�W�F�N�g�Q��
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
    private int[] _feverDamageThreshold = new int[30];      // �t�B�[�o�[�^�C���˓��_���[�W�݌v臒l
    private float[] _coinNumExpectedValue = new float[30];  // �R�C���o�������Ғl
    private int _feverCount = 0;                            // �t�B�[�o�[�^�C���񐔃J�E���g
    private int _damageTempCount;                           // �t�B�[�o�[�^�C���p�_���[�W�ꎞ�J�E���^

    // Enemy�֘A�ϐ�
    private int _enemyTotalDamage = 0;          // �G�_���[�W����
    private int _preEnemyTotalDamage = 0;       // 1�t���[���O�̓G�_���[�W����(��e���m�̂��߂Ɏg�p)

    void Start()
    {
        for(int i = 0; i < _feverDamageThreshold.Length; i++)
        {
            // log10(0):-infinit,log10(1):0�̉����������v�Z�����i�[���Ă���
            // �v�Z���̏ڍׂ̓v���W�F�N�g�t�H���_������CoinSpawnExam.xlsx�ɋL��
            _feverDamageThreshold[i] = (int)(250F * Mathf.Log10(i+2));
            _coinNumExpectedValue[i] = (float)(Mathf.Log10(i+2) * (1.0 / 0.3));

        }
    }

    // Update is called once per frame
    void Update()
    {
        // --- �Q�[���I�[�o�[��̏��� --- //
        if(GameOver(Time.deltaTime, 3f)) {
            BoardRender(0f, false);             // ����\����UI������
            GameOverTask();                     // �Q�[���I�[�o�[�^�X�N���s                                    
            return;                             // �Q�[���I�[�o�[�L������A�ȍ~�̏�������
        }   

        // --- �Q�[�����̒ʏ폈�� --- //
        BoardRender(Time.deltaTime, true);      // ����\����UI�Ǘ�
        DropItemController();                   // �h���b�v�A�C�e���Ǘ�

        
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
        _g_oMoneyNum.text = _playerGObj.GetComponent<PlayerManager>().GetMoneyNum().ToString();       // �҂�������
        _g_oDamageNum.text = _enemyObj.GetComponent<EnemyManager>().GetDamage().ToString();     // �҂����_���[�W

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
        _scoreText.text = "Money : " + _playerGObj.GetComponent<PlayerManager>().GetMoneyNum().ToString();
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

    private void DropItemController()
    {
        // -- �G���h���b�v����A�C�e���̊Ǘ������{ -- //
        // - �_���[�W�𒲂ׂ�
        _enemyTotalDamage = _enemyObj.gameObject.GetComponent<EnemyManager>().GetDamage();
        if(_enemyTotalDamage != _preEnemyTotalDamage)   // �_���[�W���ʂ̕ω����m
        {
            // ���Ғl����h���b�v����R�C�������v�Z
            float expectedValue = _coinNumExpectedValue[_feverCount];   // ���Ғl�i�[
            int tempDropNum = (int)expectedValue;                       // �b��̃h���b�v���i�[
            expectedValue = expectedValue - (int)expectedValue;         // �����_�̂ݎ擾
            if(expectedValue > UnityEngine.Random.Range(0f, 1f))
            {
                // ���Ғl������0~100%����������ǉ��̃R�C�����h���b�v����
                tempDropNum++;
            }
            // �S�[���h�h���b�v���s
            _enemyObj.gameObject.GetComponent<EnemyManager>().PopItem(EnemyManager._Item.gold, tempDropNum);
        }

        // - 1�t���[���O�̃_���[�W���ʊi�[
        _preEnemyTotalDamage = _enemyTotalDamage;
    }

    private void FeverController()
    {
        // -- �t�B�[�o�[�^�C�����R���g���[������֐� -- //
        if(_enemyTotalDamage >= _feverDamageThreshold[_feverCount])
        {
            _feverCount++;
        }
    }
}
