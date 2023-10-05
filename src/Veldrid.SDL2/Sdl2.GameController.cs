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
    public struct SDL_ControllerAxisEvent
    {
        /// <summary>
        /// SDL_CONTROLLERAXISMOTION.
        /// </summary>
        public uint type;
        /// <summary>
        /// In milliseconds, populated using SDL_GetTicks().
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
    public struct SDL_ControllerButtonEvent
    {
        /// <summary>
        /// SDL_CONTROLLERBUTTONDOWN or SDL_CONTROLLERBUTTONUP.
        /// </summary>
        public uint type;
        /// <summary>
        /// In milliseconds, populated using SDL_GetTicks().
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
    public struct SDL_ControllerDeviceEvent
    {
        /// <summary>
        /// SDL_CONTROLLERDEVICEADDED, SDL_CONTROLLERDEVICEREMOVED, or SDL_CONTROLLERDEVICEREMAPPED.
        /// </summary>
        public uint type;
        /// <summary>
        /// In milliseconds, populated using SDL_GetTicks().
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
        private delegate SDL_Gamepad SDL_OpenGamepad_t(int joystick_index);
        private static SDL_OpenGamepad_t s_SDL_OpenGamepad = LoadFunction<SDL_OpenGamepad_t>("SDL_OpenGamepad");
        /// <summary>
        /// Open a game controller for use.
        /// The index passed as an argument refers to the N'th game controller on the system. This index is not the value which
        /// will identify this controller in future controller events. The joystick's instance id will be used there instead.
        /// </summary>
        /// <returns>A controller identifier, or NULL if an error occurred.</returns>
        public static SDL_Gamepad SDL_OpenGamepad(int joystick_index) => s_SDL_OpenGamepad(joystick_index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_CloseGamepad_t(SDL_Gamepad gamecontroller);
        private static SDL_CloseGamepad_t s_SDL_CloseGamepad = LoadFunction<SDL_CloseGamepad_t>("SDL_CloseGamepad");
        /// <summary>
        /// Close a controller previously opened with SDL_OpenGamepad().
        /// </summary>
        public static void SDL_CloseGamepad(SDL_Gamepad gamecontroller) => s_SDL_CloseGamepad(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_IsGamepad_t(int joystick_index);
        private static SDL_IsGamepad_t s_SDL_IsGamepad = LoadFunction<SDL_IsGamepad_t>("SDL_IsGamepad");
        /// <summary>
        /// Is the joystick on this index supported by the game controller interface?
        /// </summary>
        public static bool SDL_IsGamepad(int joystick_index) => s_SDL_IsGamepad(joystick_index) != 0;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GetGamepadInstanceName_t(int instance_id);
        private static SDL_GetGamepadInstanceName_t s_SDL_GetGamepadInstanceName = LoadFunction<SDL_GetGamepadInstanceName_t>("SDL_GetGamepadInstanceName");
        /// <summary>
        /// Get the implementation dependent name of a game controller.
        /// This can be called before any controllers are opened.
        /// If no name can be found, this function returns null.
        /// </summary>
        public static byte* SDL_GetGamepadInstanceName(int joystick_index) => s_SDL_GetGamepadInstanceName(joystick_index);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Gamepad SDL_GamepadFromInstanceID_t(int joyid);
        private static SDL_GamepadFromInstanceID_t s_SDL_GamepadFromInstanceID = LoadFunction<SDL_GamepadFromInstanceID_t>("SDL_GamepadFromInstanceID");
        /// <summary>
        /// Return the SDL_Gamepad associated with an instance id.
        /// </summary>
        public static SDL_Gamepad SDL_GamepadFromInstanceID(int joyid) => s_SDL_GamepadFromInstanceID(joyid);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte* SDL_GamepadName_t(SDL_Gamepad gamecontroller);
        private static SDL_GamepadName_t s_SDL_GamepadName = LoadFunction<SDL_GamepadName_t>("SDL_GamepadName");
        /// <summary>
        /// Return the name for this currently opened controller.
        /// </summary>
        public static byte* SDL_GamepadName(SDL_Gamepad gamecontroller) => s_SDL_GamepadName(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort SDL_GamepadGetVendor_t(SDL_Gamepad gamecontroller);
        private static SDL_GamepadGetVendor_t s_SDL_GamepadGetVendor = LoadFunction<SDL_GamepadGetVendor_t>("SDL_GamepadGetVendor");
        /// <summary>
        /// Get the USB vendor ID of an opened controller, if available.
        /// If the vendor ID isn't available this function returns 0.
        /// </summary>
        public static ushort SDL_GamepadGetVendor(SDL_Gamepad gamecontroller) => s_SDL_GamepadGetVendor(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort SDL_GamepadGetProduct_t(SDL_Gamepad gamecontroller);
        private static SDL_GamepadGetProduct_t s_SDL_GamepadGetProduct = LoadFunction<SDL_GamepadGetProduct_t>("SDL_GamepadGetProduct");
        /// <summary>
        /// Get the USB product ID of an opened controller, if available.
        /// If the product ID isn't available this function returns 0.
        /// </summary>
        public static ushort SDL_GamepadGetProduct(SDL_Gamepad gamecontroller) => s_SDL_GamepadGetProduct(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate ushort SDL_GamepadGetProductVersion_t(SDL_Gamepad gamecontroller);
        private static SDL_GamepadGetProductVersion_t s_SDL_GamepadGetProductVersion = LoadFunction<SDL_GamepadGetProductVersion_t>("SDL_GamepadGetProductVersion");
        /// <summary>
        /// Get the product version of an opened controller, if available.
        /// If the product version isn't available this function returns 0.
        /// </summary>
        public static ushort SDL_GamepadGetProductVersion(SDL_Gamepad gamecontroller) => s_SDL_GamepadGetProductVersion(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GamepadGetAttached_t(SDL_Gamepad gamecontroller);
        private static SDL_GamepadGetAttached_t s_SDL_GamepadGetAttached = LoadFunction<SDL_GamepadGetAttached_t>("SDL_GamepadGetAttached");
        /// <summary>
        /// Returns 1 if the controller has been opened and currently connected, or 0 if it has not.
        /// </summary>
        public static int SDL_GamepadGetAttached(SDL_Gamepad gamecontroller) => s_SDL_GamepadGetAttached(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Joystick SDL_GamepadGetJoystick_t(SDL_Gamepad gamecontroller);
        private static SDL_GamepadGetJoystick_t s_SDL_GamepadGetJoystick = LoadFunction<SDL_GamepadGetJoystick_t>("SDL_GamepadGetJoystick");
        /// <summary>
        /// Get the underlying joystick object used by a controller.
        /// </summary>
        public static SDL_Joystick SDL_GamepadGetJoystick(SDL_Gamepad gamecontroller) => s_SDL_GamepadGetJoystick(gamecontroller);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GamepadEventState_t(int state);
        private static SDL_GamepadEventState_t s_SDL_GamepadEventState = LoadFunction<SDL_GamepadEventState_t>("SDL_GamepadEventState");
        /// <summary>
        /// Enable/disable controller event polling.
        /// If controller events are disabled, you must call SDL_GamepadUpdate()
        /// yourself and check the state of the controller when you want controller
        /// information.
        /// The state can be one of SDL_QUERY, SDL_ENABLE or SDL_IGNORE.
        /// </summary>
        public static int SDL_GamepadEventState(int state) => s_SDL_GamepadEventState(state);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GamepadUpdate_t();
        private static SDL_GamepadUpdate_t s_SDL_GamepadUpdate = LoadFunction<SDL_GamepadUpdate_t>("SDL_GamepadUpdate");
        /// <summary>
        /// Update the current state of the open game controllers.
        /// This is called automatically by the event loop if any game controller
        /// events are enabled.
        /// </summary>
        public static void SDL_GamepadUpdate() => s_SDL_GamepadUpdate();

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate short SDL_GamepadGetAxis_t(SDL_Gamepad gamecontroller, SDL_GamepadAxis axis);
        private static SDL_GamepadGetAxis_t s_SDL_GamepadGetAxis = LoadFunction<SDL_GamepadGetAxis_t>("SDL_GamepadGetAxis");
        /// <summary>
        /// Get the current state of an axis control on a game controller.
        /// The state is a value ranging from -32768 to 32767 (except for the triggers,
        /// which range from 0 to 32767).
        /// The axis indices start at index 0.
        /// </summary>
        public static short SDL_GamepadGetAxis(SDL_Gamepad gamecontroller, SDL_GamepadAxis axis) => s_SDL_GamepadGetAxis(gamecontroller, axis);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate byte SDL_GamepadGetButton_t(SDL_Gamepad gamecontroller, SDL_GamepadButton button);
        private static SDL_GamepadGetButton_t s_SDL_GamepadGetButton = LoadFunction<SDL_GamepadGetButton_t>("SDL_GamepadGetButton");
        /// <summary>
        /// Get the current state of a button on a game controller.
        /// The button indices start at index 0.
        /// </summary>
        public static byte SDL_GamepadGetButton(SDL_Gamepad gamecontroller, SDL_GamepadButton button) => s_SDL_GamepadGetButton(gamecontroller, button);
    }
}
