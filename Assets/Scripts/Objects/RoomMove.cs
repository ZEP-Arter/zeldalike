using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomMove : MonoBehaviour
{
    public Vector2 m_cameraChange;
    public Vector3 m_playerChange;
    private CameraMovement m_cameraMovement;
    public bool m_needText;
    public string m_placeName;
    public GameObject m_textObject;
    public UnityEngine.UI.Text m_placeText;
    private Vector3 m_textScaleOriginal;
    private float m_textAlphaOriginal;
    private bool m_animRestart;
    private bool m_debounce;

    // Start is called before the first frame update
    void Start()
    {
        m_cameraMovement = Camera.main.GetComponent<CameraMovement>();
        m_textScaleOriginal = m_textObject.transform.localScale;
        m_textAlphaOriginal = m_textObject.GetComponent<CanvasRenderer>().GetAlpha();
        m_animRestart = false;
        m_debounce = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger && !m_debounce)
        {
            StartCoroutine(Debounce());
            m_cameraMovement.m_minPos += m_cameraChange;
            m_cameraMovement.m_maxPos += m_cameraChange;
            collision.transform.position += m_playerChange;
            if(m_needText)
            {
                if(m_textObject.activeInHierarchy)
                {
                    m_animRestart = true;
                }
                else
                {
                    StartCoroutine(placeNameCo());
                }
            }
        }
    }
    private IEnumerator Debounce()
    {
        m_debounce = true;
        yield return new WaitForSeconds(0.2f);
        m_debounce = false;
    }

    private IEnumerator placeNameCo()
    {
        m_textObject.transform.localScale = m_textScaleOriginal;
        m_textObject.GetComponent<CanvasRenderer>().SetAlpha(m_textAlphaOriginal);
        m_textObject.SetActive(true);
        while (m_textObject.activeInHierarchy)
        {
            m_animRestart = false;
            m_textObject.transform.localScale = m_textScaleOriginal;
            m_textObject.GetComponent<CanvasRenderer>().SetAlpha(m_textAlphaOriginal);
            m_placeText.text = m_placeName;
            for (int i = 0; i < 400; ++i)
            {
                if (m_animRestart)
                    break;
                yield return new WaitForSeconds(0.01f);
                m_textObject.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0f);
                float objectAlpha = m_textObject.GetComponent<CanvasRenderer>().GetAlpha();
                float fadeAmount = (float)(objectAlpha - (0.2f * Time.deltaTime));
                m_textObject.GetComponent<CanvasRenderer>().SetAlpha(fadeAmount);
            }
            for (int i = 0; i < 60; ++i)
            {
                if (m_animRestart)
                    break;
                yield return new WaitForSeconds(0.01f);
                m_textObject.transform.localScale += new Vector3(0.0005f, 0.0005f, 0.0f);
                float objectAlpha = m_textObject.GetComponent<CanvasRenderer>().GetAlpha();
                float fadeAmount = (float)(objectAlpha - (14.0f * Time.deltaTime));
                m_textObject.GetComponent<CanvasRenderer>().SetAlpha(fadeAmount);
            }
            if (!m_animRestart)
                m_textObject.SetActive(false);
        }
    }
}
