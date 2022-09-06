using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _heliTargetPos;      // �w���̖ړI�ʒu
    [SerializeField]
    private List<Vector3> _enemyRandPos;    // �G�̈ړ��ʒu���i�[
    [SerializeField]
    private int _enemyRandPosNum = 10;      // �G�ړ��ʒu���w��
    [SerializeField]
    private float _moveTime = 3F;           // �G�̈ړ��X�p��(s)

   
    private float _moveTimeCount = 0F;      // �G�̈ړ��X�p���J�E���g�p
    private int _damageTotal = 0;           // �_���[�W����

    private void Start()
    {
        // �w���̃����_���ړ��ʒu���擾����
        for(int i = 0; i < _enemyRandPosNum; i++)
        {
            int x = Random.Range(-4, 14);
            int y = Random.Range(8, 12);
            _enemyRandPos.Add(new Vector3(x, y, 0));
        }
        // ����4�̋��ʒu�͗~�����̂Œǉ�����
        _enemyRandPos.Add(new Vector3(-4, 14, 0));
        _enemyRandPos.Add(new Vector3(14, 14, 0));
        _enemyRandPos.Add(new Vector3(-4, 8, 0));
        _enemyRandPos.Add(new Vector3(14, 8, 0));

    }

    private void Update()
    {
        // -- �w���̖ڕW�ʒu�X�V(��莞�ԊԊu�ŁA���X�g�̃����_���ʒu���`���C�X) -- //
        if (TimeCounter(Time.deltaTime))
        {
            int no = Random.Range(0, _enemyRandPosNum + 4);
            _heliTargetPos.transform.position = _enemyRandPos[no];
        }

       
    }

    private bool TimeCounter(float delta_time)
    {
        // -- �w�����ړ����J�n���鎞�ԊԊu�J�E���g -- //
        _moveTimeCount += delta_time;
        if (_moveTimeCount < _moveTime) return false;
        _moveTimeCount = 0;
        return true;        // �J�E���g����������, true��Ԃ�
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // -- �_���[�W����F�����I�ȓ����蔻�薳��ver -- //
        var bullet = other.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damageTotal += bullet.GetBulletDamage();    //  �_���[�W���Z
            Destroy(other.gameObject);              //  �e�ۏ���
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- �_���[�W����: �����I�ȓ����蔻��L��ver -- //
        var bullet = collision.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damageTotal += bullet.GetBulletDamage();
            Destroy(collision.gameObject);              //  �e�ۏ���
        }
    }

    public int GetDamege()
    {   // -- �w���̃_���[�W�ώZ�l��Ԃ� -- //
        return _damageTotal;
    }

}
