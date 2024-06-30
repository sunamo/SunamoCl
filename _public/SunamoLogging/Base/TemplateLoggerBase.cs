

namespace SunamoCl;

public abstract class TemplateLoggerBaseCl
{
    public TemplateLoggerBaseCl(Action<TypeOfMessageCl, string, string[]> writeLineDelegate)
    {
        _writeLineDelegate = writeLineDelegate;
    }


    static Type type = typeof(TemplateLoggerBaseCl);
    private Action<TypeOfMessageCl, string, string[]> _writeLineDelegate;

    
    
    
    
    
    
    
    public bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, string[] args)
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



    
    
    
    
    
    
    
    public bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, string[] args)
    {
        List<int> nulled = CAIndexesWithNull.IndexesWithNull(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
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
        List<int> nulled = CAIndexesWithNull.IndexesWithNullOrEmpty(args);
        if (nulled.Count > 0)
        {
            WriteLine(TypeOfMessageCl.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
            return true;
        }
        return false;
    }

    public void HaveUnallowedValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " have unallowed value");
    }
    public void MustHaveValue(string controlNameOrText)
    {
        controlNameOrText = controlNameOrText.TrimEnd(AllChars.colon);
        WriteLine(TypeOfMessageCl.Appeal, controlNameOrText + " must have value");
    }
    #endregion
}