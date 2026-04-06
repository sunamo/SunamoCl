namespace SunamoCl;

using System.Runtime.InteropServices;

/// <summary>
/// Provides methods to flash the console window in the Windows taskbar to attract user attention
/// </summary>
public class ClFlasher
{
    // Import GetConsoleWindow to obtain console window handle
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    // Import FlashWindowEx for window flashing
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    /// <summary>
    /// Flag to stop flashing the window
    /// </summary>
    public const UInt32 FLASHW_STOP = 0;
    /// <summary>
    /// Flag to flash the window caption
    /// </summary>
    public const UInt32 FLASHW_CAPTION = 1;
    /// <summary>
    /// Flag to flash the taskbar button
    /// </summary>
    public const UInt32 FLASHW_TRAY = 2;
    /// <summary>
    /// Flag to flash both the caption and taskbar button
    /// </summary>
    public const UInt32 FLASHW_ALL = 3;
    /// <summary>
    /// Flag to flash continuously with a timer
    /// </summary>
    public const UInt32 FLASHW_TIMER = 4;
    /// <summary>
    /// Flag to flash continuously until the window comes to the foreground
    /// </summary>
    public const UInt32 FLASHW_TIMERNOFG = 12;

    /// <summary>
    /// Structure containing information for the FlashWindowEx function
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        /// <summary>
        /// Size of the structure in bytes
        /// </summary>
        public UInt32 cbSize;
        /// <summary>
        /// Handle to the window to be flashed
        /// </summary>
        public IntPtr hwnd;
        /// <summary>
        /// Flash status flags
        /// </summary>
        public UInt32 dwFlags;
        /// <summary>
        /// Number of times to flash the window
        /// </summary>
        public UInt32 uCount;
        /// <summary>
        /// Rate of flashing in milliseconds, or zero for default cursor blink rate
        /// </summary>
        public UInt32 dwTimeout;
    }

    /// <summary>
    /// Starts flashing the console window in the taskbar.
    /// </summary>
    public static void FlashConsole()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle == IntPtr.Zero)
        {
            Console.WriteLine("Cannot obtain console window handle. You may not be in a console environment.");
            return;
        }

        FLASHWINFO flashInfo = new FLASHWINFO();
        flashInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(flashInfo));
        flashInfo.hwnd = consoleHandle;
        flashInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG;
        flashInfo.uCount = UInt32.MaxValue;
        flashInfo.dwTimeout = 0;

        FlashWindowEx(ref flashInfo);
    }

    /// <summary>
    /// Stops flashing the console window.
    /// </summary>
    public static void StopFlashingConsole()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle == IntPtr.Zero)
        {
            return; // Nothing to stop
        }

        FLASHWINFO flashInfo = new FLASHWINFO();
        flashInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(flashInfo));
        flashInfo.hwnd = consoleHandle;
        flashInfo.dwFlags = FLASHW_STOP;
        flashInfo.uCount = 0;
        flashInfo.dwTimeout = 0;

        FlashWindowEx(ref flashInfo);
    }
}