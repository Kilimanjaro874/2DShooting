using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMove : MonoBehaviour
{
    [SerializeField]
    private Rigidbody2D _rb2D;              // ƒwƒŠ‚ÌƒŠƒWƒbƒhƒ{ƒfƒB2DŽæ“¾
    [SerializeField]
    private Transform _heliTargetPos;       // ƒwƒŠ‚ÌˆÊ’uPID§Œä–Ú•WˆÊ’u
    // ˆÊ’u§Œä—pƒpƒ‰ƒ[ƒ^
    Vector3 _preErrorPos = new Vector3(0, 0, 0);
    Vector3 _integralError = new Vector3(0, 0, 0);
    [SerializeField]
    float _velMax = 5f;     // ‘¬“xŒÀŠE
    [SerializeField]
    float _kp = 16f;        // P§ŒäŒW”  
    [SerializeField]
    float _ki = 0.01f;      // I§ŒäŒW”
    [SerializeField]
    float _kd = 3.5f;       // D§ŒäŒW”
    [SerializeField]
    float _angleLim = 20f;  // ƒwƒŠŒXŽÎŠp“xŒÀŠE(deg) 
    [SerializeField]
    float _dth = 1f;  // ƒwƒŠŒXŽÎŠp“x§ŒäŒW”

  


    private void Update()
    {
        // ƒ_ƒ[ƒW‚É‰ž‚¶‚ÄƒS[ƒ‹ƒhƒhƒƒbƒv
    }

    private void FixedUpdate()
    {
        // ƒwƒŠ‚ðˆÊ’u§Œäi‚Ó‚í‚Ó‚í”ò‚ñ‚Å‚¢‚éŠ´‚¶‚ðÄŒ»•§ŒäŒW”‚ÅŠ´‚¶‚Ì’²®‚µ‚½‚¢)
        HeliController(Time.deltaTime, _heliTargetPos);
    }

    

    private void HeliController(float delta_time, Transform targetPos)
    {
        // --- ƒwƒŠƒRƒvƒ^[‚ÌˆÊ’uPID§Œä --- //
        // -- Œë·ŒvŽZ -- //
        Vector3 posError = targetPos.position - transform.position;
        // -- §Œä“ü—Í¶¬ -- //
        _integralError = (posError + _preErrorPos) / 2 * delta_time;
        Vector3 uf =    _kp * posError + 
                        _ki * _integralError + 
                        _kd * (posError - _preErrorPos) / delta_time;
        // -- §Œä“ü—Í‚ðƒŠƒWƒbƒhƒ{ƒfƒB‚Ö”½‰f -- //
        Vector2 uf2D = uf;  // 3D -> 2D(x, y)—Ž‚Æ‚µž‚Ý
        _rb2D.AddForce(uf);
        // ‘¬“x§ŒÀ
        if(_rb2D.velocity.magnitude >= _velMax)
        {
            _rb2D.velocity = _rb2D.velocity.normalized * _velMax;
        }

        _preErrorPos = posError;    // ¡ƒtƒŒ[ƒ€‚ÌŒë·‚ðŠi”[‚µ‚Ä‚¨‚­

        // --- ƒwƒŠƒRƒvƒ^[‚ÌŽp¨…•½§Œä --- //
        // -- …•½ˆÚ“®’†‚Ì‘¬“x‚É‰ž‚¶‚ÄŒXŽÎŠp“x•Ï‰» 

        Vector3 rotEuler = this.transform.rotation.eulerAngles;
        float rotz = 0;
        // Šp“x‚ð180 ~ -180(deg)‚É•ÏŠ·
        if(rotEuler.z > 180)
        {
            rotz = rotEuler.z - 360f;
        } else
        {
            rotz = rotEuler.z;
        }

        // ‘Oi’†
        if (_rb2D.velocity.x >= 0)
        {
            // ‘OŒXŠp“x‚É‚µ‚½‚¢
            if (rotz >= -_angleLim)
            {
                transform.Rotate(0, 0, -_dth);
            }
            else if (rotz < -_angleLim)
            {
                transform.Rotate(0, 0, _dth);
            }
        }
        else if (_rb2D.velocity.x < 0)
        {
            if (rotz >= _angleLim)
            {
                transform.Rotate(0, 0, -_dth);
            }
            else if (rotz < _angleLim)
            {
                transform.Rotate(0, 0, _dth);
            }
        }

    }

}
