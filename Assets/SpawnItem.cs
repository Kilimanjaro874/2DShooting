using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _Gold;
    private bool _spawnFlag;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // �A�C�e���X�|�[����t
        if (Input.GetKeyDown(KeyCode.Space))
        {
            _spawnFlag = true;
        }
    }

    private void FixedUpdate()
    {
        if (_spawnFlag)
        {
            SpawnGold();
            _spawnFlag = false;
        }
    }

    public void SetSpawnFlag()
    {
        _spawnFlag = true;
    }

    public void SpawnGold()
    {
        // �S�[���h�C���X�^���X��
        var gold = Instantiate(_Gold);
        gold.transform.position = transform.position;
        var rg2D = gold.GetComponent<Rigidbody2D>();    // ���W�b�h�{�f�B�擾

        // �S�[���h�ˏo�������߂�̓x�N�g���쐬����p
        Vector2 forceDir = new Vector2(Random.Range(-10, 2), Random.Range(-3, 5));
        rg2D.AddForce(forceDir, ForceMode2D.Impulse);

    }
}
