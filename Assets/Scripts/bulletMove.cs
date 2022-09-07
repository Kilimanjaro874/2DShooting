using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 1f;            // �e���x
    [SerializeField]
    private float _deleteDistance = 100f;       // ���ċ������E
    [SerializeField]
    private int _bulletDamage = 10;             // �e�ۃ_���[�W
    private bool _init = false;                 // ������bool
    private Vector3 _startPos = Vector3.zero;   // �C���X�^���X�ʒu

    void FixedUpdate()
    {
        // --- �e�̈ړ��F�P��y��������ē��������^�������� --- 
        if (!_init)
        {
            _startPos = this.transform.position;
            _init = true;
        }

        this.transform.position += transform.up * _bulletSpeed;
        // �I�u�W�F�N�g�̔j��
        if((transform.position - _startPos).magnitude > _deleteDistance)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetBulletDamage()
    {
        // - �e�ۃ_���[�W - 
        return _bulletDamage;
    }
}
