using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinScript : MonoBehaviour
{
    public void Hasbeenhit(GameObject _hitType)
    {
        string _ignoreTarget = null;
        //check if the hit comes from another coin
        if (_hitType.tag == "coin")
        {
            _ignoreTarget = _hitType.gameObject.name;
        }


        //check if there are any other coins in the vicinity
        GameObject[] _possibleTargets;
        _possibleTargets = GameObject.FindGameObjectsWithTag("Hittable");
        GameObject _closestTarget = null;
        foreach (GameObject _target in _possibleTargets)
        {
            Vector3 _distance = _target.transform.position - transform.position;
            float _curDistance = _distance.sqrMagnitude;
            if(_curDistance < Mathf.Infinity)
            {
                if(_target.gameObject.name != _ignoreTarget)
                {
                    _closestTarget = _target;
                }
                
            }
        }

        //check if the next coin is the same as the previous. if it is then check for the next one, if it isn't then reflect the shot into another coin and if it cannot find another coin then shoot the nearest enemy's weakpoint,
        if (_closestTarget.transform.parent.gameObject.tag == "Coin")
        {
            RaycastHit _hit;
            Vector3 _direction = _closestTarget.transform.position - transform.position;

            Physics.Raycast(transform.position, _direction,out _hit);
            Debug.DrawRay(transform.position, _direction, Color.magenta, 1f);
            
            if (_hit.collider.isTrigger)
            {
                if (_hit.collider.gameObject.tag == "Coin")
                {
                    _hit.collider.gameObject.GetComponent<CoinScript>().Hasbeenhit(gameObject);
                }
            }
        }
        else if (_closestTarget.transform.parent.gameObject.tag == "Enemy")
        {
            RaycastHit _hit;
            Vector3 _direction = _closestTarget.transform.position - transform.position;

            Physics.Raycast(transform.position, _direction, out _hit);
            Debug.DrawRay(transform.position, _direction, Color.magenta, 1f);

            if (_hit.collider.isTrigger)
            {
                if (_hit.collider.gameObject.tag == "WeakPoint")
                {
                    _hit.collider.gameObject.GetComponent<CoinScript>().Hasbeenhit(gameObject);
                }
            }
        }

         
    }
}
