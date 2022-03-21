using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneTransition : MonoBehaviour
{
    [Header("New Scene Variables")]
    public string m_sceneToLoad;
    public Vector2 m_playerPosition;
    public Vector2Value m_playerReturnPosition;
    public Vector2 m_cameraNewMax;
    public Vector2 m_cameraNewMin;
    public Vector2Value m_cameraMax;
    public Vector2Value m_cameraMin;

    [Header("Transition Variables")]
    public GameObject m_fadeInPanel;
    public GameObject m_fadeOutPanel;
    public float m_fadeWait;

    private void Awake()
    {
        if(m_fadeInPanel != null)
        {
            GameObject panel = Instantiate(m_fadeInPanel, Vector3.zero, Quaternion.identity) as GameObject;
            Destroy(panel, 1);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Player") && !collision.isTrigger)
        {
            m_playerReturnPosition.RuntimeValue = m_playerPosition;
            StartCoroutine(FadeCo());
            //SceneManager.LoadScene(m_sceneToLoad);
        }
    }

    public IEnumerator FadeCo()
    {
        if(m_fadeOutPanel != null)
        {
            Instantiate(m_fadeOutPanel, Vector3.zero, Quaternion.identity);
        }
        yield return new WaitForSeconds(m_fadeWait);
        ResetCameraBounds();
        AsyncOperation asyncOperation = SceneManager.LoadSceneAsync(m_sceneToLoad);
        while (!asyncOperation.isDone)
        {
            yield return null;
        }
    }

    public void ResetCameraBounds()
    {
        m_cameraMax.m_runtimeValue = m_cameraNewMax;
        m_cameraMin.m_runtimeValue = m_cameraNewMin;
    }
}
