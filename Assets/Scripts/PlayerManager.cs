using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _player;             // �v���C���[�Q�[���I�u�W�F�N�g�Q��
    [SerializeField]
    private List<AudioClip> _audioClips;    // �I�[�f�B�I�N���b�v���X�g
    [SerializeField]
    private AudioSource _audioSource;       // �v���C���[�̃I�[�f�B�I�\�[�X
    [SerializeField]
    private float _reloadTime = 0.5f;       // �ʏ�̃����[�h����
    [SerializeField]
    private float _reloadTimeInFever = 0.15f;   // �t�B�[�o�[�^�C�����̃����[�h����

    private int _totalGold = 0;         // �S�[���h����
    private int _preTotalGold = 0;      // 1�t���[���O�̃S�[���h����(�ω����m�Ɏg�p)
    private bool _feverFlag = false;    // �t�B�[�o�[�^�C���t���O

    private float _waitCount = 0;           // �O���l�[�h�����őҋ@���鎞�ԃJ�E���g
    [SerializeField]
    private float _waitTime = 1.5f;          // �O���l�[�h�����E�F�C�g���Ԑݒ�

    private bool _gameEnd = false;          // �Q�[���I�����Atrue

    void Update()
    {
        // ���[�U���͎�t
        InputReflection(WaitCount(false));
        AudioController();          // �v���C���[�̃I�[�f�B�I�Đ��Ǘ�
        ChangePlayerReloadTime();   // �����[�h���ԕύX(�t�B�[�o�[�^�C���������[�h���ԒZ�k)
        // �S�[���h�̕ω����m�p
        _preTotalGold = _totalGold;
        
    }

    void InputReflection(bool flag)
    {
        // -- ���[�U���͂𔽉f -- //
        float inputHor = Input.GetAxis("Horizontal");  // ���E�ړ�
        float fire = Input.GetAxis("Fire1");           // �ˌ�
        if (!flag || _gameEnd)
        {
            inputHor = 0;
            fire = 0;
        }
        // - �I�u�W�F�N�g�ɔ��f
        GetComponent<CarMove>().SetHorInput(inputHor);
        _player.GetComponent<PlayerShot>().GetShotInput(fire);
    }

    void AudioController()
    {
        // -- �v���C���[�̌��ʉ����Ǘ����� -- //
        // �ˌ���
        if (_player.gameObject.GetComponent<PlayerShot>().ShotedFlag_1f()) _audioSource.PlayOneShot(_audioClips[0]);  
        // �S�[���h�擾��
        if(_totalGold != _preTotalGold) _audioSource.PlayOneShot(_audioClips[1]);              

    }

    private void ChangePlayerReloadTime()
    {
        // - �v���C���[�̃����[�h���ԕύX - //
        if (_feverFlag)
        {   // �t�B�[�o�[�^�C��
            _player.gameObject.GetComponent<PlayerShot>().SetReloadTime(_reloadTimeInFever);
        }
        else
        {   // �ʏ�
            _player.gameObject.GetComponent<PlayerShot>().SetReloadTime(_reloadTime);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �A�C�e���Q�b�g�p�R���C�_(is Trigger)�ɃS�[���h�������������̏���
        var gold = other.gameObject.GetComponent<GoldScript>();
        if (gold)
        {
            _totalGold += gold.GetScore();      // �X�R�A���Z
            Destroy(other.gameObject);      // �S�[���h����
        }

        // ���e�̏���
        if (other.gameObject.CompareTag("bomb"))
        {
            WaitCount(true);    // �E�F�C�g���ԊJ�n
            _audioSource.PlayOneShot(_audioClips[2]);   // ��_���[�W���Đ�

        }
    }

    bool WaitCount(bool reset)
    {
        // ���e��e���ȂǁA�ˌ��𖳌������G�t�F�N�g�𔭐������������Ɏ��s
        if (reset) { _waitCount = _waitTime; }
        _waitCount -= Time.deltaTime;
        if(_waitCount <= 0) { _waitCount = 0; }
        if (_waitCount > 0) return false;
        return true;
    }
    public int GetMoneyNum()
    {
        // �S�[���h�ʂ�Ԃ�
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
