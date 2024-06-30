

namespace SunamoCl;

public abstract class TemplateLoggerBaseCl
{
    internal TemplateLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }


    static Type type = typeof(TemplateLoggerBaseCl);
    private Action<TypeOfMessageCl, string, string[]> _writeLineDelegate;

    /// <summary>
    /// Return true if number of counts is odd
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="nameOfCollection"></param>
    /// <param name="args"></param>
    internal bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, string[] args)
    {
        if (args.Count() % 2 == 1)
        {
            WriteLine(TypeOfMessageCl.Error, Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection));
            return false;
        }
        return true;
    }

    Tuple<string, string, string> t => Exc.GetStackTrace2(true);

    private string FullNameOfExecutedCode(object type, string methodName)
    {
        return ThrowEx.FullNameOfExecutedCode(t.Item1, t.Item2);
    }

    private void WriteLine(TypeOfMessageCl error, string v)
    {
        _writeLineDelegate(error, v, EmptyArrays.Strings);
    }



    /// <summary>
    /// Return true if any will be null
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="nameOfCollection"></param>
    /// <param name="args"></param>
    internal bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, string[] args)
    {
        List<int> nulled = CAIndexesWithNull.IndexesWithNull(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
            return true;
        }
        return false;
    }

    internal void SavedToDrive(string v)
    {
        WriteLine(TypeOfMessageCl.Success, sess.i18n(XlfKeys.SavedToDrive) + ": " + v);
    }

    internal void TryAFewSecondsLaterAfterFullyInitialized()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.TryAFewSecondsLaterAfterFullyInitialized));
    }

    internal void Finished(string nameOfOperation)
    {
        WriteLine(TypeOfMessageCl.Success, nameOfOperation + " - " + sess.i18n(XlfKeys.Finished));
    }
    internal void EndRunTime()
    {
        WriteLine(TypeOfMessageCl.Ordinal, Messages.AppWillBeTerminated);
    }
    #region Success
    internal void ResultCopiedToClipboard()
    {
        WriteLine(TypeOfMessageCl.Success, "Result was successfully copied to clipboard.");
    }

    internal void CopiedToClipboard(string what)
    {
        WriteLine(TypeOfMessageCl.Success, what + " was successfully copied to clipboard.");
    }
    #endregion
    #region Error
    internal void CouldNotBeParsed(string entity, string text)
    {
        WriteLine(TypeOfMessageCl.Error, entity + " with value " + text + " could not be parsed");
    }
    internal void SomeErrorsOccuredSeeLog()
    {
        WriteLine(TypeOfMessageCl.Error, sess.i18n(XlfKeys.SomeErrorsOccuredSeeLog));
    }
    internal void FolderDontExists(string folder)
    {
        WriteLine(TypeOfMessageCl.Error, sess.i18n(XlfKeys.Folder) + " " + folder + " doesn't exists.");
    }
    internal void FileDontExists(string selectedFile)
    {
        WriteLine(TypeOfMessageCl.Error, sess.i18n(XlfKeys.File) + " " + selectedFile + " doesn't exists.");
    }

    #endregion
    #region Information
    internal void LoadedFromStorage(string item)
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.LoadedFromStorage) + ": " + item);
    }

    internal void InsertAsIndexesZeroBased()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.InsertAsIndexesZeroBased));
    }
    internal void UnfortunatelyBadFormatPleaseTryAgain()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.UnfortunatelyBadFormatPleaseTryAgain) + ".");
    }
    internal void OperationWasStopped()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.OperationWasStopped));
    }
    internal void NoData()
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.PleaseEnterRightInputData));
    }
    /// <summary>
    /// Zmena: metoda nezapisuje primo na konzoli, misto toho pouze vraci retezec
    /// </summary>
    /// <param name="fn"></param>
    internal void SuccessfullyResized(string fn)
    {
        WriteLine(TypeOfMessageCl.Information, sess.i18n(XlfKeys.SuccessfullyResizedTo) + " " + fn);
    }



    /// <summary>
    /// Return true if any will be null or empty
    /// </summary>
    /// <param name="type"></param>
    /// <param name="methodName"></param>
    /// <param name="nameOfCollection"></param>
    /// <param name="args"></param>
    internal bool AnyElementIsNullOrEmpty(Type type, string methodName, string nameOfCollection, List<string> args)
    {
        List<int> nulled = CAIndexesWithNull.IndexesWithNullOrEmpty(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
            return true;
        }
        return false;
    }

    internal void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " have unallowed value");
    }
    internal void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " must have value");
    }
    #endregion
}