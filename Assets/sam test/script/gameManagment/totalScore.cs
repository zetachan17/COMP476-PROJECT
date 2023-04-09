using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class totalScore : MonoBehaviour
{
    // Start is called before the first frame update
    public List<int> teamScore;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
     
    /// <summary>
    /// Update the score of each team when called. 
    /// </summary>
    public void updateScore()
    {
        GameObject playerListe = GameObject.Find("ShipList");
        int nbPlayer = playerListe.transform.childCount;
        GameObject child;

        for (int i = 0; i < nbPlayer; i++)
        {
            child = playerListe.transform.GetChild(i).gameObject;
            int memberOFTeam = child.GetComponent<EntityBehaviour>().teamNumber;
            int nbkill = child.GetComponent<individualScore>().getKill();

            teamScore[memberOFTeam - 1] = teamScore[memberOFTeam - 1] + nbkill;

        }

    }


    /// <summary>
    /// Return a list of int the represent  the winig team number, followd by the winnig score.
    /// </summary>
    /// <returns>
    /// list of int
    /// 0 = winnint team number (index + 1)
    /// 1 = score of the winning teame.
    /// </returns>
    public List<int> getWinner()
    {

        int winnigTeam = -1;
        int maxScore = int.MinValue;
        int currentTeam = 1;

        foreach(int score in teamScore)
        {

            if(score > maxScore)
            {
                winnigTeam = currentTeam;
                maxScore = score;
            }

            currentTeam++;
        }

        List<int> winner = new List<int>();
        winner.Add(winnigTeam);
        winner.Add(maxScore);
        return null;
    }
}
