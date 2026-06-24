namespace SunamoCl;

using System.Runtime.InteropServices;

public class ClFlasher
{
    // Import GetConsoleWindow to obtain console window handle
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    // Import FlashWindowEx for window flashing
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    public const UInt32 FLASHW_STOP = 0;
    public const UInt32 FLASHW_CAPTION = 1;
    public const UInt32 FLASHW_TRAY = 2;
    public const UInt32 FLASHW_ALL = 3;
    public const UInt32 FLASHW_TIMER = 4;
    public const UInt32 FLASHW_TIMERNOFG = 12;

    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public UInt32 dwFlags;
        public UInt32 uCount;
        public UInt32 dwTimeout;
    }

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
