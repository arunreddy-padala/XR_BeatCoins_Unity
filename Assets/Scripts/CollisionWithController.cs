using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionWithController : MonoBehaviour 
{

    public void StopVibration()
    {
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0, 0, OVRInput.Controller.LTouch);
    }

    private void OnTriggerEnter(Collider other)
    {
   
        if (other.gameObject.tag.Equals("GreenShape") || other.gameObject.tag.Equals("RedShape"))
        {
            bool match = (gameObject.tag.Equals("LeftController") && other.gameObject.CompareTag("RedShape")) ||
                    (gameObject.tag.Equals("RightController") && other.gameObject.CompareTag("GreenShape"));

            if (gameObject.CompareTag("LeftController"))
            {
                other.gameObject.GetComponent<Breakable>()
                .collidesWithController(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.LTouch), match);
            }
            else
            {
                other.gameObject.GetComponent<Breakable>()
                .collidesWithController(OVRInput.GetLocalControllerVelocity(OVRInput.Controller.RTouch), match);

            }
            

        }

        else if (other.gameObject.tag.Equals("YellowShape") ||
            other.gameObject.tag.Equals("GameStats") ||
            other.gameObject.tag.Equals("BackToFinishMenu"))
        {
            Debug.Log(other.gameObject.GetComponent<BreakableForMenuBall>());
            other.gameObject.GetComponent<BreakableForMenuBall>().Hit();
        }


        OVRInput.SetControllerVibration(0.3f, 0.4f, OVRInput.Controller.RTouch);
        OVRInput.SetControllerVibration(0.3f, 0.4f, OVRInput.Controller.LTouch);
        Invoke("StopVibration", 0.5f);
    }

}
