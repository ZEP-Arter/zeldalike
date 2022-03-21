using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Powerup
{
    public Inventory m_playerInventory;

    // Start is called before the first frame update
    void Start()
    {
        m_powerupSignal.Raise();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            m_playerInventory.m_coins += 1;
            m_powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
