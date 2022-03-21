using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

[CreateAssetMenu]
public class Vector2Value : ScriptableObject, ISerializationCallbackReceiver
{
    public Vector2 m_initialValue;

    // I don't want this value serialized in a real build.. but I do like to see it in Unity while debugging.
    // For a real build, maybe I can just make it private (which will also make sure I'm not dodging the accessor).
    // [System.NonSerialized]
    public Vector2 m_runtimeValue;
    public Vector2 RuntimeValue { get { return m_runtimeValue; } set { m_runtimeValue = value; } }

    public void OnAfterDeserialize()
    {
        m_runtimeValue = m_initialValue;
    }

    public void OnBeforeSerialize() { }
}
