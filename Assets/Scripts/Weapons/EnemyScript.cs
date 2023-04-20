using System.Collections;
using System.Collections.Generic;
using System.Net.Sockets;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    public float _maxhealth;
    public float _health;

    public GameObject _bloodSplatter;
    bool _isAlive = true;


    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void DoMove()
    {
        if (_isAlive)
        {

        }
    }

    public void DoDamage(float _damage, Vector3 _hitPosition)
    {
        if(_health > _maxhealth)
        {
            _health = _maxhealth;
        }

        if(_health <= 0)
        {

            GameObject _blood = Instantiate(_bloodSplatter,_hitPosition,transform.rotation);
            _blood.transform.parent = gameObject.transform;

            _isAlive = false;
        }
    }

}
