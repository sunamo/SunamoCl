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

    /// <summary>
    /// Initializes a new TeeTextWriter that writes to both the original writer and a log file.
    /// </summary>
    /// <param name="originalWriter">Original text writer to tee from.</param>
    /// <param name="logFilePath">Path to the log file.</param>
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

    /// <inheritdoc/>
    public override Encoding Encoding => OriginalWriter.Encoding;

    /// <inheritdoc/>
    public override void Write(char value)
    {
        OriginalWriter.Write(value);
        fileWriter.Write(value);
    }

    /// <inheritdoc/>
    public override void Write(string? value)
    {
        OriginalWriter.Write(value);
        fileWriter.Write(value);
    }

    /// <inheritdoc/>
    public override void WriteLine(string? value)
    {
        OriginalWriter.WriteLine(value);
        fileWriter.WriteLine(value);
    }

    /// <inheritdoc/>
    public override void WriteLine()
    {
        OriginalWriter.WriteLine();
        fileWriter.WriteLine();
    }

    /// <inheritdoc/>
    public override void Flush()
    {
        OriginalWriter.Flush();
        fileWriter.Flush();
    }

    /// <inheritdoc/>
    public override async Task WriteAsync(char value)
    {
        await OriginalWriter.WriteAsync(value);
        await fileWriter.WriteAsync(value);
    }

    /// <inheritdoc/>
    public override async Task WriteAsync(string? value)
    {
        await OriginalWriter.WriteAsync(value);
        await fileWriter.WriteAsync(value);
    }

    /// <inheritdoc/>
    public override async Task WriteLineAsync(string? value)
    {
        await OriginalWriter.WriteLineAsync(value);
        await fileWriter.WriteLineAsync(value);
    }

    /// <inheritdoc/>
    public override async Task WriteLineAsync()
    {
        await OriginalWriter.WriteLineAsync();
        await fileWriter.WriteLineAsync();
    }

    /// <inheritdoc/>
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

    /// <inheritdoc/>
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