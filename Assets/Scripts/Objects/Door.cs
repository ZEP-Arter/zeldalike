using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorType
{
    key,
    enemy,
    button
};

public class Door : Interactable
{
    [Header("Door variables")]
    public DoorType m_doorType;
    public bool m_isOpen;
    public Inventory m_playerInventory;
    public SpriteRenderer m_doorSprite;
    public BoxCollider2D m_boxCollider;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(m_playerInRange && m_doorType == DoorType.key)
            {
                // Does player have a key?
                if(m_playerInventory.m_numberOfKeys > 0)
                {
                    // Call the open method
                    m_playerInventory.m_numberOfKeys--;
                    Open();
                }
            }
        }
    }

    public void Open()
    {
        // Turn off the door's sprite renderer
        m_doorSprite.enabled = false;
        // Set open to true
        m_isOpen = true;
        // Turn off the door's box collider
        m_boxCollider.enabled = false;
    }

    public void Close(bool closeAllDoorTypes = false)
    {
        if(closeAllDoorTypes || m_doorType == DoorType.enemy)
        {
            m_doorSprite.enabled = true;
            m_isOpen = false;
            m_boxCollider.enabled = true;
        }
    }
}
