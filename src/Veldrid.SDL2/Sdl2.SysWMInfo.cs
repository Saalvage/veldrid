using System;
using System.Runtime.InteropServices;

namespace Veldrid.Sdl2
{
    public static unsafe partial class Sdl2Native
    {
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetWindowWMInfo_t(SDL_Window Sdl2Window, SDL_SysWMinfo* info, uint version);
        private static readonly SDL_GetWindowWMInfo_t s_getWindowWMInfo = LoadFunction<SDL_GetWindowWMInfo_t>("SDL_GetWindowWMInfo");
        public static int SDL_GetWMWindowInfo(SDL_Window Sdl2Window, SDL_SysWMinfo* info) => s_getWindowWMInfo(Sdl2Window, info, 1);
    }

    [StructLayout(LayoutKind.Explicit)]
    public unsafe struct SDL_SysWMinfo
    {
        [FieldOffset(0)]
        public SDL_version version;
        [FieldOffset(4)]
        public SysWMType subsystem;

        private const int UnionOffset = 8 + 2 * 4;

        [FieldOffset(UnionOffset)]
        public WindowInfo info;

        [FieldOffset(UnionOffset)]
        private fixed byte padding[14 * 8];
    }

    public unsafe struct WindowInfo
    {
        public const int WindowInfoSizeInBytes = 100;
        private fixed byte bytes[WindowInfoSizeInBytes];
    }

    public struct Win32WindowInfo
    {
        /// <summary>
        /// The Sdl2Window handle.
        /// </summary>
        public IntPtr Sdl2Window;
        /// <summary>
        /// The Sdl2Window device context.
        /// </summary>
        public IntPtr hdc;
        /// <summary>
        /// The instance handle.
        /// </summary>
        public IntPtr hinstance;
    }

    public struct X11WindowInfo
    {
        public IntPtr display;
        public IntPtr Sdl2Window;
    }

    public struct WaylandWindowInfo
    {
        public IntPtr display;
        public IntPtr surface;
        public IntPtr shellSurface;
    }

    public struct CocoaWindowInfo
    {
        /// <summary>
        /// The NSWindow* Cocoa window.
        /// </summary>
        public IntPtr Window;
    }

    public struct AndroidWindowInfo
    {
        public IntPtr window;
        public IntPtr surface;
    }

    public enum SysWMType
    {
        Unknown,
        Android,
        Cocoa,
        Haiku,
        KMSDRM,
        RISCOS,
        UIKIT,
        VIVANTE,
        Wayland,
        Windows,
        WinRT,
        X11,
    }
}
