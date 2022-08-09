using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    GameObject _root;
    // Start is called before the first frame update
    void Start()
    {
        _root = GameObject.Find("momo_root");
        if( _root != null)
        {
            Debug.Log("Succes!");
        }

    }

    // Update is called once per frame
    void Update()
    {
        _root.transform.localEulerAngles += new Vector3(0, 0, 1);
    }
}
