using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.EnhancedTouch;
using Touch = UnityEngine.InputSystem.EnhancedTouch.Touch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
public class PlayerControl : MonoBehaviour
{
    // Start is called before the first frame update
    private Camera mainCam;
    private Vector3 offset;

    private float maxleft;
    private float maxright; 
    private float maxdown;
    private float maxup;
    void Start()
    {
        mainCam= Camera.main;
        maxleft = mainCam.ViewportToWorldPoint(new Vector2(0.15f, 0)).x;
        maxright = mainCam.ViewportToWorldPoint(new Vector2(0.85f, 0)).x;

        maxdown = mainCam.ViewportToWorldPoint(new Vector2(0, 0.05f)).y;
        maxup = mainCam.ViewportToWorldPoint(new Vector2(0, 0.6f)).y;
    
    }

    // Update is called once per frame
    void Update()
    {
        if (Touch.fingers[0].isActive)
        {
            Touch mytouch = Touch.activeTouches[0];
            Vector3 touchPos = mytouch.screenPosition;
            touchPos=mainCam.ScreenToWorldPoint(touchPos);

            if (Touch.activeTouches[0].phase == TouchPhase.Began) { 
            
               offset = touchPos-transform.position;
            
            }
            if (Touch.activeTouches[0].phase==TouchPhase.Moved) 
            {

                transform.position = new Vector3(touchPos.x-offset.x, touchPos.y-offset.y, 0);

            }
            if (Touch.activeTouches[0].phase==TouchPhase.Stationary) { 
            
            transform.position=new Vector3(touchPos.x-offset.x,touchPos.y-offset.y, 0);
            
            
            }

            transform.position = new Vector3(Mathf.Clamp(transform.position.x, maxleft, maxright), Mathf.Clamp(transform.position.y,maxdown,maxup),0);

        }
    }

    private void OnEnable()
    {
        EnhancedTouchSupport.Enable();
    }

    private void OnDisable()
    {
        EnhancedTouchSupport.Disable();
    }
}
