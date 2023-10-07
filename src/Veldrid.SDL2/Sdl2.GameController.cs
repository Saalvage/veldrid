using System;
using System.Runtime.InteropServices;

namespace Veldrid.Sdl2
{
    /// <summary>
    /// A transparent wrapper over a pointer to a native SDL_Gamepad.
    /// </summary>
    public struct SDL_Gamepad
    {
        /// <summary>
        /// The native SDL_Gamepad pointer.
        /// </summary>
        public readonly IntPtr NativePointer;

        public SDL_Gamepad(IntPtr pointer)
        {
            NativePointer = pointer;
        }

        public static implicit operator IntPtr(SDL_Gamepad controller) => controller.NativePointer;
        public static implicit operator SDL_Gamepad(IntPtr pointer) => new SDL_Gamepad(pointer);
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadAxisEvent
    {
        /// <summary>
        /// SDL_CONTROLLERAXISMOTION.
        /// </summary>
        public uint type;
        /// <summary>
        /// In nanoseconds, populated using SDL_GetTicksNS().
        /// </summary>
        public ulong timestamp;
        /// <summary>
        /// The joystick instance id.
        /// </summary>
        public int which;
        /// <summary>
        /// The controller axis.
        /// </summary>
        public SDL_GamepadAxis axis;
        private byte padding1;
        private byte padding2;
        private byte padding3;
        /// <summary>
        /// The axis value (range: -32768 to 32767)
        /// </summary>
        public short value;
        private ushort padding4;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadButtonEvent
    {
        /// <summary>
        /// SDL_CONTROLLERBUTTONDOWN or SDL_CONTROLLERBUTTONUP.
        /// </summary>
        public uint type;
        /// <summary>
        /// In nanoseconds, populated using SDL_GetTicksNS().
        /// </summary>
        public ulong timestamp;
        /// <summary>
        /// The joystick instance id.
        /// </summary>
        public int which;
        /// <summary>
        /// The controller button
        /// </summary>
        public SDL_GamepadButton button;
        /// <summary>
        /// SDL_PRESSED or SDL_RELEASED
        /// </summary>
        public byte state;
        private byte padding1;
        private byte padding2;
    }

    [StructLayout(LayoutKind.Sequential)]
    public struct SDL_GamepadDeviceEvent
    {
        /// <summary>
        /// SDL_CONTROLLERDEVICEADDED, SDL_CONTROLLERDEVICEREMOVED, or SDL_CONTROLLERDEVICEREMAPPED.
        /// </summary>
        public uint type;
        /// <summary>
        /// In nanoseconds, populated using SDL_GetTicksNS().
        /// </summary>
        public ulong timestamp;
        /// <summary>
        /// The joystick device index for the ADDED event, instance id for the REMOVED or REMAPPED event.
        /// </summary>
        public int which;
    }

    /// <summary>
    /// The list of axes available from a controller.
    /// Thumbstick axis values range from SDL_Joystick_AXIS_MIN to SDL_Joystick_AXIS_MAX,
    /// and are centered within ~8000 of zero, though advanced UI will allow users to set
    /// or autodetect the dead zone, which varies between controllers.
    /// Trigger axis values range from 0 to SDL_Joystick_AXIS_MAX.
    /// </summary>
    public enum SDL_GamepadAxis : byte
    {
        Invalid = unchecked((byte)-1),
        LeftX = 0,
        LeftY,
        RightX,
        RightY,
        TriggerLeft,
        TriggerRight,
        Max,
    }

    /// <summary>
    /// The list of buttons available from a controller.
    /// </summary>
    public enum SDL_GamepadButton : byte
    {
        Invalid = unchecked((byte)-1),
        A = 0,
        B,
        X,
        Y,
        Back,
        Guide,
        Start,
        LeftStick,
        RightStick,
        LeftShoulder,
        RightShoulder,
        DPadUp,
        DPadDown,
        DPadLeft,
        DPadRight,
        Max
    }

    public static unsafe partial class Sdl2Native
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Gamepad SDL_OpenGamepad_t(uint instance_id);
        private static SDL_OpenGamepad_t s_sdl_openGamepad = LoadFunction<SDL_OpenGamepad_t>("SDL_OpenGamepad");
        /// <summary>
        /// Open a game controller for use.
        /// The index passed as an argument refers to the N'th game controller on the system. This index is not the value which
        /// will identify this controller in future controller events. The joystick's instance id will be used there instead.
        /// </summary>
        /// <returns>A controller identifier, or NULL if an error occurred.</returns>
        public static SDL_Gamepad SDL_OpenGamepad(uint instance_id) => s_sdl_openGamepad(instance_id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_CloseGamepad_t(SDL_Gamepad gamepad);
        private static SDL_CloseGamepad_t s_sdl_closeGamepad = LoadFunction<SDL_CloseGamepad_t>("SDL_CloseGamepad");
        /// <summary>
        /// Close a controller previously opened with SDL_OpenGamepad().
        /// </summary>
        public static void SDL_CloseGamepad(SDL_Gamepad gamepad) => s_sdl_closeGamepad(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_IsGamepad_t(uint instance_id);
        private static SDL_IsGamepad_t s_sdl_isGamepad = LoadFunction<SDL_IsGamepad_t>("SDL_IsGamepad");
        /// <summary>
        /// Is the joystick on this index supported by the game controller interface?
        /// </summary>
        public static bool SDL_IsGamepad(uint instance_id) => s_sdl_isGamepad(instance_id) != 0;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetGamepadInstanceName_t(uint instance_id);
        private static SDL_GetGamepadInstanceName_t s_sdl_getGamepadInstanceName = LoadFunction<SDL_GetGamepadInstanceName_t>("SDL_GetGamepadInstanceName");
        /// <summary>
        /// Get the implementation dependent name of a game controller.
        /// This can be called before any controllers are opened.
        /// If no name can be found, this function returns null.
        /// </summary>
        public static byte* SDL_GetGamepadInstanceName(uint instance_id) => s_sdl_getGamepadInstanceName(instance_id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Gamepad SDL_GetGamepadFromInstanceID_t(uint instance_id);
        private static SDL_GetGamepadFromInstanceID_t s_sdl_getGamepadFromInstanceID = LoadFunction<SDL_GetGamepadFromInstanceID_t>("SDL_GetGamepadFromInstanceID");
        /// <summary>
        /// Return the SDL_Gamepad associated with an instance id.
        /// </summary>
        public static SDL_Gamepad SDL_GetGamepadFromInstanceID(uint instance_id) => s_sdl_getGamepadFromInstanceID(instance_id);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetGamepadName_t(SDL_Gamepad gamepad);
        private static SDL_GetGamepadName_t s_sdl_getGamepadName = LoadFunction<SDL_GetGamepadName_t>("SDL_GetGamepadName");
        /// <summary>
        /// Return the name for this currently opened controller.
        /// </summary>
        public static byte* SDL_GetGamepadName(SDL_Gamepad gamepad) => s_sdl_getGamepadName(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort SDL_GetGamepadVendor_t(SDL_Gamepad gamepad);
        private static SDL_GetGamepadVendor_t s_sdl_getGamepadVendor = LoadFunction<SDL_GetGamepadVendor_t>("SDL_GetGamepadVendor");
        /// <summary>
        /// Get the USB vendor ID of an opened controller, if available.
        /// If the vendor ID isn't available this function returns 0.
        /// </summary>
        public static ushort SDL_GetGamepadVendor(SDL_Gamepad gamepad) => s_sdl_getGamepadVendor(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort SDL_GetGamepadProduct_t(SDL_Gamepad gamepad);
        private static SDL_GetGamepadProduct_t s_sdl_getGamepadProduct = LoadFunction<SDL_GetGamepadProduct_t>("SDL_GetGamepadProduct");
        /// <summary>
        /// Get the USB product ID of an opened controller, if available.
        /// If the product ID isn't available this function returns 0.
        /// </summary>
        public static ushort SDL_GetGamepadProduct(SDL_Gamepad gamepad) => s_sdl_getGamepadProduct(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort SDL_GetGamepadProductVersion_t(SDL_Gamepad gamepad);
        private static SDL_GetGamepadProductVersion_t s_sdl_getGamepadProductVersion = LoadFunction<SDL_GetGamepadProductVersion_t>("SDL_GetGamepadProductVersion");
        /// <summary>
        /// Get the product version of an opened controller, if available.
        /// If the product version isn't available this function returns 0.
        /// </summary>
        public static ushort SDL_GetGamepadProductVersion(SDL_Gamepad gamepad) => s_sdl_getGamepadProductVersion(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GamepadConnected_t(SDL_Gamepad gamepad);
        private static SDL_GamepadConnected_t s_sdl_gamepadConnected = LoadFunction<SDL_GamepadConnected_t>("SDL_GamepadConnected");
        /// <summary>
        /// Returns 1 if the controller has been opened and currently connected, or 0 if it has not.
        /// </summary>
        public static int SDL_GamepadConnected(SDL_Gamepad gamepad) => s_sdl_gamepadConnected(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Joystick SDL_GetGamepadJoystick_t(SDL_Gamepad gamepad);
        private static SDL_GetGamepadJoystick_t s_sdl_getGamepadJoystick = LoadFunction<SDL_GetGamepadJoystick_t>("SDL_GetGamepadJoystick");
        /// <summary>
        /// Get the underlying joystick object used by a controller.
        /// </summary>
        public static SDL_Joystick SDL_GetGamepadJoystick(SDL_Gamepad gamepad) => s_sdl_getGamepadJoystick(gamepad);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate bool SDL_GamepadEventsEnabled_t();
        private static SDL_GamepadEventsEnabled_t s_sdl_gamepadEventsEnabled = LoadFunction<SDL_GamepadEventsEnabled_t>("SDL_GamepadEventsEnabled");
        /// <summary>
        /// Query the state of gamepad event processing.
        /// Returns true if gamepad events are being processed, false otherwise.
        /// If gamepad events are disabled, you must call SDL_UpdateGamepads()
        /// yourself and check the state of the gamepad when you want gamepad
        /// information.
        /// </summary>
        public static bool SDL_GamepadEventsEnabled() => s_sdl_gamepadEventsEnabled();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetGamepadEventsEnabled_t(bool enabled);
        private static SDL_SetGamepadEventsEnabled_t s_sdl_setGamepadEventsEnabled = LoadFunction<SDL_SetGamepadEventsEnabled_t>("SDL_SetGamepadEventsEnabled");
        /// <summary>
        /// Set the state of gamepad event processing.
        /// enabled	whether to process gamepad events or not
        /// If gamepad events are disabled, you must call SDL_UpdateGamepads()
        /// yourself and check the state of the gamepad when you want gamepad
        /// information.
        /// </summary>
        public static void SDL_SetGamepadEventsEnabled(bool enabled) => s_sdl_setGamepadEventsEnabled(enabled);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_UpdateGamepads_t();
        private static SDL_UpdateGamepads_t s_sdl_updateGamepads = LoadFunction<SDL_UpdateGamepads_t>("SDL_UpdateGamepads");
        /// <summary>
        /// Update the current state of the open game controllers.
        /// This is called automatically by the event loop if any game controller
        /// events are enabled.
        /// </summary>
        public static void SDL_UpdateGamepads() => s_sdl_updateGamepads();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate short SDL_GetGamepadAxis_t(SDL_Gamepad gamepad, SDL_GamepadAxis axis);
        private static SDL_GetGamepadAxis_t s_sdl_getGamepadAxis = LoadFunction<SDL_GetGamepadAxis_t>("SDL_GetGamepadAxis");
        /// <summary>
        /// Get the current state of an axis control on a game controller.
        /// The state is a value ranging from -32768 to 32767 (except for the triggers,
        /// which range from 0 to 32767).
        /// The axis indices start at index 0.
        /// </summary>
        public static short SDL_GetGamepadAxis(SDL_Gamepad gamepad, SDL_GamepadAxis axis) => s_sdl_getGamepadAxis(gamepad, axis);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte SDL_GetGamepadButton_t(SDL_Gamepad gamepad, SDL_GamepadButton button);
        private static SDL_GetGamepadButton_t s_sdl_getGamepadButton = LoadFunction<SDL_GetGamepadButton_t>("SDL_GetGamepadButton");
        /// <summary>
        /// Get the current state of a button on a game controller.
        /// The button indices start at index 0.
        /// </summary>
        public static byte SDL_GetGamepadButton(SDL_Gamepad gamepad, SDL_GamepadButton button) => s_sdl_getGamepadButton(gamepad, button);
    }
}
