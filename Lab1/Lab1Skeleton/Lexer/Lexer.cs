using System;

namespace Lexer
{
	public class Lexer
	{

		private string input;
    	private int position;
    	private int line; 
		private int column;

		private bool IsKeyword(string lexeme)
{
    	string[] keywords = { "if", "else", "while", "for", "return", "print", "skip", "do", "end" };
    	return Array.Exists(keywords, keyword => keyword == lexeme);
}
		
		public Lexer(string text)
		{

		input = text;           // Store the input code
    	position = 0;           // Start at beginning
    	line = 1;               // Start at line 1
    	column = 1;             // Start at column 1
			
			
		}

public Token Next()
{
    
    
    // Checking for end
    if (position >= input.Length)
    {
        return new Token(Token.Type.EOF, null, line, column);
    }

    char current = input[position];

    // Skip whitespace
    while (position < input.Length && char.IsWhiteSpace(input[position]))
    {
        if (input[position] == '\n' || input[position] == '\r')
        {
            line++;
            column = 1;
            
            // Handle \r\n sequence
            if (input[position] == '\r' && position + 1 < input.Length && input[position + 1] == '\n')
            {
                position += 2;
            }
            else
            {
                position++;
            }
        }
        else
        {
            column++;
            position++;
        }
    }

    // CHECK: After skipping whitespace, we might be at EOF
    if (position >= input.Length)
    {
        return new Token(Token.Type.EOF, null, line, column);
    }

    // ADD THESE TWO LINES HERE:
    int startLine = line;
    int startColumn = column;

    // Check for multi-character operators FIRST
    if (position + 1 < input.Length)
    {
        string twoChar = input.Substring(position, 2);
        
        if (twoChar == ":=" || twoChar == "!=" || twoChar == ">=" || twoChar == "<=" || twoChar == "&&" || twoChar == "||")
        {
            
            position += 2;
            column += 2;
            return new Token(Token.Type.OP, twoChar, startLine, startColumn);
        }
        
    }

    // CHECK: After multi-char check, we might be at EOF
    if (position >= input.Length)
    {
        return new Token(Token.Type.EOF, null, line, column);
    }

    current = input[position];  // Update current after multi-char check

    // Handle identifiers
    if (char.IsLetter(current) || current == '_')
    {
       
        int start = position;
        while (position < input.Length && 
               (char.IsLetterOrDigit(input[position]) || input[position] == '_'))
        {
            position++; column++;
        }
        string lexeme = input.Substring(start, position - start);
        
        
        
        bool isKeyword = IsKeyword(lexeme);
        Token.Type type = isKeyword ? Token.Type.KEYW : Token.Type.ID;
        return new Token(type, lexeme, startLine, startColumn);
    }

    // Handle numbers
    else if (char.IsDigit(current))
    {
        int start = position;
        while (position < input.Length && char.IsDigit(input[position]))
        {
            position++; column++;
        }
        string lexeme = input.Substring(start, position - start);
        return new Token(Token.Type.NUM, lexeme, startLine, startColumn);
    }

    // Handle separators
    else if ("(){}[],;".Contains(current))
    {
        position++;
        column++;
        return new Token(Token.Type.SEP, current.ToString(), startLine, startColumn);
    }

    // Handle single character operators
    else if ("+-*/=<>!&|".Contains(current))
    {
        position++;
        column++;
        return new Token(Token.Type.OP, current.ToString(), startLine, startColumn);
    }

    // Handle unknown characters as operators (or whatever makes sense)
    else
    {
        position++;
        column++;
        return new Token(Token.Type.OP, current.ToString(), startLine, startColumn);
    }
}

        public Token Peek()
		{
		 // Save current state
    	int savedPos = position;
    	int savedLine = line; 
    	int savedCol = column;
    
    	// Get next token
    	Token token = Next();
    
    	// Restore state
    	position = savedPos;
    	line = savedLine;
    	column = savedCol;
    
    	return token;

		}
	}
}