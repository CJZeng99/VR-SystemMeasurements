using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Raycaster : MonoBehaviour
{
    public LineRenderer laser;
    public Transform laserStart;
    public Transform laserEnd;
    public float laserLength = 50f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        laser.SetPosition(0, laserStart.position);
        Vector3 direction = Vector3.Normalize(laserEnd.position - laserStart.position);
        laser.SetPosition(1, laserStart.position + direction * laserLength);
    }
}
