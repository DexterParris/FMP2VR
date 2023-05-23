using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public int _numberOfEnemies = 0;
    Canvas _canvas;
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _canvas = GetComponentInChildren<Canvas>();
        _canvas.worldCamera = Camera.main;
        _anim = GetComponentInChildren<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(_numberOfEnemies < 0)
        {
            _numberOfEnemies = 0;
        }
    }

    public void FadeToBlack()
    {
        _anim.SetTrigger("FadeToBlack");
    }

    public void EnemyAdded()
    {
        _numberOfEnemies++;
    }

    public void EnemyKilled()
    {
        _numberOfEnemies--;
    }
}
