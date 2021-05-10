using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpatialResolutionMeasurer : MonoBehaviour
{
    public Text HARDisplay;
    public Text VARDisplay;

    public float horizontalAngularResolution;
    public float verticalAngularResolution;

    public Transform HARObj;
    public Transform VARObj;

    const float SPEED = 10f;
    const float STRIP_WIDTH = 0.1f;
    Transform currObj;

    // Start is called before the first frame update
    void Start()
    {
        currObj = HARObj;
        currObj.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        currObj = HARObj;
        currObj.gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (currObj != null)
        {
            Vector3 delta = new Vector3(0, 0, OVRInput.Get(OVRInput.Axis2D.SecondaryThumbstick).y);
            currObj.Translate(delta * SPEED * Time.deltaTime);

            if (OVRInput.GetDown(OVRInput.Button.One))
            {
                currObj.gameObject.SetActive(false);

                if (currObj == HARObj)
                {
                    measureHAR();
                    currObj = VARObj;
                    currObj.gameObject.SetActive(true);
                }
                else
                {
                    measureVAR();
                    currObj = null;
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void measureHAR()
    {
        float f = HARObj.position.z - HARObj.localScale.z / 2;
        horizontalAngularResolution = f * 2 * Mathf.Tan(Mathf.PI / 360) / STRIP_WIDTH;
        HARDisplay.text = string.Format("{0:F0} PPD", horizontalAngularResolution);
    }

    void measureVAR()
    {
        float f = VARObj.position.z - VARObj.localScale.z / 2;
        verticalAngularResolution = f * 2 * Mathf.Tan(Mathf.PI / 360) / STRIP_WIDTH;
        VARDisplay.text = string.Format("{0:F0} PPD", verticalAngularResolution);
    }
}
