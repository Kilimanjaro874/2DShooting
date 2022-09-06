using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _goldSpawner;        // �S�[���h�X�|�i�[�Q��
    [SerializeField]
    private GameObject _heliMoover;         // �w���Q�[���I�u�W�F�N�g�Q��
    [SerializeField]
    private Transform _heliTargetPos;       // �w������ڕW�ʒu
    private int[] _feverLimDamage = new int[100];   // �t�B�[�o�[�^�C���ɕK�v�ȃ_���[�W���X�g�쐬
    // Start is called before the first frame update
    void Start()
    {
        // �t�B�[�o�[�^�C���̃_���[�W�ώZ�l�z��쐬
        for(int i = 0; i < _feverLimDamage.Length; i++)
        {
            _feverLimDamage[i] = (int)(50 + i * 50 / 2);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // �w���̃_���[�W�ɉ����ăA�C�e�����h���b�v����
        float heliDamage = _heliMoover.GetComponent<EnemyMove>().GetDamage();   // �_���[�W�ώZ�l�擾

        // �W�Q�A�C�e�����h���b�v����
    }

    private void FixedUpdate()
    {
        // �w���̖ڕW�ʒu��ύX���A�w���������������

    }
}
