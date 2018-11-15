using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public enum BoxType
{
    Tree     = 0,
    Eye      = 1,
    Third    = 2
}

public class Box : MonoBehaviour
{
    public static event Action<Platform> OnCollide;

    [SerializeField] private BoxType boxType;

    private Vector3 scalePerSecond;

    public BoxType BoxType
    {
        get { return boxType; }
    }

    public void StartFalling()
    {
        transform.parent = null;
    }
    
    private void OnCollisionEnter2D(Collision2D other)
    {
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

            Spikes spikes = other.collider.GetComponentInParent<Spikes>();
            if (spikes != null)
            {
                GameManager.Instance.LoseGame(LevelManager.Instance.CurrentLevel.Score);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.transform.CompareTag("Star"))
        {
            int starsCount = PlayerPrefs.GetInt(Prefs.STARS, 0);
            starsCount++;
            PlayerPrefs.SetInt(Prefs.STARS, starsCount);
            Destroy(other.gameObject);
        }
    }
}
