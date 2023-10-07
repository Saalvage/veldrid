using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using Veldrid.Sdl2;
using static Veldrid.Sdl2.Sdl2Native;

namespace Veldrid.NeoDemo
{
    public class Sdl2GamepadTracker : IDisposable
    {
        private readonly uint _controllerId;
        private readonly SDL_Gamepad _controller;

        public string ControllerName { get; }

        private readonly Dictionary<SDL_GamepadAxis, float> _axisValues = new Dictionary<SDL_GamepadAxis, float>();
        private readonly Dictionary<SDL_GamepadButton, bool> _buttons = new Dictionary<SDL_GamepadButton, bool>();

        public unsafe Sdl2GamepadTracker(uint index)
        {
            _controller = SDL_OpenGamepad(index);
            SDL_Joystick joystick = SDL_GetGamepadJoystick(_controller);
            _controllerId = SDL_GetJoystickInstanceID(joystick);
            ControllerName = Marshal.PtrToStringUTF8((IntPtr)SDL_GetGamepadName(_controller));
            Sdl2Events.Subscribe(ProcessEvent);
        }

        public float GetAxis(SDL_GamepadAxis axis)
        {
            _axisValues.TryGetValue(axis, out float ret);
            return ret;
        }

        public bool IsPressed(SDL_GamepadButton button)
        {
            _buttons.TryGetValue(button, out bool ret);
            return ret;
        }

        public static unsafe bool CreateDefault(out Sdl2GamepadTracker sct)
        {
            int jsCount;
            var ret = SDL_GetJoysticks(&jsCount);
            for (int i = 0; i < jsCount; i++)
            {
                if (SDL_IsGamepad(ret[i]))
                {
                    sct = new Sdl2GamepadTracker(i);
                    return true;
                }
            }

            sct = null;
            return false;
        }

        private void ProcessEvent(ref SDL_Event ev)
        {
            switch (ev.type)
            {
                case SDL_EventType.GamepadAxisMotion:
                    SDL_GamepadAxisEvent axisEvent = Unsafe.As<SDL_Event, SDL_GamepadAxisEvent>(ref ev);
                    if (axisEvent.which == _controllerId)
                    {
                        _axisValues[axisEvent.axis] = Normalize(axisEvent.value);
                    }
                    break;
                case SDL_EventType.GamepadButtonDown:
                case SDL_EventType.GamepadButtonUp:
                    SDL_GamepadButtonEvent buttonEvent = Unsafe.As<SDL_Event, SDL_GamepadButtonEvent>(ref ev);
                    if (buttonEvent.which == _controllerId)
                    {
                        _buttons[buttonEvent.button] = buttonEvent.state == 1;
                    }
                    break;
            }
        }

        private float Normalize(short value)
        {
            return value < 0
                ? -(value / (float)short.MinValue)
                : (value / (float)short.MaxValue);
        }

        public void Dispose()
        {
            SDL_CloseGamepad(_controller);
        }
    }
}
