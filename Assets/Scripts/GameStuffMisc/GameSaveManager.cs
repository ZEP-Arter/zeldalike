using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameSaveManager : MonoBehaviour
{
    public static GameSaveManager sm_gameSave;
    public List<ScriptableObject> m_objects = new List<ScriptableObject>();

    // Called at creation of an object
    private void Awake()
    {
        if(sm_gameSave == null)
        {
            sm_gameSave = this;
        }
        else
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
