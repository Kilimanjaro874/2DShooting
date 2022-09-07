using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : MonoBehaviour
{
    [SerializeField]
    private Transform _muzzlePos;       // �e���ʒu
    [SerializeField]
    private Transform _targetPos;       // �Ə��ʒu
    [SerializeField]
    private GameObject _bullet;         // �e�ۃI�u�W�F�N�g�F�C���X�^���X����p
    [SerializeField]
    private float _reloadTime = 10.2f;  // �e�ۃ����[�h����
    private float _reloadCount = 0f;    // �����[�h���ԃJ�E���g�p
    private bool _shotFlag = false;     // �v���C���[�ˌ����̓t���O����
    private bool _shotedFlag_1f = false;   // �ˌ���1�t���[���̂�True�ƂȂ�t���O


    void Update()
    {
        // �����[�h���ς�ł���Βe�۔���
        Shot();
    }

    void Shot()
    {
        // �����[�h���Ԋ�����Fire1�R�}���h���s���A�e�ۃC���X�^���X���������␳
        if (Count(false) && _shotFlag)
        {
            // �ˌ�����
            Vector3 dir = _targetPos.position - _muzzlePos.position;
            dir.Normalize();    // �ˌ������P�ʃx�N�g��
            float angle = Mathf.Atan2(dir.x, dir.y);
            // �e�̐���
            var bullet = Instantiate(_bullet, _muzzlePos.position, Quaternion.Euler( 0, 0, -1*Mathf.Rad2Deg * angle ));
            Count(true);    // �����[�h���ԊJ�n
            _shotedFlag_1f = true;  // �ˌ������t���O�L����
            return;             
        }
        _shotedFlag_1f = false;     //�ˌ������t���O������
    }

    bool Count(bool reset)
    {
        // �����[�h���Ԃ̊Ǘ�
        if (reset) {_reloadCount = 0f; }
        _reloadCount += Time.deltaTime;
        if (_reloadCount <= _reloadTime) return false;
        return true;
    }

    public void GetShotInput(float fire)
    {
        // -- �}�l�[�W���[����ˌ����ߎ�t -- //
        _shotFlag = (fire > 0);
    }

    public bool ShotedFlag_1f()
    {
        return _shotedFlag_1f;
    }

    public void SetReloadTime(float reloadTime)
    {
        _reloadTime = reloadTime;
    }

}
