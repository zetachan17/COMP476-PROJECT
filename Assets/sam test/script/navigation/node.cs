using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class node : MonoBehaviour
{
    [System.Serializable]
    public struct nodeStruct
    {
        [SerializeField] public node listNeighbour;
        [SerializeField] public float pathWeight;


    }

    [SerializeField] public List<nodeStruct> listNeighbour;
    public string name;
}