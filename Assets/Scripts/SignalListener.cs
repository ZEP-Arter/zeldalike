using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SignalListener : MonoBehaviour
{
    public SignalObj m_signal;
    public UnityEngine.Events.UnityEvent m_signalEvent;

    public void OnSignalRaised()
    {
        m_signalEvent.Invoke();
    }

    private void OnEnable()
    {
        m_signal.RegisterListener(this);
    }

    private void OnDisable()
    {
        m_signal.UnregisterListener(this);
    }
}
