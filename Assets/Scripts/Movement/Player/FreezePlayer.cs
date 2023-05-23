using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class FreezePlayer : MonoBehaviour
{

    [SerializeField] private float _delay;

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Player")
        {
            StartCoroutine(DelayFreeze(_delay,other));
        }
        
    }

    IEnumerator DelayFreeze(float _delayTime,Collider other)
    {
        yield return new WaitForSeconds(_delayTime);
        other.gameObject.transform.parent.gameObject.GetComponent<LocomotionSystem>().enabled = false;
    }
}
