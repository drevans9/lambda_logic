using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;

namespace Lambda_Logic.Structure
{
    public class Abstraction : IExpression
    {
        public char val1;

        public IExpression val2;
        public List<State> states { get; set; }

        public Abstraction(char c, IExpression e)
        {
            this.val1 = c;
            this.val2 = e;
        }

        public void Print()
        {
            Console.Write("(\\" + val1 + ".");
            val2.Print();
            Console.Write(")");
        }

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

        public IExpression Evaluate(List<Closure> enviro, List<Closure> stack, Dictionary<char,int> varTracker, List<State> states)
        {
            var enviroCopy = new List<Closure>();
            var stackCopy = new List<Closure>();
            if (enviro.Count > 0)
            {
                enviroCopy = enviro.ConvertAll(c => new Closure { expression = c.expression, environment = c.environment });
            }
            if (stack.Count > 0)
            {
                stackCopy = stack.ConvertAll(c => new Closure { expression = c.expression, environment = c.environment });
            }

            var state = new State { expression = this, environment = enviroCopy, stack = stackCopy };
            states.Add(state);
            this.states = states;


            //This needs to be changed so that if it's the top level it continues to evaluate
            if (enviro.Count != 0 || stack.Count != 0)
            {

                //Increment all variables
                varTracker.Keys.ToList().ForEach(x => varTracker[x]++);

                //If the variable exists set to 0, otherwise add it
                if (varTracker.ContainsKey(val1))
                {
                    //Unsure about this
                    varTracker[val1] = 0;
                }
                else
                {
                    varTracker.Add(val1, 0);
                }

                if (stack.Count > 0)
                {
                    //Takes the top element of stack
                    var closure = stack[stack.Count - 1];

                    //Removes the top element of stack
                    stack.RemoveAt(stack.Count - 1);
                    enviro.Add(closure);

                }

               

                return val2.Evaluate(enviro, stack, varTracker, states);

            }
            else
            {
                return this;
            }
        }
    }
}
