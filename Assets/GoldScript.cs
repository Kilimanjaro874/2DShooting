using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldScript : MonoBehaviour
{
    [SerializeField]
    private int _score = 100;

    public int GetScore()
    {
        // ƒXƒRƒA‚ÌŽó‚¯“n‚µ
        return _score; 
    }
}
