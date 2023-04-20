using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OffhandScript : MonoBehaviour
{
    [SerializeField] public InputActionProperty _leftTrigger;
    [SerializeField] private GameObject _coinPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _triggerValue = _leftTrigger.action.ReadValue<float>();
        if (_triggerValue >= 0.4f)
        {
            GameObject _coin = Instantiate(_coinPrefab, transform.position, transform.rotation);
            _coin.GetComponent<>
        }
    }


}
