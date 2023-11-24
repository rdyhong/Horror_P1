using UnityEngine;

public static class InputMgr
{
    static readonly string INPUT_AXIS_VERTICAL = "Vertical";
    static readonly string INPUT_AXIS_HORIZONTAL = "Horizontal";
    static readonly string INPUT_AXIS_MOUSE_X = "Mouse X";
    static readonly string INPUT_AXIS_MOUSE_Y = "Mouse Y";
    static readonly int LEFT_MOUSE_INDEX = 0;
    static readonly int RIGHT_MOUSE_INDEX = 1;

    static bool s_isStopPlayer = false;

    // Mouse
    public static void SetCursorAvtive(bool active)
    {
        Cursor.visible = active;
        if (active) Cursor.lockState = CursorLockMode.None;
        else Cursor.lockState = CursorLockMode.Locked;
    }

    public static void StopPlayerMove(bool isStopPlayer)
    {
        s_isStopPlayer = isStopPlayer;
    }

    public static float MouseAxisX()
    {
        if (s_isStopPlayer) return 0;
        return Input.GetAxisRaw(INPUT_AXIS_MOUSE_X);
    }

    public static float MouseAxisY()
    {
        if (s_isStopPlayer) return 0;
        return Input.GetAxisRaw(INPUT_AXIS_MOUSE_Y);
    }

    public static bool LMouseDown()
    {
        if (s_isStopPlayer) return false;
        return Input.GetMouseButtonDown(LEFT_MOUSE_INDEX);
    }
    public static bool LMouse()
    {
        if (s_isStopPlayer) return false;
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
        if (s_isStopPlayer) return 0;
        return Input.GetAxis(INPUT_AXIS_HORIZONTAL);
    }
    public static float KeyboardAxisZ()
    {
        if (s_isStopPlayer) return 0;
        return Input.GetAxis(INPUT_AXIS_VERTICAL);
    }
    public static bool KeyDown(KeyCode keyCode)
    {
        return Input.GetKeyDown(keyCode);
    }
    public static bool KeyHold(KeyCode keyCode)
    {
        if (s_isStopPlayer) return false;
        return Input.GetKey(keyCode);
    }
}
