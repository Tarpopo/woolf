using System.Collections;
using System.Collections.Generic;
using Cinemachine;
using UnityEngine;

public class Shake : MonoBehaviour
{
    public CinemachineVirtualCamera VirtCam;
    private CinemachineBasicMultiChannelPerlin CamNoise;
    private bool trig=true;
    public Actor actor;
    void Start()
    {
        CamNoise = VirtCam.GetCinemachineComponent<Cinemachine.CinemachineBasicMultiChannelPerlin>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            trig = !trig;
            if (trig)
            {
                CamNoise.m_AmplitudeGain = 1;
                CamNoise.m_FrequencyGain = 1;
            }
            else
            {
                CamNoise.m_AmplitudeGain = 0;
                CamNoise.m_FrequencyGain = 0;
            }
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            actor.Restart();
            actor.transform.position=new Vector3(-1,-1,-1);
        }

    }
}
