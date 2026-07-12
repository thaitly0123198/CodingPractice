namespace PracticeProblems.Services.FileManip;

public class FileProcessor
{
    public static async Task WriteToFileAsync(string filePath, string content)
    {
        await File.WriteAllTextAsync(filePath, content);
    }

    public static async Task<string> ReadFromFileAsync(string filePath)
    {
        return await File.ReadAllTextAsync(filePath);
    }


}