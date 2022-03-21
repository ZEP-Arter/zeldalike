using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolLog : Log
{
    public Transform[] m_path;
    public int m_currentPointIndex;
    public Transform m_currentGoal;
    public float m_distEpsilon;

    // Start is called before the first frame update
    void Start()
    {
        LogInit();
        m_currentGoal = m_path[0];
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        m_animator.SetBool("wakeUp", true);
        //m_currentState = EnemyState.walk;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected override void MoveRoutine()
    {
        float dist = Vector3.Distance(m_target.position, transform.position);
        if ((dist <= m_chaseRadius) && (dist > m_attackRadius) &&
            (m_currentState == EnemyState.idle || m_currentState == EnemyState.walk) &&
            (m_currentState != EnemyState.stagger))
        {
            Vector3 moveVec = Vector3.MoveTowards(transform.position, m_target.position, m_moveSpeed * Time.deltaTime);
            ChangeAnim(moveVec - transform.position);
            m_rigidbody.MovePosition(moveVec);
            //ChangeState(EnemyState.walk);
            //m_animator.SetBool("wakeUp", true);
        }
        else if (dist > m_chaseRadius)
        {
            if(Vector3.Distance(transform.position, m_currentGoal.position) > m_distEpsilon)
            {
                //m_animator.SetBool("wakeUp", false);
                Vector3 moveVec = Vector3.MoveTowards(transform.position, m_currentGoal.position, m_moveSpeed * Time.deltaTime);
                ChangeAnim(moveVec - transform.position);
                m_rigidbody.MovePosition(moveVec);
            }
            else
            {
                ChangeGoal();
            }
        }
    }

    private void ChangeGoal()
    {
        if(m_currentPointIndex >= m_path.Length - 1)
        {
            m_currentPointIndex = 0;
            m_currentGoal = m_path[0];
        }
        else
        {
            m_currentPointIndex++;
            m_currentGoal = m_path[m_currentPointIndex];
        }
    }
}
