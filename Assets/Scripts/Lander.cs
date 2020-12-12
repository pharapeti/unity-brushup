using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Lander : MonoBehaviour
{
    [Header("Engine Thrust (N)")]
    public float engineThrust; // in Newtons (N) in positive Y direction
    private float engineOutput = 0;

    private Rigidbody rigidbody;

    // Landing Events
    public delegate void LandedAction();
    public static event LandedAction OnLanding;

    public delegate void CrashLandedAction();
    public static event CrashLandedAction OnCrashLanding;

    public delegate void UpdatedEngineOutput(float engineOutput);
    public static event UpdatedEngineOutput onEngineOutputChange;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            if (engineOutput < engineThrust)
            {
                engineOutput++;
            }
            rigidbody.AddForce(gameObject.GetComponent<Transform>().transform.up * engineOutput);

        } else
        {
            if (engineOutput > 0)
            {
                engineOutput--;
            }
        }

        if (onEngineOutputChange != null)
        {
            onEngineOutputChange(engineOutput);
        }
    }

    void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.relativeVelocity.magnitude);
        if (collision.relativeVelocity.magnitude > 10)
        {
            if (OnCrashLanding != null)
            {
                OnCrashLanding();
            }
        }
        else
        {
            if (OnLanding != null)
            {
                OnLanding();
            }
        }
    }
}
