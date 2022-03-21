using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
    public float m_thrust;
    public float m_knockTime;
    public float m_damage;

    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Breakable"))
        {
            collision.GetComponent<Breakable>().Smash();
        }

        if (collision.gameObject.CompareTag("Enemy") || collision.gameObject.CompareTag("Player"))
        {
            Rigidbody2D hit = collision.GetComponent<Rigidbody2D>();
            if (hit != null)
            {
                Vector2 diffVec = hit.transform.position - transform.position;
                Vector2 forceVec = diffVec.normalized * m_thrust;
                hit.AddForce(forceVec, ForceMode2D.Impulse);
                if (collision.gameObject.CompareTag("Enemy") && collision.isTrigger)
                {
                    hit.GetComponent<Enemy>().m_currentState = EnemyState.stagger;
                    collision.GetComponent<Enemy>().Knock(hit, m_knockTime, m_damage);
                }
                if (collision.gameObject.CompareTag("Player"))
                {
                    if(collision.GetComponent<PlayerMovement>().m_currentState != PlayerState.stagger)
                    {
                        hit.GetComponent<PlayerMovement>().m_currentState = PlayerState.stagger;
                        collision.GetComponent<PlayerMovement>().Knock(m_knockTime, m_damage);
                    }
                }
            }
        }
    }
}
