namespace SunamoCl._public.SunamoLogging.Base;
public abstract class TemplateLoggerBaseCl
{
    private static Type type = typeof(TemplateLoggerBaseCl);
    private readonly Action<TypeOfMessageCl, string, string[]> _writeLineDelegate;

    public TemplateLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }

    public bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, string[] args)
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

    private void WriteLine(TypeOfMessageCl error, string v)
    {
        _writeLineDelegate(error, v, []);
    }


    public bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, string[] args)
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

    public void SavedToDrive(string v)
    {
        WriteLine(TypeOfMessageCl.Success, sess.i18n(XlfKeys.SavedToDrive) + ": " + v);
    }

    public void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    public void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessageCl.Success, nameOfOperation + " - " + sess.i18n(XlfKeys.Finished));
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

    public void CouldNotBeParsed(string entity, string text)
    {
        WriteLine(TypeOfMessageCl.Error, entity + " with value " + text + " could not be parsed");
    }

    public void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageCl.Error, sess.i18n(XlfKeys.SomeErrorsOccuredSeeLog));
    }

    public void FolderDontExists(string folder)
    {
        WriteLine(TypeOfMessageCl.Error, sess.i18n(XlfKeys.Folder) + " " + folder + " doesn't exists.");
    }

    public void FileDontExists(string selectedFile)
    {
        WriteLine(TypeOfMessageCl.Error, sess.i18n(XlfKeys.File) + " " + selectedFile + " doesn't exists.");
    }

    #endregion

    #region Information

    public void LoadedFromStorage(string item)
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.LoadedFromStorage) + ": " + item);
    }

    public void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.InsertAsIndexesZeroBased));
    }

    public void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }

    public void OperationWasStopped()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.OperationWasStopped));
    }

    public void NoData()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.PleaseEnterRightInputData));
    }


    public void SuccessfullyResized(string fn)
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.SuccessfullyResizedTo) + " " + fn);
    }


    public bool AnyElementIsNullOrEmpty(Type type, string methodName, string nameOfCollection, List<string> args)
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