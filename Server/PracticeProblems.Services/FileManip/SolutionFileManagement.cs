using System;
using System.Diagnostics;
using System.IO;
using System.Text.Json;
using System.Security.Cryptography;
using PracticeProblems.Core.Entities;
using System.Net;
using MongoDB.Bson;
namespace PracticeProblems.Services.FileManip;

public class SolutionFileManagement
{
    // build the full solution file with the submitted solution to run against test cases 
    public async Task BuildSolutionFileAsync(string solutionPath, string solution, BsonValue input, string lang)
    {
        Console.WriteLine($"SolutionService.cs->BuildSolutionFileAsync->input: {input}.");

        // Create a file to write to.
        if (!File.Exists(solutionPath))
        {
            if (lang == "python")
            {
                var imports = """
                            import json
                            import sys
                            from typing import *
                            """;
                var main = $$$"""
                            if __name__ == "__main__":
                                # read whole input
                                input = sys.stdin.read()
                                
                                # parse json to dict
                                data = json.loads(input)

                                # get typed data
                                if data is not None:
                                    a = data.get("a")
                                    b = data.get("b")
                                    if a is not None and b is not None:
                                        sol = Solution().addBinary(a, b)
                                        print(json.dumps(sol))
                                    else:
                                        print("Invalid input.")
                            """;

                await File.AppendAllTextAsync(solutionPath, imports);
                await File.AppendAllTextAsync(solutionPath, solution);

            }

        }
    }


    public void DeleteSolutionFile(string solutionPath)
    {
        // Delete the temporary solution file   
        if (File.Exists(solutionPath))
        {
            Console.WriteLine($"JudgeService.cs: Deleting solution file at: {solutionPath}");
            File.Delete(solutionPath);
        }
    }

    public string BuildSolutionPath(string solution, string lang)
    {
        string ext = BuildFileExtension(lang);
        string uniqueFileName = $"{Convert.ToHexString(RandomNumberGenerator.GetBytes(8))}.{ext}";
        return Path.Combine(Path.GetTempPath(), uniqueFileName);
    }

    private static string BuildFileExtension(string language)
    {
        if (language.ToLower() == "python") { return "py"; }
        else if (language.ToLower() == "javascript") { return "js"; }
        else
        {
            throw new ArgumentException($"Unsupported programming language: {language}");
        }
    }
}