using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class selectRandomTarget : MonoBehaviour
{

    public GameObject parentTarget;
    private List<GameObject> listTarget = new List<GameObject>();
    // Start is called before the first frame update
    void Start()
    {
        Transform[] allChildren =parentTarget.GetComponentsInChildren<Transform>();
        
        foreach (Transform child in allChildren)
        {
            listTarget.Add(child.gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public GameObject returnNewTarget()
    {
        int maxchild = listTarget.Count;
        int newTargetIndex = Random.Range(0, maxchild);
        return listTarget[newTargetIndex];
    }

}
