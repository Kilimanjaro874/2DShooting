using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class mouseChaice : MonoBehaviour
{
    private Vector3 mouse;
    private Vector3 target;

    void Update()
    {
        mouse = Input.mousePosition;
        target = Camera.main.ScreenToWorldPoint(new Vector3(mouse.x, mouse.y, 0f));
        target = new Vector3(target.x, target.y, 0f);
        this.transform.position = target;
    }
}
