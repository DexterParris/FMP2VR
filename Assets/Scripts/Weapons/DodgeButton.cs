using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DodgeButton : MonoBehaviour
{
    [SerializeField] private bool _isRight;
    public ShipScript _shipScript;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag != null && other.gameObject.tag == "Player")
        {
            _shipScript.DoABarrelRoll(_isRight);
        }
    }
}
