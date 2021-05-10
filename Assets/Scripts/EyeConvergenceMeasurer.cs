using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeConvergenceMeasurer : MonoBehaviour
{
    public Text ECDisplay;
    public Text realtimeDisplay;

    public float minimalEyeConvergenceDistance;

    public Transform ECObj;

    const float SPEED = 0.1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        ECObj.position = new Vector3(0f, 1.57f, 1f);
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 delta = new Vector3(0, 0, OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y);
        ECObj.Translate(delta * SPEED * Time.deltaTime);
        realtimeDisplay.text = string.Format("{0:F2} m", ECObj.position.z);
        realtimeDisplay.gameObject.transform.parent.position = new Vector3(0f, 1.57f, ECObj.position.z / 2);

        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            measureCD();
            gameObject.SetActive(false);
        }
    }

    void measureCD()
    {
        minimalEyeConvergenceDistance = ECObj.position.z;
        ECDisplay.text = string.Format("{0:F2} m", minimalEyeConvergenceDistance);
    }
}
