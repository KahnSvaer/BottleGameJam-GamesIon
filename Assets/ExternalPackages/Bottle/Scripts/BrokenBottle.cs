using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BrokenBottle : MonoBehaviour
{
    [SerializeField] float outwardExplosionForceModifier = 1f;
    [SerializeField] float upwardsModifier = 0.5f;
    [SerializeField] float randomForce = 1f; // Added for extra random force
    [SerializeField] float explosionModifier = 0.1f;

    public void RandomVelocities(Vector3 collisionPoint, Vector3 collisionDirection, float collisionSpeed)
    {
        float scaledExplosionForce = collisionSpeed * explosionModifier;
        foreach (Transform shard in transform)
        {
            Rigidbody rb = shard.GetComponent<Rigidbody>();
            if (rb != null)
            {
                Vector3 explosionDir = (shard.position - collisionPoint).normalized * outwardExplosionForceModifier + collisionDirection * upwardsModifier;
                Vector3 randomDir = Random.insideUnitSphere * randomForce; // Add some randomness

                rb.AddForce((explosionDir + randomDir) * scaledExplosionForce, ForceMode.Impulse);
                rb.AddTorque(Random.insideUnitSphere * scaledExplosionForce, ForceMode.Impulse);
            }
        }
    }
}
