/* 
 
YOU ARE NOT ALLOWED TO MODIFY ANY FUNCTION DEFINITION'S PROVIDED.
WRITE YOUR CODE IN THE RESPECTIVE QUESTION FUNCTION BLOCK


*/

using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks.Dataflow;

namespace ISM6225_Fall_2023_Assignment_2
{
    class Program
    {
        static void Main(string[] args)
        {
            //Question 1:
            Console.WriteLine("Question 1:");
            int[] nums1 = { 0, 1, 3, 50, 75 };
            int upper = 99, lower = 0;
            IList<IList<int>> missingRanges = FindMissingRanges(nums1, lower, upper);
            string result = ConvertIListToNestedList(missingRanges);
            Console.WriteLine(result);
            Console.WriteLine();
            Console.WriteLine();

            //Question2:
            Console.WriteLine("Question 2");
            string parenthesis = "()[]{}";
            bool isValidParentheses = IsValid(parenthesis);
            Console.WriteLine(isValidParentheses);
            Console.WriteLine();
            Console.WriteLine();

            //Question 3:
            Console.WriteLine("Question 3");
            int[] prices_array = { 7, 1, 5, 3, 6, 4 };
            int max_profit = MaxProfit(prices_array);
            Console.WriteLine(max_profit);
            Console.WriteLine();
            Console.WriteLine();

            //Question 4:
            Console.WriteLine("Question 4");
            string s1 = "69";
            bool IsStrobogrammaticNumber = IsStrobogrammatic(s1);
            Console.WriteLine(IsStrobogrammaticNumber);
            Console.WriteLine();
            Console.WriteLine();

            //Question 5:
            Console.WriteLine("Question 5");
            int[] numbers = { 1, 2, 3, 1, 1, 3 };
            int noOfPairs = NumIdenticalPairs(numbers);
            Console.WriteLine(noOfPairs);
            Console.WriteLine();
            Console.WriteLine();

            //Question 6:
            Console.WriteLine("Question 6");
            int[] maximum_numbers = { 3, 2, 1 };
            int third_maximum_number = ThirdMax(maximum_numbers);
            Console.WriteLine(third_maximum_number);
            Console.WriteLine();
            Console.WriteLine();

            //Question 7:
            Console.WriteLine("Question 7:");
            string currentState = "++++";
            IList<string> combinations = GeneratePossibleNextMoves(currentState);
            string combinationsString = ConvertIListToArray(combinations);
            Console.WriteLine(combinationsString);
            Console.WriteLine();
            Console.WriteLine();

            //Question 8:
            Console.WriteLine("Question 8:");
            string longString = "leetcodeisacommunityforcoders";
            string longStringAfterVowels = RemoveVowels(longString);
            Console.WriteLine(longStringAfterVowels);
            Console.WriteLine();
            Console.WriteLine();
        }

        /*
        
        Question 1:
        You are given an inclusive range [lower, upper] and a sorted unique integer array nums, where all elements are within the inclusive range. A number x is considered missing if x is in the range [lower, upper] and x is not in nums. Return the shortest sorted list of ranges that exactly covers all the missing numbers. That is, no element of nums is included in any of the ranges, and each missing number is covered by one of the ranges.
        Example 1:
        Input: nums = [0,1,3,50,75], lower = 0, upper = 99
        Output: [[2,2],[4,49],[51,74],[76,99]]  
        Explanation: The ranges are:
        [2,2]
        [4,49]
        [51,74]
        [76,99]
        Example 2:
        Input: nums = [-1], lower = -1, upper = -1
        Output: []
        Explanation: There are no missing ranges since there are no missing numbers.

        Constraints:
        -109 <= lower <= upper <= 109
        0 <= nums.length <= 100
        lower <= nums[i] <= upper
        All the values of nums are unique.

        Time complexity: O(n), space complexity:O(1)
        */

