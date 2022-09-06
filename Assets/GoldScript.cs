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
        // -- 地面に衝突しなかった場合のデストロイ実施(念のため) -- //
        if(this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        // --- 地面に接触した場合(tag比較)は本オブジェクト破壊 --- //
        if (other.gameObject.CompareTag("Ground"))
        {
            // 一回目の着地：バウンドさせる(プレイヤーに回収のチャンスを与える)
            if (!_boundFlag)
            {
                _boundFlag = true;
                _rb2.velocity = new Vector3(_rb2.velocity.x, -_rb2.velocity.y * 0.8f, 0);
            }
            // 引数の時間(s)後、自身を破壊
            StartCoroutine(CountDestroy(10f));

        }
    }

    private IEnumerator CountDestroy(float destroyNum)
    {
        // --- コルーチン：自身の破壊 --- //
        yield return new WaitForSeconds(destroyNum);
        Destroy(this.gameObject); 
    }

    public int GetScore()
    {
        // スコアの受け渡し
        return _score;
    }
}
