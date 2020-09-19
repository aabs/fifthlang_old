using fifth.Parser.AST;
using fifth.Parser.AST.Builders;


using System;



public class Parser {
	public const int _EOF = 0;
	public const int _ident = 1;
	public const int _string = 2;
	public const int _int = 3;
	public const int _float = 4;
	public const int _use = 5;
	public const int _semicolon = 6;
	public const int _comma = 7;
	public const int _lbracket = 8;
	public const int _rbracket = 9;
	public const int _lambda = 10;
	public const int _plus = 11;
	public const int _minus = 12;
	public const int _times = 13;
	public const int _divide = 14;
	public const int _equ = 15;
	public const int _lt = 16;
	public const int _gt = 17;
	public const int _le = 18;
	public const int _ge = 19;
	public const int maxT = 20;

	const bool _T = true;
	const bool _x = false;
	const int minErrDist = 2;
	
	public Scanner scanner;
	public Errors  errors;

	public Token t;    // last recognized token
	public Token la;   // lookahead token
	int errDist = minErrDist;

public ProgramBuilder			  programBuilder;
	FunctionBuilder					  funcBuilder;
	ListBuilder<ParameterDeclaration> parameterDeclarationListBuilder;
	ListBuilder<Expr>				  expressionListBuilder;
	ParameterDeclarationBuilder		  parameterDeclarationBuilder;
	FunctionInvocationBuilder		  functionInvocationBuilder;
	ExprBuilder						  exprBuilder;
	SimExprBuilder					  simExprBuilder;
	TermBuilder						  termBuilder;
	bool FollowedByBracket() 
	{   
	Token x = scanner.Peek();  
	return x.kind == _lbracket; 
	}

/*--------------------------------------------------------------------------*/


	public Parser(Scanner scanner) {
		this.scanner = scanner;
		errors = new Errors();
	}

	void SynErr (int n) {
		if (errDist >= minErrDist) errors.SynErr(la.line, la.col, n);
		errDist = 0;
	}

	public void SemErr (string msg) {
		if (errDist >= minErrDist) errors.SemErr(t.line, t.col, msg);
		errDist = 0;
	}
	
	void Get () {
		for (;;) {
			t = la;
			la = scanner.Scan();
			if (la.kind <= maxT) { ++errDist; break; }

			la = t;
		}
	}
	
	void Expect (int n) {
		if (la.kind==n) Get(); else { SynErr(n); }
	}
	
	bool StartOf (int s) {
		return set[s, la.kind];
	}
	
	void ExpectWeak (int n, int follow) {
		if (la.kind == n) Get();
		else {
			SynErr(n);
			while (!StartOf(follow)) Get();
		}
	}


	bool WeakSeparator(int n, int syFol, int repFol) {
		int kind = la.kind;
		if (kind == n) {Get(); return true;}
		else if (StartOf(repFol)) {return false;}
		else {
			SynErr(n);
			while (!(set[syFol, kind] || set[repFol, kind] || set[0, kind])) {
				Get();
				kind = la.kind;
			}
			return StartOf(syFol);
		}
	}

	
	void AddOp(out AddOperator op) {
		op = AddOperator.Plus;	
		if (la.kind == 11) {
			Get();
			
		} else if (la.kind == 12) {
			Get();
			op = AddOperator.Minus;	
		} else SynErr(21);
	}

	void MulOp(out MulOperator op) {
		op = MulOperator.Multiply;	
		if (la.kind == 13) {
			Get();
			
		} else if (la.kind == 14) {
			Get();
			op = MulOperator.Divide;	
		} else SynErr(22);
	}

	void RelOp(out RelationalOperator op) {
		op = RelationalOperator.Equal;	
		if (la.kind == 15) {
			Get();
			
		} else if (la.kind == 16) {
			Get();
			op = RelationalOperator.LessThan;	
		} else if (la.kind == 17) {
			Get();
			op = RelationalOperator.GreaterThan;	
		} else if (la.kind == 18) {
			Get();
			op = RelationalOperator.LessThanOrEqual;	
		} else if (la.kind == 19) {
			Get();
			op = RelationalOperator.GreaterThanOrEqual;	
		} else SynErr(23);
	}

	void Expr(out Expr expr) {
		SimExpr left, right; 
		RelationalOperator relop;
		expr = null;
		
		SimExpr(out left);
		if (StartOf(1)) {
			RelOp(out relop);
			SimExpr(out right);
		}
	}

	void SimExpr(out SimExpr expr) {
		Term left, right; 
		AddOperator addop;
		expr = null;
		
		Term(out left);
		while (la.kind == 11 || la.kind == 12) {
			AddOp(out addop);
			Term(out right);
		}
	}

	void Term(out Term term) {
		Factor left, right; 
		MulOperator mulop;
		term = null;
		
		Factor(out left);
		while (la.kind == 13 || la.kind == 14) {
			MulOp(out mulop);
			Factor(out right);
		}
	}

	void Factor(out Factor factor) {
		string stringValue; 
		int intValue; 
		float floatValue; 
		FunctionInvocationExpression fiexp;
		factor = null;
		Expr e;
		
		if (la.kind == 8) {
			Get();
			Expr(out e);
			
			Expect(9);
		} else if (FollowedByBracket()) {
			FunctionInvocation();
			factor = functionInvocationBuilder.Build(); 
		} else if (la.kind == 3) {
			Int(out intValue);
			factor = new LiteralExpression<int>(intValue); 
		} else if (la.kind == 4) {
			Float(out floatValue);
			factor = new LiteralExpression<float>(floatValue); 
		} else if (la.kind == 2) {
			String(out stringValue);
			factor = new LiteralExpression<string>(stringValue); 
		} else SynErr(24);
	}

