namespace Blinky
{
    public interface IMorsePatternGenerator
    {
        string GetStringFromCode(string inputMessage);
        string GetCodeFromString(string inputMessage);
    }
}