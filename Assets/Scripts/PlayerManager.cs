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

    void Update()
    {
        InputReflection();          // �L�����R������
        AudioController();          // �v���C���[�̃I�[�f�B�I�Đ��Ǘ�
        ChangePlayerReloadTime();   // �����[�h���ԕύX(�t�B�[�o�[�^�C���������[�h���ԒZ�k)
        // �S�[���h�̕ω����m
        _preTotalGold = _totalGold;
        
    }

    void InputReflection()
    {
        // -- ���[�U���͂𔽉f -- //
        float inputHor = Input.GetAxis("Horizontal");  // ���E�ړ�
        float fire = Input.GetAxis("Fire1");           // �ˌ�
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
}
