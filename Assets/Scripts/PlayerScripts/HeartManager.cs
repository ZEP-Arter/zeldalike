using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeartManager : MonoBehaviour
{
    public UnityEngine.UI.Image[] m_hearts;
    public Sprite m_fullHeart;
    public Sprite m_halfHeart;
    public Sprite m_emptyHeart;
    public FloatValue m_heartContainers;
    public FloatValue m_playerCurrentHealth;

    // Start is called before the first frame update
    void Start()
    {
        InitHearts();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void InitHearts()
    {
        for(int i=0; i<m_heartContainers.RuntimeValue; i++)
        {
            m_hearts[i].gameObject.SetActive(true);
            m_hearts[i].sprite = m_fullHeart;
        }
        UpdateHearts();
    }

    public void UpdateHearts()
    {
        float tempHealth = m_playerCurrentHealth.RuntimeValue;
        for(int i=0; i<m_heartContainers.RuntimeValue; i++)
        {
            if(Mathf.Round(tempHealth) >= (i+1)*2)
            {
                m_hearts[i].sprite = m_fullHeart;
            }
            else if (Mathf.Round(tempHealth) >= (i+1)*2-1)
            {
                m_hearts[i].sprite = m_halfHeart;
            }
            else
            {
                m_hearts[i].sprite = m_emptyHeart;
            }
        }
    }
}
