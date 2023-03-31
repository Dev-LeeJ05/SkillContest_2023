using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScrolling : MonoBehaviour
{
    public Renderer background;
    public float scrollingSpeed;
    void Start()
    {
        
    }

    void Update()
    {
        Scrolling();
    }

    void Scrolling()
    {
        background.material.mainTextureOffset = new Vector2(0, background.material.mainTextureOffset.y + Time.deltaTime * scrollingSpeed);
    }
}
