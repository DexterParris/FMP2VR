using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [Tooltip("Trigger only one time")]
    [SerializeField] private bool _triggerOnce = false;
    private bool _hasBeenTriggered = false;
    private GameManager _gameManager;
    private SceneHandler _sceneHandler;

    [Tooltip("Triggers a scene change instead of an animator")]
    [SerializeField] private bool _changeSceneMode;

    [Tooltip("Triggers any connected spawners when activated")]
    [SerializeField] private bool _spawnerMode;

    [Tooltip("Scene to change to if ChangeSceneMode is enabled")]
    [SerializeField] private string _desiredScene;

    [Tooltip("How long after triggering should the scene change")]
    [SerializeField] private float _sceneChangeDelay;


    [Tooltip("All enemies must be killed before activating")]
    [SerializeField] private bool _requireAllEnemiesKilled;

    public Animator[] _animators;
    public string[] _boolNames;

    public SpawnEnemy[] _spawners;

    private void Start()
    {
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _sceneHandler = GameObject.Find("GameManager").GetComponent<SceneHandler>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != null && other.gameObject.tag == "Player")
        {
            if (_changeSceneMode)
            {
                if (_requireAllEnemiesKilled && _gameManager._numberOfEnemies != 0)
                {
                    //do nothing
                }
                else
                {
                    _sceneHandler.ChangeScene(_desiredScene, _sceneChangeDelay);
                }
                
            }
            else if (_spawnerMode)
            {
                if (_requireAllEnemiesKilled && _gameManager._numberOfEnemies != 0)
                {
                    //do nothing
                }
                else
                {
                    if(_hasBeenTriggered == false)
                    {
                        foreach(SpawnEnemy _spawner in _spawners)
                        {
                            _spawner.SpawnSingleEnemy();
                        }
                    }
                }

                if (_triggerOnce)
                {
                    _hasBeenTriggered = true;
                }
            }
            else
            {
                if (_requireAllEnemiesKilled && _gameManager._numberOfEnemies != 0)
                {
                    //do nothing
                }
                else
                {
                    if (_hasBeenTriggered == false)
                    {
                        int _arrayElement = 0;
                        foreach (Animator _anim in _animators)
                        {
                            _anim.SetBool(_boolNames[_arrayElement], true);
                            _arrayElement++;
                        }
                    }
                    if (_triggerOnce)
                    {
                        _hasBeenTriggered = true;
                    }
                }
            }

        }
    }
}
