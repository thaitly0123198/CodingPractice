namespace PracticeProblems.Api.Contracts;

public record ProblemsChunkResponse(string Id, string Title, string Difficulty);

// use this to test if TestCases come thru
// public record ProblemByIdResponse(string Id, string Title, string Description, string Constraint, List<string> Examples, string Category, object TestCases = null);
public record ProblemByIdResponse(string Id, string Title, string Description, string Constraint, List<string> Examples, string Category, string SolutionBoxPlaceholder, string FunctionName);

// shape for receiving from client upon clicking submit
public record SubmittedSolution(string Solution, string FunctionName);