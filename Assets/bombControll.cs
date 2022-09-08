using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bombControll : MonoBehaviour
{
    private ParticleSystem _ps;
    private ParticleSystem _dangerPs;
    private SpriteRenderer _bomb;
    [SerializeField]
    private GameObject _effect;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.B))
        {
            _ps = GetComponent<ParticleSystem>();
            Destroy(_effect);
            
            _bomb = GetComponent<SpriteRenderer>();
            _ps.Play();
            _bomb.color = new Color(1, 1, 1, 0);
        }  
    }

   

    
}
