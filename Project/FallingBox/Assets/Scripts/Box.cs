using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Box : MonoBehaviour
{
    public static event Action<Platform> OnCollide;

    public void StartFalling()
    {
        transform.parent = null;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
        Platform platform = other.collider.GetComponentInParent<Platform>();
        if (platform != null)
        {
            transform.parent = other.transform;

            if (OnCollide != null)
            {
                OnCollide(platform);
            }
        }
    }
}
