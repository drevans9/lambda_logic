using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{

    public class Application : IExpression
    {
        public IExpression val1;

        public IExpression val2;

        public List<State> states { get; set; }

        public Application(IExpression e1, IExpression e2)
        {
            this.val1 = e1;
            this.val2 = e2;
        }

        public void Print()
        {
            val1.Print();
            val2.Print();
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
                if(elem != list[list.Count - 1])
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
                enviroCopy = enviro.ConvertAll(c => new Closure {expression = c.expression, environment = c.environment });
            }
            if (stack.Count > 0)
            {
                stackCopy = stack.ConvertAll(c => new Closure { expression = c.expression, environment = c.environment });
            }

            var state = new State { expression = this, environment = enviroCopy, stack = stackCopy };
            states.Add(state);
            this.states = states;


            //Creates a clone of enviro so that it uses ByVal instead of ByRef
            var newEnviro = enviro.ConvertAll(c => new Closure { expression = c.expression, environment = c.environment });

            var closure = new Closure { expression = val2, environment = newEnviro };            
            stack.Add(closure);
            

            return val1.Evaluate(enviro, stack, varTracker, states);
        }


    }

}
