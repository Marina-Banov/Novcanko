using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using static Wallet;

public class EditWalletItems : MonoBehaviour
{
    public const int COINS_ITEMS = 4;
    public const int BANKNOTES_ITEMS = 5;
    public bool showCoins = true;
    public Wallet wallet;
    public int currentItemIndex = 0;
    public Image walletItemPlaceholder;
    public TextMeshProUGUI description;

    // Start is called before the first frame update
    void Start()
    {
        walletItemPlaceholder = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();
        description = gameObject.transform.GetChild(9).gameObject.GetComponent<TextMeshProUGUI>();
        string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/wallet.json");
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        setCurrentItem(wallet.coins[currentItemIndex]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void setCurrentItem(WalletItem walletItem)
    {
        walletItemPlaceholder.sprite = Resources.Load<Sprite>("Money/kn5"); // TODO image not showing!!
        description.text = walletItem.name;
    }

    public void updateCurrentItemIndex(int offset)
    {
        currentItemIndex += offset;
        if (showCoins)
        {
            currentItemIndex = (currentItemIndex >= 0) ? currentItemIndex % COINS_ITEMS : COINS_ITEMS-1;
            setCurrentItem(wallet.coins[currentItemIndex]);
        }
        else
        {
            currentItemIndex = (currentItemIndex >= 0) ? currentItemIndex % BANKNOTES_ITEMS : BANKNOTES_ITEMS-1;
            setCurrentItem(wallet.banknotes[currentItemIndex]);
        }
    }

    public void setShowCoins(bool sc)
    {
        if (showCoins != sc)
        {
            currentItemIndex = 0;
            setCurrentItem(sc ? wallet.coins[currentItemIndex] : wallet.banknotes[currentItemIndex]);
        }
        showCoins = sc;
    }
}
