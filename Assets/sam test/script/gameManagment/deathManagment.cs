using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathManagment : MonoBehaviour
{
    public Camera deathCam;
    public GameObject respownPoint;
    // Start is called before the first frame update
    void Start()
    {
        deathCam = GameObject.Find("deathCam").GetComponent<Camera>();
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getkilled()
    {
        if(gameObject.GetComponent<strg_steerinAgent>().player == true)
        {
            Camera mainCamera = Camera.main;

            deathCam.transform.position = mainCamera.transform.position;
            deathCam.transform.rotation = mainCamera.transform.rotation;

            mainCamera.enabled = false;
            deathCam.enabled = true;
        }

        this.gameObject.SetActive(false);
        GetComponent<individualScore>().getDeath();
        //start explosion particle
        StartCoroutine(Wait());


    }

    private IEnumerator Wait()
    {
        yield return new WaitForSeconds(4);
        this.transform.position = respownPoint.transform.position;
        Camera.main.enabled = true;
        this.gameObject.SetActive(true);
        this.gameObject.GetComponent<strg_steerinAgent>().initialiseAgent();

    }
}
