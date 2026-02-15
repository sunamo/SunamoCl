namespace SunamoCl.SunamoCmd;

/// <summary>
/// TextWriter that writes to both the original console output and a log file simultaneously.
/// This allows AI tools to read the log file and understand what happened during the last application run.
/// The file is overwritten on each new run.
/// </summary>
public class TeeTextWriter : TextWriter, IDisposable
{
    private readonly StreamWriter fileWriter;
    private bool disposed;

    /// <summary>
    /// The original TextWriter that was wrapped. Used to restore Console.Out/Error after logging is done.
    /// </summary>
    public TextWriter OriginalWriter { get; }

    public TeeTextWriter(TextWriter originalWriter, string logFilePath)
    {
        OriginalWriter = originalWriter;
        this.fileWriter = new StreamWriter(logFilePath, append: false, encoding: Encoding.UTF8)
        {
            AutoFlush = true
        };

        fileWriter.WriteLine($"=== Application run started at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
        fileWriter.WriteLine();
    }

    public override Encoding Encoding => OriginalWriter.Encoding;

    public override void Write(char value)
    {
        OriginalWriter.Write(value);
        fileWriter.Write(value);
    }

    public override void Write(string? value)
    {
        OriginalWriter.Write(value);
        fileWriter.Write(value);
    }

    public override void WriteLine(string? value)
    {
        OriginalWriter.WriteLine(value);
        fileWriter.WriteLine(value);
    }

    public override void WriteLine()
    {
        OriginalWriter.WriteLine();
        fileWriter.WriteLine();
    }

    public override void Flush()
    {
        OriginalWriter.Flush();
        fileWriter.Flush();
    }

    public override async Task WriteAsync(char value)
    {
        await OriginalWriter.WriteAsync(value);
        await fileWriter.WriteAsync(value);
    }

    public override async Task WriteAsync(string? value)
    {
        await OriginalWriter.WriteAsync(value);
        await fileWriter.WriteAsync(value);
    }

    public override async Task WriteLineAsync(string? value)
    {
        await OriginalWriter.WriteLineAsync(value);
        await fileWriter.WriteLineAsync(value);
    }

    public override async Task WriteLineAsync()
    {
        await OriginalWriter.WriteLineAsync();
        await fileWriter.WriteLineAsync();
    }

    public override async Task FlushAsync()
    {
        await OriginalWriter.FlushAsync();
        await fileWriter.FlushAsync();
    }

    /// <summary>
    /// Writes a final footer to the log file and restores the original console writer
    /// </summary>
    public void FinalizeLog()
    {
        fileWriter.WriteLine();
        fileWriter.WriteLine($"=== Application run finished at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
        fileWriter.Flush();
    }

    protected override void Dispose(bool disposing)
    {
        if (!disposed)
        {
            if (disposing)
            {
                fileWriter.Dispose();
            }
            disposed = true;
        }
        base.Dispose(disposing);
    }
}