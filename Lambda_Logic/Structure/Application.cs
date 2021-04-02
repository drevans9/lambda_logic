using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{

    public class Application : IExpression
    {
        public string typeOfTerm { get; set; }

        public IExpression val1;

        public IExpression val2;

        public List<State> states { get; set; }

        //Constructor
        public Application(IExpression e1, IExpression e2)
        {           
            this.val1 = e1;
            this.val2 = e2;
            this.typeOfTerm = "Application";
        }

        public IExpression DeepCopy()
        {
            Application other = (Application)this.MemberwiseClone();
            other.val1 = val1.DeepCopy();
            other.val2 = val2.DeepCopy();
            return other;
        }

        //Prints all expressions within this lambda application
        public void Print()
        {
            val1.Print();
            val2.Print();
        }

        //Prints the contents of a closure list 
        public void PrintList(List<Closure> list)
        {
            //If empty list, print []
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


        //Evaluates this lambda expression by recursively evaluating each sub expression
        public IExpression Evaluate(List<Closure> enviro, List<Closure> stack, List<State> states)
        {
            
            //Copies the environment, stack and expression and adds them to this term's state
            var enviroCopy = new List<Closure>();
            var stackCopy = new List<Closure>();
            var expressionCopy = this.DeepCopy();

            if (enviro.Count > 0)
            {
                enviroCopy = enviro.ConvertAll(c => new Closure { variable = c.variable, expression = c.expression.DeepCopy(), environment = c.environment });
            }
            if (stack.Count > 0)
            {
                stackCopy = stack.ConvertAll(c => new Closure { variable = c.variable, expression = c.expression.DeepCopy(), environment = c.environment });
            }

            var state = new State { expression = expressionCopy, environment = enviroCopy, stack = stackCopy };
            states.Add(state);           
            this.states = states;

            //Creates a clone of enviro so that it uses ByVal instead of ByRef
            var newEnviro = enviro.ConvertAll(c => new Closure { variable = c.variable, expression = c.expression, environment = c.environment });

            var closure = new Closure { expression = val2, environment = newEnviro };            
            stack.Add(closure);
            

            return val1.Evaluate(enviro, stack, states);
        }


    }

}
