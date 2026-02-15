namespace SunamoCl._public.SunamoLogging.Base;

/// <summary>
/// Base class for template-based logging that provides predefined message methods for common scenarios
/// </summary>
public abstract class TemplateLoggerBaseCl
{
    private readonly Action<TypeOfMessageCl, string, string[]> _writeLineDelegate;

    /// <summary>
    /// Initializes a new instance with the specified write delegate
    /// </summary>
    /// <param name="writeLineDelegate">Delegate used to write messages with type, text and arguments</param>
    public TemplateLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }

    /// <summary>
    /// Checks whether the collection has an even number of elements
    /// </summary>
    /// <param name="nameOfCollection">Name of the collection being checked</param>
    /// <param name="args">Array of elements to check</param>
    /// <returns>True if the collection has an even number of elements, false otherwise</returns>
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

    private void WriteLine(TypeOfMessageCl error, string? message)
    {
        _writeLineDelegate(error, message ?? string.Empty, []);
    }


    /// <summary>
    /// Checks whether any element in the collection is null
    /// </summary>
    /// <param name="nameOfCollection">Name of the collection being checked</param>
    /// <param name="args">Array of elements to check for null values</param>
    /// <returns>True if any element is null, false otherwise</returns>
    public bool AnyElementIsNull(string nameOfCollection, string[] args)
    {
        var nulled = CAIndexesWithNull.IndexesWithNull(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information,
                Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), nameOfCollection, nulled));
            return true;
        }

        return false;
    }

    /// <summary>
    /// Logs a success message indicating data was saved to drive
    /// </summary>
    /// <param name="filePath">Path of the saved file</param>
    public void SavedToDrive(string filePath)
    {
        WriteLine(TypeOfMessageCl.Success, Translate.FromKey(XlfKeys.SavedToDrive) + ": " + filePath);
    }

    /// <summary>
    /// Logs an informational message asking user to try again later after full initialization
    /// </summary>
    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    /// <summary>
    /// Logs a success message indicating an operation has finished
    /// </summary>
    /// <param name="nameOfOperation">Name of the completed operation</param>
    public void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessageCl.Success, nameOfOperation + " - " + Translate.FromKey(XlfKeys.Finished));
    }

    /// <summary>
    /// Logs a message indicating the application will be terminated
    /// </summary>
    public void EndRunTime()
    {
        WriteLine(TypeOfMessageCl.Ordinal, Messages.AppWillBeTerminated);
    }

    #region Success

    /// <summary>
    /// Logs a success message indicating the result was copied to clipboard
    /// </summary>
    public void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessageCl.Success, "Result was successfully copied to clipboard.");
    }

    /// <summary>
    /// Logs a success message indicating something was copied to clipboard
    /// </summary>
    /// <param name="what">Description of what was copied</param>
    public void CopiedToClipboard(string what)
    {
        WriteLine(TypeOfMessageCl.Success, what + " was successfully copied to clipboard.");
    }

    #endregion

    #region Error

    /// <summary>
    /// Logs an error message indicating an entity could not be parsed
    /// </summary>
    /// <param name="entityName">Name of the entity that failed to parse</param>
    /// <param name="entityValue">Value that could not be parsed</param>
    public void CouldNotBeParsed(string entityName, string entityValue)
    {
        WriteLine(TypeOfMessageCl.Error, entityName + " with value " + entityValue + " could not be parsed");
    }

    /// <summary>
    /// Logs an error message indicating some errors occurred and directing user to see the log
    /// </summary>
    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageCl.Error, Translate.FromKey(XlfKeys.SomeErrorsOccuredSeeLog));
    }

    /// <summary>
    /// Logs an error message indicating a folder does not exist
    /// </summary>
    /// <param name="path">Path of the non-existent folder</param>
    public void FolderDontExists(string path)
    {
        WriteLine(TypeOfMessageCl.Error, Translate.FromKey(XlfKeys.Folder) + " " + path + " doesn't exists.");
    }

    /// <summary>
    /// Logs an error message indicating a file does not exist
    /// </summary>
    /// <param name="path">Path of the non-existent file</param>
    public void FileDontExists(string path)
    {
        WriteLine(TypeOfMessageCl.Error, Translate.FromKey(XlfKeys.File) + " " + path + " doesn't exists.");
    }

    #endregion

    #region Information

    /// <summary>
    /// Logs an informational message indicating data was loaded from storage
    /// </summary>
    /// <param name="item">Description of the loaded item</param>
    public void LoadedFromStorage(string item)
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.LoadedFromStorage) + ": " + item);
    }

    /// <summary>
    /// Logs an informational message asking user to insert as zero-based indexes
    /// </summary>
    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.InsertAsIndexesZeroBased));
    }

    /// <summary>
    /// Logs an informational message indicating bad format and asking user to try again
    /// </summary>
    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }

    /// <summary>
    /// Logs an informational message indicating the operation was stopped
    /// </summary>
    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.OperationWasStopped));
    }

    /// <summary>
    /// Logs an informational message indicating no data was provided
    /// </summary>
    public void NoData()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.PleaseEnterRightInputData));
    }


    /// <summary>
    /// Logs an informational message indicating something was successfully resized
    /// </summary>
    /// <param name="fn">Name or description of the resized item</param>
    public void SuccessfullyResized(string fn)
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.SuccessfullyResizedTo) + " " + fn);
    }


    /// <summary>
    /// Checks whether any element in the list is null or empty
    /// </summary>
    /// <param name="nameOfCollection">Name of the collection being checked</param>
    /// <param name="args">List of string elements to check</param>
    /// <returns>True if any element is null or empty, false otherwise</returns>
    public bool AnyElementIsNullOrEmpty(string nameOfCollection, List<string> args)
    {
        var nulled = CAIndexesWithNull.IndexesWithNullOrEmpty(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information,
                Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(), nameOfCollection, nulled));
            return true;
        }

        return false;
    }

    /// <summary>
    /// Logs an appeal message indicating a control or text has an unallowed value
    /// </summary>
    /// <param name="controlNameOrText">Name of the control or text description</param>
    public void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(':');
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " have unallowed value");
    }

    /// <summary>
    /// Logs an appeal message indicating a control or text must have a value
    /// </summary>
    /// <param name="controlNameOrText">Name of the control or text description</param>
    public void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(':');
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " must have value");
    }

    #endregion
}