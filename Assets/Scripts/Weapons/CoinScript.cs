using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class CoinScript : MonoBehaviour
{
    string _prevTarget = null;
    public void Hasbeenhit(GameObject _hitType)
    {


        StartCoroutine(ReflectShot(_hitType));

         
    }

    IEnumerator ReflectShot(GameObject _hitType)
    {
        yield return new WaitForSeconds(0.1f);

        string _objectName = gameObject.name;



        if (_hitType.name != "EnergyPistol")
        {
            _prevTarget = _hitType.name;
            Destroy(_hitType);
        }
        else if (_hitType.name == "EnergyPistol")
        {
            _prevTarget = _hitType.name;
        }

        

        float _oldDistance = 10000f;


        //check if there are any other coins in the vicinity
        GameObject[] _possibleTargets;
        _possibleTargets = GameObject.FindGameObjectsWithTag("Hittable");
        GameObject _closestTarget = null;
        foreach (GameObject _target in _possibleTargets)
        {
            print(_target.transform.parent.name);
            if (_target.transform.parent.name != gameObject.name)
            {
                if (_target.transform.parent.name != _prevTarget)
                {
                    Vector3 _diff = _target.transform.parent.position - transform.position;
                    float _curDistance = _diff.sqrMagnitude;
                    if (_curDistance < _oldDistance)
                    {
                        _closestTarget = _target;
                        _oldDistance = _curDistance;

                    }
                }

            }

        }

        //check if the next coin is the same as the previous. if it is then check for the next one, if it isn't then reflect the shot into another coin and if it cannot find another coin then shoot the nearest enemy's weakpoint,
        if (_closestTarget.transform.parent.gameObject.tag == "Coin")
        {
            print(_closestTarget.transform.parent.name);

            RaycastHit _hit;
            Vector3 _direction = _closestTarget.transform.parent.position - transform.position;

            Physics.Raycast(transform.position, _direction, out _hit);
            Debug.DrawRay(transform.position, _direction, Color.magenta, 1f);

            if (_hit.collider.isTrigger)
            {
                if (_hit.collider.gameObject.transform.parent.tag == "Coin")
                {
                    _hit.collider.gameObject.transform.parent.GetComponent<CoinScript>().Hasbeenhit(gameObject);
                    

                }
            }
        }
        else if (_closestTarget.transform.parent.gameObject.tag == "Enemy")
        {
            RaycastHit _hit;
            Vector3 _direction = _closestTarget.transform.parent.position - transform.position;

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
