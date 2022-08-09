using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_MAS : MonoBehaviour
{

    // Setting
    [SerializeField]
    private float kp = 0.2f;
    private float _pre_dth;

    public void CalcIKPosAll(float delta_time, Transform Pe, Transform Pr)
    {
        CalcIKPos(delta_time, Pe, Pr, transform.forward, kp);
    }

    private void CalcIKPos(float delta_time, Transform Pe, Transform Pr, Vector3 Ai, float kp)
    {
        Vector3 dPe = Pe.position - this.transform.position;
        Vector3 dPr = Pr.position - this.transform.position;
        Vector3 x = Vector3.Cross(dPr, Ai);
        Vector3 y = Vector3.Cross(dPe, Ai);
        float dth = delta_time * 100f * kp * Mathf.Acos(Mathf.Clamp(Vector3.Dot(x, y) / x.magnitude / y.magnitude, -1, 1));
        if(dth == float.NaN)
        {
            dth = 0f;
        }
        Vector3 axis = Vector3.Cross(y, x) / x.magnitude / y.magnitude;
        float sign = Mathf.Sign(Vector3.Dot(Ai, axis));
        dth *= sign;

        Quaternion q = Quaternion.AngleAxis(dth * Mathf.Rad2Deg, Ai);
        this.transform.rotation = q * transform.rotation;
    }

}
