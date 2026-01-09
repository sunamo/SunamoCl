// variables names: ok
namespace SunamoCl;

using System.Runtime.InteropServices;

public class ClFlasher
{
    // Import GetConsoleWindow pro získání handlu konzolového okna
    [DllImport("kernel32.dll")]
    static extern IntPtr GetConsoleWindow();

    // Import FlashWindowEx pro blikání okna
    [DllImport("user32.dll")]
    [return: MarshalAs(UnmanagedType.Bool)]
    static extern bool FlashWindowEx(ref FLASHWINFO pwfi);

    // Definiční konstanty pro FLASHWINFO flags
    public const UInt32 FLASHW_STOP = 0;
    public const UInt32 FLASHW_CAPTION = 1;
    public const UInt32 FLASHW_TRAY = 2; // Toto je pro blikání v taskbaru
    public const UInt32 FLASHW_ALL = 3;  // Kapitola i taskbar
    public const UInt32 FLASHW_TIMER = 4;
    public const UInt32 FLASHW_TIMERNOFG = 12; // Bliká, dokud okno nepřejde do popředí

    // Struktura pro FlashWindowEx
    [StructLayout(LayoutKind.Sequential)]
    public struct FLASHWINFO
    {
        public UInt32 cbSize;
        public IntPtr hwnd;
        public UInt32 dwFlags;
        public UInt32 uCount;
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
            Console.WriteLine("⚠️ Cannot obtain console window handle. You may not be in a console environment.");
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