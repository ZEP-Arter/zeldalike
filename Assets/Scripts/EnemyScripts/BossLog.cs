using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossLog : TurretEnemy
{
    public override IEnumerator KnockCo(Rigidbody2D myRigidbody, float knockTime)
    {
        // Overriding KnockCo() is a hack to prevent the Boss from getting knocked back.
        // It exposes some other issues too.. such as:
        //   - The player's attack is more likely to hit the boss more than once per swing (which is generally a problem
        //     with the other enemies too, but is more of a problem when the enemy doesn't get knocked back).
        if (myRigidbody != null)
        {
            yield return new WaitForSeconds(0.0f);  // We're using 0.0f instead of knockTime here. As a result, velocity gets set back to 0.0f asap.
            myRigidbody.velocity = Vector2.zero;
            m_currentState = EnemyState.idle;
        }
    }
}
