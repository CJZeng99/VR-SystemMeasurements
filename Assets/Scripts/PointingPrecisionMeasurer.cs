using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PointingPrecisionMeasurer : MonoBehaviour
{
    public Text MPDDisplay;

    public float maximalPointingDistance;

    public GameObject target;

    public Transform laserStart;
    public Transform laserEnd;
    public float laserLength = 50f;

    public LineRenderer myRay;
    public LineRenderer UIRay;

    float timer;
    const float MEASURE_TIME = 20f;

    int hitTime;
    bool justHit;
    float precision;
    const float PRECISION_THRESHOLD = 0.5f;

    const float TRIGGER_THRESHOLD = 0.7f;

    // Start is called before the first frame update
    void Start()
    {
        timer = 0f;
        hitTime = 0;
        Time.timeScale = 1f;
    }

    void OnEnable()
    {
        timer = 0f;
        hitTime = 0;
        Time.timeScale = 1f;

        myRay.enabled = true;
        UIRay.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (hitTime >= MEASURE_TIME || timer >= MEASURE_TIME)
        {
            measurePP();
            timer = 0f;
            hitTime = 0;
        }
        else
        {
            if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) >= TRIGGER_THRESHOLD && !justHit)
            {
                Vector3 direction = Vector3.Normalize(laserEnd.position - laserStart.position);
                RaycastHit hitInfo;
                int layerMask = 1 << 2;
                layerMask = ~layerMask;
                
                if (Physics.Raycast(laserStart.position, direction, out hitInfo, laserLength, layerMask))
                {
                    GameObject hitObject = hitInfo.collider.gameObject;
                    if (hitObject == target)
                    {
                        hitTime++;
                        justHit = true;
                    }
                }
            }
            else if (OVRInput.Get(OVRInput.Axis1D.SecondaryIndexTrigger) < TRIGGER_THRESHOLD)
            {
                justHit = false;
            }

            timer += Time.deltaTime;
        }
    }

    void measurePP()
    {
        precision = hitTime / MEASURE_TIME;
        if (precision < PRECISION_THRESHOLD)
        { 
            MPDDisplay.text = string.Format("{0:F1} m", maximalPointingDistance);
            myRay.enabled = false;
            UIRay.enabled = true;
            this.gameObject.SetActive(false);
        }
        else
        {
            maximalPointingDistance = target.transform.position.z;
            target.transform.Translate(new Vector3(0f, 0f, 0.6f));
        }
    }
}

