using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bulletMove : MonoBehaviour
{
    [SerializeField]
    private float _bulletSpeed = 1f;
    [SerializeField]
    private float _deleteDistance = 100f;
    [SerializeField]
    private int _bulletDamage = 10;
    private bool _init = false;
    private Vector3 _startPos = Vector3.zero;


    private void Start()
    {
       
    }
    void FixedUpdate()
    {
        if (!_init)
        {
            _startPos = this.transform.position;
            _init = true;
        }

        this.transform.position += transform.up * _bulletSpeed;

        if((transform.position - _startPos).magnitude > _deleteDistance)
        {
            Destroy(this.gameObject);
        }
    }

    public int GetBulletDamage()
    {
        return _bulletDamage;
    }
}
