using System;
using System.Diagnostics;
using System.Text.Json;
using PracticeProblems.Core.Entities;

namespace PracticeProblems.Services.FileManip;

// this should die at the end of solution checking -> addTransient
public class ProcessManagement
{
    public async Task<Result> ExecuteFileAsync(string solutionPath, string stdinJson)
    {

        var psi = new ProcessStartInfo
        {
            FileName = "python3", // todo: assuming python is installed and in PATH, change in PROD
            Arguments = $"{solutionPath}",
            RedirectStandardInput = true,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            UseShellExecute = false,
            CreateNoWindow = true
        };

        // guarantees Dispose() gets called, no worry about forgetting to kill it
        using var process = Process.Start(psi) 
                        ?? throw new InvalidOperationException("Failed to start the Python process.");

        try
        {
            Console.WriteLine($"JudgeService.cs: Running solution file with input: {stdinJson}");
            string testInputChars = JsonSerializer.Serialize(stdinJson);
            await process.StandardInput.WriteAsync(testInputChars);
            process.StandardInput.Close();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JudgeService.cs: Error running solution file: {ex.Message}");
            throw;
        }

        try
        {
            await process.WaitForExitAsync();
            var output = await process.StandardOutput.ReadToEndAsync();
            var error = await process.StandardError.ReadToEndAsync();

            if (!string.IsNullOrEmpty(error))
            {
                Console.WriteLine($"JudgeService.cs: Error from solution file: {error}");
                return new Result(error);
                // throw new InvalidOperationException($"Error from solution file: {error}");
            }

            Console.WriteLine($"JudgeService.cs: Output from solution file: {output}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"JudgeService.cs: Error reading output from solution file: {ex.Message}");
            throw;
        }

        return new Result();
    }
}