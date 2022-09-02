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
    float _kp = 16f;        // P����W��  
    [SerializeField]
    float _ki = 0.01f;      // I����W��
    [SerializeField]
    float _kd = 3.5f;       // D����W��
    float _velMax = 1.0f;   // ���x���
    float _accMax = 0.5f;   // �����x���

    private int _damage = 0;

    private void Start()
    {
        // ����p�p�����[�^������
        _preErrorPos = transform.position;   // Vector3D(x,y,z) -> Vector2D(x,y);
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
        // --- �w���R�v�^�[��PID���� --- //
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

        _preErrorPos = posError;    // ���t���[���̌덷���i�[���Ă���

        // --- �w���R�v�^�[�̐������� --- //
    }
}
