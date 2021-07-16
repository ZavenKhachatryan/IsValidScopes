using System;
using System.Collections.Generic;

namespace ConsoleApp1
{
	class Program
	{
		static void Main(string[] args)
		{
			Console.WriteLine(IsValid("(myArr{[1][2][3][4][5][6][7][8][9][0]})"));
		}

		static bool IsValid(string input)
		{
			char[] openScopesArr = { '{', '1', '(', '2', '[', '3' };
			char[] closeScopesArr = { '}', '1', ')', '2', ']', '3' };
			char[] charNumbersArr = { '1', '2', '3' };

			List<char> openScopesList = new List<char>(openScopesArr);
			List<char> closeScopesList = new List<char>(closeScopesArr);
			List<char> openedScopesList = new List<char>();
			List<char> closedScopesList = new List<char>();
			List<char> charNumbersList = new List<char>(charNumbersArr);
			bool isReverse = false;
			bool isNotItsClosedScope = true;
			bool isHaveClosedScope = false;
			int k = 1;

			for (int i = 0; i < input.Length; i++)
			{
				if (!openScopesList.Contains(input[i]) && !closeScopesList.Contains(input[i]) && charNumbersList.Contains(input[i]))
				{
					continue;
				}
				isNotItsClosedScope = true;
				isHaveClosedScope = false;

				for (int scopesListCounter = 0; scopesListCounter < 6; scopesListCounter += 2)
				{
					if (input[i] == openScopesList[scopesListCounter])
					{
						k = 1;
						while (i + k < input.Length)
						{
							if (!openScopesList.Contains(input[i + k]) && !closeScopesList.Contains(input[i + k]) && charNumbersList.Contains(input[i]))
							{
								k++;
							}
							break;
						}
						if (i + k < input.Length && openScopesList.Contains(input[i + k]))
						{
							isReverse = true;
						}
						for (int closesCounter = 0; closesCounter < 6; closesCounter += 2)
						{
							if (i + k < input.Length && input[i + k] == closeScopesList[closesCounter])
							{
								isHaveClosedScope = true;
								if (openScopesList[scopesListCounter + 1] == closeScopesList[closesCounter + 1])
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
							openedScopesList.Add(openScopesList[scopesListCounter + 1]);
					}

					if (isNotItsClosedScope && input[i] == closeScopesList[scopesListCounter])
					{
						closedScopesList.Add(closeScopesList[scopesListCounter + 1]);
					}
				}
			}

			if (openedScopesList.Count != closedScopesList.Count)
			{
				return false;
			}

			int scopesCount = openedScopesList.Count - 1;

			for (int i = scopesCount; i >= 0; i--)
			{
				if (openedScopesList[i] == closedScopesList[i])
				{
					openedScopesList.RemoveRange(i, 1);
					closedScopesList.RemoveRange(i, 1);
				}
			}

			if (openedScopesList.Count == 0)
			{
				return true;
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