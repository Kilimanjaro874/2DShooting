using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombControll : MonoBehaviour
{
    [SerializeField]
    private float _bombDeleteTime = 0.5f;   // ������A�I�u�W�F�N�g���폜����܂ł̎��� 
    private float _bombCount = 0f;
    private ParticleSystem _ps;
    private SpriteRenderer _bomb;
    [SerializeField]
    private GameObject _effect;
    private bool _bombed = false;
    // Start is called before the first frame update
    private Vector3 _bombPos;               // ���e�����ʒu�Œ�p
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private List<AudioClip> _audioClips;

    private bool _init = false;

    private void FixedUpdate()
    {
        // �n�ʂɏՓ˂��Ȃ������ꍇ�̃f�X�g���C���{(�O�̂���)
        if(this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // ��������
        if (!_init)
        {
            // �����鉹�Đ�
            _audioSource.PlayOneShot(_audioClips[0]);
            _init = true;
        }
        // �ʏ폈��

        // ���j����
        if(!_bombed) { return; }
        transform.position = _bombPos;
        _bombCount += Time.deltaTime;
        if(_bombCount <= _bombDeleteTime) { return; }
        Destroy(this.gameObject);
       

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!_bombed && !collision.gameObject.CompareTag("Enemy") && !collision.gameObject.CompareTag("bomb"))
        {
            SetBomb();
            _bombPos = this.transform.position;
            _audioSource.PlayOneShot(_audioClips[1]);
            
        }
    }

    private void SetBomb()
    {
        if (_bombed) { return; }
        // ���e���ŃJ�E���g�X�^�[�g
        _bombed = true;
        // ���j�G�t�F�N�g�J�n
        _ps = GetComponent<ParticleSystem>();
        Destroy(_effect);
        _bomb = GetComponent<SpriteRenderer>();
        _ps.Play();
        // ����������
        _bomb.color = new Color(1, 1, 1, 0);
    }
    
}
