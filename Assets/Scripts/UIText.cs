using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIText : MonoBehaviour
{
    // UI Elements
    public Text uiText;
    public Text altitudeText;
    public Text descentRateText;
    public Text engineOutputText;

    public GameObject lander;
    private Transform lander_transform;

    private float previousAltitude;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
        lander_transform = lander.GetComponent<Transform>();
        Lander.OnLanding += handleLanding; // subscribe handleLanding() to the Lander's onLanding event
        Lander.OnCrashLanding += handleCrashLanding; // subscribe handleLanding() to the Lander's onLanding event
        Lander.onEngineOutputChange += handleEngineOutputChange; // subscribe handleLanding() to the Lander's onLanding event
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(lander_transform.position, Vector3.down, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            float currentAltitude = hit.distance;
            float descentRate = (currentAltitude - previousAltitude) / Time.deltaTime;

            altitudeText.text = "Altitude (m): " + System.Math.Round(currentAltitude, 2);
            descentRateText.text = "Descent rate (m/s): " + System.Math.Round(descentRate, 2);

            previousAltitude = currentAltitude;
        }
    }

    void handleLanding()
    {
        uiText.text = "Nice landing!";
    }

    void handleCrashLanding()
    {
        uiText.text = "You hit the ground too hard!";
    }

    void handleEngineOutputChange(float engineOutput)
    {
        engineOutputText.text = "Engine output (N): " + System.Math.Round(engineOutput, 2);
    }
}
