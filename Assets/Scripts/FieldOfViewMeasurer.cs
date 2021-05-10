using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FieldOfViewMeasurer : MonoBehaviour
{
    public Text HFOVDisplay;
    public Text VFOVDisplay;
    public Text DFOVDisplay;

    public float horizontalFieldOfView;
    public float verticalFieldOfView;
    public float diagonalFieldOfView;

    public Transform HFOVObj;
    public Transform VFOVObj;

    const float SPEED = 10f;
    Transform currObj;

    // Start is called before the first frame update
    void Start()
    {
        currObj = HFOVObj;
        currObj.gameObject.SetActive(true);
    }

    void OnEnable()
    {
        currObj = HFOVObj;
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

                if (currObj == HFOVObj)
                {
                    measureHFOV();
                    currObj = VFOVObj;
                    currObj.gameObject.SetActive(true);
                }
                else
                {
                    measureVFOV();
                    currObj = null;
                    measureDFOV();
                    gameObject.SetActive(false);
                }
            }
        }
    }

    void measureHFOV()
    {
        float w = HFOVObj.localScale.x;
        float f = HFOVObj.position.z - HFOVObj.localScale.z / 2;
        horizontalFieldOfView = 2 * Mathf.Atan(w/2 / f) / Mathf.PI * 180;
        HFOVDisplay.text = string.Format("{0:F0}°", horizontalFieldOfView);
    }

    void measureVFOV()
    {
        float h = VFOVObj.localScale.y;
        float f = VFOVObj.position.z - VFOVObj.localScale.z / 2;
        verticalFieldOfView = 2 * Mathf.Atan(h/2 / f) / Mathf.PI * 180;
        VFOVDisplay.text = string.Format("{0:F0}°", verticalFieldOfView);
    }

    void measureDFOV()
    {
        diagonalFieldOfView = Mathf.Sqrt(Mathf.Pow(horizontalFieldOfView, 2) + Mathf.Pow(verticalFieldOfView, 2));
        DFOVDisplay.text = string.Format("{0:F0}°", diagonalFieldOfView);
    }
}
