using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class deathManagment : MonoBehaviour
{
    public Camera deathCam;
    public GameObject respownPoint;
    public List<ParticleSystem> listParticle;

    private Quaternion originalRotation;
    // Start is called before the first frame update
    void Start()
    {
        deathCam = GameObject.Find("deathCam").GetComponent<Camera>();
        originalRotation = this.transform.rotation;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void getkilled()
    {
        if(GetComponent<strg_steerinAgent>().dead == false)
        {
            GetComponent<strg_steerinAgent>().dead = true;
            GetComponent<strg_steerinAgent>().Velocity = Vector3.zero;
            Debug.Log("in get killed");
            if(gameObject.GetComponent<strg_steerinAgent>().player == true)
            {

             Debug.Log("player got killed");
                Camera mainCamera = Camera.main;
 /*
                deathCam.transform.position = mainCamera.transform.position;
                deathCam.transform.rotation = mainCamera.transform.rotation;

                mainCamera.enabled = false;
                deathCam.enabled = true;*/
            }

            disablePlayer();
            GetComponent<individualScore>().getDeath();
            //start explosion particle

            StartCoroutine(Wait());
        }


    }

    private IEnumerator Wait()
    {
        Debug.Log("Before wait");
        yield return new WaitForSeconds(4);
        Debug.Log("After the wait");
        enablePlayer();



    }

    public void disablePlayer()
    {
        this.gameObject.GetComponent<strg_steerinAgent>().enabled = false;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = false;
        foreach(ParticleSystem particle in listParticle)
        {
            particle.Play();
        }
    }

    public void enablePlayer()
    {
        GetComponent<healthManagment>().resetHealth();
        foreach (ParticleSystem particle in listParticle)
        {
            particle.Stop();
        }
        this.transform.rotation = originalRotation;
        this.transform.position = respownPoint.transform.position;
        gameObject.transform.GetChild(0).GetComponent<MeshRenderer>().enabled = true;
        this.gameObject.GetComponent<strg_steerinAgent>().enabled = true;
        GetComponent<strg_steerinAgent>().dead = false;
        this.gameObject.GetComponent<strg_steerinAgent>().initialiseAgent();
        
    }
}
