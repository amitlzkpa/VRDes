using UnityEngine;
using System.Collections;


namespace WandControlsManager
{

    // ref: https://www.youtube.com/watch?v=LZTctk19sx8
    public class WandControllerLeft : MonoBehaviour
    {



        //////////////////////////////////////////////////////////////////////////



        private SteamVR_TrackedObject trackedObj;
        private SteamVR_Controller.Device controller { get { return SteamVR_Controller.Input((int)trackedObj.index); } }



        private Valve.VR.EVRButtonId gripButton = Valve.VR.EVRButtonId.k_EButton_Grip;
        private static bool gripButtonDown = false;
        private static bool gripButtonUp = false;
        private static bool gripButtonPressed = false;



        public static bool getGripDown()
        {
            return gripButtonDown;
        }

        public static bool getGripUp()
        {
            return gripButtonUp;
        }

        public static bool getGripPressed()
        {
            return gripButtonPressed;
        }



        //////////////////////////////////////////////////////////////////////////



        private Valve.VR.EVRButtonId triggerButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Trigger;
        private static bool triggerButtonDown = false;
        private static bool triggerButtonUp = false;
        private static bool triggerButtonPressed = false;



        public static bool getTriggerDown()
        {
            return triggerButtonDown;
        }


        public static bool getTriggerUp()
        {
            return triggerButtonUp;
        }


        public static bool getTriggerPressed()
        {
            return triggerButtonPressed;
        }



        //////////////////////////////////////////////////////////////////////////



        private Valve.VR.EVRButtonId menuButton = Valve.VR.EVRButtonId.k_EButton_ApplicationMenu;
        private static bool menuButtonDown = false;
        private static bool menuButtonUp = false;
        private static bool menuButtonPressed = false;



        public static bool getMenuDown()
        {
            return menuButtonDown;
        }


        public static bool getMenuUp()
        {
            return menuButtonUp;
        }


        public static bool getMenuPressed()
        {
            return menuButtonPressed;
        }



        //////////////////////////////////////////////////////////////////////////



        private Valve.VR.EVRButtonId touchPadButton = Valve.VR.EVRButtonId.k_EButton_SteamVR_Touchpad;
        private static bool touchPadButtonDown = false;
        private static bool touchPadButtonUp = false;
        private static bool touchPadButtonPressed = false;



        public static bool getTouchPadButtonDown()
        {
            return touchPadButtonDown;
        }


        public static bool getTouchPadButtonUp()
        {
            return touchPadButtonUp;
        }


        public static bool getTouchPadButtonPressed()
        {
            return touchPadButtonPressed;
        }



        //////////////////////////////////////////////////////////////////////////






        private Valve.VR.EVRButtonId touchPadAxis = Valve.VR.EVRButtonId.k_EButton_Axis0;
        private static float touchPadX = 0f;
        private static float touchPadY = 0f;
        private static bool touchPadTouched = false;
        private static bool touchPadTouchedDown = false;
        private static bool touchPadTouchedUp = false;

        private static bool touchPadTouchedPrev = false;
        private static float touchPadPrevX = 0f;
        private static float touchPadPrevY = 0f;



        public static float getTouchPadX()
        {
            return touchPadX;
        }


        public static float getTouchPadY()
        {
            return touchPadY;
        }


        public static bool getTouchPadTouchedDown()
        {
            return touchPadTouchedDown;
        }


        public static bool getTouchPadTouchedUp()
        {
            return touchPadTouchedUp;
        }


        public static bool getTouchPadTouched()
        {
            return touchPadTouched;
        }




        private static bool swipeRight = false;
        private static bool swipeLeft = false;
        private static float touchDownX;
        private static float touchUpX;
        private static bool swipeUp = false;
        private static bool swipeDown = false;
        private static float touchDownY;
        private static float touchUpY;
        private static float touchDownTime;
        private static float touchUpTime;
        private static float swipeInterval = 1.2f;
        private static float swipeWidth = 1f;


        public static bool getTouchPadSwipeRight()
        {
            return touchPadTouchedUp && swipeRight;
        }

        public static bool getTouchPadSwipeLeft()
        {
            return touchPadTouchedUp && swipeLeft;
        }

        public static bool getTouchPadSwipeUp()
        {
            return touchPadTouchedUp && swipeUp;
        }

        public static bool getTouchPadSwipeDown()
        {
            return touchPadTouchedUp && swipeDown;
        }



        //////////////////////////////////////////////////////////////////////////



            
        private static bool touchPadButtonClickUp = false;
        private static bool touchPadButtonClickDown = false;
        private static bool touchPadButtonClickRight = false;
        private static bool touchPadButtonClickLeft = false;
        private static float buttonActiveOffset = 0.4f;



        public static bool getTouchPadButtonClickUp()
        {
            return touchPadButtonClickUp;
        }

        public static bool getTouchPadButtonClickDown()
        {
            return touchPadButtonClickDown;
        }

