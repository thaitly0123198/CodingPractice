using System.Text.Json;
using System.Text.Json.Nodes;
using System.Text.Json.Serialization;
using MongoDB.Driver;

namespace PracticeProblems.Core.Entities;

public class SolutionProgramOutput()
{
    public bool Ok { get; set; }
    public JsonNode? Result { get; set; }   // jsonnode: int, array, bool, object
    
    // case sensitive matching with the field  "error" from the output from the python script stdout.
    // or [JsonPropertyName("error")]
    public string Stdout { get; set; } = string.Empty;   // the user's captured prints
    public string RuntimeError { get; set; } = string.Empty;
    public string CompilationError { get; set; } = string.Empty;
    public string TimeoutError { get; set; } = string.Empty;

}