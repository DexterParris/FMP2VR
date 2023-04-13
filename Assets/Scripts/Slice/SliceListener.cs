using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SliceListener : MonoBehaviour
{
    bool _canSlice;

    public Slicer slicer;
    private void OnTriggerEnter(Collider other)
    {
        if (_canSlice)
        {
            _canSlice = false;
            slicer.isTouched = true;
        }
  
    }

    private void OnTriggerExit(Collider other)
    {
        _canSlice = true;
    }
}
