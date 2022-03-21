using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinTextManager : MonoBehaviour
{
    public Inventory m_playerInventory;
    public TMPro.TextMeshProUGUI m_coinDisplay;

    public void UpdateCoinCount()
    {
        m_coinDisplay.text = "" + m_playerInventory.m_coins;
    }
}
