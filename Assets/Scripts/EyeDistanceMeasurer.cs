using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EyeDistanceMeasurer : MonoBehaviour
{
    public Text EDDisplay;

    public float interPupillaryDistance;

    TouchScreenKeyboard overlayKeyboard;
    string inputText = "";

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void OnEnable()
    {
        enterIPD();
    }

    // Update is called once per frame
    void Update()
    {
        

        if (overlayKeyboard != null)
        {
            inputText = overlayKeyboard.text;
        }

        if (overlayKeyboard.status == TouchScreenKeyboard.Status.Done)
        {
            EDDisplay.text = inputText + " cm";
            gameObject.SetActive(false);
        }
    }

    void enterIPD()
    {
        //interPupillaryDistance = 6.7f;
        //EDDisplay.text = string.Format("{0:F1} cm", interPupillaryDistance);

        overlayKeyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.DecimalPad);
    }
}
