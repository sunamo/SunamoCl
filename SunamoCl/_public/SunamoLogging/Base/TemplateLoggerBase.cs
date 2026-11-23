namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
/// Represents the template logger base cl type.
/// </summary>
public abstract class TemplateLoggerBaseCl
{
    private readonly Action<TypeOfMessageCl, string, string[]> writeLineDelegate;

    /// <summary>
    /// Initializes a new instance of the template logger base cl class.
    /// </summary>
    /// <param name="writeLineDelegate">The write line delegate.</param>
    public TemplateLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> writeLineDelegate)
    {
        this.writeLineDelegate = writeLineDelegate;
    }

    /// <summary>
    /// Not even number of elements.
    /// </summary>
    /// <param name="nameOfCollection">The name of collection.</param>
    /// <param name="args">The args.</param>
    /// <returns>The not even number of elements result.</returns>
    public bool NotEvenNumberOfElements(string nameOfCollection, string[] args)
    {
        if (args.Count() % 2 == 1)
        {
            WriteLine(TypeOfMessageCl.Error,
                Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(), nameOfCollection));
            return false;
        }

        return true;
    }

    private string FullNameOfExecutedCode()
    {
        return ThrowEx.FullNameOfExecutedCode();
    }

    private void WriteLine(TypeOfMessageCl typeOfMessage, string? message)
    {
        writeLineDelegate(typeOfMessage, message ?? string.Empty, []);
    }

    /// <summary>
    /// Any element is null.
    /// </summary>
    /// <param name="nameOfCollection">The name of collection.</param>
    /// <param name="args">The args.</param>
    /// <returns>The any element is null result.</returns>
    public bool AnyElementIsNull(string nameOfCollection, string[] args)
    {
        var nullIndexes = CAIndexesWithNull.IndexesWithNull(args);
        if (nullIndexes.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information,
                Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), nameOfCollection, nullIndexes));
            return true;
        }

        return false;
    }

    /// <summary>
    /// Saved to drive.
    /// </summary>
    /// <param name="filePath">The file path.</param>
    public void SavedToDrive(string filePath)
    {
        WriteLine(TypeOfMessageCl.Success, "SavedToDrive" + ": " + filePath);
    }

    /// <summary>
    /// Try a few seconds later after fully initialized.
    /// </summary>
    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageCl.Information, "Try a few seconds later after fully initialized");
    }

    /// <summary>
    /// Finished.
    /// </summary>
    /// <param name="nameOfOperation">The name of operation.</param>
    public void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessageCl.Success, nameOfOperation + " - " + "Finished");
    }

    /// <summary>
    /// End run time.
    /// </summary>
    public void EndRunTime()
    {
        WriteLine(TypeOfMessageCl.Ordinal, Messages.AppWillBeTerminated);
    }

    #region Success

    /// <summary>
    /// Result copied to clipboard.
    /// </summary>
    public void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessageCl.Success, "Result was successfully copied to clipboard.");
    }

    /// <summary>
    /// Copied to clipboard.
    /// </summary>
    /// <param name="what">The what.</param>
    public void CopiedToClipboard(string what)
    {
        WriteLine(TypeOfMessageCl.Success, what + " was successfully copied to clipboard.");
    }

    #endregion

    #region Error

    /// <summary>
    /// Could not be parsed.
    /// </summary>
    /// <param name="entityName">The entity name.</param>
    /// <param name="entityValue">The entity value.</param>
    public void CouldNotBeParsed(string entityName, string entityValue)
    {
        WriteLine(TypeOfMessageCl.Error, entityName + " with value " + entityValue + " could not be parsed");
    }

    /// <summary>
    /// Some errors occured see log.
    /// </summary>
    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageCl.Error, "Some errors occured see log");
    }

    /// <summary>
    /// Folder dont exists.
    /// </summary>
    /// <param name="path">The path.</param>
    public void FolderDontExists(string path)
    {
        WriteLine(TypeOfMessageCl.Error, "Folder" + " " + path + " doesn't exists.");
    }

    /// <summary>
    /// File dont exists.
    /// </summary>
    /// <param name="path">The path.</param>
    public void FileDontExists(string path)
    {
        WriteLine(TypeOfMessageCl.Error, "File" + " " + path + " doesn't exists.");
    }

    #endregion

    #region Information

    /// <summary>
    /// Loaded from storage.
    /// </summary>
    /// <param name="text">The text.</param>
    public void LoadedFromStorage(string text)
    {
        WriteLine(TypeOfMessageCl.Information, "LoadedFromStorage" + ": " + text);
    }

    /// <summary>
    /// Insert as indexes zero based.
    /// </summary>
    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageCl.Information, "Insert as indexes zero based");
    }

    /// <summary>
    /// Unfortunately bad format please try again.
    /// </summary>
    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageCl.Information, "Unfortunately bad format please try again" + ".");
    }

    /// <summary>
    /// Operation was stopped.
    /// </summary>
    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageCl.Information, "OperationWasStopped");
    }

    /// <summary>
    /// No data.
    /// </summary>
    public void NoData()
    {
        WriteLine(TypeOfMessageCl.Information, "Please enter right input data");
    }

    /// <summary>
    /// Successfully resized.
    /// </summary>
    /// <param name="text">The text.</param>
    public void SuccessfullyResized(string text)
    {
        WriteLine(TypeOfMessageCl.Information, "SuccessfullyResizedTo" + " " + text);
    }

    /// <summary>
    /// Any element is null or empty.
    /// </summary>
    /// <param name="nameOfCollection">The name of collection.</param>
    /// <param name="args">The args.</param>
    /// <returns>The any element is null or empty result.</returns>
    public bool AnyElementIsNullOrEmpty(string nameOfCollection, List<string> args)
    {
        var nullIndexes = CAIndexesWithNull.IndexesWithNullOrEmpty(args);
        if (nullIndexes.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information,
                Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), nameOfCollection, nullIndexes));
            return true;
        }

        return false;
    }

    /// <summary>
    /// Have unallowed value.
    /// </summary>
    /// <param name="controlNameOrText">The control name or text.</param>
    public void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(':');
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " have unallowed value");
    }

    /// <summary>
    /// Must have value.
    /// </summary>
    /// <param name="controlNameOrText">The control name or text.</param>
    public void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(':');
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " must have value");
    }

    #endregion
}
