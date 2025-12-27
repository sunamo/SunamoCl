namespace SunamoCl._public.SunamoLogging.Base;

public abstract class TemplateLoggerBaseCl
{
    private static Type _type = typeof(TemplateLoggerBaseCl);
    private readonly Action<TypeOfMessageCl, string, string[]> _writeLineDelegate;

    public TemplateLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }

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
        _writeLineDelegate(error, message, []);
    }


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

    public void SavedToDrive(string filePath)
    {
        WriteLine(TypeOfMessageCl.Success, Translate.FromKey(XlfKeys.SavedToDrive) + ": " + filePath);
    }

    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    public void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessageCl.Success, nameOfOperation + " - " + Translate.FromKey(XlfKeys.Finished));
    }

    public void EndRunTime()
    {
        WriteLine(TypeOfMessageCl.Ordinal, Messages.AppWillBeTerminated);
    }

    #region Success

    public void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessageCl.Success, "Result was successfully copied to clipboard.");
    }

    public void CopiedToClipboard(string what)
    {
        WriteLine(TypeOfMessageCl.Success, what + " was successfully copied to clipboard.");
    }

    #endregion

    #region Error

    public void CouldNotBeParsed(string entityName, string entityValue)
    {
        WriteLine(TypeOfMessageCl.Error, entityName + " with value " + entityValue + " could not be parsed");
    }

    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageCl.Error, Translate.FromKey(XlfKeys.SomeErrorsOccuredSeeLog));
    }

    public void FolderDontExists(string path)
    {
        WriteLine(TypeOfMessageCl.Error, Translate.FromKey(XlfKeys.Folder) + " " + path + " doesn't exists.");
    }

    public void FileDontExists(string path)
    {
        WriteLine(TypeOfMessageCl.Error, Translate.FromKey(XlfKeys.File) + " " + path + " doesn't exists.");
    }

    #endregion

    #region Information

    public void LoadedFromStorage(string item)
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.LoadedFromStorage) + ": " + item);
    }

    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.InsertAsIndexesZeroBased));
    }

    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }

    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.OperationWasStopped));
    }

    public void NoData()
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.PleaseEnterRightInputData));
    }


    public void SuccessfullyResized(string fn)
    {
        WriteLine(TypeOfMessageCl.Information, Translate.FromKey(XlfKeys.SuccessfullyResizedTo) + " " + fn);
    }


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

    public void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(':');
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " have unallowed value");
    }

    public void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(':');
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " must have value");
    }

    #endregion
}