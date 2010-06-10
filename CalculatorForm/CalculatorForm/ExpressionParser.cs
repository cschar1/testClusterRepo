using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace CalculatorForm
{
    public enum tokenType
    {
        ARG_NUM,
        ARG_VAR,
        OPEN,
        PRE,                //unimplemented , log(5*2) log 5  log5 log(5^2) log5^2
        POST,
        DI_OP,
        CLOSE,
        UNRECOGNIZED
    }


    class ExpressionParser
    {
        private List<dummyObject> variableList;
        

        public ExpressionParser(List<dummyObject> vList)
        {
            variableList = vList;
            
        }


        Token getNextToken(string input)
        {
            input = input.Trim();

            Token nextTok = new Token();

            //Create Regexes for each string value
            Regex ARG_NUM_Regex = new Regex(@"^[0-9]*\.?[0-9]+");

            //For now , variables can be any combination of letters and numbers
            Regex ARG_VAR_Regex = new Regex(@"^[a-zA-Z0-9]+");
             
            Regex DI_OP_Regex = new Regex(@"^[+|\-|\/|*]");
            Regex OPEN_Regex = new Regex(@"^\(");
            Regex CLOSE_Regex = new Regex(@"^\)");
            Regex POST_Regex = new Regex(@"^[\^]");

            //Case out matches 
            if (ARG_NUM_Regex.IsMatch(input))
            {
                nextTok.Type = tokenType.ARG_NUM;
                nextTok.TokenValue = ARG_NUM_Regex.Match(input).Value;
                nextTok.Precedence = 1;

            }else if(ARG_VAR_Regex.IsMatch(input)){

                nextTok.Type = tokenType.ARG_VAR;
                nextTok.TokenValue = ARG_VAR_Regex.Match(input).Value;
                nextTok.Precedence = 1;
            }
            else if (DI_OP_Regex.IsMatch(input))
            {
                
                nextTok.Type = tokenType.DI_OP;           
                nextTok.TokenValue = DI_OP_Regex.Match(input).Value;
                switch (nextTok.TokenValue)
                {
                    case "+":
                    case "-": nextTok.Precedence = 2; break;
                    case "*":
                    case "/": nextTok.Precedence = 3; break;
                }
            }
            else if (OPEN_Regex.IsMatch(input))
            {
                nextTok.Type = tokenType.OPEN;
                nextTok.Precedence = -2;
                nextTok.TokenValue = "(";

            }
            else if (CLOSE_Regex.IsMatch(input))
            {
                nextTok.Type = tokenType.CLOSE;
                nextTok.Precedence = -1;
                nextTok.TokenValue = ")";

            }else if (POST_Regex.IsMatch(input))
            {
                nextTok.Type = tokenType.POST;
                nextTok.TokenValue = POST_Regex.Match(input).Value;
                nextTok.Precedence = 4;

            }
            else{

                Console.WriteLine("Could not regex match next token,");
                nextTok.Type = tokenType.UNRECOGNIZED;
                
            }
              //set values and return token
            
            return nextTok;
        }

        private string consume(string input, Token t){
            return input.Substring(t.TokenValue.Length);
        }


        private bool verifyBracketPairs(string input)
        {
            int LBCount = 0;
            int RBCount = 0;

            for(int i = 0 ; i < input.Length ; i ++) 
            {
                //If we currently have more closing brackets than opening brackets
                //the brackets have been input incorrectly
                if( LBCount < RBCount)
                    return false;

                if (input[i] == '(')
                    LBCount++;
                else if (input[i] == ')')
                    RBCount++;      
            }//end for


            if( (LBCount - RBCount) == 0 )
                return true;
            else
                return false;
            
        }



        public Queue<Token> postFixTokenize(string input)
        {

            if (verifyBracketPairs(input) == false)
                throw new Exception("Mismatching Brackets ");

            //state that helps verify that the expression alternates between
            //args and operators ( + , - , * , / )
            int exprState = 0;

            //get rid of whitespace
            input = input.Trim();

            Token t = null;
            Queue<Token> outputQueue = new Queue<Token>();
            OperatorStack opStack = new OperatorStack();

            while (input.Length != 0 && 
                (t = getNextToken(input)).Type != tokenType.UNRECOGNIZED )
            {
                Console.WriteLine("Parsing " + input);
                
                switch (t.Type)
                {
                    case tokenType.ARG_NUM :
                        assertStateEquals(0, exprState);
                        exprState = 1;
                        outputQueue.Enqueue(t); 
                        break;

                    case tokenType.ARG_VAR :
                        assertStateEquals(0, exprState);
                        exprState = 1;
                        outputQueue.Enqueue(t);
                        break;

                    case tokenType.DI_OP :
                        assertStateEquals(1, exprState);
                        exprState = 0;
                        opStack.pushSpew(outputQueue, t);  
                        break;

                    case tokenType.OPEN:
                        opStack.push(t);
                        break;

                    case tokenType.CLOSE:
                        //spew out all the tokens inside the bracket space
                        opStack.pushSpew(outputQueue, t);
                        //remove the container pair of OC brackets
                        opStack.removeOCPair();
                        break;

                    case tokenType.POST:
                        assertStateEquals(1, exprState);
                        exprState = 0; 
                        opStack.pushSpew(outputQueue, t);
                        break;

                    default :
                        throw new Exception("TokenType not implemented or recognized ");
                }

                //chomp the token off the input string
        
                input = consume(input, t);
                input = input.Trim();

                #region degbugPrint

                Console.WriteLine(" Parsed " + t.TokenValue + " of type: " + t.Type + "   Remaining input string is ::" + input +"::");
            
                Console.Write("\n Current Queue is ");
                foreach (Token s in outputQueue)
                {
                    Console.Write(s.TokenValue + "|");
                }
               
                Console.WriteLine();
                Console.WriteLine("and Current stack is " + opStack.ToString());
                #endregion

            }//end while

            //if the next token is unrecognized, 
            //and there is still input, 
            if (input.Length != 0 && t != null && t.Type == tokenType.UNRECOGNIZED)
            {
                throw new Exception("Calculator can only take in Integers , Decimal numbers," +
                 " + , - , / , * , ^ and variable names in the corresponding list " );
            }
            else
            {
                //dump rest of operators on stack to output
                while (opStack.Count != 0)
                    outputQueue.Enqueue(opStack.pop());

                assertStateEquals(1, exprState);
                return outputQueue;
            }
        }

        private void assertStateEquals(int state, int stateToCompare)
        {
            if (state != stateToCompare)
            {
                throw new Exception("Invalid state of expression, make sure to " +
                    "alternate between args and diadic operators \n state should be : " + state + " \n state is : " + stateToCompare);
            }
        }


        public void addObject(string name, double val)
        {
            this.variableList.Add(new dummyObject(val, name));
        }


        private double extractVariableValue(string variableName)
        {
            foreach (dummyObject d in variableList)
            {
                if (d.name == variableName)
                {
                    return d.dummyValue;
                }
            }
            return Double.NaN;

        }

        public double evaluatePostFix(Queue<Token> inputQueue, int significantNumbers)
        {
            Stack<Double> Num_Stack = new Stack<Double>();
            double tmp1;
            double tmp2;
            double result;

            while (inputQueue.Count != 0)
            {
                Token t = inputQueue.Dequeue();
                
                switch (t.Type)
                {
                    
                    case tokenType.ARG_NUM:
                        result = Double.Parse(t.TokenValue);
                        break;

                    case tokenType.ARG_VAR:
                        Console.WriteLine(extractVariableValue(t.TokenValue));
                        if (this.extractVariableValue(t.TokenValue).Equals(Double.NaN))
                        {
                            Console.WriteLine("Variable not found in list");
                            return Double.NaN;
                        }
                        else
                        {
                            result = this.extractVariableValue(t.TokenValue);
                        }
                            break;

                    case tokenType.DI_OP:

                        tmp1 = Num_Stack.Pop();
                        tmp2 = Num_Stack.Pop();
                        switch (t.TokenValue)
                        {
                            //Diadic operators 
                            case "+": result = tmp2 + tmp1; break;
                            case "-": result = tmp2 - tmp1; break;
                            case "/": result = tmp2 / tmp1; break;
                            case "*": result = tmp2 * tmp1; break;
                            default: throw new Exception("OPs not matched");
                        }
                        //Num_Stack.Push(result);
                        break;

                    case tokenType.POST:
                        
                             //Exponents   3^5 
                                tmp1 = Num_Stack.Pop();
                                tmp2 = Num_Stack.Pop();
                                result = Math.Pow(tmp2,tmp1);
                                //Num_Stack.Push(result);
                                break;

                             //Factorials with decimals ????
                             // Optional to add..... 
                             // See Gamma Functions in Wikipedia

                    default:
                                throw new Exception("Token unrecognized in evaluator");  
                               
                }//end switch 
                Num_Stack.Push(result);
            }//end while

            if (Num_Stack.Count != 1)
            {
                Console.WriteLine("Method did not evaluate correctly");
                return -99999;
            }
            //After going through entire Queue, our Num_stack should have 1
            //Element on it, the final result.
            double element_to_return = Num_Stack.Pop();
            element_to_return = Math.Round(element_to_return, significantNumbers);
            return element_to_return;
       
            

        }//end evaluatePostFix


    }//end class
   
}
