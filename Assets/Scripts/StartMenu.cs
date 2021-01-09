﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using TMPro;
using System;
using System.IO;

public class StartMenu : MonoBehaviour
{
    static public string walletSavePath;

    void Start()
    {
        walletSavePath = Path.Combine(Application.persistentDataPath, "wallet.json");
        try
        {
            string jsonString = System.IO.File.ReadAllText(walletSavePath);
        }
        catch (IOException ioex)
        {
            Debug.Log("nema bajo moj");
            string json = "{\"coins\":[{\"name\":\"Kovanica od 5 kn\",\"value\":5.0,\"quantity\":0,\"imageAPath\":\"5A\",\"imageBPath\":\"5B\"},{\"name\":\"Kovanica od 2 kn\",\"value\":2.0,\"quantity\":0,\"imageAPath\":\"2A\",\"imageBPath\":\"2B\"},{\"name\":\"Kovanica od 1 kn\",\"value\":1.0,\"quantity\":0,\"imageAPath\":\"1A\",\"imageBPath\":\"1B\"},{\"name\":\"Kovanica od 0,50 kn\",\"value\":0.5,\"quantity\":0,\"imageAPath\":\"05A\",\"imageBPath\":\"05B\"},{\"name\":\"Kovanica od 0,20 kn\",\"value\":0.2,\"quantity\":0,\"imageAPath\":\"02A\",\"imageBPath\":\"02B\"},{\"name\":\"Kovanica od 0,10 kn\",\"value\":0.1,\"quantity\":0,\"imageAPath\":\"01A\",\"imageBPath\":\"01B\"},{\"name\":\"Kovanica od 0,05 kn\",\"value\":0.05,\"quantity\":0,\"imageAPath\":\"005A\",\"imageBPath\":\"005B\"},{\"name\":\"Kovanica od 0,02 kn\",\"value\":0.02,\"quantity\":0,\"imageAPath\":\"002A\",\"imageBPath\":\"002B\"},{\"name\":\"Kovanica od 0,01 kn\",\"value\":0.01,\"quantity\":0,\"imageAPath\":\"001A\",\"imageBPath\":\"001B\"}],\"banknotes\":[{\"name\":\"Novčanica od 10 kn\",\"value\":10.0,\"quantity\":0,\"imageAPath\":\"10A\",\"imageBPath\":\"10B\"},{\"name\":\"Novčanica od 20 kn\",\"value\":20.0,\"quantity\":0,\"imageAPath\":\"20A\",\"imageBPath\":\"20B\"},{\"name\":\"Novčanica od 50 kn\",\"value\":50.0,\"quantity\":0,\"imageAPath\":\"50A\",\"imageBPath\":\"50B\"},{\"name\":\"Novčanica od 100 kn\",\"value\":100.0,\"quantity\":0,\"imageAPath\":\"100A\",\"imageBPath\":\"100B\"},{\"name\":\"Novčanica od 200 kn\",\"value\":200.0,\"quantity\":0,\"imageAPath\":\"200A\",\"imageBPath\":\"200B\"},{\"name\":\"Novčanica od 500 kn\",\"value\":500.0,\"quantity\":0,\"imageAPath\":\"500A\",\"imageBPath\":\"500B\"},{\"name\":\"Novčanica od 1000 kn\",\"value\":1000.0,\"quantity\":0,\"imageAPath\":\"1000A\",\"imageBPath\":\"1000B\"}]}";
            File.WriteAllText(walletSavePath, json);
            Debug.Log("ima bajo moj");
        }
    }
}

