using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseChaice : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 target;

    void Update()
    {
        // -- �e�̏Ə����_�l�F�}�E�X�ʒu���擾 -- //
        mouse = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 0f));
        target = new Vector3(target.x, target.y, 0f);

        // -- �e��
        this.transform.position = target;
    }

    private void FixedUpdate()
    {
        // -- �e�̏Ə��ʒu���_�l�ɑ΂��A�^�[�Q�b�g��PID����ŒǏ]������ -- //
        /*
         * �����Đ���𓱓����鎖�ŁA�e�́u��U��v���Č��B
         */
    }

}
