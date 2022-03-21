using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heart : Powerup
{
    public FloatValue m_playerHealth;
    public FloatValue m_heartContainers;
    public float m_amountToIncrease;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        if(other.CompareTag("Player") && !other.isTrigger)
        {
            float newHealth = m_playerHealth.RuntimeValue + m_amountToIncrease;
            if(newHealth > m_heartContainers.RuntimeValue * 2f)
            {
                newHealth = m_heartContainers.RuntimeValue * 2f;
            }
            m_playerHealth.RuntimeValue = newHealth;
            m_powerupSignal.Raise();
            Destroy(this.gameObject);
        }
    }
}
