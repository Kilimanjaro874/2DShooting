using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _heliTargetPos;      // �w���̖ړI�ʒu
    [SerializeField]
    private GameObject _itemSpawner;        // �A�C�e���X�|�i�[
    [SerializeField]
    private List<Vector3> _enemyRandPos;    // �G�̈ړ��ʒu���i�[
    [SerializeField]
    private int _enemyRandPosNum = 10;      // �G�ړ��ʒu���w��
    [SerializeField]
    private float _moveTime = 3F;           // �G�̈ړ��X�p��(s)
    [SerializeField]
    private List<AudioClip> _audioClips;     // �I�[�f�B�I�N���b�v���X�g
    [SerializeField]
    private AudioSource _audioSource;       // �G�̃I�[�f�B�I�\�[�X
   
    private float _moveTimeCount = 0F;      // �G�̈ړ��X�p���J�E���g�p
    private int _damageTotal = 0;           // �_���[�W����
    private int _predamageTotal = 0;        // 1�t���[���O�̃_���[�W����(�ω����m�Ɏg�p)

    private float _heliMoveSoundCount = 0;      // �w�����ʉ����[�v�Đ��p�J�E���g
    private float _heliLoopSec = 3f;            // �w�����ʉ��Đ�����
    private bool _heliHitFlag_1f;               // �w���e�ۃq�b�g�t���O(1�t���[���L��)

    // �摜�擾
    private SpriteRenderer _heliImage;              // �w���摜�擾
    private float _heliDmgColorChangeTime = 0.05f;   // �w���_���[�W���A�J���[�`�F���W���鎞�ԊԊu
    private float _heliDmgColorChangeCount = 0f;    // �J���[�`�F���W���ԃJ�E���g

    private bool _gameEnd = false;              // �Q�[���I���t���O

    public enum _Item{                     // �A�C�e���񋓌^
        gold,
        bomb
    }
    _Item _item;

    private void Start()
    {
        // �w���̃����_���ړ��ʒu���擾����
        for(int i = 0; i < _enemyRandPosNum; i++)
        {
            int x = Random.Range(-4, 14);
            int y = Random.Range(8, 12);
            _enemyRandPos.Add(new Vector3(x, y, 0));
        }
        // ����4�̋��ʒu�͗~�����̂Œǉ�����
        _enemyRandPos.Add(new Vector3(-4, 14, 0));
        _enemyRandPos.Add(new Vector3(14, 14, 0));
        _enemyRandPos.Add(new Vector3(-4, 8, 0));
        _enemyRandPos.Add(new Vector3(14, 8, 0));
        // �w���摜�擾
        _heliImage = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        // -- �w���̖ڕW�ʒu�X�V(��莞�ԊԊu�ŁA���X�g�̃����_���ʒu���`���C�X) -- //
        if (TimeCounter(Time.deltaTime))
        {
            int no = Random.Range(0, _enemyRandPosNum + 4);
            _heliTargetPos.transform.position = _enemyRandPos[no];
        }
        // -- �I�[�f�B�I�Ǘ� -- //
        AudioController();
        // -- �_���[�W���J���[�`�F���W -- //
        DamageColorChange(Time.deltaTime);

        // �_���[�W�ω����m�p
        _predamageTotal = _damageTotal;
    }

    private bool TimeCounter(float delta_time)
    {
        // -- �w�����ړ����J�n���鎞�ԊԊu�J�E���g -- //
        _moveTimeCount += delta_time;
        if (_moveTimeCount < _moveTime) return false;
        _moveTimeCount = 0;
        return true;        // �J�E���g����������, true��Ԃ�
    }

    private void DamageColorChange(float delta_time)
    {
        
        if(_predamageTotal != _damageTotal)
        {
            _heliDmgColorChangeCount = _heliDmgColorChangeTime;
            _heliImage.color = new Color(1, 0.5f, 0.5f);  // �ԐF�ɕ\��
        }

        if(_heliDmgColorChangeCount >= 0)
        {
            _heliDmgColorChangeCount -= delta_time;
            return;
        }
        _heliImage.color = new Color(1, 1, 1);      // �f�t�H���g�J���[�ɖ߂�

    }

    void AudioController()
    {
        // -- �G�l�~�[�̌��ʉ����Ǘ����� -- //
        // �w�����쉹
        if(_heliMoveSoundCount == 0) { _audioSource.PlayOneShot(_audioClips[0], 0.5f); }
        _heliMoveSoundCount += Time.deltaTime;
        if(_heliMoveSoundCount > _heliLoopSec) { _heliMoveSoundCount = 0; }
        // �_���[�W��(�y)
        if(_damageTotal != _predamageTotal) { _audioSource.PlayOneShot(_audioClips[1], 0.5f); }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // -- �_���[�W����F�����I�ȓ����蔻�薳��ver -- //
        var bullet = other.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damageTotal += bullet.GetBulletDamage();    //  �_���[�W���Z
            Destroy(other.gameObject);              //  �e�ۏ���
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        // -- �_���[�W����: �����I�ȓ����蔻��L��ver -- //
        var bullet = collision.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damageTotal += bullet.GetBulletDamage();
            Destroy(collision.gameObject);              //  �e�ۏ���

        }
    }

    public int GetDamage()
    {
        return _damageTotal;
    }

    public void PopItem(_Item item, int num)
    {
        // -- �w��A�C�e�����w�萔�X�|�[�� -- //
        switch (item)
        {
            case _Item.gold:    // �S�[���h
                _itemSpawner.GetComponent<SpawnItem>().SpawnGold(num);
                break;
            case _Item.bomb:    // �O���l�[�h
                if (!_gameEnd) { 
                    _itemSpawner.GetComponent<SpawnItem>().SpawnBomb(num);
                }
                break;
        }
    }

    public void SetGameEnd()
    {
        _gameEnd = true;
    }

}