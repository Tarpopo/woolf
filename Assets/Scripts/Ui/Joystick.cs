using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.UI;

public class Joystick : MonoBehaviour
{
    private bool aroundPos=true;
    public GameObject Allstick;
    public Transform cicleStick;
    public float radius;
    private Vector2 direct;
    public Vector3 posDirection;
    public float deistvie;
    public bool go=false;
    public Camera mainCamera;
    
    // private void Start()
    // {
    //     
    // }
    
    private void Update()
    { 
        if (Input.touches.Length>0)
        {
            var mouse=mainCamera.ScreenToWorldPoint(Input.GetTouch(0).position);
            //print(Vector2.Distance(Allstick.transform.position, mouse));
            if (aroundPos)
            {
                Allstick.SetActive(true);
                Allstick.transform.position = (Vector2) mouse;
                aroundPos = false;
            }
            if (Vector2.Distance(Allstick.transform.position, mouse) < radius)
            {
                if (Vector2.Distance(Allstick.transform.position, mouse) > deistvie)
                    go = true;
                else
                {
                    posDirection = Vector3.zero;
                    go = false;
                }

                cicleStick.position = mouse;
            }
            if (Input.GetTouch(0).phase==TouchPhase.Ended)
            {
                go = false;
                cicleStick.position = Allstick.transform.position;
                aroundPos = true;
                posDirection = Vector3.zero;
                direct = Vector2.zero;
            }
            else
            {
                direct=(Vector2)mouse-new Vector2(Allstick.transform.position.x,Allstick.transform.position.y);
                float angle = Mathf.Atan2(direct.y, direct.x);
                posDirection = new Vector3(Mathf.Cos(angle), Mathf.Sin(angle), 0f);
                direct=Allstick.transform.position+posDirection*radius;
                cicleStick.position = direct;
            }
            
        } 

        // if (Input.GetMouseButtonUp(0))
        // {
        //     go = false;
        //     cicleStick.position = Allstick.transform.position;
        //     aroundPos = true;
        //     posDirection = Vector3.zero;
        // }
    }
   
}
