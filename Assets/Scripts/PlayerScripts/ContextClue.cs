using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContextClue : MonoBehaviour
{
    public GameObject m_contextClue;

    public void Enable()
    {
        m_contextClue.SetActive(true);
    }

    public void Disable()
    {
        m_contextClue.SetActive(false);
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
