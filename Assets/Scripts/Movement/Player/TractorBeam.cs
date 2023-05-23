using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TractorBeam : MonoBehaviour
{
    public Transform _player;
    public float _speed;
    // Start is called before the first frame update
    void Start()
    {
        //_player = GameObject.FindWithTag("Player").transform.parent;
        _player = _player.transform.parent.parent;
        
    }

    // Update is called once per frame
    void Update()
    {
        float step = _speed * Time.deltaTime;
        _player.position = Vector3.MoveTowards(_player.transform.position, transform.position, step);
    }
}
