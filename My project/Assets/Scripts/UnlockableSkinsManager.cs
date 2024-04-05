using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UnlockableSkinsManager : MonoBehaviour
{
    public static UnlockableSkinsManager Instance { get; private set; }
    
    public int currentIndex = 0;
    public GameObject[] ShopItem;
    public GameItem[] shopDetails;
    public Button BuyButton;
    public Button SellButton;

    public GameObject[] InventorySkins;
    public bool AllowToSell;
    
    // Start is called before the first frame update
    void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        foreach (GameItem item in shopDetails)
        {
            if (item.price == 0)
                item.isUnlocked = true;
            else
                item.isUnlocked = PlayerPrefs.GetInt(item.name, 0) == 0 ? false : true;
        }
        
        foreach (GameObject item in ShopItem)
        {
            item.SetActive(false);
        }
        
        foreach (GameObject skin in InventorySkins)
        {
            skin.SetActive(false);
        }
    }


    public void Buy()
    {
        GameItem item = shopDetails[currentIndex];
        PlayerPrefs.SetInt(item.name, 1);
        PlayerPrefs.SetInt("Skin", currentIndex);
        item.isUnlocked = true;
        PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) - item.price);
        UpdateUI();
        
        // Update inventory visibility
        CheckIfSkinWasPurchased();
    }  
    
    public void Sell()
    {
        if (CharacterController2D.Instance.SkinEquipped)
        {
            Debug.LogError("UNeQUIP SKIN BEFORE TRYING TO SELL");
            return;
        }
            
        
        GameItem item = shopDetails[currentIndex];

        // Check if the item can be sold (not a default skin and is currently unlocked)
        if (item.price > 0 && item.isUnlocked)
        {
            int sellPrice = Mathf.FloorToInt(item.price * 0.5f); // Sell for half the purchase price, adjust as needed

            // Update player's money
            PlayerPrefs.SetInt("Money", PlayerPrefs.GetInt("Money", 0) + sellPrice);

            // Mark the item as locked again
            PlayerPrefs.SetInt(item.name, 0);
            item.isUnlocked = false;


            PlayerPrefs.SetInt("Skin", 0); 

            UpdateUI();

            // Update inventory visibility
            CheckIfSkinWasPurchased();
        }
        else
        {
            Debug.LogError("This item cannot be sold.");
        }
    }
  
    public void UpdateUI()
    {
        OpenInventory.Instance.UpdateCoinText();
        
        if (currentIndex < 0 || currentIndex >= shopDetails.Length)
        {
            Debug.LogError($"currentIndex ({currentIndex}) is out of bounds.");
            return; 
        }

        GameItem item = shopDetails[currentIndex];

        if (item.isUnlocked && AllowToSell)
        {
            SellButton.gameObject.SetActive(true);
        }
        else
        {
            SellButton.gameObject.SetActive(false);
        }
        
        SellButton.GetComponentInChildren<TextMeshProUGUI>().text = "Sell FOR " +  Mathf.FloorToInt(item.price * 0.5f);
        
        if (item.isUnlocked)
        {
            BuyButton.gameObject.SetActive(false);
        }
        else
        {
            BuyButton.gameObject.SetActive(true);
            BuyButton.GetComponentInChildren<TextMeshProUGUI>().text = "" + item.price;
            if (item.price <= PlayerPrefs.GetInt("Money", 0))
            {
                BuyButton.interactable = true;
            }
            else
            {
                BuyButton.interactable = false;
            }
        }
    }

    public void CheckIfSkinWasPurchased()
    {
        foreach (GameItem item in shopDetails)
        {
            if (item.isUnlocked)
            {
                InventorySkins[item.index].SetActive(true);
            }
            else
            {
                InventorySkins[item.index].SetActive(false);
            }
        }
    }
}