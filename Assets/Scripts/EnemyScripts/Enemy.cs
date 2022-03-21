using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyState
{
    idle,
    walk,
    attack,
    stagger
}

public class Enemy : MonoBehaviour
{
    [Header("State Machine")]
    public EnemyState m_currentState;

    [Header("Stats")]
    public FloatValue m_maxHealth;
    public float m_health;
    public string m_name;
    public int m_baseAttack;
    public float m_moveSpeed;
    private Vector2 m_homePosition;

    [Header("Death Effects")]
    public GameObject m_deathEffect;
    private const float m_deathEffectDelay = 1.0f;
    public SignalObj m_deathSignal;

    // Start is called before the first frame update
    void Awake()
    {
        m_health = m_maxHealth.RuntimeValue;
        m_homePosition = transform.position;
    }

    protected virtual void OnEnable()
    {
        transform.position = m_homePosition;
        m_health = m_maxHealth.m_initialValue;
        //transform.Rotate(0f, 0f, 45f);
        //transform.localScale = new Vector2(2f, 2f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void TakeDamage(float damage)
    {
        m_health -= damage;
        if(m_health<=0f)
        {
            DeathEffect();
            this.gameObject.SetActive(false);
            if(m_deathSignal != null)
            {
                m_deathSignal.Raise();
            }
        }
    }

    private void DeathEffect()
    {
        if(m_deathEffect != null)
        {
            GameObject effect = Instantiate(m_deathEffect, transform.position, Quaternion.identity);
            Destroy(effect, m_deathEffectDelay);
        }
    }

    public void Knock(Rigidbody2D myRigidbody, float knockTime, float damage)
    {
        StartCoroutine(KnockCo(myRigidbody, knockTime));
        TakeDamage(damage);
    }

    public virtual IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        if(myRigidbody != null)
        {
            yield return new WaitForSeconds(knockTime);
            myRigidbody.velocity = Vector2.zero;
            m_currentState = EnemyState.idle;
        }
    }
}