        public static IList<IList<int>> FindMissingRanges(int[] nums, int lower, int upper)
        {
            try
            {
                // if nums is an empty array, return an empty list
                if (nums.Length == 0)
                {
                    return new List<IList<int>>();
                }

                // initialize a list to hold the ranges
                List<IList<int>> ranges = new List<IList<int>>();

                // use temporary variables to hold values of the lower bound, 
                // j holds a counter which prevents th loop from iterating forever. 
                // N is the length of the array, we wont loop more than how many elements are in the array
                int i = lower, j = 0, N = nums.Length;


                // start while loop, continue until the counter reaches the length of the array
                while (j < N)
                {
                    // if the number (being counted up from the lower bound) equals the jth element in
                    // the array, add one to the counter and to i
                    if (i == nums[j])
                    {
                        i++;
                        j++;
                    }

                    // otherwise, there is a gap in the list. document where gap started and the next element in the list
                    // less one will be the end of the gap. 
                    else
                    {
                        // make a new variable to hold the value of the first element in the gap (i)
                        int first = i;
                        // variable to hold last element (nums[j] - 1)
                        int last = nums[j] - 1;

                        // Add first and last to the list
                        ranges.Add(new List<int> { first, last });
                        
                        // update i to be the next array element value + 1
                        i = nums[j++] + 1;
                    }
                }

                // use if loop to check that the value of the current number is less than or equal to the upper bound
                // if it is less than the upper bound, there is a gap
                if (i <= upper)
                {
                    // if it is equal to the upper bound, add it to the ranges list
                    if (i == upper)
                    {
                        ranges.Add(new List<int> { i });
                    }
                    //otherwise there is a gap between the value and upper bound, record both values in the list
                    else
                    {
                        ranges.Add(new List<int> { i, upper });
                    }
                }

                // return the ranges list
                return ranges;
            }
            catch (Exception)
            {
                throw;
            }

        }

        /*
         
        Question 2

        Given a string s containing just the characters '(', ')', '{', '}', '[' and ']', determine if the input string is valid.An input string is valid if:
        Open brackets must be closed by the same type of brackets.
        Open brackets must be closed in the correct order.
        Every close bracket has a corresponding open bracket of the same type.
 
        Example 1:

        Input: s = "()"
        Output: true
        Example 2:

        Input: s = "()[]{}"
        Output: true
        Example 3:

        Input: s = "(]"
        Output: false

        Constraints:

        1 <= s.length <= 104
        s consists of parentheses only '()[]{}'.

        Time complexity:O(n^2), space complexity:O(1)
        */

