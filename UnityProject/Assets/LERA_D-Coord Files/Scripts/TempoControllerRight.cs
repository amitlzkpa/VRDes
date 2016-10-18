using UnityEngine;
using System.Collections;


namespace WandControlsManager
{

	// ref: https://www.youtube.com/watch?v=LZTctk19sx8
	public class TempoControllerRight : MonoBehaviour
	{



		//////////////////////////////////////////////////////////////////////////



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
			return swipeRight;
		}

		public static bool getTouchPadSwipeLeft()
		{
			return swipeLeft;
		}

		public static bool getTouchPadSwipeUp()
		{
			return swipeUp;
		}

		public static bool getTouchPadSwipeDown()
		{
			return swipeDown;
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
		}







		string triggerButtonAlternative = "Fire1";
		string touchPadButtonAlternative = "Fire2";
		string gripButtonAlternative = "Fire3";
		KeyCode swipeRightAlternative = KeyCode.L;
		KeyCode swipeLeftAlternative = KeyCode.J;
		KeyCode swipeUpAlternative = KeyCode.I;
		KeyCode swipeDownAlternative = KeyCode.K;







		// Update is called once per frame
		void Update()
		{

			/////////////////////////////////////

			gripButtonDown = Input.GetButtonDown(gripButtonAlternative);
			gripButtonUp = Input.GetButtonUp(gripButtonAlternative);
			gripButtonPressed = Input.GetButton(gripButtonAlternative);

			/////////////////////////////////////

			triggerButtonDown = Input.GetButtonDown(triggerButtonAlternative);
			triggerButtonUp = Input.GetButtonUp(triggerButtonAlternative);
			triggerButtonPressed = Input.GetButton(triggerButtonAlternative);

			/////////////////////////////////////

			menuButtonDown = false;
			menuButtonUp = false;
			menuButtonPressed = false;

			/////////////////////////////////////

			swipeRight = Input.GetKeyDown(swipeRightAlternative);
			swipeDown = Input.GetKeyDown(swipeDownAlternative);
			swipeLeft = Input.GetKeyDown(swipeLeftAlternative);
			swipeUp = Input.GetKeyDown(swipeUpAlternative);

			/////////////////////////////////////

			touchPadButtonDown = Input.GetButtonDown(touchPadButtonAlternative);
			touchPadButtonUp = Input.GetButtonUp(touchPadButtonAlternative);
			touchPadButtonPressed = Input.GetButton(touchPadButtonAlternative);
		}
	}
}