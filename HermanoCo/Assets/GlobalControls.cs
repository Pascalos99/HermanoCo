using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public static class GlobalControls
{
    // all controls listed here will be changable by the user
    public static ControlKey Jump = new ControlKey(KeyCode.Space, "Jump");
    public static ControlKey Up = new ControlKey(KeyCode.W, "Up");
    public static ControlKey Left = new ControlKey(KeyCode.A, "Left");
    public static ControlKey Down = new ControlKey(KeyCode.S, "Down");
    public static ControlKey Right = new ControlKey(KeyCode.D, "Right");
    public static ControlKey RotateLeft = new ControlKey(KeyCode.Q, "Rotate Left");
    public static ControlKey RotateRight = new ControlKey(KeyCode.E, "Rotate Right");
    public static ControlKey Grapple = new ControlKey(KeyCode.F, "Grapple");
    //
    // Summary:
    //     Returns true during the frame the user starts pressing down the key identified
    //     by the control ControlKey parameter.
    //
    // Parameters:
    //   control:
    public static bool GetKeyDown(ControlKey control)
    {
        return Input.GetKeyDown(control.keycode);
    }
    //
    // Summary:
    //     Returns true while the user holds down the key identified by the control ControlKey
    //     parameter.
    //
    // Parameters:
    //   control:
    public static bool GetKey(ControlKey control)
    {
        return Input.GetKey(control.keycode);
    }
    //
    // Summary:
    //     Returns true during the frame the user releases the key identified by the control
    //     ControlKey parameter.
    //
    // Parameters:
    //   control:
    public static bool GetKeyUp(ControlKey control)
    {
        return Input.GetKeyUp(control.keycode);
    }

    public static void SetKeyTo(ControlKey key, KeyCode keycode)
    {
        key.keycode = keycode;
    }

    public class ControlKey
    {
        public KeyCode keycode;
        public readonly string name;
        public ControlKey(KeyCode keycode, string name)
        {
            this.keycode = keycode;
            this.name = name;
        }
    }
}
