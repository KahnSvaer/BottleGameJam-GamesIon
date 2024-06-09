using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bottle : MonoBehaviour
{
    [SerializeField] float breakSpeedPoint = 3f;
    private void OnEnable() {
       foreach(Transform child in transform){
            child.gameObject.SetActive(child.GetComponent<BrokenBottle>() == null);
        } 
    }
    void Update() // just for testing
    {
        if(Input.GetKeyDown(KeyCode.K))
        {
            Explode(1, Vector3.zero, Vector3.up);
        }
    }

    private void OnCollisionEnter(Collision other) {
        Vector3 collisionPoint = other.contacts[0].point;
        Vector3 collisionDirection = other.relativeVelocity.normalized;
        float collisionSpeed = other.relativeVelocity.magnitude;
        if (collisionSpeed>breakSpeedPoint){
            Explode(collisionSpeed, collisionPoint, collisionDirection);
        }
    }
    
    void Explode(float collisionSpeed, Vector3 collisionPoint, Vector3 collisionDirection)
    {
        foreach (Transform child in transform)
        {
            child.gameObject.SetActive(false);
        }
        GameObject brokenBottle = transform.GetChild(3).gameObject;
        brokenBottle.SetActive(true);
        brokenBottle.GetComponent<BrokenBottle>().RandomVelocities(collisionPoint, collisionDirection, collisionSpeed);
    }

}
