using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTest : MonoBehaviour
{
    void Start()
    {
        var pixelMult = 1; // scaling factor, assumes 100ppu unity default, and scales up to my desired 3 pixel squares.
 
        var camera = GetComponent<Camera>();
        var camFrustWidthShouldBe = Screen.height / 100f;
        var frustrumInnerAngles = (180f - camera.fieldOfView) / 2f * Mathf.PI / 180f;
        var newCamDist = Mathf.Tan(frustrumInnerAngles) * (camFrustWidthShouldBe/2);
        transform.position = new Vector3(0, 0, -newCamDist / pixelMult);
    }
    
}
