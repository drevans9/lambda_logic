using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{   
    public class Variable : IExpression
    {
        public char val;
        public List<State> states { get; set; }


        public Variable(char c)
        {
            this.val = c;
        }

        public void Print()
        {
            Console.Write(val);
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

        public IExpression Evaluate(List<Closure> enviro, List<Closure> stack, Dictionary<char, int> varTracker, List<State> states)
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


            //Does not currently handle free variables. Must be bound


            if (enviro.Count != 0 || stack.Count != 0 )
            {
                if (varTracker[val] == 0)
                {
                    //Creates a copy of the top environment
                    var closure = enviro[enviro.Count - 1];
                    var element = closure.expression;
                    var newEnviro = closure.environment.ConvertAll(c => new Closure { expression = c.expression, environment = c.environment });                                    

                    return element.Evaluate(newEnviro, stack, varTracker, states);
                }
                else
                {
                    var closure = enviro[varTracker[val]];
                    var element = closure.expression;
                    var newEnviro = closure.environment.ConvertAll(c => new Closure { expression = c.expression, environment = c.environment });                    

                    return element.Evaluate(newEnviro, stack, varTracker, states);
                }
            }
            else
            {
                return this;
            }



        }
       
    }
}
