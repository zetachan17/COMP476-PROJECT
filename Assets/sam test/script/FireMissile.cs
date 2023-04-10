using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;

[RequireComponent(typeof(EntityBehaviour))]
public class FireMissile : MonoBehaviour
{
    public GameObject missilePrefab;
    public float missileRange = 25f;
    public float cooldownBeforeAfterFire = 5f;
    public LayerMask validTargets;
    public float missileLifetime = 10.0f;

    [field: SerializeField]
    public GameObject closestTarget { get; private set; }
    public bool isCircling { get; private set; }
    private Coroutine coroutine;
    public bool canFire { get; private set; }

    void Update()
    {
        FindClosestTarget();

        if (closestTarget != null && Vector3.Distance(transform.position, closestTarget.transform.position) <= missileRange)
        {
            isCircling = true;
        }
        else
        {
            isCircling = false;
        }

        if (isCircling && canFire && Input.GetKeyUp(KeyCode.Space))
        {
              Fire();
              canFire = false;
              coroutine = StartCoroutine(Cooldown(cooldownBeforeAfterFire, () => canFire = true));
        }
    }

    void FindClosestTarget()
    {
        //GameObject[] targets = GameObject.FindGameObjectsWithTag("Target");
        Collider[] targets = Physics.OverlapSphere(transform.position, missileRange, validTargets);
        float closestDistance = Mathf.Infinity;

        GameObject tempClosed = null;
        foreach (var target in targets)
        {
            if (GetComponent<EntityBehaviour>().teammate.gameObject == target.gameObject) {
                Debug.Log($"{target.name} is a teammate!");
                continue;
            }

            float distance = Vector3.Distance(transform.position, target.transform.position);

            if (distance < closestDistance)
            {
                closestDistance = distance;

                // Check if the closest has changed
                tempClosed = target.gameObject;            
            }
        }

        if (closestTarget != tempClosed) {
            canFire = false;
            if (coroutine is not null)
                StopCoroutine(coroutine);
            coroutine = StartCoroutine(Cooldown(cooldownBeforeAfterFire, () => canFire = true));
        }

        closestTarget = tempClosed;
    }

    public void Fire()
    {
        if (!canFire || closestTarget == null)
            return;

        GameObject missile = Instantiate(missilePrefab, transform.position, Quaternion.identity);
        /*Rigidbody missileRb = missile.GetComponent<Rigidbody>();

        if (missileRb != null)
        {
            Vector3 direction = (closestTarget.transform.position - transform.position).normalized;
            missileRb.velocity = direction * missileSpeed;
        }*/

        missile.GetComponent<strg_steerinAgent>().setSteering(strg_steerinAgent.SteeringOptions.Persue, new() { closestTarget });
        StartCoroutine(Cooldown(missileLifetime, () => Destroy(missile)));
    }

    IEnumerator Cooldown(float seconds, Action then)
    {
        yield return new WaitForSeconds(seconds);
        then();
    }
}
