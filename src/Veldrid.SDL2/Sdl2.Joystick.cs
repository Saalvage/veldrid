﻿using System;
using System.Runtime.InteropServices;

namespace Veldrid.Sdl2
{
    /// <summary>
    /// A transparent wrapper over a pointer to a native SDL_Joystick.
    /// </summary>
    public struct SDL_Joystick
    {
        /// <summary>
        /// The native SDL_Joystick pointer.
        /// </summary>
        public readonly IntPtr NativePointer;

        public SDL_Joystick(IntPtr pointer)
        {
            NativePointer = pointer;
        }

        public static implicit operator IntPtr(SDL_Joystick controller) => controller.NativePointer;
        public static implicit operator SDL_Joystick(IntPtr pointer) => new SDL_Joystick(pointer);
    }


    public static unsafe partial class Sdl2Native
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int* SDL_GetJoysticks_t(int* count);
        private static SDL_GetJoysticks_t s_sdl_getJoysticks = LoadFunction<SDL_GetJoysticks_t>("SDL_GetJoysticks");
        /// <summary>
        /// Count the number of joysticks attached to the system right now.
        /// </summary>
        public static int* SDL_GetJoysticks(int* count) => s_sdl_getJoysticks(count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetJoystickInstanceID_t(SDL_Joystick joystick);
        private static SDL_GetJoystickInstanceID_t s_sdl_getJoystickInstanceID = LoadFunction<SDL_GetJoystickInstanceID_t>("SDL_GetJoystickInstanceID");
        /// <summary>
        /// Returns the instance ID of the specified joystick on success or a negative error code on failure; call SDL_GetError() for more information.
        /// </summary>
        public static uint SDL_GetJoystickInstanceID(SDL_Joystick joystick) => s_sdl_getJoystickInstanceID(joystick);
    }
}