        public static bool getTouchPadButtonClickLeft()
        {
            return touchPadButtonClickLeft;
        }

        public static bool getTouchPadButtonClickRight()
        {
            return touchPadButtonClickRight;
        }



        //////////////////////////////////////////////////////////////////////////



        // Use this for initialization
        void Start()
        {
            trackedObj = GetComponent<SteamVR_TrackedObject>();
        }









        // Update is called once per frame
        void Update()
        {
            if (controller == null)
            {
                Debug.Log("Left controller not connected");
                return;
            }

            /////////////////////////////////////

            gripButtonDown = controller.GetPressDown(gripButton);
            gripButtonUp = controller.GetPressUp(gripButton);
            gripButtonPressed = controller.GetPress(gripButton);

            /////////////////////////////////////

            triggerButtonDown = controller.GetPressDown(triggerButton);
            triggerButtonUp = controller.GetPressUp(triggerButton);
            triggerButtonPressed = controller.GetPress(triggerButton);

            /////////////////////////////////////

            menuButtonDown = controller.GetPressDown(menuButton);
            menuButtonUp = controller.GetPressUp(menuButton);
            menuButtonPressed = controller.GetPress(menuButton);

            /////////////////////////////////////

            touchPadX = controller.GetAxis().x;
            touchPadY = controller.GetAxis().y;
            touchPadTouched = ((touchPadX != 0) || (touchPadY != 0));
            touchPadTouchedDown = false;
            swipeRight = false;
            swipeLeft = false;
            swipeUp = false;
            swipeDown = false;
            touchPadButtonClickUp = false;
            touchPadButtonClickDown = false;
            touchPadButtonClickRight = false;
            touchPadButtonClickLeft = false;
            if (!touchPadTouchedPrev && touchPadTouched)
            {
                touchPadTouchedDown = true;
                touchPadTouchedUp = false;
                touchPadTouched = false;
            }
            else if (touchPadTouchedPrev && !touchPadTouched)
            {
                touchPadTouchedDown = false;
                touchPadTouchedUp = true;
                touchPadTouched = false;
            }
            else if (touchPadTouchedPrev && touchPadTouched)
            {
                touchPadTouchedDown = false;
                touchPadTouchedUp = false;
            }
            else
            {
                touchPadTouchedDown = false;
                touchPadTouchedUp = false;
            }


            if (touchPadTouchedDown)
            {
                touchDownX = touchPadX;
                touchDownY = touchPadY;
                touchDownTime = Time.time;
            }
            if (touchPadTouchedUp)
            {
                touchUpX = touchPadPrevX;
                touchUpY = touchPadPrevY;
                touchUpTime = Time.time;
            }


            if (touchPadTouchedUp && ((touchUpX - touchDownX) > swipeWidth))
            {
                if ((touchUpTime - touchDownTime) < swipeInterval)
                {
                    swipeRight = true;
                }
                touchDownX = 0;
                touchUpX = 0;
                touchDownY = 0;
                touchUpY = 0;
            }


            if (touchPadTouchedUp && ((touchDownX - touchUpX) > swipeWidth))
            {
                if ((touchUpTime - touchDownTime) < swipeInterval)
                {
                    swipeLeft = true;
                }
                touchDownX = 0;
                touchUpX = 0;
                touchDownY = 0;
                touchUpY = 0;
            }


            if (touchPadTouchedUp && ((touchUpY - touchDownY) > swipeWidth))
            {
                if ((touchUpTime - touchDownTime) < swipeInterval)
                {
                    swipeUp = true;
                }
                touchDownX = 0;
                touchUpX = 0;
                touchDownY = 0;
                touchUpY = 0;
            }


            if (touchPadTouchedUp && ((touchDownY - touchUpY) > swipeWidth))
            {
                if ((touchUpTime - touchDownTime) < swipeInterval)
                {
                    swipeDown = true;
                }
                touchDownX = 0;
                touchUpX = 0;
                touchDownY = 0;
                touchUpY = 0;
            }

            /////////////////////////////////////

            touchPadButtonDown = controller.GetPressDown(touchPadButton);
            touchPadButtonUp = controller.GetPressUp(touchPadButton);
            touchPadButtonPressed = controller.GetPress(touchPadButton);

            /////////////////////////////////////

            if (touchPadButtonPressed)
            {
                if (touchDownY > buttonActiveOffset) touchPadButtonClickUp = true;
                if (touchDownY < -buttonActiveOffset) touchPadButtonClickDown = true;
                if (touchDownX > buttonActiveOffset) touchPadButtonClickRight = true;
                if (touchDownX < -buttonActiveOffset) touchPadButtonClickLeft = true;
            }

            /////////////////////////////////////

            touchPadPrevX = touchPadX;
            touchPadPrevY = touchPadY;
            touchPadTouchedPrev = (touchPadTouched || touchPadTouchedDown);
        }
    }
}