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

    }
}
