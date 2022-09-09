using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DirtMove : MonoBehaviour
{
    void FixedUpdate()
    {
        this.transform.position += new Vector3(-0.3f, 0, 0);   
        if(this.transform.position.x < -15)
        {
            Destroy(this.gameObject);
        }
    }
}
