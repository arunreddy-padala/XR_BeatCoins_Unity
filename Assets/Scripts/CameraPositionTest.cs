using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraPositionTest : MonoBehaviour
{
    public OVRCameraRig overCameraRig;
    // Start is called before the first frame update
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = overCameraRig.centerEyeAnchor.position + new Vector3(0, -0.2f, 0.1f); 
    }
}