	void Ident(out string name) {
		Expect(1);
		name = t.val; 
	}

	void Int(out int value) {
		Expect(3);
		value = Int32.Parse(t.val); 
	}

	void String(out string value) {
		Expect(2);
		value = t.val; 
	}

	void Float(out float value) {
		Expect(4);
		value = Single.Parse(t.val); 
	}

	void FunctionInvocation() {
		string funcName;
		functionInvocationBuilder = FunctionInvocationBuilder.Start();		
		Ident(out funcName);
		functionInvocationBuilder.WithName(funcName); 
		ArgumentList();
		
	}

	void ArgumentList() {
		
		Expect(8);
		Arguments();
		
		Expect(9);
	}

	void Arguments() {
		Expr expr; 
		Expr(out expr);
		functionInvocationBuilder.WithArgument(new Argument("", expr)); 
		while (la.kind == 7) {
			Get();
			Arguments();
		}
	}

	void ExpressionList() {
		Expr expr;
		expressionListBuilder = ExpressionListBuilder.Start(); 
		
		Expr(out expr);
		expressionListBuilder.WithItem(expr); 
		while (la.kind == 7) {
			Get();
			ExpressionList();
		}
	}

	void ModuleName() {
		string modName; 
		Ident(out modName);
		
	}

	void TypeImport() {
		Expect(5);
		ModuleName();
		Expect(6);
	}

	void TypeImports() {
		TypeImport();
		while (la.kind == 5) {
			TypeImport();
		}
	}

	void ParameterDeclaration() {
		string typeName, paramName; 
		parameterDeclarationBuilder = ParameterDeclarationBuilder.Start(); 
		
		Ident(out typeName);
		parameterDeclarationBuilder.WithTypeName(typeName); 
		Ident(out paramName);
		parameterDeclarationBuilder.WithName(paramName); 
	}

	void ParameterDeclarations() {
		ParameterDeclaration();
		parameterDeclarationListBuilder.WithItem(parameterDeclarationBuilder.Build()); 
		while (la.kind == 7) {
			Get();
			ParameterDeclarations();
		}
	}

	void ParameterDeclarationList() {
		parameterDeclarationListBuilder = ParameterDeclarationListBuilder.Start(); 
		Expect(8);
		if (la.kind == 1) {
			ParameterDeclarations();
		}
		Expect(9);
	}

	void FunctionName() {
		string funcName; 
		Ident(out funcName);
		funcBuilder.WithName(funcName);
	}

	void FunctionDefinition() {
		funcBuilder = FunctionBuilder.Start(); 
		FunctionName();
		ParameterDeclarationList();
		funcBuilder.WithParameters( parameterDeclarationListBuilder.Build() );
		Expect(10);
		ExpressionList();
		Expect(6);
	}

	void FunctionDefinitions() {
		FunctionDefinition();
		programBuilder.WithFunction(funcBuilder.Build()); 
		while (la.kind == 1) {
			FunctionDefinitions();
		}
	}

	void Fifth() {
		programBuilder = ProgramBuilder.Start(); 
		TypeImports();
		FunctionDefinitions();
	}



	public void Parse() {
		la = new Token();
		la.val = "";		
		Get();
		Fifth();
		Expect(0);

	}
	
	static readonly bool[,] set = {
		{_T,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x},
		{_x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_x, _x,_x,_x,_T, _T,_T,_T,_T, _x,_x}

	};
} // end Parser


public class Errors {
	public int count = 0;                                    // number of errors detected
	public System.IO.TextWriter errorStream = Console.Out;   // error messages go to this stream
	public string errMsgFormat = "-- line {0} col {1}: {2}"; // 0=line, 1=column, 2=text

	public virtual void SynErr (int line, int col, int n) {
		string s;
		switch (n) {
			case 0: s = "EOF expected"; break;
			case 1: s = "ident expected"; break;
			case 2: s = "string expected"; break;
			case 3: s = "int expected"; break;
			case 4: s = "float expected"; break;
			case 5: s = "use expected"; break;
			case 6: s = "semicolon expected"; break;
			case 7: s = "comma expected"; break;
			case 8: s = "lbracket expected"; break;
			case 9: s = "rbracket expected"; break;
			case 10: s = "lambda expected"; break;
			case 11: s = "plus expected"; break;
			case 12: s = "minus expected"; break;
			case 13: s = "times expected"; break;
			case 14: s = "divide expected"; break;
			case 15: s = "equ expected"; break;
			case 16: s = "lt expected"; break;
			case 17: s = "gt expected"; break;
			case 18: s = "le expected"; break;
			case 19: s = "ge expected"; break;
			case 20: s = "??? expected"; break;
			case 21: s = "invalid AddOp"; break;
			case 22: s = "invalid MulOp"; break;
			case 23: s = "invalid RelOp"; break;
			case 24: s = "invalid Factor"; break;

			default: s = "error " + n; break;
		}
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}

	public virtual void SemErr (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
		count++;
	}
	
	public virtual void SemErr (string s) {
		errorStream.WriteLine(s);
		count++;
	}
	
	public virtual void Warning (int line, int col, string s) {
		errorStream.WriteLine(errMsgFormat, line, col, s);
	}
	
	public virtual void Warning(string s) {
		errorStream.WriteLine(s);
	}
} // Errors


public class FatalError: Exception {
	public FatalError(string m): base(m) {}
}

