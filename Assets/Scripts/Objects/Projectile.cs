using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    [Header("Movement")]
    public float m_moveSpeed;
    public Vector2 m_directionToMove;

    [Header("Lifetime")]
    public float m_lifetimeSec;
    private float m_lifetimeRemainingSec;
    public Rigidbody2D m_rigidbody;

    // Start is called before the first frame update
    void Start()
    {
        m_lifetimeRemainingSec = m_lifetimeSec; // Ever see the movie In Time? Me neither. So I should probably mention Logan's Run, which I also haven't watched.
    }

    // Update is called once per frame
    void Update()
    {
        m_lifetimeRemainingSec -= Time.deltaTime;
        if(m_lifetimeRemainingSec <= 0f)
        {
            Destroy(this.gameObject);
        }
    }

    public void Launch(Vector2 initialDir)
    {
        Debug.Assert(Mathf.Abs(initialDir.magnitude - 1.0f) < 0.01f); // Ensure it's a unit vector (i.e. Normalized)
        m_rigidbody.velocity = initialDir * m_moveSpeed;
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Destroy(this.gameObject);
    }
}
