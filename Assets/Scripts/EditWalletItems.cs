﻿using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class EditWalletItems : MonoBehaviour
{
    [SerializeField] public Wallet wallet;
    public const int COINS_ITEMS = 4;
    public const int BANKNOTES_ITEMS = 5;
    public bool showCoins = true;
    public int currentItemIndex = 0;
    public Button walletItemPlaceholder;
    public TextMeshProUGUI description;
    public TMP_InputField quantityInput;

    void Start()
    {
        walletItemPlaceholder = gameObject.transform.GetChild(4).gameObject.GetComponent<Button>();
        description = gameObject.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>();
        quantityInput = gameObject.transform.GetChild(7).gameObject.GetComponent<TMP_InputField>();
        string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/wallet.json");
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        Sprite sprite = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageAPath);
        SetCurrentItem(wallet.coins[currentItemIndex], sprite);
    }

    void SetCurrentItem(WalletItem walletItem, Sprite sprite)
    {
        walletItemPlaceholder.image.sprite = sprite;
        description.text = walletItem.name + "\nUkupna vrijednost: " + (walletItem.quantity * walletItem.value).ToString() + " kn";
        quantityInput.text = walletItem.quantity.ToString();
    }

    public void UpdateCurrentItemIndex(int offset)
    {
        currentItemIndex += offset;
        if (showCoins)
        {
            currentItemIndex = (currentItemIndex >= 0) ? currentItemIndex % COINS_ITEMS : COINS_ITEMS-1;
            Sprite sprite = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageAPath);
            SetCurrentItem(wallet.coins[currentItemIndex], sprite);
        }
        else
        {
            currentItemIndex = (currentItemIndex >= 0) ? currentItemIndex % BANKNOTES_ITEMS : BANKNOTES_ITEMS-1;
            Sprite sprite = Resources.Load<Sprite>("Money/" + wallet.banknotes[currentItemIndex].imageAPath);
            SetCurrentItem(wallet.banknotes[currentItemIndex], sprite);
        }
    }

    public void SetShowCoins(bool sc)
    {
        if (showCoins != sc)
        {
            currentItemIndex = 0;
            WalletItem walletItem = sc ? wallet.coins[currentItemIndex] : wallet.banknotes[currentItemIndex];
            Sprite sprite = Resources.Load<Sprite>("Money/" + walletItem.imageAPath);
            SetCurrentItem(walletItem, sprite);
        }
        showCoins = sc;
    }

    public void UpdateQuantity(int offset)
    {        
        if (showCoins)
        {
            wallet.coins[currentItemIndex].quantity = System.Math.Max(0, wallet.coins[currentItemIndex].quantity + offset);
            Sprite a = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageAPath);
            Sprite b = Resources.Load<Sprite>("Money/" + wallet.coins[currentItemIndex].imageBPath);
            SetCurrentItem(wallet.coins[currentItemIndex], (walletItemPlaceholder.image.sprite == a) ? a : b);
        }
        else
        {
            wallet.banknotes[currentItemIndex].quantity = System.Math.Max(0, wallet.banknotes[currentItemIndex].quantity + offset);
            Sprite a = Resources.Load<Sprite>("Money/" + wallet.banknotes[currentItemIndex].imageAPath);
            Sprite b = Resources.Load<Sprite>("Money/" + wallet.banknotes[currentItemIndex].imageBPath);
            SetCurrentItem(wallet.banknotes[currentItemIndex], (walletItemPlaceholder.image.sprite == a) ? a : b);
        }
    }

    public void UpdateQuantityFromInput()
    {
        int value = System.Math.Abs(System.Int32.Parse(quantityInput.text));
        if (showCoins)
        {
            UpdateQuantity(value - wallet.coins[currentItemIndex].quantity);
        }
        else
        {
            UpdateQuantity(value - wallet.banknotes[currentItemIndex].quantity);
        }
    }

    public void SaveToJson()
    {
        System.IO.File.WriteAllText(Application.dataPath + "/Resources/wallet.json", JsonUtility.ToJson(wallet));
    }

    public void FlipMoney()
    {
        WalletItem currentWalletItem = (showCoins) ? wallet.coins[currentItemIndex] : wallet.banknotes[currentItemIndex];
        Sprite a = Resources.Load<Sprite>("Money/" + currentWalletItem.imageAPath);
        Sprite b = Resources.Load<Sprite>("Money/" + currentWalletItem.imageBPath);
        walletItemPlaceholder.image.sprite = (walletItemPlaceholder.image.sprite == a) ? b : a;
    }
}

[System.Serializable]
public class Wallet
{
    public List<WalletItem> coins = new List<WalletItem>();
    public List<WalletItem> banknotes = new List<WalletItem>();
}

[System.Serializable]
public class WalletItem
{
    public string name;
    public float value;
    public int quantity;
    public string imageAPath;
    public string imageBPath;
}
