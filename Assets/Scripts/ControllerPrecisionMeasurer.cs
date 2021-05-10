using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ControllerPrecisionMeasurer : MonoBehaviour
{
    public Text PSTDDisplay;
    public Text OSTDDisplay;

    public Vector3 positionStandardDeviation;
    public Vector3 orientationStandardDeviation;

    public Transform controller;

    float timer;
    const float MEASURE_TIME = 5f;

    List<Vector3> positions;
    List<Vector3> orientations;
    Vector3 pSum;
    Vector3 oSum;

    // Start is called before the first frame update
    void Start()
    {
        positions = new List<Vector3>();
        orientations = new List<Vector3>();
        pSum = new Vector3(0f, 0f, 0f);
        oSum = new Vector3(0f, 0f, 0f);

        timer = 0f;
        Time.timeScale = 0f;
    }

    void OnEnable()
    {
        positions = new List<Vector3>();
        orientations = new List<Vector3>();
        pSum = new Vector3(0f, 0f, 0f);
        oSum = new Vector3(0f, 0f, 0f);

        timer = 0f;
        Time.timeScale = 0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            Time.timeScale = 1f;
        }

        if (timer >= MEASURE_TIME)
        {
            measurePSTD();
            measureOSTD();
            this.gameObject.SetActive(false);
        }
        else if (Time.timeScale != 0f)
        {
            positions.Add(controller.position);
            orientations.Add(controller.rotation.eulerAngles);
            pSum = pSum + controller.position;
            oSum = oSum + controller.rotation.eulerAngles;

            timer += Time.deltaTime;
        }
    }

    void measurePSTD()
    {
        Vector3 mean = pSum / positions.Count;
        Vector3 sse = new Vector3(0f, 0f, 0f);
        foreach (Vector3 p in positions)
        {
            Vector3 error = p - mean;
            sse = sse + Vector3.Scale(error, error);
        }
        Vector3 msse = sse / positions.Count;
        positionStandardDeviation = new Vector3(Mathf.Sqrt(msse.x), Mathf.Sqrt(msse.y), Mathf.Sqrt(msse.z));
        PSTDDisplay.text = string.Format("{0:0.###E+0}, {1:0.###E+0}, {2:0.###E+0}", positionStandardDeviation.x, positionStandardDeviation.y, positionStandardDeviation.z);
    }

    void measureOSTD()
    {
        Vector3 mean = oSum / orientations.Count;
        Vector3 sse = new Vector3(0f, 0f, 0f);
        foreach (Vector3 o in orientations)
        {
            Vector3 error = o - mean;
            sse = sse + Vector3.Scale(error, error);
        }
        Vector3 msse = sse / orientations.Count;
        orientationStandardDeviation = new Vector3(Mathf.Sqrt(msse.x), Mathf.Sqrt(msse.y), Mathf.Sqrt(msse.z));
        OSTDDisplay.text = string.Format("{0:0.###E+0}, {1:0.###E+0}, {2:0.###E+0}", orientationStandardDeviation.x, orientationStandardDeviation.y, orientationStandardDeviation.z);
    }
}
