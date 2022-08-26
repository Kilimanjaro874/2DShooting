using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb2D;
    [SerializeField]
    private float _topSpeed = 10.0f;
    [SerializeField]
    private float _frontTorqueCoff = 0.5f;
    [SerializeField]
    private float _backTorqueCoff = 1.0f;
    [SerializeField]
    private float _inputHor = 0;

    private void Update()
    {
        _inputHor = Input.GetAxisRaw("Horizontal");
        this.transform.position = _rb2D.transform.position;
        this.transform.rotation = _rb2D.transform.rotation;
    }

    private void FixedUpdate()
    {
        if(Mathf.Abs(_rb2D.velocity.x) < _topSpeed)
        {
            // ì¸óÕÅFâ¡ë¨
            if (_inputHor >= 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _frontTorqueCoff, 0));

            }

            // ì¸óÕÅFå∏ë¨
            if (_inputHor < 0)
            {
                _rb2D.AddForce(new Vector2(_inputHor * _backTorqueCoff, 0));
            }
        }
    }


}
