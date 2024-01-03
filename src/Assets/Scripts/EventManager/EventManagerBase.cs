using Assets.Scripts.Helpers;
using Assets.Settings.InputSystem;
using UnityEngine;

public class EventManagerBase : Singleton<EventManagerBase>
{
    protected TouchManager _touchManager;
    protected GameObject _canvas;
    protected GameObject _joyStick;

    protected const string JOYSTICK_TAG = "JoyStick";

}
