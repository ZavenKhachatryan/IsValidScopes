using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(IsValid("{([({[]]}))}"));
		}

		static bool IsValid(string input)
		{
			char[] openScopesArr = { '{', '1', '(', '2', '[', '3' };
			char[] closeScopesArr = { '}', '1', ')', '2', ']', '3' };

			List<char> openScopesList = new List<char>(openScopesArr);
			List<char> openedScopesList = new List<char>();
			List<char> closedScopesList = new List<char>();
			bool isReverse = false;
			bool isNotItsClosedScope = true;
			bool isHaveClosedScope = false;

			for (int i = 0; i < input.Length; i++)
			{
				isNotItsClosedScope = true;
				isHaveClosedScope = false;
				for (int opensCounter = 0; opensCounter < 6; opensCounter += 2)
				{
					if (input[i] == openScopesList[opensCounter])
					{
						if (i + 1 < input.Length && openScopesList.Contains(input[i + 1]))
						{
							isReverse = true;
						}
						for (int closesCounter = 0; closesCounter < 6; closesCounter += 2)
						{
							if (i + 1 < input.Length && input[i + 1] == closeScopesArr[closesCounter])
							{
								isHaveClosedScope = true;
								if (openScopesList[opensCounter + 1] == closeScopesArr[closesCounter + 1])
								{
									i++;
									isNotItsClosedScope = false;
								}
							}
						}

						if (isNotItsClosedScope && isHaveClosedScope)
						{
							return false;
						}

						if (isNotItsClosedScope)
							openedScopesList.Add(openScopesList[opensCounter + 1]);
					}

					if (isNotItsClosedScope && input[i] == closeScopesArr[opensCounter])
					{
						closedScopesList.Add(closeScopesArr[opensCounter + 1]);
					}
				}
			}

			if (openedScopesList.Count != closedScopesList.Count)
			{
				return false;
			}

			for (int i = 0; i < openedScopesList.Count; i++)
			{
				if (openedScopesList[i] == closedScopesList[i])
				{
					openedScopesList.RemoveRange(i, 1);
					closedScopesList.RemoveRange(i, 1);
				}
			}

			if (isReverse)
			{
				closedScopesList.Reverse();
			}

			for (int i = 0; i < openedScopesList.Count; i++)
			{
				if (openedScopesList[i] != closedScopesList[i])
				{
					return false;
				}
			}

			return true;
		}
	}
}