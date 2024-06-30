namespace SunamoCl;


public interface ICryptCl
{
    List<byte> s { set; get; }
    List<byte> iv { set; get; }
    string pp { set; get; }
}