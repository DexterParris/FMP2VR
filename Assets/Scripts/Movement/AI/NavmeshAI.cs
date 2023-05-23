using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAI : MonoBehaviour
{
    public Transform[] _points;

    public int _health;


    [SerializeField] private Animator _anim;

    private NavMeshAgent _nav;
    private int _destPoint;

    private GameManager _gameManager;

    private Transform _playerTransform;
    CapsuleCollider _collider;

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
        _playerTransform = GameObject.Find("Camera Offset").transform;
        _points[0] = _playerTransform;
        _collider = gameObject.GetComponent<CapsuleCollider>();
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
    }

    private void Update()
    {
        

        if(_health <= 0)
        {
            _anim.SetBool("IsDead", true);
            Destroy(_nav);
            StartCoroutine(Die());
        }
        else
        {
            float _playerdistance = Vector3.Distance(_playerTransform.position, transform.position);


            if (_playerdistance <1f && _nav.velocity.magnitude < 0.1f)
            {
                _anim.SetTrigger("Attack");
            }
            else
            {
                if (!_nav.pathPending && _nav.remainingDistance < 1.1f)
                {

                    GoToNextPoint();
                }



                _anim.SetFloat("Speed", _nav.velocity.magnitude);
                float distance = Vector3.Distance(_playerTransform.position, _nav.destination);
                if (distance > 1)
                {
                    GoToNextPoint();
                }
            }

            
        }

        
        
    }


    void GoToNextPoint()
    {
        if(_points.Length == 0)
        {
            return;
        }
        _nav.destination = _points[_destPoint].position;
        _destPoint = (_destPoint+1) % _points.Length;
    }

    public void TakeDamage()
    {
        _health--;
    }

    IEnumerator Die()
    {
        Destroy(_collider);
        yield return new WaitForSeconds(3.1f);
        _gameManager.EnemyKilled();
        Destroy(gameObject);
    }
}
