using System;
using System.Runtime.InteropServices;
using System.Text;

namespace Veldrid.Sdl2
{
    public static unsafe partial class Sdl2Native
    {
        /// <summary>
        /// A special sentinel value indicating that a newly-created window should be centered in the screen.
        /// </summary>
        public const int SDL_WINDOWPOS_CENTERED = 0x2FFF0000;

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_CreateWindow_t(byte* title, int w, int h, SDL_WindowFlags flags);
        private static SDL_CreateWindow_t s_sdl_createWindow = LoadFunction<SDL_CreateWindow_t>("SDL_CreateWindow");
        public static SDL_Window SDL_CreateWindow(string title, int w, int h, SDL_WindowFlags flags)
        {
            byte* utf8Bytes;
            if (title != null)
            {
                int byteCount = Encoding.UTF8.GetByteCount(title);
                if (byteCount == 0)
                {
                    byte zeroByte = 0;
                    utf8Bytes = &zeroByte;
                }
                else
                {
                    byte* utf8BytesAlloc = stackalloc byte[byteCount + 1];
                    utf8Bytes = utf8BytesAlloc;
                    fixed (char* titlePtr = title)
                    {
                        int actualBytes = Encoding.UTF8.GetBytes(titlePtr, title.Length, utf8Bytes, byteCount);
                        utf8Bytes[actualBytes] = 0;
                    }
                }
            }
            else
            {
                utf8Bytes = null;
            }

            return s_sdl_createWindow(utf8Bytes, w, h, flags);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_CreateWindowWithPosition_t(byte* title, int x, int y, int w, int h, SDL_WindowFlags flags);
        private static SDL_CreateWindowWithPosition_t s_sdl_createWindowWithPosition = LoadFunction<SDL_CreateWindowWithPosition_t>("SDL_CreateWindowWithPosition");
        public static SDL_Window SDL_CreateWindowWithPosition(string title, int x, int y, int w, int h, SDL_WindowFlags flags)
        {
            // TODO: Code duplication bad!
            byte* utf8Bytes;
            if (title != null)
            {
                int byteCount = Encoding.UTF8.GetByteCount(title);
                if (byteCount == 0)
                {
                    byte zeroByte = 0;
                    utf8Bytes = &zeroByte;
                }
                else
                {
                    byte* utf8BytesAlloc = stackalloc byte[byteCount + 1];
                    utf8Bytes = utf8BytesAlloc;
                    fixed (char* titlePtr = title)
                    {
                        int actualBytes = Encoding.UTF8.GetBytes(titlePtr, title.Length, utf8Bytes, byteCount);
                        utf8Bytes[actualBytes] = 0;
                    }
                }
            }
            else
            {
                utf8Bytes = null;
            }

            return s_sdl_createWindowWithPosition(utf8Bytes, x, y, w, h, flags);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_Window SDL_CreateWindowFrom_t(IntPtr data);
        private static SDL_CreateWindowFrom_t s_sdl_createWindowFrom = LoadFunction<SDL_CreateWindowFrom_t>("SDL_CreateWindowFrom");
        public static SDL_Window SDL_CreateWindowFrom(IntPtr data) => s_sdl_createWindowFrom(data);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_DestroyWindow_t(SDL_Window SDL2Window);
        private static SDL_DestroyWindow_t s_sdl_destroyWindow = LoadFunction<SDL_DestroyWindow_t>("SDL_DestroyWindow");
        public static void SDL_DestroyWindow(SDL_Window Sdl2Window) => s_sdl_destroyWindow(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetWindowSize_t(SDL_Window SDL2Window, int* w, int* h);
        private static SDL_GetWindowSize_t s_getWindowSize = LoadFunction<SDL_GetWindowSize_t>("SDL_GetWindowSize");
        public static void SDL_GetWindowSize(SDL_Window Sdl2Window, int* w, int* h) => s_getWindowSize(Sdl2Window, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_GetWindowPosition_t(SDL_Window SDL2Window, int* x, int* y);
        private static SDL_GetWindowPosition_t s_getWindowPosition = LoadFunction<SDL_GetWindowPosition_t>("SDL_GetWindowPosition");
        public static void SDL_GetWindowPosition(SDL_Window Sdl2Window, int* x, int* y) => s_getWindowPosition(Sdl2Window, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowPosition_t(SDL_Window SDL2Window, int x, int y);
        private static SDL_SetWindowPosition_t s_setWindowPosition = LoadFunction<SDL_SetWindowPosition_t>("SDL_SetWindowPosition");
        public static void SDL_SetWindowPosition(SDL_Window Sdl2Window, int x, int y) => s_setWindowPosition(Sdl2Window, x, y);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowSize_t(SDL_Window SDL2Window, int w, int h);
        private static SDL_SetWindowSize_t s_setWindowSize = LoadFunction<SDL_SetWindowSize_t>("SDL_SetWindowSize");
        public static int SDL_SetWindowSize(SDL_Window Sdl2Window, int w, int h) => s_setWindowSize(Sdl2Window, w, h);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate string SDL_GetWindowTitle_t(SDL_Window SDL2Window);
        private static SDL_GetWindowTitle_t s_getWindowTitle = LoadFunction<SDL_GetWindowTitle_t>("SDL_GetWindowTitle");
        public static string SDL_GetWindowTitle(SDL_Window Sdl2Window) => s_getWindowTitle(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowTitle_t(SDL_Window SDL2Window, byte* title);
        private static SDL_SetWindowTitle_t s_setWindowTitle = LoadFunction<SDL_SetWindowTitle_t>("SDL_SetWindowTitle");
        public static void SDL_SetWindowTitle(SDL_Window Sdl2Window, string title)
        {
            byte* utf8Bytes;
            if (title != null)
            {
                int byteCount = Encoding.UTF8.GetByteCount(title);
                if (byteCount == 0)
                {
                    byte zeroByte = 0;
                    utf8Bytes = &zeroByte;
                }
                else
                {
                    byte* utf8BytesAlloc = stackalloc byte[byteCount + 1];
                    utf8Bytes = utf8BytesAlloc;
                    fixed (char* titlePtr = title)
                    {
                        int actualBytes = Encoding.UTF8.GetBytes(titlePtr, title.Length, utf8Bytes, byteCount);
                        utf8Bytes[actualBytes] = 0;
                    }
                }
            }
            else
            {
                utf8Bytes = null;
            }

            s_setWindowTitle(Sdl2Window, utf8Bytes);
        }

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_WindowFlags SDL_GetWindowFlags_t(SDL_Window SDL2Window);
        private static SDL_GetWindowFlags_t s_getWindowFlags = LoadFunction<SDL_GetWindowFlags_t>("SDL_GetWindowFlags");
        public static SDL_WindowFlags SDL_GetWindowFlags(SDL_Window Sdl2Window) => s_getWindowFlags(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowBordered_t(SDL_Window SDL2Window, uint bordered);
        private static SDL_SetWindowBordered_t s_setWindowBordered = LoadFunction<SDL_SetWindowBordered_t>("SDL_SetWindowBordered");
        public static void SDL_SetWindowBordered(SDL_Window Sdl2Window, uint bordered) => s_setWindowBordered(Sdl2Window, bordered);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_MaximizeWindow_t(SDL_Window SDL2Window);
        private static SDL_MaximizeWindow_t s_maximizeWindow = LoadFunction<SDL_MaximizeWindow_t>("SDL_MaximizeWindow");
        public static void SDL_MaximizeWindow(SDL_Window Sdl2Window) => s_maximizeWindow(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_MinimizeWindow_t(SDL_Window SDL2Window);
        private static SDL_MinimizeWindow_t s_minimizeWindow = LoadFunction<SDL_MinimizeWindow_t>("SDL_MinimizeWindow");
        public static void SDL_MinimizeWindow(SDL_Window Sdl2Window) => s_minimizeWindow(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_RaiseWindow_t(SDL_Window SDL2Window);
        private static SDL_RaiseWindow_t s_raiseWindow = LoadFunction<SDL_RaiseWindow_t>("SDL_RaiseWindow");
        public static void SDL_RaiseWindow(SDL_Window Sdl2Window) => s_raiseWindow(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowFullscreen_t(SDL_Window Sdl2Window, bool fullscreen);
        private static SDL_SetWindowFullscreen_t s_setWindowFullscreen = LoadFunction<SDL_SetWindowFullscreen_t>("SDL_SetWindowFullscreen");
        public static int SDL_SetWindowFullscreen(SDL_Window Sdl2Window, bool fullscreen) => s_setWindowFullscreen(Sdl2Window, fullscreen);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowFullscreenMode_t(SDL_Window Sdl2Window, SDL_DisplayMode mode);
        private static SDL_SetWindowFullscreenMode_t s_setWindowFullscreenMode = LoadFunction<SDL_SetWindowFullscreenMode_t>("SDL_SetWindowFullscreenMode");
        public static int SDL_SetWindowFullscreenMode(SDL_Window Sdl2Window, SDL_DisplayMode mode) => s_setWindowFullscreenMode(Sdl2Window, mode);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_DisplayMode SDL_GetWindowFullscreenMode_t(SDL_Window Sdl2Window);
        private static SDL_GetWindowFullscreenMode_t s_GetWindowFullscreenMode = LoadFunction<SDL_GetWindowFullscreenMode_t>("SDL_GetWindowFullscreenMode");
        public static SDL_DisplayMode SDL_GetWindowFullscreenMode(SDL_Window Sdl2Window) => s_GetWindowFullscreenMode(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_ShowWindow_t(SDL_Window SDL2Window);
        private static SDL_ShowWindow_t s_showWindow = LoadFunction<SDL_ShowWindow_t>("SDL_ShowWindow");
        public static void SDL_ShowWindow(SDL_Window Sdl2Window) => s_showWindow(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_HideWindow_t(SDL_Window SDL2Window);
        private static SDL_HideWindow_t s_hideWindow = LoadFunction<SDL_HideWindow_t>("SDL_HideWindow");
        public static void SDL_HideWindow(SDL_Window Sdl2Window) => s_hideWindow(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_GetWindowID_t(SDL_Window SDL2Window);
        private static SDL_GetWindowID_t s_getWindowID = LoadFunction<SDL_GetWindowID_t>("SDL_GetWindowID");
        public static uint SDL_GetWindowID(SDL_Window Sdl2Window) => s_getWindowID(Sdl2Window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_SetWindowOpacity_t(SDL_Window window, float opacity);
        private static SDL_SetWindowOpacity_t s_setWindowOpacity = LoadFunction<SDL_SetWindowOpacity_t>("SDL_SetWindowOpacity");
        public static int SDL_SetWindowOpacity(SDL_Window Sdl2Window, float opacity) => s_setWindowOpacity(Sdl2Window, opacity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetWindowOpacity_t(SDL_Window window, float* opacity);
        private static SDL_GetWindowOpacity_t s_getWindowOpacity = LoadFunction<SDL_GetWindowOpacity_t>("SDL_GetWindowOpacity");
        public static int SDL_GetWindowOpacity(SDL_Window Sdl2Window, float* opacity) => s_getWindowOpacity(Sdl2Window, opacity);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate void SDL_SetWindowResizable_t(SDL_Window window, uint resizable);
        private static SDL_SetWindowResizable_t s_setWindowResizable = LoadFunction<SDL_SetWindowResizable_t>("SDL_SetWindowResizable");
        public static void SDL_SetWindowResizable(SDL_Window window, uint resizable) => s_setWindowResizable(window, resizable);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate int SDL_GetDisplayBounds_t(uint displayID, Rectangle* rect);
        private static SDL_GetDisplayBounds_t s_sdl_getDisplayBounds = LoadFunction<SDL_GetDisplayBounds_t>("SDL_GetDisplayBounds");
        public static int SDL_GetDisplayBounds(uint displayID, Rectangle* rect) => s_sdl_getDisplayBounds(displayID, rect);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint SDL_GetDisplayForWindow_t(SDL_Window window);
        private static SDL_GetDisplayForWindow_t s_sdl_getDisplayForWindow = LoadFunction<SDL_GetDisplayForWindow_t>("SDL_GetDisplayForWindow");
        public static uint SDL_GetDisplayForWindow(SDL_Window window) => s_sdl_getDisplayForWindow(window);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_DisplayMode* SDL_GetCurrentDisplayMode_t(uint displayID);
        private static SDL_GetCurrentDisplayMode_t s_sdl_getCurrentDisplayMode = LoadFunction<SDL_GetCurrentDisplayMode_t>("SDL_GetCurrentDisplayMode");
        public static SDL_DisplayMode* SDL_GetCurrentDisplayMode(uint displayID) => s_sdl_getCurrentDisplayMode(displayID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_DisplayMode* SDL_GetDesktopDisplayMode_t(uint displayID);
        private static SDL_GetDesktopDisplayMode_t s_sdl_getDesktopDisplayMode = LoadFunction<SDL_GetDesktopDisplayMode_t>("SDL_GetDesktopDisplayMode");
        public static SDL_DisplayMode* SDL_GetDesktopDisplayMode(uint displayID) => s_sdl_getDesktopDisplayMode(displayID);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate SDL_DisplayMode* SDL_GetFullscreenDisplayModes_t(uint displayID, int* count);
        private static SDL_GetFullscreenDisplayModes_t s_sdl_getFullscreenDisplayModes = LoadFunction<SDL_GetFullscreenDisplayModes_t>("SDL_GetFullscreenDisplayModes");
        public static SDL_DisplayMode* SDL_GetFullscreenDisplayModes(uint displayID, int* count) => s_sdl_getFullscreenDisplayModes(displayID, count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        private delegate uint* SDL_GetDisplays_t(int* count);
        private static SDL_GetDisplays_t s_sdl_getDisplays = LoadFunction<SDL_GetDisplays_t>("SDL_GetDisplays");
        public static uint* SDL_GetDisplays(int* count) => s_sdl_getDisplays(count);

        [UnmanagedFunctionPointer(CallingConvention.Cdecl, CharSet = CharSet.Ansi)]
        private delegate bool SDL_SetHint_t(string name, string value);
        private static SDL_SetHint_t s_sdl_setHint = LoadFunction<SDL_SetHint_t>("SDL_SetHint");
        public static bool SDL_SetHint(string name, string value) => s_sdl_setHint(name, value);

    }

    [Flags]
    public enum SDL_WindowFlags : uint
    {
        /// <summary>
        /// fullscreen Sdl2Window.
        /// </summary>
        Fullscreen = 0x00000001,
        /// <summary>
        /// Sdl2Window usable with OpenGL context.
        /// </summary>
        OpenGL = 0x00000002,
        /// <summary>
        /// Sdl2Window is occluded.
        /// </summary>
        Occluded = 0x00000004,
        /// <summary>
        /// Sdl2Window is not visible.
        /// </summary>
        Hidden = 0x00000008,
        /// <summary>
        /// no Sdl2Window decoration.
        /// </summary>
        Borderless = 0x00000010,
        /// <summary>
        /// Sdl2Window can be resized.
        /// </summary>
        Resizable = 0x00000020,
        /// <summary>
        /// Sdl2Window is minimized.
        /// </summary>
        Minimized = 0x00000040,
        /// <summary>
        /// Sdl2Window is maximized.
        /// </summary>
        Maximized = 0x00000080,
        /// <summary>
        /// Sdl2Window has grabbed input focus.
        /// </summary>
        MouseGrabbed = 0x00000100,
        /// <summary>
        /// Sdl2Window has input focus.
        /// </summary>
        InputFocus = 0x00000200,
        /// <summary>
        /// Sdl2Window has mouse focus.
        /// </summary>
        MouseFocus = 0x00000400,
        /// <summary>
        /// Sdl2Window not created by SDL.
        /// </summary>
        Foreign = 0x00000800,
        /// <summary>
        /// Sdl2Window uses high pixel density back buffer if possible.
        /// </summary>
        HighPixelDensity = 0x00002000,
        /// <summary>
        /// Sdl2Window has mouse captured (unrelated to MouseGrabbed).
        /// </summary>
        MouseCapture = 0x00004000,
        /// <summary>
        /// Sdl2Window should always be above others.
        /// </summary>
        AlwaysOnTop = 0x00008000,
        /// <summary>
        /// Sdl2Window should be treated as a utility Sdl2Window.
        /// </summary>
        Utility = 0x00020000,
        /// <summary>
        /// Sdl2Window should be treated as a tooltip.
        /// </summary>
        Tooltip = 0x00040000,
        /// <summary>
        /// Sdl2Window should be treated as a popup menu.
        /// </summary>
        PopupMenu = 0x00080000,
        /// <summary>
        /// Sdl2Window has grabbed keyboard input.
        /// </summary>
        KeyboardGrabbed = 0x00100000,
        /// <summary>
        /// Sdl2Window usable for Vulkan surface.
        /// </summary>
        Vulkan = 0x10000000,
        /// <summary>
        /// Sdl2Window usable for Metal view.
        /// </summary>
        Metal = 0x20000000,
        /// <summary>
        /// Sdl2Window with transparent buffer.
        /// </summary>
        Transparent = 0x40000000,
        /// <summary>
        /// Sdl2Window should not be focusable.
        /// </summary>
        NotFocusable = 0x80000000,
    }

    public unsafe struct SDL_DisplayMode
    {
        public uint displayID;
        public uint format;
        public int w;
        public int h;
        public float pixel_density;
        public float refresh_rate;
        public void* driverdata;
    }
}
