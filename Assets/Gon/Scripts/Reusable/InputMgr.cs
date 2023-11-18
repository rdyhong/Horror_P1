using UnityEngine;

public static class InputMgr
{
    static readonly string INPUT_AXIS_VERTICAL = "Vertical";
    static readonly string INPUT_AXIS_HORIZONTAL = "Horizontal";
    static readonly string INPUT_AXIS_MOUSE_X = "Mouse X";
    static readonly string INPUT_AXIS_MOUSE_Y = "Mouse Y";
    static readonly int LEFT_MOUSE_INDEX = 0;
    static readonly int RIGHT_MOUSE_INDEX = 1;

    // Mouse
    public static float MouseAxisX()
    {
        return Input.GetAxis(INPUT_AXIS_MOUSE_X);
    }

    public static float MouseAxisY()
    {
        return Input.GetAxis(INPUT_AXIS_MOUSE_Y);
    }

    public static bool LMouseDown()
    {
        return Input.GetMouseButtonDown(LEFT_MOUSE_INDEX);
    }
    public static bool LMouse()
    {
        return Input.GetMouseButton(LEFT_MOUSE_INDEX);
    }
    public static bool LMouseUp()
    {
        return Input.GetMouseButtonUp(LEFT_MOUSE_INDEX);
    }
    public static bool RMouseDown()
    {
        return Input.GetMouseButtonDown(RIGHT_MOUSE_INDEX);
    }
    public static bool RMouse()
    {
        return Input.GetMouseButton(RIGHT_MOUSE_INDEX);
    }
    public static bool RMouseUp()
    {
        return Input.GetMouseButtonUp(RIGHT_MOUSE_INDEX);
    }

    //Keyboard
    public static float KeyboardAxisX()
    {
        return Input.GetAxis(INPUT_AXIS_HORIZONTAL);
    }
    public static float KeyboardAxisY()
    {
        return Input.GetAxis(INPUT_AXIS_VERTICAL);
    }
    public static bool KeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }
    public static bool KeyHold(KeyCode keyCode)
    {
        return Input.GetKey(keyCode);
    }
}
