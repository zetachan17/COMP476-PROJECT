using System.Collections;
using System.Collections.Generic;
using BT;
using UnityEngine;

public class SpawnPowerUps : MonoBehaviour
{
    [SerializeField] private List<GameObject> powerUpList;
    [SerializeField] private List<Transform> spawnPositionList;

    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("creatPowerUp", 2.0f, 15.0f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void creatPowerUp()
    {
        for (int i = 0; i < spawnPositionList.Count; i++)
        {
            float x = Random.Range(-10, 10);
            float y = Random.Range(-10, 10);
            float z = Random.Range(-10, 10);
            Vector3 nodePosition = spawnPositionList[i].position;
            Vector3 spawnPosition = new Vector3(x + nodePosition.x, y + nodePosition.y, z + nodePosition.z);
            int numOfPowerUps = powerUpList.Count;

           GameObject newPowerUp = Instantiate(powerUpList[Random.Range(0, numOfPowerUps)], spawnPosition, Quaternion.identity);
            newPowerUp.GetComponent<PowerUp>().SelfDestroy();
        }
    }
}
