using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimTrigger : MonoBehaviour
{
    [SerializeField] private bool _triggerOnce = false;
    private bool _hasBeenTriggered = false;

    public Animator[] _animators;
    public string[] _boolNames;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != null && other.gameObject.tag == "Player")
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
