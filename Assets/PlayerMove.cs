using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// �L�����N�^�[�����e���^�[�Q�b�g�֌�����X�N���v�g
    /// �L�����N�^�[�̘r�̋t�^���w���������Ȃ���A�X���[�Y�ɓ��삳����
    /// 2D�L�����N�^�[�ɂ�bone���ݒ�ς݂ł���A�ebone�̋t�^���w���������œ���������
    /// (�����I�Ɏ��삵�� n - link�Q�[���I�u�W�F�N�g�̋t�^���w�������X�N���v�g����p������)
    /// </summary>

    [SerializeField]
    private Transform _target;          // �Ə��F�^�[�Q�b�g

    // ---- �t�^���w�������Ώۂ�ݒ� ----
    [SerializeField]
    private List<GameObject> _arms_r;       // �E�r�Q�[���I�u�W�F�N�g���X�g
    [SerializeField]
    private List<GameObject> _arms_l;       // ���r�Q�[���I�u�W�F�N�g���X�g
   
    // ---- �t�^���w�̖ڕW�ʒu��ݒ� ----
    [SerializeField]
    private List<GameObject> _arms_r_TG;    // �e�E�r�Q�[���I�u�W�F�N�g�̖ڕW�ʒu���X�g
    [SerializeField]
    private List<GameObject> _arms_l_TG;    // �e���r�Q�[���I�u�W�F�N�g�̖ڕW�ʒu���X�g

    // ---- �t�^���w�̖ڕW�ʒu�֒Ǐ]�����郊���N��ݒ� ----
    [SerializeField]
    private List<GameObject> _arms_r_EE;
    [SerializeField]
    private List<GameObject> _arms_l_EE;

    GameObject _root;
   
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        //transform.position += new Vector3(horizontal * Time.deltaTime * 5, 0, 0);

        for(int i = 0; i < _arms_r.Count; i++)
        {
            _arms_r[i].GetComponent<n_MAS>().CalcIKPosAll(Time.deltaTime, _arms_r_EE[i].transform, _arms_r_TG[i].transform);
           
        }
        for(int i = 0; i < _arms_l.Count; i++)
        {
            _arms_l[i].GetComponent<n_MAS>().CalcIKPosAll(Time.deltaTime, _arms_l_EE[i].transform, _arms_l_TG[i].transform);
        }

        if(_target.transform.position.x - transform.position.x < 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        } else
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
    }
}
