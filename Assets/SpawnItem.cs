using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : MonoBehaviour
{
    [SerializeField]
    private GameObject _Gold;
    private bool _spawnFlag;

    public void SpawnGold(int num)
    {
        // �S�[���h�C���X�^���X��
        for (int i = 0; i < num; i++)
        {
            var gold = Instantiate(_Gold);
            gold.transform.position = transform.position;
            var rg2D = gold.GetComponent<Rigidbody2D>();    // ���W�b�h�{�f�B�擾
            // �S�[���h�ˏo�������߂�̓x�N�g���쐬����p
            Vector2 forceDir = new Vector2(Random.Range(-8, -2), Random.Range(-3, 2));
            rg2D.AddForce(forceDir, ForceMode2D.Impulse);
        }
    }
}
