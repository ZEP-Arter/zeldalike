using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sign : Interactable
{
    public GameObject m_dialogBox;
    public UnityEngine.UI.Text m_dialogText;
    public string m_dialog;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space) && m_playerInRange) // TODO: Make key assignable
        {
            if (m_dialogBox.activeInHierarchy)
            {
                m_dialogBox.SetActive(false);
            }
            else
            {
                m_dialogBox.SetActive(true);
                m_dialogText.text = m_dialog;
            }
        }
    }

    protected override void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag("Player") && !collision.isTrigger)
        {
            m_dialogBox.SetActive(false);

            // We could either put this inside the collision condition (where it is now), or outside of it.
            // It makes not difference to us right now.. but it could.
            base.OnTriggerExit2D(collision);
        }
    }
}
