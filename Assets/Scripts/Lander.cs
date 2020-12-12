using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour
{
    [Header("Engine Thrust (N)")]
    public float engineThrust; // in Newtons (N) in positive Y direction

    private Rigidbody rigidbody;

    // Landing Events
    public delegate void LandedAction();
    public static event LandedAction OnLanding;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        rigidbody.AddForce(gameObject.GetComponent<Transform>().transform.up * engineThrust);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (OnLanding != null)
        {
            OnLanding();
        }
    }
}
