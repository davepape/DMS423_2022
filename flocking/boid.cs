/* boid.cs - Example of flocking technique.
  This is very much a less-than-perfect implementation.  It was made as a quick demonstation, and would need improvements for real use
  */
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class boid : MonoBehaviour
{   
    static List<boid> allBoids;
    [SerializeField] private Vector3 velocity = Vector3.forward;
    [SerializeField] private float visionRange = 10.0f;
    [SerializeField] private float separationDistance = 1.0f;
    [SerializeField] private float maxAcceleration = 1.0f;
    [SerializeField] private float startSpeed = 1.0f;
    [SerializeField] private Vector3 startCenter = Vector3.zero;
    [SerializeField] private Vector3 startSize = Vector3.one;

    void Start()
    {
        if (allBoids == null)
            allBoids = new List<boid>();
        allBoids.Add(this);
        Vector3 startMin = startCenter - startSize;
        Vector3 startMax = startCenter + startSize;
        transform.position = new Vector3(Random.Range(startMin.x,startMax.x),
                                        Random.Range(startMin.y,startMax.y),
                                        Random.Range(startMin.z,startMax.z));
        velocity = Random.insideUnitSphere;
        velocity.y = 0;
        velocity *= startSpeed;
    }

    void Update()
    {
        Vector3 acceleration = Flock();
        velocity += acceleration * Time.deltaTime;
        transform.position += velocity * Time.deltaTime;
        if (velocity.sqrMagnitude > 0.0001)
            transform.LookAt(transform.position + velocity);
    }

    private Vector3 Flock()
    {
        Vector3 acceleration;
        if (!Separate(out acceleration))
        {
            if (!Align(out acceleration))
                if (!Cohere(out acceleration))
                {
                    acceleration = Random.Range(-maxAcceleration,maxAcceleration) * Vector3.Cross(Vector3.up, velocity.normalized);
                    acceleration.y = 0;
                }
        }
        return acceleration;
    }

    private bool Separate(out Vector3 acceleration)
    {
        Vector3 separationSum = Vector3.zero;
        int separationNumNeighbors = 0;

        acceleration = Vector3.zero;
        foreach (boid b in allBoids)
            if (b != this)
            {
                float d = Vector3.Distance(transform.position, b.transform.position);
                if (d < separationDistance)
                {
                    separationSum += b.transform.position;
                    separationNumNeighbors++;
                }
            }
            
        if (separationNumNeighbors > 0)
        {
            Vector3 separationPosition = separationSum / separationNumNeighbors;
            Vector3 separationDiff = transform.position - separationPosition;
            if (separationDiff.magnitude > maxAcceleration)
                separationDiff = maxAcceleration * separationDiff.normalized;
            acceleration = separationDiff;
            return true;
        }
        else
        {
            return false;
        }
    }

    private bool Align(out Vector3 acceleration)
    {
        Vector3 velocitySum = Vector3.zero;
        int numNeighbors = 0;
        acceleration = Vector3.zero;

        foreach (boid b in allBoids)
            if (b != this)
            {
                if (Visible(b))
                {
                    velocitySum += b.velocity;
                    numNeighbors++;
                 }
            }
        if (numNeighbors > 0)
        {
            Vector3 avgVelocity = velocitySum / numNeighbors;
            Vector3 diff = avgVelocity - velocity;
            if (diff.magnitude > 0.1)
            {
                if (diff.magnitude > maxAcceleration)
                    diff = maxAcceleration * diff.normalized;
                acceleration = diff;
                return true;
            }
        }
        return false;
    }

    private bool Cohere(out Vector3 acceleration)
    {
        Vector3 positionSum = Vector3.zero;
        int numNeighbors = 0;
        acceleration = Vector3.zero;

        foreach (boid b in allBoids)
            if (b != this)
            {
                if (Visible(b))
                {
                    positionSum += b.transform.position;
                    numNeighbors++;
                 }
            }
        if (numNeighbors > 0)
        {
            Vector3 avgPosition = positionSum / numNeighbors;
            Vector3 diff = avgPosition - transform.position;
            if (diff.magnitude > maxAcceleration)
                diff = maxAcceleration * diff.normalized;
            acceleration = diff;
            return true;
        }
        return false;
    }



    private bool Visible(boid other)
    {
        float d = Vector3.Distance(transform.position, other.transform.position);
        return (d <= visionRange);
    }
}
