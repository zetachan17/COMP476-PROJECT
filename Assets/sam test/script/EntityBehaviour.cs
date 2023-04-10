using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

public  class EntityBehaviour : BT.Tree
{
    public LayerMask objectsToDetect;
    public EntityBehaviour teammate;
    public GameObject preferedTarget;

    private Dictionary<string, object> dataStorage = new();

    public void SetData(string key, object value)
    {
        dataStorage[key] = value;
    }

    public T GetData<T>(string key) where T : class
    {
        if (dataStorage.ContainsKey(key))
            return (T)dataStorage[key];
        return null;
    }
}

