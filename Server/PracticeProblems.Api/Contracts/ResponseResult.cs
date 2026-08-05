namespace PracticeProblems.Api.Contracts;

// public record Result(bool IsPassed);
public record FailCase(string Input);
// shape to send back to client upon submission done
public record ResultsResponse(bool IsPass, FailCase? FailedTestcase, string RuntimeErrorMessage, string CompilationErrorMessage);