        public static bool IsValid(string s)
        {
            try
            {
                // Code with O(n^2) complexity;
                // if the string input is empty or if there is an odd number of parentheses, return false
                if (s.Length == 0 || s.Length % 2 != 0)
                {
                    return false;
                }

                // loop through string. while it contains consecutive parentheses, remove them
                while (s.Contains("()") || s.Contains("[]") || s.Contains("{}"))
                {
                    s = s.Replace("()", "").Replace("[]", "").Replace("{}", "");
                }

                // once there are no more consecutive matching pairs, return the value. 
                // if the string had all matching pairs, the length will be zero and return true
                // otherwise, it will return false
                return s.Length == 0;

                /* CODE BELOW HAS O(n) COMPLEXITY
                 * I had already written it before realizing the instructions asked for O(n^2)
                // if the string input is empty or if there is an odd number of parentheses, return false
                if (s.Length == 0 || s.Length % 2 != 0)
                {
                    return false;
                }

                //create a dictionary to hold value pairs
                Dictionary<char, char> parDict = new Dictionary<char, char>
                {
                    { ')', '(' },
                    { '}', '{' },
                    { ']', '[' }
                };
                
                //create a stack to hold brackets
                Stack<char> charStack = new Stack<char>();

                //loop through string contents
                foreach (char c in s)
                {
                    // if the element is an opening parentheses, add it to the stack
                    if (parDict.ContainsValue(c))
                    {
                        charStack.Push(c);
                    }

                    // if the element is a closing parentheses, check if the stack is empty or if the 
                    // corresponding value opening bracket is on the top.
                    else if (parDict.ContainsKey(c))
                    {
                        // if the stack is empty or the matching value is not on top, return false
                        // if the opening bracket is not already on the stack, it wont be present
                        if (charStack.Count == 0 || charStack.Peek() != parDict[c])
                        {
                            return false;
                        }
                        // if stack is not empty and the top element is the matching key-value pair of the 
                        // closed bracket element c, pop the opening bracket from the stack
                        else
                        {
                            charStack.Pop();
                        }
                    }
                }
                // if the stack is not empty, not all matching pairs were found, return false. 
                // if it is empty, all matches were found, return true
                return (charStack.Count == 0);
                */

            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 3:
        You are given an array prices where prices[i] is the price of a given stock on the ith day.You want to maximize your profit by choosing a single day to buy one stock and choosing a different day in the future to sell that stock.Return the maximum profit you can achieve from this transaction. If you cannot achieve any profit, return 0.
        Example 1:
        Input: prices = [7,1,5,3,6,4]
        Output: 5
        Explanation: Buy on day 2 (price = 1) and sell on day 5 (price = 6), profit = 6-1 = 5.
        Note that buying on day 2 and selling on day 1 is not allowed because you must buy before you sell.

        Example 2:
        Input: prices = [7,6,4,3,1]
        Output: 0
        Explanation: In this case, no transactions are done and the max profit = 0.
 
        Constraints:
        1 <= prices.length <= 105
        0 <= prices[i] <= 104

        Time complexity: O(n), space complexity:O(1)
        */

        public static int MaxProfit(int[] prices)
        {
            try
            {
                // if there is just one element in the prices array, the profit is 0.
                if (prices.Length == 1)
                {
                    return 0;
                }

                // make two variables to hold the min value and the profit
                // assume the min price is on the first day and we have 0 profit
                int min = prices[0];
                int profit = 0;

                // loop through elements in prices array
                foreach (int day in prices)
                {
                    // if the value of profit is less than the current day's price minus the lowest price so far, 
                    // update profit with the new highest profit
                    profit = Math.Max(profit, day-min);

                    //if the current price of the day is less than the lowest price so far, update the min with the 
                    // new lowest price
                    min = Math.Min(min, day);
                }

                // return the profit 
                return profit;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        
        Question 4:
        Given a string num which represents an integer, return true if num is a strobogrammatic number.A strobogrammatic number is a number that looks the same when rotated 180 degrees (looked at upside down).
        Example 1:

        Input: num = "69"
        Output: true
        Example 2:

        Input: num = "88"
        Output: true
        Example 3:

        Input: num = "962"
        Output: false

        Constraints:
        1 <= num.length <= 50
        num consists of only digits.
        num does not contain any leading zeros except for zero itself.

        Time complexity:O(n), space complexity:O(1)
        */

        public static bool IsStrobogrammatic(string s)
        {
            try
            {
                // if the string is empty, return false
                if (s.Length == 0)
                {
                    return false;
                }

                //create a dictionary to hold strobogrammatic pairs
                Dictionary<char, char> strobogrammaticPairs = new Dictionary<char, char>
                {
                    {'0', '0'},
                    {'1', '1'},
                    {'8', '8'},
                    {'6', '9'},
                    {'9', '6'}
                };

                // use temporary variables to hold counts while iterating
                // i starts at index 0 and j starts at the end of the string
                int i = 0, j = s.Length - 1;

                // perform the loop until the counters meet at the middle of the string
                while (i <= j)
                {
                    // if i's element is not a key in the dictionary or if it is an element and it's corresponding
                    // value doesnt match the jth element, return false
                    if (!strobogrammaticPairs.ContainsKey(s[i]) || strobogrammaticPairs[s[i]] != s[j])
                    {
                        return false;
                    }

                    // if they match, iterate one space in from the right and one place in from the left
                    i++;
                    j--;
                }

                // if the loop completes without a false value, the number is strobogrammatic. return true
                return true;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 5:
        Given an array of integers nums, return the number of good pairs.A pair (i, j) is called good if nums[i] == nums[j] and i < j. 

        Example 1:

        Input: nums = [1,2,3,1,1,3]
        Output: 4
        Explanation: There are 4 good pairs (0,3), (0,4), (3,4), (2,5) 0-indexed.
        Example 2:

        Input: nums = [1,1,1,1]
        Output: 6
        Explanation: Each pair in the array are good.
        Example 3:

        Input: nums = [1,2,3]
        Output: 0

        Constraints:

        1 <= nums.length <= 100
        1 <= nums[i] <= 100

        Time complexity:O(n), space complexity:O(n)

        */

        public static int NumIdenticalPairs(int[] nums)
        {
            try
            {
                // if the array is empty, return 0. There are no pairs
                if (nums.Length == 0)
                {
                    return 0;
                }

                // make a dictionary to hold unique values and their counts
                Dictionary<int, int> countDict = new Dictionary<int, int>();

                // use pairs variable to hold the total number of number pairs
                int pairs = 0;

                // loop through values in the array
                foreach (int i in nums)
                {
                    // if the dictionary contains the current value, add the total instances to the pairs variable
                    // and one to key-value of the number
                    if (countDict.ContainsKey(i))
                    {
                        pairs += countDict[i];
                        countDict[i]++;
                    }

                    // if the number is not yet in the dictionary, add it and set the matching value to 1
                    else
                    {
                        countDict[i] = 1;
                    }
                }

                //return the number of pairs 
                return pairs;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        Question 6

        Given an integer array nums, return the third distinct maximum number in this array. If the third maximum does not exist, return the maximum number.

        Example 1:

        Input: nums = [3,2,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2.
        The third distinct maximum is 1.
        Example 2:

        Input: nums = [1,2]
        Output: 2
        Explanation:
        The first distinct maximum is 2.
        The second distinct maximum is 1.
        The third distinct maximum does not exist, so the maximum (2) is returned instead.
        Example 3:

        Input: nums = [2,2,3,1]
        Output: 1
        Explanation:
        The first distinct maximum is 3.
        The second distinct maximum is 2 (both 2's are counted together since they have the same value).
        The third distinct maximum is 1.
        Constraints:

        1 <= nums.length <= 104
        -231 <= nums[i] <= 231 - 1

        Time complexity:O(nlogn), space complexity:O(n)
        */

        public static int ThirdMax(int[] nums)
        {
            try
            {
                // if there are no number in the array, the max is 0.
                if(nums.Length == 0)
                {
                    return 0;
                }

                // sort the array so the larget values are in order
                // performing this sort makes the algoritm O(n log n) time complex
                Array.Sort(nums);

                // if the array is less than 3 elements long, return the last element (the max of the array)
                if (nums.Length < 3)
                {
                    return nums[nums.Length - 1];
                }

                // if it is longer than 3 elements, return the number that is 3rd from the last position
                return nums[nums.Length - 3];
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*
        
        Question 7:

        You are playing a Flip Game with your friend. You are given a string currentState that contains only '+' and '-'. You and your friend take turns to flip two consecutive "++" into "--". The game ends when a person can no longer make a move, and therefore the other person will be the winner.Return all possible states of the string currentState after one valid move. You may return the answer in any order. If there is no valid move, return an empty list [].
        Example 1:
        Input: currentState = "++++"
        Output: ["--++","+--+","++--"]
        Example 2:

        Input: currentState = "+"
        Output: []
 
        Constraints:
        1 <= currentState.length <= 500
        currentState[i] is either '+' or '-'.

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static IList<string> GeneratePossibleNextMoves(string currentState)
        {
            try
            {
                // if the current state sting has less than 2 elements, return an empty list
                if (currentState.Length < 2)
                {
                    return new List<string>();
                }

                // initialize a new list
                List<string> result = new List<string>();

                // loop through elements in currentState string
                for (int i = 1; i < currentState.Length; i++)
                {
                    // if the current element is not a plus or if the previous element is not a plus, 
                    // The move is not valid, the loop cycles to the next iteration
                    if (currentState[i] != '+' || currentState[i - 1] != '+')
                        continue;

                    // if the moves are valid, modify the list with the move
                    // create a new string builder with the contents of the currentState string
                    // can modify contents of string builder, not a regular string
                    StringBuilder nextMove = new StringBuilder(currentState);

                    // change the ith element to a - and the ith - 1 element to a -
                    nextMove[i] = '-';
                    nextMove[i - 1] = '-';

                    // add the modified string to the result list
                    result.Add(nextMove.ToString());
                }

                // once all the chars in the string have been looped through, print the result.
                return result;
            }
            catch (Exception)
            {
                throw;
            }
        }

        /*

        Question 8:

        Given a string s, remove the vowels 'a', 'e', 'i', 'o', and 'u' from it, and return the new string.
        Example 1:

        Input: s = "leetcodeisacommunityforcoders"
        Output: "ltcdscmmntyfrcdrs"

        Example 2:

        Input: s = "aeiou"
        Output: ""

        Timecomplexity:O(n), Space complexity:O(n)
        */

        public static string RemoveVowels(string s)
        {
            //if the input string is empty, return an empty string
            if (s.Length == 0)
            {
                return "";
            }
            // use a stringbuilder to create a modifiable string
            StringBuilder noVowels = new StringBuilder();

            // loop through characters in input string
            foreach (char c in s)
            {
                // if the character is not a vowel, add it to the new string noVowels
                // if it is a vowel, don't add it
                if (c != 'a' && c != 'e' && c != 'i' && c != 'o' && c != 'u')
                {
                    noVowels.Append(c);
                }
            }

            // convert the stringbuilder to a string and return it from the method
            return noVowels.ToString();
        }

        /* Inbuilt Functions - Don't Change the below functions */
        static string ConvertIListToNestedList(IList<IList<int>> input)
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("["); // Add the opening square bracket for the outer list

            for (int i = 0; i < input.Count; i++)
            {
                IList<int> innerList = input[i];
                sb.Append("[" + string.Join(",", innerList) + "]");

                // Add a comma unless it's the last inner list
                if (i < input.Count - 1)
                {
                    sb.Append(",");
                }
            }

            sb.Append("]"); // Add the closing square bracket for the outer list

            return sb.ToString();
        }


        static string ConvertIListToArray(IList<string> input)
        {
            // Create an array to hold the strings in input
            string[] strArray = new string[input.Count];

            for (int i = 0; i < input.Count; i++)
            {
                strArray[i] = "\"" + input[i] + "\""; // Enclose each string in double quotes
            }

            // Join the strings in strArray with commas and enclose them in square brackets
            string result = "[" + string.Join(",", strArray) + "]";

            return result;
        }
    }
}
