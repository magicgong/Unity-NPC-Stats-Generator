using System.IO;

public class WriteStringToJSONFile
{
    const string extension = ".json";
    public static void WriteToJSON(string path, string name, string JSONString)
    {
        System.IO.File.WriteAllText(path + name + extension, JSONString);
    }
}
