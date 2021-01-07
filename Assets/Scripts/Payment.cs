using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System;

public class Payment : MonoBehaviour
{
    public GameObject[] moneyList;
    public TextMeshProUGUI[] quantityList;
    public TextMeshProUGUI noMoney;

    private float[,] novcanik = {
                                    {0.01f, 2},
                                    {0.02f, 4},
                                    {0.05f, 6},
                                    {0.1f, 2},
                                    {0.2f, 4},
                                    {0.5f, 6},
                                    {1f, 2},
                                    {2f, 4},
                                    {5f, 6},
                                    {10f, 2},
                                    {20f, 4},
                                    {50f, 0},
                                    {100f, 0},
                                    {200f, 2},
                                    {500f, 1},
                                    {1000f, 0},
                                };



    int slijedecaNiza(float ostatak)
    {
        for (int i = 15; i >= 0; i--)
        {
            if (novcanik[i, 0] <= ostatak && novcanik[i, 1] > 0) return i;
        }

        return -1;
    }

    float oduzmiOdOstatka(int novcanica, float ostatak)
    {
        ostatak -= novcanik[novcanica, 0];
        novcanik[novcanica, 1] -= 1;

        return ostatak;
    }

    float slijedecaVisa(float ostatak)
    {
        for (int i = 0; i < 16; i++)
        {
            if (novcanik[i, 1] > 0)
            {
                ostatak -= novcanik[i, 0];
                novcanik[i, 1] -= 1;
            }
        }

        return ostatak;
    }

 
    void Start()
    {
        int n = 200;
        int i = 0;
        float iznos = 569.66f;
        float ost = iznos;

        float[,] novcanik_pocetak = novcanik.Clone() as float[,];
        float[,] payment = novcanik.Clone() as float[,];

        for (int j = 0; j < 16; j++)
            iznos -= (novcanik[j, 0] * novcanik[j, 1]);

        if (iznos > 0)
        {
            ost = 0;
            noMoney.enabled = true;
            noMoney.gameObject.SetActive(true);
        }


        while (ost > 0)
        {
            if (i != -1) i = slijedecaNiza(ost); //ima nizih novcanica
            if (i == -1) ost = slijedecaVisa(ost); //nema nizih novcanica
            else ost = oduzmiOdOstatka(i, ost);

            ost = (float)Math.Round(ost, 2);

            Debug.Log(ost);
            n -= 1;
            if (n == 0) break;
        }

        for (int j = 0; j < 16; j++)
            payment[j, 1] = novcanik_pocetak[j, 1] - novcanik[j, 1];

        if (i == -1)
        {
            for (int j = 15; j >= 0; j--)
            {
                if (payment[j,1] > 0 && ost + payment[j,0] <= 0)
                {
                    int broj_novcanica = (int)payment[j,1];
                    for (int k = 1; k <= broj_novcanica; k++)
                    {
                        if (ost + payment[j,0] > 0) break;
                        ost += payment[j,0];
                        payment[j,1] -= 1;
                    }
                }
            }
        }

        for (int k = 0; k < 16; k++)
        {
            if (payment[k, 1] == 0) moneyList[k].SetActive(false);
            else quantityList[k].text = "x" + payment[k,1].ToString();
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
