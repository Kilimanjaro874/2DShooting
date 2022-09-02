using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackGroundManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _backGround;             // �w�i�v���n�u
    [SerializeField]
    private int _spawnNum = 2;                  // �w�i���\����
    [SerializeField]
    private float _spawnSpan = 92.26f;
    [SerializeField]
    private float _backGroundSpeed = -0.2f;      // �w�i�X�N���[���X�s�[�h
    [SerializeField]
    private float _backGround_delete_x = -66f;  // �w�i�����ʒu

    void Start()
    {
        // �����̔w�i��p�ӂ���
        for(int i = 0; i < _spawnNum; i++)
        {
            var backGround = Instantiate(_backGround);
            backGround.transform.SetParent(transform, false);
            backGround.transform.position = new Vector3(
                backGround.transform.position.x + i * _spawnSpan, 
                transform.position.y, 
                1);
        }
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        // �w�i


        // backGround�𑬓x�ɉ����Ĉړ�������
        for(int i = 0; i < transform.childCount; i++)
        {
            // �ړ�
            var x = transform.GetChild(i).transform.position.x;
            transform.GetChild(i).transform.position = new Vector3(x + _backGroundSpeed, transform.position.y, 1);

            // backGround�ʒu����l(_background_delete_x)�ȉ��ɂȂ����Ƃ��A�i�s�����Ō����background������ground(i)�폜
            //var backGround = Instantiate(_backGround);
            //backGround.transform.SetParent(transform, false);
            //var diff_x = transform.GetChild(_spawnNum - 1).position.x + _spawnSpan;
            //backGround.transform.position = new Vector3(diff_x, transform.position.y, 1);
            //// �폜
            //Destroy(transform.GetChild(i).gameObject);
        }
        
    }
}
