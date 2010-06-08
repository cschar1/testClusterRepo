using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CalculatorForm
{
    //Class to hold information to represent parsed token
    class Token
    {
        private tokenType type;
        private int precedence;
        private string tokenValue;

        public string TokenValue
        {
            get { return tokenValue; }
            set { tokenValue = value; }
        }

        public int Precedence
        {
            get { return precedence; }
            set { precedence = value; }
        }

        public tokenType Type
        {
            get { return type; }
            set { type = value; }
        }

    }
}
