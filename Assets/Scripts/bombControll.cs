using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombControll : MonoBehaviour
{
    [SerializeField]
    private float _bombDeleteTime = 0.5f;   // 爆発後、オブジェクトを削除するまでの時間 
    private float _bombCount = 0f;
    private ParticleSystem _ps;
    private SpriteRenderer _bomb;
    [SerializeField]
    private GameObject _effect;
    private bool _bombed = false;
    // Start is called before the first frame update
    private Vector3 _bombPos;               // 爆弾爆発位置固定用
    [SerializeField]
    private AudioSource _audioSource;
    [SerializeField]
    private List<AudioClip> _audioClips;

    private bool _init = false;

    private void FixedUpdate()
    {
        // 地面に衝突しなかった場合のデストロイ実施(念のため)
        if(this.transform.position.y < -10)
        {
            Destroy(this.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        // 初期処理
        if (!_init)
        {
            // 投げる音再生
            _audioSource.PlayOneShot(_audioClips[0]);
            _init = true;
        }
        // 通常処理

        // 爆破処理
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
        // 爆弾消滅カウントスタート
        _bombed = true;
        // 爆破エフェクト開始
        _ps = GetComponent<ParticleSystem>();
        Destroy(_effect);
        _bomb = GetComponent<SpriteRenderer>();
        _ps.Play();
        // 爆発透明化
        _bomb.color = new Color(1, 1, 1, 0);
    }
    
}
