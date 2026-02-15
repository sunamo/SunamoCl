namespace SunamoCl;

using System.Runtime.InteropServices;

/// <summary>
/// Provides methods to flash the console window in the Windows taskbar to attract user attention
/// </summary>
public class ClFlasher
{
    // Import GetConsoleWindow pro získání handlu konzolového okna
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    // Import FlashWindowEx pro blikání okna
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
    public const UInt32 FLASHW_TRAY = 2; // Toto je pro blikání v taskbaru
    /// <summary>
    /// Flag to flash both the caption and taskbar button
    /// </summary>
    public const UInt32 FLASHW_ALL = 3;  // Kapitola i taskbar
    /// <summary>
    /// Flag to flash continuously with a timer
    /// </summary>
    public const UInt32 FLASHW_TIMER = 4;
    /// <summary>
    /// Flag to flash continuously until the window comes to the foreground
    /// </summary>
    public const UInt32 FLASHW_TIMERNOFG = 12; // Bliká, dokud okno nepřejde do popředí

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
    /// Spustí blikání konzolového okna v taskbaru.
    /// </summary>
    public static void FlashConsole()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle == IntPtr.Zero)
        {
            Console.WriteLine("Cannot obtain console window handle. You may not be in a console environment.");
            return;
        }

        FLASHWINFO fInfo = new FLASHWINFO();
        fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
        fInfo.hwnd = consoleHandle;
        fInfo.dwFlags = FLASHW_ALL | FLASHW_TIMERNOFG; // Blikání ikony i titulku, dokud se nezíská fokus
        fInfo.uCount = UInt32.MaxValue; // Bliká donekonečna
        fInfo.dwTimeout = 0; // Použije se výchozí frekvence blikání

        FlashWindowEx(ref fInfo);
    }

    /// <summary>
    /// Zastaví blikání konzolového okna.
    /// </summary>
    public static void StopFlashingConsole()
    {
        IntPtr consoleHandle = GetConsoleWindow();
        if (consoleHandle == IntPtr.Zero)
        {
            return; // Nic k zastavení
        }

        FLASHWINFO fInfo = new FLASHWINFO();
        fInfo.cbSize = Convert.ToUInt32(Marshal.SizeOf(fInfo));
        fInfo.hwnd = consoleHandle;
        fInfo.dwFlags = FLASHW_STOP; // Zastaví blikání
        fInfo.uCount = 0;
        fInfo.dwTimeout = 0;

        FlashWindowEx(ref fInfo);
    }
}