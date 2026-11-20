namespace SunamoCl.SunamoCmd;

// TextWriter that writes to both the original console output and a log file simultaneously.
// This allows AI tools to read the log file and understand what happened during the last application run.
// The file is overwritten on each new run.
/// <summary>
/// Represents the tee text writer type.
/// </summary>
public class TeeTextWriter : TextWriter, IDisposable
{
    private readonly StreamWriter fileWriter;
    private bool disposed;

    /// <summary>
    /// Gets the original writer.
    /// </summary>
    public TextWriter OriginalWriter { get; }

    /// <summary>
    /// Tee text writer.
    /// </summary>
    /// <param name="originalWriter">The original writer.</param>
    /// <param name="logFilePath">The log file path.</param>
    /// <returns>The tee text writer result.</returns>
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

    /// <summary>
    /// Gets the encoding.
    /// </summary>
    public override Encoding Encoding => OriginalWriter.Encoding;

    /// <summary>
    /// Write.
    /// </summary>
    /// <param name="value">The value.</param>
    /// <returns>The write result.</returns>
    public override void Write(char value)
    {
        OriginalWriter.Write(value);
        fileWriter.Write(value);
    }

    /// <summary>
    /// Write.
    /// </summary>
    /// <param name="value">The value.</param>
    public override void Write(string? value)
    {
        OriginalWriter.Write(value);
        fileWriter.Write(value);
    }

    /// <summary>
    /// Write line.
    /// </summary>
    /// <param name="value">The value.</param>
    public override void WriteLine(string? value)
    {
        OriginalWriter.WriteLine(value);
        fileWriter.WriteLine(value);
    }

    /// <summary>
    /// Write line.
    /// </summary>
    /// <param name="value">The value.</param>
    public override void WriteLine()
    {
        OriginalWriter.WriteLine();
        fileWriter.WriteLine();
    }

    /// <summary>
    /// Flush.
    /// </summary>
    public override void Flush()
    {
        OriginalWriter.Flush();
        fileWriter.Flush();
    }

    /// <summary>
    /// Write async.
    /// </summary>
    public override async Task WriteAsync(char value)
    {
        await OriginalWriter.WriteAsync(value);
        await fileWriter.WriteAsync(value);
    }

    /// <summary>
    /// Write async.
    /// </summary>
    /// <param name="value">The value.</param>
    public override async Task WriteAsync(string? value)
    {
        await OriginalWriter.WriteAsync(value);
        await fileWriter.WriteAsync(value);
    }

    /// <summary>
    /// Write line async.
    /// </summary>
    /// <param name="value">The value.</param>
    public override async Task WriteLineAsync(string? value)
    {
        await OriginalWriter.WriteLineAsync(value);
        await fileWriter.WriteLineAsync(value);
    }

    /// <summary>
    /// Write line async.
    /// </summary>
    /// <param name="value">The value.</param>
    public override async Task WriteLineAsync()
    {
        await OriginalWriter.WriteLineAsync();
        await fileWriter.WriteLineAsync();
    }

    /// <summary>
    /// Flush async.
    /// </summary>
    public override async Task FlushAsync()
    {
        await OriginalWriter.FlushAsync();
        await fileWriter.FlushAsync();
    }

    /// <summary>
    /// Finalize log.
    /// </summary>
    public void FinalizeLog()
    {
        fileWriter.WriteLine();
        fileWriter.WriteLine($"=== Application run finished at {DateTime.Now:yyyy-MM-dd HH:mm:ss} ===");
        fileWriter.Flush();
    }

    /// <summary>
    /// Dispose.
    /// </summary>
    /// <returns>The dispose result.</returns>
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
