using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb2D;              // �w���̃��W�b�h�{�f�B2D�擾
    [SerializeField]
    private Transform _heliTargetPos;       // �w���̈ʒuPID����ڕW�ʒu
    // �ʒu����p�p�����[�^
    Vector3 _preErrorPos = new Vector3(0, 0, 0);
    Vector3 _integralError = new Vector3(0, 0, 0);
    [SerializeField]
    float _velMax = 5f;     // ���x���E
    [SerializeField]
    float _kp = 16f;        // P����W��  
    [SerializeField]
    float _ki = 0.01f;      // I����W��
    [SerializeField]
    float _kd = 3.5f;       // D����W��
    [SerializeField]
    float _angleLim = 20f;  // �w���X�Ίp�x���E(deg) 
    [SerializeField]
    float _dth = 1f;  // �w���X�Ίp�x����W��

    private int _damage = 0;


    private void Update()
    {
        // �_���[�W�ɉ����ăS�[���h�h���b�v
    }

    private void FixedUpdate()
    {
        // �w�����ʒu����i�ӂ�ӂ���ł��銴�����Č�������W���Ŋ����̒���������)
        HeliController(Time.deltaTime, _heliTargetPos);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damage += bullet.GetBulletDamage();    //  �_���[�W���Z
            Destroy(other.gameObject);              //  �e�ۏ���
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bullet = collision.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damage += bullet.GetBulletDamage();
            Destroy(collision.gameObject);              //  �e�ۏ���
        }
    }

    private void HeliController(float delta_time, Transform targetPos)
    {
        // --- �w���R�v�^�[�̈ʒuPID���� --- //
        // -- �덷�v�Z -- //
        Vector3 posError = targetPos.position - transform.position;
        // -- ������͐��� -- //
        _integralError = (posError + _preErrorPos) / 2 * delta_time;
        Vector3 uf =    _kp * posError + 
                        _ki * _integralError + 
                        _kd * (posError - _preErrorPos) / delta_time;
        // -- ������͂����W�b�h�{�f�B�֔��f -- //
        Vector2 uf2D = uf;  // 3D -> 2D(x, y)���Ƃ�����
        _rb2D.AddForce(uf);
        // ���x����
        if(_rb2D.velocity.magnitude >= _velMax)
        {
            _rb2D.velocity = _rb2D.velocity.normalized * _velMax;
        }

        _preErrorPos = posError;    // ���t���[���̌덷���i�[���Ă���

        // --- �w���R�v�^�[�̎p���������� --- //
        // -- �����ړ����̑��x�ɉ����ČX�Ίp�x�ω� 

        Vector3 rotEuler = this.transform.rotation.eulerAngles;
        float rotz = 0;
        // �p�x��180 ~ -180(deg)�ɕϊ�
        if(rotEuler.z > 180)
        {
            rotz = rotEuler.z - 360f;
        } else
        {
            rotz = rotEuler.z;
        }

        // �O�i��
        if (_rb2D.velocity.x >= 0)
        {
            // �O�X�p�x�ɂ�����
            if (rotz >= -_angleLim)
            {
                transform.Rotate(0, 0, -_dth);
            }
            else if (rotz < -_angleLim)
            {
                transform.Rotate(0, 0, _dth);
            }
        }
        else if (_rb2D.velocity.x < 0)
        {
            if (rotz >= _angleLim)
            {
                transform.Rotate(0, 0, -_dth);
            }
            else if (rotz < _angleLim)
            {
                transform.Rotate(0, 0, _dth);
            }
        }

    }

    public int GetDamage()
    {
        // �w���̃_���[�W���ʂ�Ԃ�
        return _damage;
    }
}
