using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    [SerializeField]
    private int _score = 100;
    [SerializeField]
    private Rigidbody2D _rb2;
    private bool _boundFlag = false;

    private void FixedUpdate()
    {
        // -- �n�ʂɏՓ˂��Ȃ������ꍇ�̃f�X�g���C���{(�O�̂���) -- //
        if(this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // --- �n�ʂɐڐG�����ꍇ(tag��r)�͖{�I�u�W�F�N�g�j�� --- //
        if (other.gameObject.CompareTag("Ground"))
        {
            // ���ڂ̒��n�F�o�E���h������(�v���C���[�ɉ���̃`�����X��^����)
            if (!_boundFlag)
            {
                _boundFlag = true;
                _rb2.velocity = new Vector3(_rb2.velocity.x, -_rb2.velocity.y * 0.8f, 0);
            }
            // �����̎���(s)��A���g��j��
            StartCoroutine(CountDestroy(10f));

        }
    }

    private IEnumerator CountDestroy(float destroyNum)
    {
        // --- �R���[�`���F���g�̔j�� --- //
        yield return new WaitForSeconds(destroyNum);
        Destroy(this.gameObject); 
    }

    public int GetScore()
    {
        // �X�R�A�̎󂯓n��
        return _score;
    }
}
