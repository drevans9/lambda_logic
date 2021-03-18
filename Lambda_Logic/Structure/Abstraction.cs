using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lambda_Logic.Structure
{
    public class Abstraction : IExpression
    {
        public string typeOfTerm { get; set; }

        public char val1;

        public IExpression val2;
        public List<State> states { get; set; }

        //Constructor
        public Abstraction(char c, IExpression e)
        {
            this.val1 = c;
            this.val2 = e;
            this.typeOfTerm = "Abstraction";
        }

        //Prints all expressions within this lambda abstraction
        public void Print()
        {
            Console.Write("(\\" + val1 + ".");
            val2.Print();
            Console.Write(")");
        }

        //Prints the contents of a closure list 
        public void PrintList(List<Closure> list)
        {

            if (list.Count == 0)
            {
                Console.Write("[]");
                return;
            }

            foreach (var elem in list)
            {
                Console.Write("<");
                elem.expression.Print();
                Console.Write(",");
                PrintList(elem.environment);
                Console.Write(">");

                //If element is not last in list
                if (elem != list[list.Count - 1])
                {
                    Console.Write(", ");
                }

            }
        }

        //Evaluates this lambda expression by recursively evaluating each sub expression
        public IExpression Evaluate(List<Closure> enviro, List<Closure> stack, List<State> states)
        {
            //Copies the environment and stack and adds them to this term's state
            var enviroCopy = new List<Closure>();
            var stackCopy = new List<Closure>();
            if (enviro.Count > 0)
            {
                enviroCopy = enviro.ConvertAll(c => new Closure { variable = c.variable, expression = c.expression, environment = c.environment });
            }
            if (stack.Count > 0)
            {
                stackCopy = stack.ConvertAll(c => new Closure { variable = c.variable, expression = c.expression, environment = c.environment });
            }

            var state = new State { expression = this, environment = enviroCopy, stack = stackCopy };
            states.Add(state);
            this.states = states;


            
            if (enviro.Count != 0 || stack.Count != 0)
            {              

                if (stack.Count > 0)
                {
                    //Takes the top element of stack
                    var closure = stack[stack.Count - 1];
                    closure.variable = this.val1;

                    //Removes the top element of stack
                    stack.RemoveAt(stack.Count - 1);
                    enviro.Add(closure);
                }               

                return val2.Evaluate(enviro, stack, states);

            }
            else
            {
                return this;
            }
        }
    }
}
