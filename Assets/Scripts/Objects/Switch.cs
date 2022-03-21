using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Switch : MonoBehaviour
{
    public bool m_isActive;
    public BoolValue m_storedValue;
    public Sprite m_activatedSprite;
    private SpriteRenderer m_currentSprite;
    public Door m_door;

    // Start is called before the first frame update
    void Start()
    {
        m_currentSprite = GetComponent<SpriteRenderer>();
        m_isActive = m_storedValue.RuntimeValue;
        if (m_isActive)
        {
            ActivateSwitch();
        }
    }

    public void ActivateSwitch()
    {
        m_isActive = true;
        m_storedValue.RuntimeValue = true;
        m_door.Open();
        m_currentSprite.sprite = m_activatedSprite;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        // Is it the player?
        if(other.CompareTag("Player"))
        {
            if (!m_isActive)
            {
                ActivateSwitch();
            }
        }
    }
}
