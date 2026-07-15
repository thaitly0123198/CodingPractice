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
    public async Task BuildSolutionFileAsync(string solution, string solutionFuncName, string solutionPath, string lang)
    {
        // Create a file to write to.
        if (!File.Exists(solutionPath))
        {
            if (lang == "python")
            {
                var importStr = BuildSolutionImports(lang);
                var mainStr = BuildSolutionMain(solutionFuncName, lang);

                await File.AppendAllTextAsync(solutionPath, importStr + "\n" + solution + "\n\n" + mainStr);
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

    private static string BuildSolutionImports(string lang = "python")
    {
        string imports = string.Empty;
        if (lang == "python")
        {
            imports =
                    """
                    import io, contextlib, json, sys
                    import math
                    import re
                    from bisect import bisect_left, bisect_right
                    from collections import Counter, defaultdict, deque
                    from functools import cache, cmp_to_key, reduce
                    from heapq import heapify, heappop, heappush, nlargest
                    from itertools import accumulate, chain, count, pairwise
                    from math import ceil, dist, factorial, gcd, inf, sqrt
                    from operator import xor
                    from string import ascii_lowercase
                    from typing import Deque, List, Optional, Tuple
                    """;
        }
        return imports;
    }

    private static string BuildSolutionMain(string functionName, string lang = "python")
    {
        string main = string.Empty;
        // class Solution:
        //     def addBinary(self, a: str, b: str) -> str:
        //     ans = []
        //     i, j, carry = len(a) - 1, len(b) - 1, 0
        //     while i >= 0 or j >= 0 or carry:
        //         carry += (0 if i < 0 else int(a[i])) +(0 if j < 0 else int(b[j]))
        //         carry, v = divmod(carry, 2)
        //         ans.append(str(v))
        //         i, j = i - 1, j - 1
        //     return "".join(ans[::- 1])
          
        if (lang == "python")
        {
            main = $$$"""
                    if __name__ == "__main__":
                        # read whole input
                        payload = sys.stdin.read()
                        
                        # parse json to dict
                        data = json.loads(payload)

                        buf = io.StringIO()
                        # user prints land in buf, not stdout
                        # so stdout only gets the result from the solution run
                        try:
                            with contextlib.redirect_stdout(buf):          
                                # get typed data
                                if data is not None:
                                    fn = getattr(Solution(), "{{{functionName}}}")
                                    if isinstance(data, dict):
                                        # data is a dict i.e {"nums": [...], "target": 9} ->  fn(nums=[...], target=9)
                                        result = fn(**data)
                                    else:
                                        # data is a type like [1,2,3] or 121  ->  fn([1,2,3])
                                        result = fn(data)
                            body = json.dumps({"ok": True, "result": result, "stdout": buf.getvalue()})   
                        except Exception as e:
                            body = json.dumps({"ok": False, "error": f"{type(e).__name__}: {e}", "stdout": buf.getvalue()})
                        sys.stdout.write(body)
                    """;

        }

        return main;
    }
}