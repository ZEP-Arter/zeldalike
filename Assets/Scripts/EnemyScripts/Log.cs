using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Log : Enemy
{
    protected Rigidbody2D m_rigidbody;
    [Header("Target Variables")]
    public Transform m_target;
    public float m_chaseRadius;
    public float m_attackRadius;

    [Header("Animator")]
    public Animator m_animator;

    protected void LogInit()
    {
        m_currentState = EnemyState.idle;
        m_rigidbody = GetComponent<Rigidbody2D>();
        m_animator = GetComponent<Animator>();
        m_target = GameObject.FindWithTag("Player").transform;
        //m_animator.SetBool("wakeUp", true);
    }

    // Start is called before the first frame update
    void Start()
    {
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        LogInit();
        m_currentState = EnemyState.idle;
    }

    // Update is called once per frame
    void Update()
    {
    }

    private void FixedUpdate()
    {
        MoveRoutine();
    }

    protected virtual void MoveRoutine()
    {
        float dist = Vector3.Distance(m_target.position, transform.position);
        if ((dist <= m_chaseRadius) && (dist > m_attackRadius) &&
            (m_currentState == EnemyState.idle || m_currentState == EnemyState.walk) &&
            (m_currentState != EnemyState.stagger))
        {
            Vector3 moveVec = Vector3.MoveTowards(transform.position, m_target.position, m_moveSpeed * Time.deltaTime);
            ChangeAnim(moveVec - transform.position);
            m_rigidbody.MovePosition(moveVec);
            ChangeState(EnemyState.walk);
            m_animator.SetBool("wakeUp", true);
        }
        else if(dist > m_chaseRadius)
        {
            m_animator.SetBool("wakeUp", false);
        }
    }
    protected void SetAnimFloat(float x, float y)
    {
        m_animator.SetFloat("moveX", x);
        m_animator.SetFloat("moveY", y);
    }

    protected void ChangeAnim(Vector2 direction)
    {
        if (Mathf.Abs(direction.x) > Mathf.Abs(direction.y))
        {
            if(direction.x>0)
            {
                SetAnimFloat(1f, 0f);
            }
            else if(direction.x<0)
            {
                SetAnimFloat(-1f, 0f);
            }
        }
        else if (Mathf.Abs(direction.x) < Mathf.Abs(direction.y))
        {
            if (direction.y > 0)
            {
                SetAnimFloat(0f, 1f);
            }
            else if (direction.y < 0)
            {
                SetAnimFloat(0f, -1f);
            }
        }
    }

    protected void ChangeState(EnemyState newState)
    {
        if(m_currentState != newState)
        {
            m_currentState = newState;
        }
    }
}
