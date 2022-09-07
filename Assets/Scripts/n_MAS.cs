using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class n_MAS : MonoBehaviour
{

    // Setting
    [SerializeField]
    private float kp = 0.2f;
    private float _pre_dth;

    // 目標位置 - 現在位置に応じて逆運動学数値解を得る
    public void CalcIKPosAll(float delta_time, Transform Pe, Transform Pr)
    {
        CalcIKPos(delta_time, Pe, Pr, transform.forward, kp);
    }

    private void CalcIKPos(float delta_time, Transform Pe, Transform Pr, Vector3 Ai, float kp)
    {
        // n link の逆運動学を解くモジュールを自作。2次元平面にスケールダウン。
        Vector3 dPe = Pe.position - this.transform.position;    // 本座標 -> 現在の手先位置までのベクトル
        Vector3 dPr = Pr.position - this.transform.position;    // 本座標 -> 目標の手先位置までのベクトル
        Vector3 x = Vector3.Cross(dPr, Ai);                     // dPeをAi軸に垂直な平面に投影
        Vector3 y = Vector3.Cross(dPe, Ai);                     // dPr 〃
        // dPeをdPrにAi周りで近づけるdth計算＆係数(1以下)を乗じて過大な回転を防止する。また、delta_timeを乗じ、PC個体差での動作速度変化をを抑える.
        float dth = delta_time * 100f * kp * Mathf.Acos(Mathf.Clamp(Vector3.Dot(x, y) / x.magnitude / y.magnitude, -1, 1));     
        if(dth == float.NaN)
        {
            dth = 0f;
        }
        // 回転の方向判定
        Vector3 axis = Vector3.Cross(y, x) / x.magnitude / y.magnitude;
        float sign = Mathf.Sign(Vector3.Dot(Ai, axis));
        dth *= sign;
        // 回転用のクォータニオン生成
        Quaternion q = Quaternion.AngleAxis(dth * Mathf.Rad2Deg, Ai);
        this.transform.rotation = q * transform.rotation;
    }

}
