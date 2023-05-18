using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollTexture : MonoBehaviour
{

    public float _scrollX;
    public float _scrollY;




    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float _offsetX = Time.time * _scrollX;
        float _offsetY = Time.time * _scrollY;

        GetComponent<Renderer>().material.mainTextureOffset = new Vector2(_offsetX, _offsetY);
    }
}
