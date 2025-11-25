using System;

namespace Lexer
{
	public class Lexer
	{

		private string input;
    	private int position;
    	private int line; 
		private int column;
		
		public Lexer(string text)
		{

		input = text;           // Store the input code
    	position = 0;           // Start at beginning
    	line = 1;               // Start at line 1
    	column = 1;             // Start at column 1
			
			throw new NotImplementedException();
		}

		public Token Next()
		{
			throw new NotImplementedException();
		}

		public Token Peek()
		{
			throw new NotImplementedException();
		}

	}
}
