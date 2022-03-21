using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurretEnemy : Log
{
    public GameObject m_projectile;
    public float m_fireDelay;
    private float m_fireDelayRemainingSec;

    private void Update()
    {
        m_fireDelayRemainingSec -= Time.deltaTime;
        if(m_fireDelayRemainingSec <= 0f)
        {
            m_fireDelayRemainingSec = 0.0f;
        }
    }

    private bool CanFire()
    {
        return m_fireDelayRemainingSec == 0.0f;
    }

    protected override void MoveRoutine()
    {
        if (CanFire())
        {
            float dist = Vector3.Distance(m_target.position, transform.position);
            if ((dist <= m_chaseRadius) && (dist > m_attackRadius) &&
                (m_currentState == EnemyState.idle || m_currentState == EnemyState.walk) &&
                (m_currentState != EnemyState.stagger))
            {
                Vector2 diffVec = m_target.transform.position - transform.position;
                Vector2 dirVec = diffVec.normalized;
                GameObject currentGameObj = Instantiate(m_projectile, transform.position, Quaternion.identity);
                m_fireDelayRemainingSec = m_fireDelay;
                currentGameObj.GetComponent<Projectile>().Launch(dirVec);
                ChangeState(EnemyState.walk);
                m_animator.SetBool("wakeUp", true);
            }
            else if (dist > m_chaseRadius)
            {
                m_animator.SetBool("wakeUp", false);
            }
        }
    }
}
