using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu]
public class SignalObj : ScriptableObject
{
    public List<SignalListener> m_listeners = new List<SignalListener>();

    public void Raise()
    {
        for(int i=m_listeners.Count-1; i>=0; i--)
        {
            m_listeners[i].OnSignalRaised();
        }
    }

    public void RegisterListener(SignalListener listener)
    {
        m_listeners.Add(listener);
    }

    public void UnregisterListener(SignalListener listener)
    {
        m_listeners.Remove(listener);
    }
}
