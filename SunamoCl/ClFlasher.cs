namespace SunamoCl;

using System.Runtime.InteropServices;

/// <summary>
/// Represents the cl flasher type.
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
    /// The FLASHW STOP constant.
    /// </summary>
    public const UInt32 FLASHW_STOP = 0;
    /// <summary>
    /// The FLASHW CAPTION constant.
    /// </summary>
    public const UInt32 FLASHW_CAPTION = 1;
    /// <summary>
    /// The FLASHW TRAY constant.
    /// </summary>
    public const UInt32 FLASHW_TRAY = 2;
    /// <summary>
    /// The FLASHW ALL constant.
    /// </summary>
    public const UInt32 FLASHW_ALL = 3;
    /// <summary>
    /// The FLASHW TIMER constant.
    /// </summary>
    public const UInt32 FLASHW_TIMER = 4;
    /// <summary>
    /// The FLASHW TIMERNOFG constant.
    /// </summary>
    public const UInt32 FLASHW_TIMERNOFG = 12;

    /// <summary>
    /// Represents the FLASHWINFO type.
    /// </summary>
    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        /// <summary>
        /// The cb size.
        /// </summary>
        public UInt32 cbSize;
        /// <summary>
        /// The hwnd.
        /// </summary>
        public IntPtr hwnd;
        /// <summary>
        /// The dw flags.
        /// </summary>
        public UInt32 dwFlags;
        /// <summary>
        /// The u count.
        /// </summary>
        public UInt32 uCount;
        /// <summary>
        /// The dw timeout.
        /// </summary>
        public UInt32 dwTimeout;
    }

    /// <summary>
    /// Flash console.
    /// </summary>
    /// <param name="LayoutKind.Sequential">The layout kind sequential.</param>
    /// <returns>The flash console result.</returns>
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
    /// Stop flashing console.
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
