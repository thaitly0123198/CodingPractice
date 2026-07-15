using System;
using System.Diagnostics;
using System.Text.Json;
using System.Text.Json.Nodes;
using PracticeProblems.Core.Entities;

namespace PracticeProblems.Services.FileManip;

// this should die at the end of solution checking -> addTransient
public class ProcessManagement
{

    public async Task<SolutionProgramOutput> ExecuteFileAsync(string solutionPath, string stdinJson, string lang = "python")
    {
        SolutionProgramOutput solOutput = new();

        if (lang == "python")
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


            Console.WriteLine($"ProcessManagement.cs: Running solution file with input: {stdinJson}");

            await process.StandardInput.WriteAsync(stdinJson);
            process.StandardInput.Close();

            // infinite loops, timeout guard
            using var cts = new CancellationTokenSource(TimeSpan.FromSeconds(10));

            // start both ReadToEndAsync tasks before awaiting either,
            //  so a large stdout can't fill its pipe buffer while you're draining stderr
            var outTask = process.StandardOutput.ReadToEndAsync(cts.Token);
            var errTask = process.StandardError.ReadToEndAsync(cts.Token);
            string output = await outTask;
            string error = await errTask;

            try
            {

                await process.WaitForExitAsync();

                // if output from script is empty -> script failed to compile
                if (string.IsNullOrWhiteSpace(output))
                    return new SolutionProgramOutput { Ok = false, CompilationError = "Compilation Error!" };

                try
                {
                    // deserialize output into SolutionProgramOutput
                    // if deserialized value is null, solution FAILS
                    solOutput = JsonSerializer.Deserialize<SolutionProgramOutput>(output,
                    new JsonSerializerOptions { PropertyNameCaseInsensitive = true }) ?? new SolutionProgramOutput { Ok = false };

                    if (!solOutput.Ok)
                    {
                        solOutput.RuntimeError = "Runtime Error!";
                    }
                    return solOutput;
                }
                catch (JsonException)
                {
                    Console.Write($"ProcessManagement.cs->ExecuteFileAsync: JsonException Error");
                    return new SolutionProgramOutput() { CompilationError = $"Server unavailable, try again!" };
                }
            }
            catch (OperationCanceledException)
            {
                process.Kill(entireProcessTree: true);   // entireProcessTree: catches child processes it spawned
                return new SolutionProgramOutput { Ok = false, TimeoutError = "Time limit exceeded" };
            }
        }
        return solOutput;
    }
}