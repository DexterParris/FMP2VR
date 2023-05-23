using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class OffhandScript : MonoBehaviour
{
    [SerializeField] public InputActionProperty _leftTrigger;
    [SerializeField] private GameObject _coinPrefab;
    [SerializeField] private GameObject _hand;
    // Start is called before the first frame update

    private bool _canLaunch = true;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _triggerValue = _leftTrigger.action.ReadValue<float>();
        if (_triggerValue >= 0.4f && _canLaunch)
        {
            _canLaunch = false;
            StartCoroutine(Debounce());
        }
    }

    IEnumerator Debounce()
    {
        
        GameObject _coin = Instantiate(_coinPrefab, _hand.transform.position, transform.rotation);
        _coin.GetComponent<Rigidbody>().AddForce(0, 200, 0);
        yield return new WaitForSeconds(0.5f);
        _canLaunch = true;
    }


}
