using System;
using System.Diagnostics;
using System.Security.Cryptography;
using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;
namespace PracticeProblems.Services.MainServices;

public class JudgeService : IJudge
{
    public async Task<Result> CheckSolutionAsync(string problemId, string solution, string language)
    {
        // Get Test cases for the problem from the database
        // using Add Two Numbers for testing purposes, the test cases are:
        // Input l1 = [2,4,3], l2 = [5,6,4] => [2,4,3] [5,6,4]
        // output: list of same length as input list 
        // const testcases = 
        string ext = GetFileExtension(language);
        string uniqueFileName = $"{Convert.ToHexString(RandomNumberGenerator.GetBytes(8))}.{ext}";
        string solutionPath = Path.Combine(Directory.GetCurrentDirectory(), uniqueFileName);

        // Create solution file with the solution code
        try
        {
            await CreateSolutionFile(solution, solutionPath);

            // Run the solution file with test case inputs 

            // and gets the output

            // Compare the output with the expected output for each test case

            // Verdict 

        }
        catch (Exception ex)
        {
            Console.WriteLine($"JudgeService.cs: Error creating solution file: {ex.Message}");
            throw;
        }
        finally
        {
            // Delete the solution file after execution
            DeleteSolutionFile(solutionPath);
        }


        // Return the result to the user
        return new Result(); // Replace with actual result logic
    }

    // TODO: add testcases to each problem in db as db->practiceproblems->problems->testcases->testcase1, testcase2, testcase3, etc
    // For now use problem->examples as test cases for the problem

    private static async Task<Result> RunSolutionFile(string solutionPath, string input)
    {
        // Run the solution file with the given input and return the output
        // For now, just return a dummy output
        await Task.Delay(100); // Simulate some processing time
        return new Result(); // Replace with actual execution logic
    }


    private static string GetFileExtension(string language)
    {
        if (language.ToLower() == "python") { return "py"; }
        else if (language.ToLower() == "javascript") { return "js"; }
        else
        {
            throw new ArgumentException($"Unsupported programming language: {language}");
        }
    }

    private static async Task CreateSolutionFile(string solution, string solutionPath)
    {
        // Create a temporary file with the solution code
        // only support python for now
        Console.WriteLine($"JudgeService.cs: Creating solution file at: {solutionPath}");
        await File.WriteAllTextAsync(solutionPath, solution);
    }

    private static void DeleteSolutionFile(string solutionPath)
    {
        // Delete the temporary solution file   
        if (File.Exists(solutionPath))
        {
            Console.WriteLine($"JudgeService.cs: Deleting solution file at: {solutionPath}");
            File.Delete(solutionPath);
        }
    }
}