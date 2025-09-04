using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMovMenu : MonoBehaviour
{
    public Transform maxX;
    public Transform minX;

    [Range(0.0015f, 0.0005f)]public float speed = 0.001f;
    private float startTime;

    private bool isMovingRight;

    private void Start()
    {
        startTime = Time.time; //there's a little acceleration on the movement
        isMovingRight = false;
    }

    private void Update()
    {
        if (GetPosition().x > minX.position.x && !isMovingRight)
        {
            float distance = Vector3.Distance(GetPosition(), minX.position);

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / distance;

            transform.position = Vector3.Lerp(GetPosition(), minX.position, fractionOfJourney);

            float reachedDistance = 0.5f;
            if (Vector3.Distance(GetPosition(), minX.position) < reachedDistance)
            {
                //transform.position = minX.position;
                isMovingRight = true;
                startTime = Time.time;
            }
        }
        else if(GetPosition().x < maxX.position.x && isMovingRight)
        {
            float distance = Vector3.Distance(GetPosition(), maxX.position);

            float distCovered = (Time.time - startTime) * speed;
            float fractionOfJourney = distCovered / distance;

            transform.position = Vector3.Lerp(GetPosition(), maxX.position, fractionOfJourney);

            float reachedDistance = 0.5f;
            if (Vector3.Distance(GetPosition(), maxX.position) < reachedDistance)
            {
                //transform.position = maxX.position;
                isMovingRight = false;
                startTime = Time.time;
            }
        }
    }

    public Vector3 GetPosition()
    {
        return transform.position;
    }
}
