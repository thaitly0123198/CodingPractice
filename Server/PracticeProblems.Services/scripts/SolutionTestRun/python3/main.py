#!/usr/bin/env python3
import sys
from typing import List
from solution import Solution 

if __name__ == "__main__":
    if len(sys.argv) > 1:
        problemId = sys.argv[1]
        problemInputs = []

        for arg in sys.argv[2:]:
            print(arg)

        # for arg in sys.argv[2:]:
        #     problemInputs.append(arg)
        
        # print(problemInputs[0], problemInputs[1])
        
        # if problemId == "6a3d4b26ffa8b4df48922285":
        #     sol = Solution().searchRange(List[int](problemInputs[0]), int(problemInputs[1]))
            
        #     print(sol)
    else:
        print("No arguments provided.")