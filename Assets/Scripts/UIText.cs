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

    public GameObject lander;
    public Transform lander_transform;

    // Start is called before the first frame update
    void Start()
    {
        uiText = GetComponent<Text>();
        lander_transform = lander.GetComponent<Transform>();
        Lander.OnLanding += handleLanding; // subscribe handleLanding() to the Lander's onLanding event
    }

    void FixedUpdate()
    {
        RaycastHit hit;

        if (Physics.Raycast(lander_transform.position, Vector3.down, out hit))
        {
            GameObject hitObject = hit.collider.gameObject;
            altitudeText.text = "Altitude: " + System.Math.Round(hit.distance, 2);
        }
    }

    void handleLanding()
    {
        uiText.text = "We have impact!";
    }
}
