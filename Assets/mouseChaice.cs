using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseChaice : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 target;

    void Update()
    {
        // -- 銃の照準理論値：マウス位置を取得 -- //
        mouse = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 0f));
        target = new Vector3(target.x, target.y, 0f);

        // -- 銃の
        this.transform.position = target;
    }

    private void FixedUpdate()
    {
        // -- 銃の照準位置理論値に対し、ターゲットをPID制御で追従させる -- //
        /*
         * あえて制御を導入する事で、銃の「手振れ」を再現。
         */
    }

}
