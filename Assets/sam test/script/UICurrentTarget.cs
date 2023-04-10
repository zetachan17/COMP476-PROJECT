using UnityEngine;
using UnityEngine.Experimental.GlobalIllumination;
using UnityEngine.UI;

public class UICurrentTarget : MonoBehaviour
{
    public FireMissile fireMissile;
    public float minDistance = 0.01f;
    private float maxDistance = 25f;
    public float minScale = 1.0f;
    public float maxScale = 1f;

    public int circleSegments = 36;
    public float lineWidth = 0.5f;

    private LineRenderer lineRenderer;
    private Camera mainCamera;

    void Start()
    {
        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        maxDistance = fireMissile.missileRange;

        SetupLineRenderer();
        CreateCircle();
    }

    void Update()
    {
        if (fireMissile.isCircling)
        {
            lineRenderer.enabled = true;

            Vector3 targetPosition = fireMissile.closestTarget.transform.position;
            transform.position = new Vector3(targetPosition.x, targetPosition.y, targetPosition.z);
            transform.LookAt(mainCamera.transform);

            float distance = Vector3.Distance(fireMissile.transform.position, fireMissile.closestTarget.transform.position);
            float distancePercentage = Mathf.Clamp01((distance - minDistance) / (maxDistance - minDistance));
            float targetScale = Mathf.Lerp(maxScale, minScale, distancePercentage);

            transform.localScale = new Vector3(targetScale, targetScale, 1f);

            if (fireMissile.canFire) {
                var colour = Color.red; //new Color(190.0f / 255.0f, 67.0f / 255.0f, 67.0f / 255.0f);
                lineRenderer.material.color = colour;
                lineRenderer.material.color = colour;
            }
            else {
                lineRenderer.material.color = Color.yellow;
                lineRenderer.material.color = Color.yellow;
            }
        }
        else
        {
            lineRenderer.enabled = false;
        }
    }

    void SetupLineRenderer()
    {
        lineRenderer.positionCount = circleSegments + 1;
        lineRenderer.useWorldSpace = false;
        lineRenderer.startWidth = lineWidth;
        lineRenderer.endWidth = lineWidth;

        // Ensure the correct color space is used

    }

    void CreateCircle()
    {
        float angle = 360f / circleSegments;

        for (int i = 0; i < circleSegments + 1; i++)
        {
            float x = Mathf.Sin(Mathf.Deg2Rad * angle * i);
            float y = Mathf.Cos(Mathf.Deg2Rad * angle * i);

            lineRenderer.SetPosition(i, new Vector3(x, y, 0f));
        }
    }

}
