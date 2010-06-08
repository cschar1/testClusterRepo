using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorForm
{
    class OperatorStack
    {
        private Stack<Token> stack;

        public int Count { get {return  stack.Count ;} private set{Count= value;} }

        public OperatorStack()
        {
            stack = new Stack<Token>();
        }


        /// <summary>
        /// modified method that implements part of Shunting Yard
        /// Algorithm.
        /// 
        /// //Pseudo Code
        /// If token is an operator, o1 , then use pushSpew.
        /// 
        /// ===Spew part=====
        /// while there is an operator token, o2, at the top of stack and :
        ///        either o1 is left-associative (-,+,*,/) and its priority is less
        ///        than or equal to o2, 
        ///        or o1 is right-associative ( ^ ), and priority is less than o2
        ///        
        ///    pop o2 off stack onto output Queue
        /// end while
        /// 
        /// ====Push part====
        /// push o1 onto stack
        /// 
        /// </summary>
        /// <param name="output">The Queue that holds the tokens in
        /// post fix notation</param>
        /// <param name="tokenToPush">Incoming token to be compared with
        /// stack</param>
        public void pushSpew(Queue<Token> output, Token tokenToPush_o1)
        {

            //deals with right-associative
            Token stackToken_o2 ;
            if (tokenToPush_o1.Type == tokenType.POST)
            {
                while (stack.Count != 0 &&
                    ((stackToken_o2 = stack.Peek()).Precedence > tokenToPush_o1.Precedence))
                {
                    output.Enqueue(stack.Pop());
                }
            }
                //deals with left-associative
            else
            {
                while (stack.Count != 0 &&
                    ((stackToken_o2 = stack.Peek()).Precedence >= tokenToPush_o1.Precedence))
                {
                    output.Enqueue(stack.Pop());
                }
            }

            

            stack.Push(tokenToPush_o1);
        }


        //Pop close and open bracket off top of stack without adding
        //to output Queue of tokens
        //
        //Called after receiving a CLOSE token , which causes a pushSpew,
        // dumping everything inside the bracket space to the output.
        public void removeOCPair()
        {
            Token tmp = stack.Peek();
            if (tmp.Type != tokenType.CLOSE)
            {
                throw new Exception("Brackets not properly nested");
            }
            stack.Pop();
            tmp = stack.Peek();
            if (tmp.Type != tokenType.OPEN)
            {
                throw new Exception("Brackets not properly nested");
            }
            stack.Pop();

            return;
        }
            


        public void push(Token tokenToPush)
        {
            this.stack.Push(tokenToPush);
        }

        public Token pop()
        {
            return this.stack.Pop();
        }

        public override string ToString()
        {
            string result = String.Empty;

            foreach (Token s in stack)
            {
                result += s.TokenValue + "|";

            }
            
            return result;
        }


    }
}
