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

    private void FixedUpdate()
    {
        // -- Player�����E�ړ������� -- //
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

    public void SetHorInput(float inputHor)
    {
        _inputHor = inputHor;   // ���[�U���́i�����ړ�)���擾�F�}�l�[�W���[����
    }
   
}
