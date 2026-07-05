namespace PracticeProblems.Api.Contracts;

public record ProblemsChunkResponse(string Id, string Title, string Difficulty); 
public record ProblemByIdResponse(string Id, string Title, string Description, List<string> Examples, string Category );
public record SubmittedSolution(string Id, string Solution);