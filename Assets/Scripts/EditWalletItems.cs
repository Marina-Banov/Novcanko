using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Wallet;

public class EditWalletItems : MonoBehaviour
{
    public Wallet wallet;
    public WalletItem currentItem;
    public Image walletItemPlaceholder;

    // Start is called before the first frame update
    void Start()
    {
        string jsonString = System.IO.File.ReadAllText(Application.dataPath + "/Resources/wallet.json");
        wallet = JsonUtility.FromJson<Wallet>(jsonString);
        currentItem = wallet.walletItems[0];
        walletItemPlaceholder = gameObject.transform.GetChild(4).gameObject.GetComponent<Image>();
        walletItemPlaceholder.sprite = Resources.Load<Sprite>("Money/kn5"); // TODO image not showing!!
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
