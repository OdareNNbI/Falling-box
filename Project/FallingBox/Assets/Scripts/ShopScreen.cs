using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


[System.Serializable]
public class ShopItem
{
    public Button buyButton;
    public Button chooseButton;
    public BoxType boxType;
    public int cost;
    public Text costText;
    public bool isAlwaysAvailable;

    public void Initialize()
    {
        bool isAvailable = PlayerPrefs.GetInt(Prefs.BOX_PREFIX + ((int) boxType).ToString(), 0) == 1 ||
                           isAlwaysAvailable;

        if (isAvailable)
        {
            chooseButton.gameObject.SetActive(true);
            buyButton.gameObject.SetActive(false);
            
            chooseButton.onClick.AddListener(() =>
            {
                PlayerPrefs.SetInt(Prefs.CURRENT_BOX, ((int)boxType));
            });
        }
        else
        {
            chooseButton.gameObject.SetActive(false);
            buyButton.gameObject.SetActive(true);
            buyButton.onClick.AddListener(() =>
            {
                int currentStart = PlayerPrefs.GetInt(Prefs.STARS, 0);
                if (currentStart >= cost)
                {
                    PlayerPrefs.SetInt(Prefs.STARS, currentStart - cost);
                    PlayerPrefs.SetInt(Prefs.BOX_PREFIX + ((int) boxType).ToString(), 1);
                    chooseButton.gameObject.SetActive(true);
                    buyButton.gameObject.SetActive(false);
            
                    chooseButton.onClick.AddListener(() =>
                    {
                        PlayerPrefs.SetInt(Prefs.CURRENT_BOX, ((int)boxType));
                    });
                }
            });

            costText.text = cost.ToString();
        }
    }
}


public class ShopScreen : BaseScreen
{
    [SerializeField] private List<ShopItem> shopItems;
    [SerializeField] private Text currentStarText;

    public override void ShowScreen()
    {
        base.ShowScreen();

        foreach (var shopItem in shopItems)
        {
            shopItem.Initialize();
        }
    }

    public void Back()
    {
        GameManager.Instance.OpenMenu();
    }

    private void Update()
    {
        currentStarText.text = PlayerPrefs.GetInt(Prefs.STARS, 0).ToString();
    }
}
