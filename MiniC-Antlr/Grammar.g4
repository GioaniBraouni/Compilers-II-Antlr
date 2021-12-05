grammar Grammar;

compileUnit : (statement|functionDefinition)+ #ST_CompileUnit
			;

functionDefinition : FUNCTION IDENTIFIER LP fargs? RP compoundStatement		#ST_FunctionDefinition			
				   ;

statement : expression QM		#ST_Expression
		  | selectionStatement	#ST_selection
		  | iteretionStatement	#ST_Iteration
		  | compoundStatement	#ST_Compound
		  | RETURN expression QM #ST_Return
		  | BREAK QM			 #ST_Break
		  ;

selectionStatement : ifStatement     #ST_If
				   | switchStatement #ST_Switch
				   ;

ifStatement : IF LP expression RP statement (ELSEIF statementList)* (ELSE statementList)?  
			;

switchStatement: SWITCH LP expression RP LB caseOptions+ defaultOption? RB						 
			   ;

caseOptions : CASE expression COLON statementList 			#ST_CaseOptions	
					 ;

defaultOption : DEFAULT COLON statementList					#ST_DefaultOptions
				 ;

	
iteretionStatement : whileStatement		#ST_While
				   | doWhileStatement	#ST_DoWhile
				   | forStatement   	#ST_For	
				   ;

whileStatement : WHILE LP expression RP compoundStatement	
			   ;

doWhileStatement : DO compoundStatement WHILE LP expression RP
				 ;
forStatement : LP expression? QM expression? QM expression RP statement
			   ;

compoundStatement : LB statementList? RB
				  ;

statementList : (statement)+  #ST_List
			  ;

expression : NUMBER											#ExprNUMBER	
		   | IDENTIFIER										#ExprIDENTIFIER
		   | expression op=(DIV|MULT) expression 			#ExprDIVMULT   
		   | expression op=(PLUS|MINUS) expression		    #ExprPLUSMINUS
		   | PLUS PLUS expression							#ExprPLUSPLUS
		   | MINUS PLUS expression							#ExprMINUSPLUS
		   | LP expression RP								#ExprPARENTHESIS
		   | IDENTIFIER ASSIGN expression					#ExprASSIGN
		   | NOT expression									#ExprNOT
	       | expression AND expression						#ExprAND
		   | expression OR expression						#ExprOR
		   | expression GT expression						#ExprGT
		   | expression GTE expression						#ExprGTE
		   | expression LT expression						#ExprLT
		   | expression LTE expression						#ExprLTE
		   | expression EQUAL expression					#ExprEQUAL
		   | expression NEQUAL expression					#ExprNEQUAL
		   ;

args : (expression (COMMA)?)+
	 ;

fargs : (IDENTIFIER (COMMA)?)+
	  ;

/*
 * Lexer Rules
 */

// Reserved words
FUNCTION :'function';
RETURN :'return'; 
IF:'if';
ELSEIF : 'else if';
ELSE:'else';
SWITCH : 'switch';
CASE : 'case';
DEFAULT:'default';
WHILE:'while';
DO: 'do';
FOR:'for';
BREAK: 'break';

// Operators
PLUS:'+'; 
MINUS:'-';
DIV:'/'; 
MULT:'*';
OR:'||';
AND:'&&';
NOT:'!';
EQUAL:'==';
NEQUAL:'!='; 
GT:'>';
LT:'<';
GTE:'>=';
LTE:'<=';
QM:';';
LP:'(';
RP:')';
LB:'{';
RB:'}'; 
COMMA:',';
ASSIGN:'=';
COLON :':';


// Identifiers - Numbers
IDENTIFIER: [a-zA-Z_][a-zA-Z0-9_]*;
NUMBER: '0'|[1-9][0-9]*;

// Whitespace
WS: [ \r\n\t]-> skip;
