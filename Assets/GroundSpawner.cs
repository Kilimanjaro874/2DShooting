using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _ground;
    [SerializeField]
    private int _spawnNum = 10;
    [SerializeField]
    private float _spawnSpan = 8f;
    [SerializeField]
    private float _groundSpeed = -1f;
    [SerializeField]
    private float _ground_delete_x = -25;


    private void Start()
    {
        // �����̒n�`��p�ӂ���
        for(int i = 0; i < _spawnNum; i++)
        {
            var ground = Instantiate(_ground);
            ground.transform.SetParent(transform, false);
            ground.transform.position = new Vector3(ground.transform.position.x + i * _spawnSpan, transform.position.y, 0);

        }
    }

    private void FixedUpdate()
    {
        // ground�̍폜�����m���C���X�^���X��
        if(transform.childCount < _spawnNum)
        {
            var ground = Instantiate(_ground);
            ground.transform.SetParent(transform, false);
            
        }

        // ground�𑬓x�ɉ����Ĉړ�������
        for(int i = 0; i < transform.childCount; i++)
        {
            var x = transform.GetChild(i).position.x;
            transform.GetChild(i).transform.position = new Vector3(x + _groundSpeed, transform.position.y, 0);

            // ground����l(_ground_delete_x)�ȉ��ƂȂ����Ƃ��Aground������ground�폜
            if(transform.GetChild(i).transform.position.x <= _ground_delete_x)
            {
                // ����(�Ō�ɐ������ꂽ�q�I�u�W�F�N�g�̈ʒu���Q�Ƃ��A�V����ground�̑��Έʒu������)
                var ground = Instantiate(_ground);
                ground.transform.SetParent(transform);
                var diff_x = transform.GetChild(_spawnNum - 1).position.x + _spawnSpan;
                ground.transform.position = new Vector3(diff_x, transform.position.y, 0);
                // �폜
                Destroy(transform.GetChild(i).gameObject);

            }
        }

        // ground��
    }

}
