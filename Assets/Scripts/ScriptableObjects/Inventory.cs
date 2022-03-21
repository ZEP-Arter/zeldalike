using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class Inventory : ScriptableObject
{
    public Item m_currentItem;
    public List<Item> m_items = new List<Item>();
    public int m_numberOfKeys;
    public int m_coins;

    public void AddItem(Item itemToAdd)
    {
        if( itemToAdd.m_isKey)
        {
            m_numberOfKeys++;
        }
        else
        {
            if(!m_items.Contains(itemToAdd))
            {
                m_items.Add(itemToAdd);
            }
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
