using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("Eye Distance")]
    public GameObject EDManager;
    public Button EDButton;

    [Header("Field of View")]
    public GameObject FOVManager;
    public Button FOVButton;

    [Header("Spatial Resolution")]
    public GameObject SRManager;
    public Button SRButton;

    [Header("Controller Tracking Precision")]
    public GameObject CPManager;
    public Button CPButton;

    [Header("Pointing Precision with Controller")]
    public GameObject PPManager;
    public Button PPButton;

    [Header("Closest Eye Convergence Distance")]
    public GameObject ECManager;
    public Button ECButton;

    GameObject currMeasurement;
    Queue<GameObject> measurements;

    // Start is called before the first frame update
    void Start()
    {
        //measurements = new Queue<GameObject>();
        //measurements.Enqueue(EDManager);
        //measurements.Enqueue(FOVManager);
        //measurements.Enqueue(SRManager);
        //measurements.Enqueue(CPManager);
        //measurements.Enqueue(PPManager);
        //measurements.Enqueue(ECManager);

        //currMeasurement = measurements.Dequeue();
        currMeasurement = EDManager;

        EDButton.GetComponent<Button>().onClick.AddListener(enterED);
        FOVButton.GetComponent<Button>().onClick.AddListener(measureFOV);
        SRButton.GetComponent<Button>().onClick.AddListener(measureSR);
        CPButton.GetComponent<Button>().onClick.AddListener(measureCP);
        PPButton.GetComponent<Button>().onClick.AddListener(measurePP);
        ECButton.GetComponent<Button>().onClick.AddListener(measureEC);
    }

    // Update is called once per frame
    void Update()
    {
        //if (currMeasurement != null)
        //{
        //    if (!currMeasurement.activeSelf)
        //    {
        //        currMeasurement = measurements.Dequeue();
        //        currMeasurement.SetActive(true);
        //    }
        //}
    }

    void enterED()
    {
        currMeasurement.SetActive(false);
        currMeasurement = EDManager;
        currMeasurement.SetActive(true);
    }

    void measureFOV()
    {
        currMeasurement.SetActive(false);
        currMeasurement = FOVManager;
        currMeasurement.SetActive(true);
    }

    void measureSR()
    {
        currMeasurement.SetActive(false);
        currMeasurement = SRManager;
        currMeasurement.SetActive(true);
    }

    void measureCP()
    {
        currMeasurement.SetActive(false);
        currMeasurement = CPManager;
        currMeasurement.SetActive(true);
    }

    void measurePP()
    {
        currMeasurement.SetActive(false);
        currMeasurement = PPManager;
        currMeasurement.SetActive(true);
    }

    void measureEC()
    {
        currMeasurement.SetActive(false);
        currMeasurement = ECManager;
        currMeasurement.SetActive(true);
    }
}
