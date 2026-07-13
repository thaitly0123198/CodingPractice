using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;
using PracticeProblems.Services.FileManip;

namespace PracticeProblems.Services.MainServices;

public class JudgeService : IJudge
{
    ProcessManagement judgeProcess = new();
    SolutionFileManagement fileManip = new();

    public async Task<Result> CheckSolutionAsync(string problemId, string solution, List<TestCase> testcases, string language)
    {
        // Get Test cases for the problem from the database
        // using Add Binary for testing purposes, the test cases are:
        // Input: a = "11", b = "1" Output: "100"
        // Input: a = "1010", b = "1011" Output: "10101"
        // due to scope of project: inputs and outputs types are only primitive types: int, string, [], dict, char,...
        // string ext = fileManipulation.GetFileExtension(language);
        // string uniqueFileName = $"{Convert.ToHexString(RandomNumberGenerator.GetBytes(8))}.{ext}";
        string solutionPath = fileManip.BuildSolutionPath(solution, language);
        var testInput = new { };

        foreach (TestCase tc in testcases)
        {
            var input = tc.Input;
            // Console.WriteLine($"JudgeService.cs->CheckSolutionAsync->testcase.input: {tc.Input}.");
            // Console.WriteLine($"JudgeService.cs->CheckSolutionAsync->testcase.output: {tc.Output}.\n\n");
            // break;
            try
            {
                // Create solution file with the submitted solution code
                // file created at temp dir at solutionPath 
                await fileManip.BuildSolutionFileAsync(solution, solutionPath, input, language);


                // Run the solution file with test case inputs, and get its outputs as list
                // var outputs = await RunPythonSolutionFileAgainstTestCases(solutionPath, testInput);

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
                fileManip.DeleteSolutionFile(solutionPath);
            }
        }

        // Return the result to the user
        return new Result(); // Replace with actual result logic
    }

    // TODO: add testcases to each problem in db as db->practiceproblems->problems->testcases->testcase1, testcase2, testcase3, etc
    // For now use problem->examples as test cases for the problem

    // private static async Task<Result> RunPythonSolutionFileAgainstTestCases(string solutionPath, object testInput)
    // {
        // start a new process to run the solution file
        // var processStartInfo = new ProcessStartInfo
        // {
        //     FileName = "python3", // todo: assuming python is installed and in PATH, change in PROD
        //     Arguments = $"{solutionPath}",
        //     RedirectStandardInput = true,
        //     RedirectStandardOutput = true,
        //     RedirectStandardError = true,
        //     UseShellExecute = false,
        //     CreateNoWindow = true
        // };
        // var process = Process.Start(processStartInfo);
        // if (process == null)
        // {
        //     Console.WriteLine($"JudgeService.cs: Failed to start the Python process.");
        //     throw new InvalidOperationException("Failed to start the Python process.");
        // }

        // try
        // {
        //     Console.WriteLine($"JudgeService.cs: Running solution file with input: {testInput}");
        //     string testInputChars = JsonSerializer.Serialize(testInput);
        //     await process.StandardInput.WriteAsync(testInputChars);
        //     process.StandardInput.Close();
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"JudgeService.cs: Error running solution file: {ex.Message}");
        //     throw;
        // }

        // try
        // {
        //     await process.WaitForExitAsync();
        //     var output = await process.StandardOutput.ReadToEndAsync();
        //     var error = await process.StandardError.ReadToEndAsync();

        //     if (!string.IsNullOrEmpty(error))
        //     {
        //         Console.WriteLine($"JudgeService.cs: Error from solution file: {error}");
        //         return new Result(error);
        //         // throw new InvalidOperationException($"Error from solution file: {error}");
        //     }

        //     Console.WriteLine($"JudgeService.cs: Output from solution file: {output}");
        // }
        // catch (Exception ex)
        // {
        //     Console.WriteLine($"JudgeService.cs: Error reading output from solution file: {ex.Message}");
        //     throw;
        // }


        // For now, just return a dummy output
        // await Task.Delay(100); // simulate some processing time
    //     return new Result();
    // }


}