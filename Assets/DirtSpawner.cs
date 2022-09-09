using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtSpawner : MonoBehaviour
{
    [SerializeField]
    private GameObject _dirt;

    // Update is called once per frame
    void FixedUpdate()
    {
        // - プレイヤーを邪魔する泥をスポーン
        if (UnityEngine.Random.Range(0, 500) > (500 - 5))
        {
            var dirt = Instantiate(_dirt, transform.position,Quaternion.identity);
        }
    }
}
