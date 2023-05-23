using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEnemy : MonoBehaviour
{
    public GameObject _enemy;
    GameManager _gameManager;
    public bool _spawnOnStart;

    // Start is called before the first frame update
    void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        if( _spawnOnStart)
        {
            SpawnSingleEnemy();
        }
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SpawnSingleEnemy()
    {
        _gameManager.EnemyAdded();
        Instantiate(_enemy, transform.position, Quaternion.identity);
    }
}
