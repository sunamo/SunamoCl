//namespace SunamoCl;
//internal class SHSE
//{
//    public static string ConvertTypedWhitespaceToString(string delimiter)
//    {
//        const string nl = @"
//";

//        switch (delimiter)
//        {
//            // must use \r\n, not Environment.NewLine (is not constant)
//            case "\\r\\n":
//            case "\\n":
//            case "\\r":
//                return nl;
//            case "\\t":
//                return "\t";
//        }

//        return delimiter;
//    }
//}
