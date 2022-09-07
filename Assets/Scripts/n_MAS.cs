using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_MAS : MonoBehaviour
{

    // Setting
    [SerializeField]
    private float kp = 0.2f;
    private float _pre_dth;

    // �ڕW�ʒu - ���݈ʒu�ɉ����ċt�^���w���l���𓾂�
    public void CalcIKPosAll(float delta_time, Transform Pe, Transform Pr)
    {
        CalcIKPos(delta_time, Pe, Pr, transform.forward, kp);
    }

    private void CalcIKPos(float delta_time, Transform Pe, Transform Pr, Vector3 Ai, float kp)
    {
        // n link �̋t�^���w���������W���[��������B2�������ʂɃX�P�[���_�E���B
        Vector3 dPe = Pe.position - this.transform.position;    // �{���W -> ���݂̎��ʒu�܂ł̃x�N�g��
        Vector3 dPr = Pr.position - this.transform.position;    // �{���W -> �ڕW�̎��ʒu�܂ł̃x�N�g��
        Vector3 x = Vector3.Cross(dPr, Ai);                     // dPe��Ai���ɐ����ȕ��ʂɓ��e
        Vector3 y = Vector3.Cross(dPe, Ai);                     // dPr �V
        // dPe��dPr��Ai����ŋ߂Â���dth�v�Z���W��(1�ȉ�)���悶�ĉߑ�ȉ�]��h�~����B�܂��Adelta_time���悶�APC�̍��ł̓��쑬�x�ω�����}����.
        float dth = delta_time * 100f * kp * Mathf.Acos(Mathf.Clamp(Vector3.Dot(x, y) / x.magnitude / y.magnitude, -1, 1));     
        if(dth == float.NaN)
        {
            dth = 0f;
        }
        // ��]�̕�������
        Vector3 axis = Vector3.Cross(y, x) / x.magnitude / y.magnitude;
        float sign = Mathf.Sign(Vector3.Dot(Ai, axis));
        dth *= sign;
        // ��]�p�̃N�H�[�^�j�I������
        Quaternion q = Quaternion.AngleAxis(dth * Mathf.Rad2Deg, Ai);
        this.transform.rotation = q * transform.rotation;
    }

}
