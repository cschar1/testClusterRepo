using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CalculatorForm
{
    public partial class calcForm : Form
    {
        private ExpressionParser parser;
        Queue<Token> tokens;

        private List<dummyObject> factoryDataList;

        public calcForm()
        {
            InitializeComponent();
           
            factoryDataList = new List<dummyObject>();
            //Populate list to simulate variables user will do calculations with
            factoryDataList.Add(new dummyObject(50, "R1"));
            factoryDataList.Add(new dummyObject(5.75, "R2"));
            factoryDataList.Add(new dummyObject(1000, "R3"));

            foreach (dummyObject d in factoryDataList)
            {
                listBox1.Items.Add(d);
            }



            parser = new ExpressionParser(factoryDataList);

        }

        private void EvaluateButton_Click(object sender, EventArgs e)
        {
            tokens = parser.postFixTokenize(inputTextBox.Text);
            postfixTextBox.Text = displayList(tokens);

            double tmp = parser.evaluatePostFix(tokens);
            if (tmp.Equals(Double.NaN))
            {
                evaluationTextBox.Text = "variable name not found";
            }
            else
            {
                evaluationTextBox.Text = tmp.ToString();
            }
            
            
        }

        private string displayList(Queue<Token> q)
        {
            string result = String.Empty;
            foreach (Token t in q){
                result += t.TokenValue + "  ";
            }
            return result;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            parser.addObject(textBox1.Text , (double) numericUpDown1.Value);
            listBox1.Items.Clear();
            foreach (dummyObject d in factoryDataList)
            {
                listBox1.Items.Add(d);
            }
        }


#region ButtonInput

        private void addButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "+";
        }

        private void subtractButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "-";
        }

        private void multiplyButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "*";
        }

        private void divideButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "/";
        }

        private void Num1Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "1";
        }

        private void Num2Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "2";
        }

        private void Num3Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "3";
        }

        private void Num4Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "4";
        }

        private void Num5Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "5";
        }

        private void Num6Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "6";
        }

        private void Num7Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "7";
        }

        private void Num8Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "8";
        }

        private void Num9Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "9";
        }

        private void Num0Button_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "0";
        }

        private void decimalButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += ".";
        }

        private void ExponentButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "^";
        }


        private void RightBracketButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += "(";
        }

        private void LeftBracketButton_Click(object sender, EventArgs e)
        {
            inputTextBox.Text += ")";
        }

#endregion

    }
}
