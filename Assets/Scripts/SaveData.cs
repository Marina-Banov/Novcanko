using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SaveData : MonoBehaviour
{
    [SerializeField] private Wallet wallet = new Wallet();

    public void SaveToJson()
    {
        System.IO.File.WriteAllText(Application.persistentDataPath + "/wallet.json", JsonUtility.ToJson(wallet));
    }
}

[System.Serializable]
public class Wallet
{
    public List<WalletItem> walletItems = new List<WalletItem>();
}

[System.Serializable]
public class WalletItem
{
    public string name;
    public int value;
    public int quantity;
}
