using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovement : MonoBehaviour
{
    [Header("Position Variables")]
    public Transform m_target;
    public float m_smoothing;
    public Vector2 m_maxPos;
    public Vector2 m_minPos;

    [Header("Animator")]
    public Animator m_animator;

    [Header("Position Reset")]
    public Vector2Value m_cameraMax;
    public Vector2Value m_cameraMin;

    [Header("Added by me - not in tutorial")]
    public bool m_justOnce;

    // Start is called before the first frame update
    void Start()
    {
        m_maxPos = m_cameraMax.m_runtimeValue;
        m_minPos = m_cameraMin.m_runtimeValue;
        m_justOnce = false;
        m_animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if(!m_justOnce)
        {
            m_justOnce = true;
            transform.position = GetClampedCameraPosition();
        }
        if (transform.position != m_target.position)
        {
            transform.position = Vector3.Lerp(transform.position, GetClampedCameraPosition(), m_smoothing);
        }
    }

    private Vector3 GetClampedCameraPosition()
    {
        Vector3 targetPos = new Vector3(m_target.position.x,
                                        m_target.position.y,
                                        transform.position.z);
        targetPos.x = Mathf.Clamp(targetPos.x, m_minPos.x, m_maxPos.x);
        targetPos.y = Mathf.Clamp(targetPos.y, m_minPos.y, m_maxPos.y);
        return targetPos;
    }

    public void BeginKick()
    {
        m_animator.SetBool("kick_active", true);
        StartCoroutine(KickCo());
    }

    public IEnumerator KickCo()
    {
        yield return null;
        m_animator.SetBool("kick_active", false);
    }
}
