using System.Collections;
using System.Collections.Generic;
using UnityEditor.Animations;
using UnityEngine;
using UnityEngine.AI;

public class NavmeshAI : MonoBehaviour
{
    public Transform[] _points;

    [SerializeField] private Animator _anim;
    [SerializeField] private BlendTree _animTree;

    private NavMeshAgent _nav;
    private int _destPoint;

    private void Start()
    {
        _nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        _anim.SetFloat("Speed", _nav.velocity.magnitude);
        
    }

    private void FixedUpdate()
    {
        if(!_nav.pathPending && _nav.remainingDistance < 0.5f)
        {
            GoToNextPoint();
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
}
