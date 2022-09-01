using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    private int _damage = 0;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

        Debug.Log(_damage);
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var bullet = other.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damage += bullet.GetBulletDamage();
            Destroy(other.gameObject);
        }
        

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        var bullet = collision.gameObject.GetComponent<bulletMove>();
        if (bullet)
        {
            _damage += bullet.GetBulletDamage();
        }
 
    }
}
