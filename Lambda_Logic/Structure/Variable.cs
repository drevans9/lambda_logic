using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{   
    public class Variable : IExpression
    {
        public string typeOfTerm { get; set; }

        public char val;
        public List<State> states { get; set; }

        //Constructor
        public Variable(char c)
        {
            this.val = c;
            this.typeOfTerm = "Variable";
        }
        public IExpression DeepCopy()
        {
            Variable other = (Variable)this.MemberwiseClone();
            other.val = val;
            return other;
        }

        //Prints all expressions within this lambda variable
        public void Print()
        {
            Console.Write(val);
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

            var state = new State { expression = this, environment = enviroCopy, stack = stackCopy };
            states.Add(state);
            this.states = states;


            
            if (enviro.Count != 0)
            {

                var closure = enviro[enviro.Count - 1];

                if(closure.variable == this.val)
                {
                    var element = closure.expression;
                    var newEnviro = closure.environment.ConvertAll(c => new Closure { variable = c.variable, expression = c.expression, environment = c.environment });
                    return element.Evaluate(newEnviro, stack, states);
                }
                else
                {
                    enviro.RemoveAt(enviro.Count - 1);
                    return this.Evaluate(enviro, stack, states);
                }
            }
            else
            {
                return this;
            }



        }
       
    }
}
