using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class Trgovina : MonoBehaviour
{
    [SerializeField] Wallet wallet;
    int COINS_ITEMS, BANKNOTES_ITEMS;
    public TextMeshProUGUI total;

    static public float amount;
    static public Boolean krupno;
    public GameObject inputField;

    void Start()
    {
        krupno = false;
        string jsonString = System.IO.File.ReadAllText(StartMenu.walletSavePath);
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        COINS_ITEMS = wallet.coins.Count;
        BANKNOTES_ITEMS = wallet.banknotes.Count;
        total.text = "U novčaniku imam:\n " + GetTotalValue().ToString() + " kn";
    }

    void Update()
    {
        string input = inputField.GetComponent<TMP_InputField>().text;
        if (input.Length > 0) amount = float.Parse(input);
        else amount = 0;
    }

    double GetTotalValue()
    {
        double totalValue = 0;
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

    public void LargePayment()
    {
        krupno = true;
        SceneManager.LoadScene("Placanje");
    }
}
