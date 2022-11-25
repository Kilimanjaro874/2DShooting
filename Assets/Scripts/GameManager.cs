using System;
using UnityEngine;
using UnityEngine.UI;
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
    [SerializeField]
    private Slider _feverGage;                  // �t�B�[�o�[�Q�[�W�Q��
    [SerializeField]
    private GameObject _feverEffect;            // �t�B�[�o�[�^�C���G�t�F�N�g�I�u�W�F�N�g�Q��
    [SerializeField]
    private TextMeshProUGUI _feverLvText;       // �t�B�[�o�[���x���e�L�X�g

    private float _gameOverWindowCount = 0f;    // �^�C���A�b�v��̃J�E���^�p
    private int[] _feverDamageThreshold = new int[30];      // �t�B�[�o�[�^�C���˓��_���[�W�݌v臒l
    private float[] _coinNumExpectedValue = new float[30];  // �R�C���o�������Ғl
    private int _feverCount = 0;                            // �t�B�[�o�[�^�C���񐔃J�E���g
    private int _damageTempCount;                           // �t�B�[�o�[�^�C���p�_���[�W�ꎞ�J�E���^
    [SerializeField]
    private float _feverTimeSpan = 3f;                      // �t�B�[�o�[�^�C���p������
    [SerializeField]
    private float _maxRankScore = 450000;                   // S�����N�X�R�A�����
    private float _feverTimeCount = 0f;                     // �t�B�[�o�[�^�C���o�ߎ��ԃJ�E���g 
    private bool _feverFlag = false;                        // �t�B�[�o�[�^�C�������t���O

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
        // �G�t�F�N�g��\��
        _feverEffect.SetActive(false);
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
        FeverController(Time.deltaTime);        // �t�B�[�o�[�^�C���Ǘ�
        // - �_���[�W�ω����m
        _preEnemyTotalDamage = _enemyTotalDamage;
    }

    private void FixedUpdate()
    {
        // - �v���C���[���ז����锚�e�X�|�[��
        if (UnityEngine.Random.Range(0, 1000) > (995 - _feverCount))
        {
            _enemyObj.gameObject.GetComponent<EnemyManager>().PopItem(EnemyManager._Item.bomb, 1);
        }
    }

    private bool GameOver(float delta_time, float viewTime)
    {
        // -- �Q�[���I�[�o�[�Ȃ��true��Ԃ��֐��F�^�C��0�𐔂�����AviewTime��ɃQ�[���I�[�o�[��ʂ�\�� -- //
        if(_countdownSec <= 0)
        {
            // �v���C���[���얳����
            _playerGObj.gameObject.GetComponent<PlayerManager>().SetGameEnd();
            // �G�̔��e����������
            _enemyObj.gameObject.GetComponent<EnemyManager>().SetGameEnd();
            
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
        float moneyNum = _playerGObj.GetComponent<PlayerManager>().GetMoneyNum();
        float damageNum = _enemyObj.GetComponent<EnemyManager>().GetDamage();
        _g_oMoneyNum.text = moneyNum.ToString();       // �҂�������
        _g_oDamageNum.text = damageNum.ToString();     // �҂����_���[�W
        _g_oScoreNum.text = (moneyNum * damageNum/100).ToString();
        _g_oRankChar.text = GetRank(moneyNum * damageNum / 100);

    }

    private void BoardRender(float delta_time, bool render)
    {
        // -- ��ʍ����UI�\���Ǘ� -- //
        // - �����_�[��~�̏ꍇ
        if (!render)
        {
            _timeText.text = "";
            _scoreText.text = "";
            _feverLvText.text = "";
            _feverGage.gameObject.SetActive(false);
            return;
        }
        // - �����_�[�\���̏ꍇ
        // ���ԊǗ�
        _timeText.text = "Time Limit : " + CountDown(Time.deltaTime);
        // �X�R�A�\��
        _scoreText.text = "Money : " + _playerGObj.GetComponent<PlayerManager>().GetMoneyNum().ToString();
        // �t�B�[�o�[���x���\��
        _feverLvText.text = "Lv : " + _feverCount.ToString();

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

    }

    private void FeverController(float delta_time)
    {

        // - �t�B�[�o�[�^�C�������`�F�b�N
        if (!_feverFlag)
        {
            // �t�B�[�o�[�^�C�����������̎������Q�[�W�𒙂߂�
            if (_enemyTotalDamage != _preEnemyTotalDamage)
            {
                _damageTempCount += 10;
                // �Q�[�W�̒l�𔽉f
                float gagenNum = Mathf.Clamp((float)_damageTempCount / _feverDamageThreshold[_feverCount], 0, 1f); // %�\��
                _feverGage.value = gagenNum;
            }

            // 100%�ł���΃t�B�[�o�[�^�C���J�n
            if (_damageTempCount >= _feverDamageThreshold[_feverCount])
            {
                _feverFlag = true;          // �t�B�[�o�[�^�C���J�n
                _feverCount++;              // ���̃��x���֐i�߂�
                _damageTempCount = 0;       // ���Z�b�g
                // �v���C���[�}�l�[�W���[�Ƀt�B�[�o�[�^�C���ł��鎖��`����
                _playerGObj.gameObject.GetComponent<PlayerManager>().ToggleFeverFlag();
                // �G�t�F�N�g�\��
                _feverEffect.SetActive(true);
            }
        }
        else
        {
            // - �t�B�[�o�[�^�C���������̏��� - //
            _feverGage.value = 1;               // �Q�[�W�\����MAX�̂܂܁B

            _feverTimeCount += delta_time;
            if(_feverTimeCount >= _feverTimeSpan)
            {
                // �t�B�[�o�[�^�C���I������
                _feverTimeCount = 0;
                _feverFlag = false;
                // �v���C���[�}�l�[�W���[�Ƀt�B�[�o�[�^�C���I���ł��鎖��`����
                _playerGObj.gameObject.GetComponent<PlayerManager>().ToggleFeverFlag();
                // �G�t�F�N�g��\��
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
