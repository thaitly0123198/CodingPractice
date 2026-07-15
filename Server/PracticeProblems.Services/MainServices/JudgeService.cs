using System.Text.Json.Nodes;
using MongoDB.Bson;
using MongoDB.Bson.IO;
using PracticeProblems.Core.Entities;
using PracticeProblems.Core.Interfaces;
using PracticeProblems.Services.FileManip;

namespace PracticeProblems.Services.MainServices;

public class JudgeService : IJudge
{
    ProcessManagement judgeProcess = new();
    SolutionFileManagement fileManip = new();

    public async Task<Result> CheckSolutionAsync(string problemId, string solution, string solutionFuncName, List<TestCase> testcases, string language)
    {
        Console.WriteLine($"JudgeService.cs: Begin judging problem with id: {problemId}");

        // Get Test cases for the problem from the database
        // due to scope of project: inputs and outputs types are only primitive types: int, string, [], dict, char,...
        string solutionPath = fileManip.BuildSolutionPath(solution, language);
        string errMessage = string.Empty;

        foreach (TestCase tc in testcases)
        {
            // convert bsonvalue object to json string 
            // so it can be piped to the python solution script thru stdin
            string testInput = tc.Input.ToJson();
            string testOutput = tc.Output.ToJson();
            // Console.WriteLine($"JudgeService.cs->CheckSolutionAsync->testcase.input: {tc.Input}.");
            // Console.WriteLine($"JudgeService.cs->CheckSolutionAsync->testcase.output: {tc.Output}.\n\n");
            // break;

            // todo: put in guards to prevent infinite loops, long runtime, program gobble up too much resources
            try
            {
                // Create solution file with the submitted solution code
                // file created at temp dir at solutionPath 
                await fileManip.BuildSolutionFileAsync(solution, solutionFuncName, solutionPath, language);


                // run the solution file with test case inputs, and get its outputs as SolutionProgramOutput
                SolutionProgramOutput programOutput = await judgeProcess.ExecuteFileAsync(solutionPath, testInput);

                // compare the output with the expected output for each test case
                bool isPass = CompareOutputWithTestOutput(programOutput.Result, JsonNode.Parse(testOutput));

                // Verdict 
                if (!isPass)
                {
                    // stopped runnning test case at the first time solution produce wrong output
                    return new Result() {
                        IsPassed = false,
                        FailedCase = tc,
                        RuntimeErrorMessage = programOutput.RuntimeError,
                        CompilationErrorMessage = programOutput.CompilationError,
                    };
                }

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
        return new Result() {IsPassed = true};
    }

    private static bool CompareOutputWithTestOutput(JsonNode output,JsonNode testOutput)
    {
        if (output is null)
        {
            return false;
        }
        
        return JsonNode.DeepEquals(output, testOutput);
    }
}