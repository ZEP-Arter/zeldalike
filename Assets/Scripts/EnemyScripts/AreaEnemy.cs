using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaEnemy : Log
{
    public Collider2D m_boundary;

    protected override void MoveRoutine()
    {
        float dist = Vector3.Distance(m_target.position, transform.position);
        bool playerInBounds = m_boundary.bounds.Contains(m_target.transform.position);
        if (playerInBounds && (dist <= m_chaseRadius) && (dist > m_attackRadius) &&
            (m_currentState == EnemyState.idle || m_currentState == EnemyState.walk) &&
            (m_currentState != EnemyState.stagger))
        {
            Vector3 moveVec = Vector3.MoveTowards(transform.position, m_target.position, m_moveSpeed * Time.deltaTime);
            ChangeAnim(moveVec - transform.position);
            m_rigidbody.MovePosition(moveVec);
            ChangeState(EnemyState.walk);
            m_animator.SetBool("wakeUp", true);
        }
        else if (dist > m_chaseRadius || !playerInBounds)
        {
            m_animator.SetBool("wakeUp", false);
        }
    }
}
