using UnityEngine;

[RequireComponent(typeof(GUITexture))]
public class Joystick : MonoBehaviour
{
    //边缘范围
    class Boundary
    {
        public Vector2 min = Vector2.zero;
        public Vector2 max = Vector2.zero;
    }
    //所有的摇杆
    private static Joystick[] joysticks;
    //是否枚举所有的摇杆
    private static bool enumeratedJoysticks = false;
    //两个敲击之间的时间
    private static float tapTimeDelta = 0.3f;
    public bool touchPad;
    //摇杆的位置（x,z)
    public Vector2 position = Vector2.zero;
    //触摸的区域
    public Rect touchZone;
    //超出范围
    public Vector2 deadZone = Vector2.zero;	 // Control when position is output

    public bool normalize = false; // Normalize output after the dead-zone?

    public int tapCount;

    private int lastFingerId = -1;	 // Finger last used for this joystick

    private float tapTimeWindow;	 // How much time there is left for a tap to occur

    private Vector2 fingerDownPos;

    //private float fingerDownTime;

    //private float firstDeltaTime = 0.5f;
    private GUITexture gui;

    private Rect defaultRect;	 // Default position / extents of the joystick graphic

    private Boundary guiBoundary = new Boundary();	 // Boundary for joystick graphic

    private Vector2 guiTouchOffset;	 // Offset to apply to touch input

    private Vector2 guiCenter;	 // Center of joystick
    void Start()
    {

        gui = (GUITexture)GetComponent(typeof(GUITexture));
        defaultRect = gui.pixelInset;

        defaultRect.x += transform.position.x * Screen.width;// + gui.pixelInset.x; // -  Screen.width * 0.5;

        defaultRect.y += transform.position.y * Screen.height;// - Screen.height * 0.5;
        transform.position = Vector3.zero;
        if (touchPad)
        {

            // If a texture has been assigned, then use the rect ferom the gui as our touchZone

            if (gui.texture)

                touchZone = defaultRect;

        }
        else
        {

            guiTouchOffset.x = defaultRect.width * 0.5f;

            guiTouchOffset.y = defaultRect.height * 0.5f;
            // Cache the center of the GUI, since it doesn't change

            guiCenter.x = defaultRect.x + guiTouchOffset.x;

            guiCenter.y = defaultRect.y + guiTouchOffset.y;
            // Let's build the GUI boundary, so we can clamp joystick movement

            guiBoundary.min.x = defaultRect.x - guiTouchOffset.x;

            guiBoundary.max.x = defaultRect.x + guiTouchOffset.x;

            guiBoundary.min.y = defaultRect.y - guiTouchOffset.y;

            guiBoundary.max.y = defaultRect.y + guiTouchOffset.y;

        }

    }
    public Vector2 getGUICenter()
    {

        return guiCenter;

    }
    void Disable()
    {

        gameObject.SetActive(false);

        //enumeratedJoysticks = false;

    }
    private void ResetJoystick()
    {

        gui.pixelInset = defaultRect;

        lastFingerId = -1;

        position = Vector2.zero;

        fingerDownPos = Vector2.zero;

    }
    private bool IsFingerDown()
    {

        return (lastFingerId != -1);

    }
    public void LatchedFinger(int fingerId)
    {

        // If another joystick has latched this finger, then we must release it

        if (lastFingerId == fingerId)

            ResetJoystick();

    }
    void Update()
    {

        if (!enumeratedJoysticks)
        {

            // Collect all joysticks in the game, so we can relay finger latching messages

            joysticks = (Joystick[])FindObjectsOfType(typeof(Joystick));

            enumeratedJoysticks = true;

        }
        int count = Input.touchCount;
        if (tapTimeWindow > 0)

            tapTimeWindow -= Time.deltaTime;

        else

            tapCount = 0;
        if (count == 0)

            ResetJoystick();

        else
        {

            for (int i = 0; i < count; i++)
            {

                Touch touch = Input.GetTouch(i);

                Vector2 guiTouchPos = touch.position - guiTouchOffset;
                bool shouldLatchFinger = false;

                if (touchPad)
                {

                    if (touchZone.Contains(touch.position))

                        shouldLatchFinger = true;

                }

                else if (gui.HitTest(touch.position))
                {

                    shouldLatchFinger = true;

                }
                // Latch the finger if this is a new touch

                if (shouldLatchFinger && (lastFingerId == -1 || lastFingerId != touch.fingerId))
                {
                    if (touchPad)
                    {

                        //gui.color.a = 0.15;

                        lastFingerId = touch.fingerId;

                        //fingerDownPos = touch.position;

                        //fingerDownTime = Time.time;

                    }
                    lastFingerId = touch.fingerId;



                    // Accumulate taps if it is within the time window

                    if (tapTimeWindow > 0)

                        tapCount++;

                    else
                    {

                        tapCount = 1;

                        tapTimeWindow = tapTimeDelta;

                    }
                    // Tell other joysticks we've latched this finger

                    //for (  j : Joystick in joysticks )

                    foreach (Joystick j in joysticks)
                    {

                        if (j != this)

                            j.LatchedFinger(touch.fingerId);

                    }

                }
                if (lastFingerId == touch.fingerId)
                {

                    // Override the tap count with what the iPhone SDK reports if it is greater

                    // This is a workaround, since the iPhone SDK does not currently track taps

                    // for multiple touches

                    if (touch.tapCount > tapCount)

                        tapCount = touch.tapCount;
                    if (touchPad)
                    {

                        // For a touchpad, let's just set the position directly based on distance from initial touchdown

                        position.x = Mathf.Clamp((touch.position.x - fingerDownPos.x) / (touchZone.width / 2), -1, 1);

                        position.y = Mathf.Clamp((touch.position.y - fingerDownPos.y) / (touchZone.height / 2), -1, 1);

                    }
                    else
                    {

                        // Change the location of the joystick graphic to match where the touch is

                        Rect r = gui.pixelInset;

                        r.x = Mathf.Clamp(guiTouchPos.x, guiBoundary.min.x, guiBoundary.max.x);

                        r.y = Mathf.Clamp(guiTouchPos.y, guiBoundary.min.y, guiBoundary.max.y);

                        gui.pixelInset = r;

                    }
                    if (touch.phase == TouchPhase.Ended || touch.phase == TouchPhase.Canceled)

                        ResetJoystick();

                }

            }

        }
        if (!touchPad)
        {

            // Get a value between -1 and 1 based on the joystick graphic location

            position.x = (gui.pixelInset.x + guiTouchOffset.x - guiCenter.x) / guiTouchOffset.x;

            position.y = (gui.pixelInset.y + guiTouchOffset.y - guiCenter.y) / guiTouchOffset.y;

        }
        // Adjust for dead zone

        var absoluteX = Mathf.Abs(position.x);

        var absoluteY = Mathf.Abs(position.y);



        if (absoluteX < deadZone.x)
        {

            // Report the joystick as being at the center if it is within the dead zone

            position.x = 0;

        }

        else if (normalize)
        {

            // Rescale the output after taking the dead zone into account

            position.x = Mathf.Sign(position.x) * (absoluteX - deadZone.x) / (1 - deadZone.x);

        }
        if (absoluteY < deadZone.y)
        {

            // Report the joystick as being at the center if it is within the dead zone

            position.y = 0;

        }

        else if (normalize)
        {

            // Rescale the output after taking the dead zone into account

            position.y = Mathf.Sign(position.y) * (absoluteY - deadZone.y) / (1 - deadZone.y);

        }
    }
}