using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    idle,
    walk,
    attack,
    interact,
    stagger
}

public class PlayerMovement : MonoBehaviour
{
    public PlayerState m_currentState;
    public float m_speed;
    private Animator m_animator;
    private Rigidbody2D m_rigidbody2d;
    private Vector3 m_inputVec;
    private bool m_timeToUpdateAnimationAndMove;
    public FloatValue m_currentHealth;
    public SignalObj m_playerHealthSignal;
    public Vector2Value m_startingPosition;
    public Inventory m_playerInventory;
    public SpriteRenderer m_receivedItemSprite;
    public SignalObj m_playerHit;

    // Start is called before the first frame update
    void Start()
    {
        m_currentState = PlayerState.walk;
        m_animator = GetComponent<Animator>();
        m_rigidbody2d = GetComponent<Rigidbody2D>();
        // Set it to down to match initial state of the player. Otherwise, attacking before moving may
        // activate all hitboxes.
        m_animator.SetFloat("moveX", 0f);
        m_animator.SetFloat("moveY", -1f);
        transform.position = m_startingPosition.RuntimeValue;
    }

    // Update is called once per frame
    void Update()
    {
        // Is the player in an interaction
        if(m_currentState == PlayerState.interact)
        {
            return;
        }

        m_inputVec = Vector3.zero;
        m_inputVec.x = Input.GetAxisRaw("Horizontal");
        m_inputVec.y = Input.GetAxisRaw("Vertical");

        if(Input.GetButtonDown("attack") && m_currentState != PlayerState.attack && m_currentState != PlayerState.stagger)
        {
            StartCoroutine(AttackCo());
        }
        else if(m_currentState == PlayerState.walk || m_currentState == PlayerState.idle)
        {
            m_timeToUpdateAnimationAndMove = true;
        }
    }
    private IEnumerator AttackCo()
    {
        m_animator.SetBool("attacking", true);
        m_currentState = PlayerState.attack;
        yield return null; // wait one frame
        m_animator.SetBool("attacking", false);
        yield return new WaitForSeconds(0.33333333f);

        if(m_currentState != PlayerState.interact)
        {
            m_currentState = PlayerState.walk;
        }
    }

    public void RaiseItem()
    {
        if(m_playerInventory.m_currentItem != null)
        {
            if (m_currentState != PlayerState.interact)
            {
                m_animator.SetBool("receiveItem", true);
                m_currentState = PlayerState.interact;
                m_receivedItemSprite.sprite = m_playerInventory.m_currentItem.m_itemSprite;
            }
            else
            {
                m_animator.SetBool("receiveItem", false);
                m_currentState = PlayerState.idle;
                m_receivedItemSprite.sprite = null;
                m_playerInventory.m_currentItem = null;
            }
        }
    }

    private void FixedUpdate()
    {
        if(m_timeToUpdateAnimationAndMove)
        {
            UpdateAnimationAndMove();
            m_timeToUpdateAnimationAndMove = false;
        }
    }

    void UpdateAnimationAndMove()
    {
        if (m_inputVec != Vector3.zero)
        {
            MoveCharacter();
            m_animator.SetFloat("moveX", m_inputVec.x);
            m_animator.SetFloat("moveY", m_inputVec.y);
            m_animator.SetBool("moving", true);
        }
        else
        {
            m_animator.SetBool("moving", false);
        }
    }

    void MoveCharacter()
    {
        // Let's decide not to be ultra-retro.. and so we'll make sure he doesn't walk faster diagonally.
        m_inputVec.Normalize();

        Vector3 initialPos = transform.position;
        m_rigidbody2d.MovePosition(initialPos + m_inputVec * m_speed * Time.deltaTime);
    }

    // This is to make sure a joystick doesn't cause multiple animations (and hitboxes) to play at once.
    // (maybe the real issue is we don't want blend trees.. because we don't want blending!)
    public void setAnimatorDirection(Vector2 movementInput)
    {
        if (Mathf.Abs(movementInput.x) > Mathf.Abs(movementInput.y))
        {
            m_animator.SetFloat("moveX", movementInput.x * (1 / Mathf.Abs(movementInput.x)));
            m_animator.SetFloat("moveY", 0);
        }
        else
        {
            m_animator.SetFloat("moveX", 0);
            m_animator.SetFloat("moveY", movementInput.y * (1 / Mathf.Abs(movementInput.y)));
        }
    }

    public void Knock(float knockTime, float damage)
    {
        m_currentHealth.RuntimeValue -= damage;
        m_playerHealthSignal.Raise();
        if (m_currentHealth.RuntimeValue > 0)
        {
            StartCoroutine(KnockCo(knockTime));
        }
        else
        {
            this.gameObject.SetActive(false);
        }
    }

    private IEnumerator KnockCo(float knockTime)
    {
        if (m_rigidbody2d != null)
        {
            m_playerHit.Raise();
            yield return new WaitForSeconds(knockTime);
            m_rigidbody2d.velocity = Vector2.zero;
            m_currentState = PlayerState.idle;
        }
    }
}
