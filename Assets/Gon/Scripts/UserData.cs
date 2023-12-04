using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UserData
{
    static bool _isLoaded = false;

    public static float s_MouseSensitiveX => _mouseSensitiveX;
    static float _mouseSensitiveX = 0;
    public static float s_MouseSensitiveY => _mouseSensitiveY;
    static float _mouseSensitiveY = 0;
    public static float s_ScreenFov => _screenFov;
    static float _screenFov = 0;

    public static void Init()
    {
        if (_isLoaded) return;
        _isLoaded = true;

        RefreshData();
    }

    public static void RefreshData()
    {
        _mouseSensitiveX = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_X);
        _mouseSensitiveY = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_MOUSE_SENSITIVE_Y);
        _screenFov = PlayerPrefsHelper.GetFlt(PlayerPrefsHelper.PPKEY_SCREEN_FOV);
    }
}
