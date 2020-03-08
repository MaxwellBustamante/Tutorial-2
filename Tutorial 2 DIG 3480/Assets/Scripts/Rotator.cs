using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour

{

    public Transform startMarker;
    public Transform endMarker;
    public float speed = 30.0F;

    private float startTime;
    private float journeyLength;

    void Start()
    {
        startTime = Time.time;
        journeyLength = Vector2.Distance(startMarker.position, endMarker.position);
    }

    void Update()
    {
        transform.Rotate(new Vector3(0, 0, 45) * Time.deltaTime);
        float distCovered = (Time.time - startTime) * speed;
        float fracJourney = distCovered / journeyLength;
        transform.position = Vector2.Lerp(startMarker.position, endMarker.position, Mathf.PingPong(fracJourney, 1));
    }
}

