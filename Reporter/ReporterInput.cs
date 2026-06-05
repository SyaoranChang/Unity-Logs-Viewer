using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.EnhancedTouch;
using TouchPhase = UnityEngine.InputSystem.TouchPhase;
using InputTouch = UnityEngine.InputSystem.EnhancedTouch.Touch;

public static class ReporterInput
{
	public struct TouchInfo
	{
		public Vector2 position;
		public TouchPhase phase;
	}

	static bool enhancedTouchEnabled;

	public static void Enable()
	{
		if (enhancedTouchEnabled)
			return;

		EnhancedTouchSupport.Enable();
		enhancedTouchEnabled = true;
	}

	public static void Disable()
	{
		if (!enhancedTouchEnabled)
			return;

		EnhancedTouchSupport.Disable();
		enhancedTouchEnabled = false;
	}

	public static bool IsTouchPlatform =>
		Application.platform == RuntimePlatform.Android ||
		Application.platform == RuntimePlatform.IPhonePlayer;

	public static int touchCount => InputTouch.activeTouches.Count;

	public static TouchInfo GetTouch(int index)
	{
		var touch = InputTouch.activeTouches[index];
		return new TouchInfo
		{
			position = touch.screenPosition,
			phase = touch.phase
		};
	}

	public static bool GetMouseButton(int button)
	{
		var mouse = Mouse.current;
		return mouse != null && button == 0 && mouse.leftButton.isPressed;
	}

	public static bool GetMouseButtonDown(int button)
	{
		var mouse = Mouse.current;
		return mouse != null && button == 0 && mouse.leftButton.wasPressedThisFrame;
	}

	public static bool GetMouseButtonUp(int button)
	{
		var mouse = Mouse.current;
		return mouse != null && button == 0 && mouse.leftButton.wasReleasedThisFrame;
	}

	public static Vector2 mousePosition
	{
		get
		{
			var mouse = Mouse.current;
			return mouse != null ? mouse.position.ReadValue() : Vector2.zero;
		}
	}
}
