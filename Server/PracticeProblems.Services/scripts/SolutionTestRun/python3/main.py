#!/usr/bin/env python3
import sys
import json
from typing import *

# class Solution:
#     def threeSum(self, nums: List[int]) -> List[List[int]]:
#         nums.sort()
#         n = len(nums)
#         ans = []
#         for i in range(n - 2):
#             if nums[i] > 0:
#                 break
#             if i and nums[i] == nums[i - 1]:
#                 continue
#             j, k = i + 1, n - 1
#             while j < k:
#                 x = nums[i] + nums[j] + nums[k]
#                 if x < 0:
#                     j += 1
#                 elif x > 0:
#                     k -= 1
#                 else:
#                     ans.append([nums[i], nums[j], nums[k]])
#                     j, k = j + 1, k - 1
#                     while j < k and nums[j] == nums[j - 1]:
#                         j += 1
#                     while j < k and nums[k] == nums[k + 1]:
#                         k -= 1
#         return ans








# name of solution function is variable
# number of arguments for solution function is variable
# types of each argument is variable (primitives: string, int, char, [], {}... complex: linked list, stack, queue...)

class Solution:
    def addBinary(self, a: str, b: str) -> str:
        ans = []
        i, j, carry = len(a) - 1, len(b) - 1, 0
        while i >= 0 or j >= 0 or carry:
            carry += (0 if i < 0 else int(a[i])) + (0 if j < 0 else int(b[j]))
            carry, v = divmod(carry, 2)
            ans.append(str(v))
            i, j = i - 1, j - 1
        return "".join(ans[::-1])

if __name__ == "__main__":
    # read whole input
    input = sys.stdin.read()
    print(f"Input received: {input.strip()}")  # Debugging line to check
    
    # parse json to dict
    data = json.loads(input)

    # get typed data
    if data is not None:
        a = data.get("a")
        b = data.get("b")
        if a is not None and b is not None:
            sol = Solution().addBinary(a, b)
            print(json.dumps(sol))
        else:
            print("Invalid input.")

