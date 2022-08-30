using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerMove : MonoBehaviour
{
    /// <summary>
    /// �L�����N�^�[���L�[���͂œ������N���X�B
    /// �܂��A�L�����N�^�[�̋t�^���w�����������Ȃ���A�X���[�Y�ɓ��삳����
    /// </summary>

    [SerializeField]
    private Transform _target;

    // ---- �t�^���w�������Ώۂ�ݒ� ----
    [SerializeField]
    private List<GameObject> _arms_r;
    [SerializeField]
    private List<GameObject> _arms_l;
    [SerializeField]
    private List<GameObject> _legs_r;
    [SerializeField]
    private List<GameObject> _legs_l;

    // ---- �t�^���w�̖ڕW�ʒu��ݒ� ----
    [SerializeField]
    private List<GameObject> _arms_r_TG;
    [SerializeField]
    private List<GameObject> _arms_l_TG;


    // ---- �t�^���w�̖ڕW�ʒu�֒Ǐ]�����郊���N��ݒ� ----
    [SerializeField]
    private List<GameObject> _arms_r_EE;
    [SerializeField]
    private List<GameObject> _arms_l_EE;




    GameObject _root;
    // Start is called before the first frame update
    void Start()
    {
      

    }

    // Update is called once per frame
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
