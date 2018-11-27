using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum ScreenType
{
    Game        = 0,
    Menu        = 1,
    Lose        = 2,
    Shop        = 3
}

public class BaseScreen : MonoBehaviour
{
    [SerializeField] private ScreenType screenType;

    public ScreenType Type
    {
        get
        {
            return screenType;
        }
    }
    
    public virtual void ShowScreen()
    {
        gameObject.SetActive(true);
    }

    public virtual void HideScreen()
    {
        gameObject.SetActive(false);
    }
}
