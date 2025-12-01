using System;

namespace Lexer
{
	public class Token 
	{
		//Changed to add TOKERR (Token Error)
		public enum Type { KEYW, SEP, OP, ID, NUM, EOF, TOKERR};
		public Type type;
		private string lexeme;

		private int line;
		private int column;

		

		public Token(Type type, string lexeme, int line, int column)
		{
			this.type = type;
			this.lexeme = lexeme;
			this.line = line;
			this.column = column;
		}

		override public string ToString()
{
    	if (lexeme != null) { return string.Format("{0} ({1} {2} {3})", lexeme, type, line, column); }
    	return string.Format("({0} {1} {2})", type, line, column);
}

	}
}
