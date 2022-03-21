using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Room : MonoBehaviour
{
    private Enemy[] m_enemies;
    private Breakable[] m_breakables;
    protected Door[] m_doors;
    public UnityEvent m_onEnemiesDeadEvent;

    protected virtual void Awake()        // I'm using awake because Start() wasn't getting called
    {
        if(m_onEnemiesDeadEvent == null)
        {
            m_onEnemiesDeadEvent = new UnityEvent();
        }
        m_enemies = GetComponentsInChildren<Enemy>(true);
        m_breakables = GetComponentsInChildren<Breakable>(true);
        m_doors = GetComponentsInChildren<Door>(true);
    }

    private void OpenDoors()
    {
        foreach (Door door in m_doors)
        {
            door.Open();
        }
    }

    public void EnemiesDeadCheck()
    {
        foreach (Enemy enemy in m_enemies)
        {
            if(enemy.gameObject.activeInHierarchy)
            {
                return;
            }
        }
        OpenDoors();
    }

    protected void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (Enemy enemy in m_enemies)
                ChangeActivation(enemy, true);

            foreach (Breakable breakable in m_breakables)
                ChangeActivation(breakable, true);

            CloseDoors();
        }
    }

    protected void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Player") && !other.isTrigger)
        {
            foreach (Enemy enemy in m_enemies)
                ChangeActivation(enemy, false);

            foreach (Breakable breakable in m_breakables)
                ChangeActivation(breakable, false);
        }
    }

    private void ChangeActivation(Component component, bool activate)
    {
        component.gameObject.SetActive(activate);
    }

    public void CloseDoors()
    {
        foreach (Door door in m_doors)
        {
            door.Close();
        }
    }


}
