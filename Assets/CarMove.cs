using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb2D;              // rigidbody 2d
    [SerializeField]
    private float _topSpeed = 10.0f;        // Car�ō����x���E
    [SerializeField]
    private float _frontTorqueCoff = 0.5f;  // �O���i�s�^�C���g���N�W��
    [SerializeField]
    private float _backTorqueCoff = 1.0f;   // ����i�s�^�C���g���N�W��(�u���[�L��)
 
    private float _inputHor = 0;            // ���[�U�[���͐��������L�^�p

    private int _money = 0;                 // �Q�[���X�R�A

    private void Update()
    {
        // ���[�U�[���͎�t
        _inputHor = Input.GetAxisRaw("Horizontal");
    }


    private void FixedUpdate()
    {
        if(Mathf.Abs(_rb2D.velocity.x) < _topSpeed)
        {
            // ���́F����
            if (_inputHor >= 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _frontTorqueCoff, 0));

            }

            // ���́F����(�u���[�L)
            if (_inputHor < 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _backTorqueCoff, 0));
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // �A�C�e���Q�b�g�p�R���C�_(is Trigger)�ɃS�[���h�������������̏���
        var gold = other.gameObject.GetComponent<GoldScript>();
        if (gold)
        {
            _money += gold.GetScore();      // �X�R�A���Z
            Destroy(other.gameObject);      // �S�[���h����
        }
    }

    public int GetMoneyNum()
    {
        // �X�R�A�ώZ�ʂ�Ԃ�
        return _money;
    }


}
