using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TotalValue : MonoBehaviour
{
    [SerializeField] public Wallet wallet;
    public const int COINS_ITEMS = 4;
    public const int BANKNOTES_ITEMS = 5;
    public TextMeshProUGUI total;

    void Start()
    {
        string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/wallet.json");
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        total.text = "UKUPNO: " + GetTotalValue().ToString() + " kn";
    }

    float GetTotalValue()
    {
        float totalValue = 0;
        for (int i = 0; i < COINS_ITEMS; i++)
        {
            totalValue += wallet.coins[i].quantity * wallet.coins[i].value;
        }
        for (int i = 0; i < BANKNOTES_ITEMS; i++)
        {
            totalValue += wallet.banknotes[i].quantity * wallet.banknotes[i].value;
        }
        return totalValue;
    }
}
