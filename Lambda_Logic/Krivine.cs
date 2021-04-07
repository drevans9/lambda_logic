using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Lambda_Logic.Structure;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Lambda_Logic
{
    public class Krivine
    {          

        public IExpression Reduce(IExpression term)
        {
            var output = term.Evaluate(new List<Closure>(), new List<Closure>(), new List<State>());
            return output;
        }


        //Converts the object receieved from JS into C#
        public IExpression Convert(String term)
        {

            var jsonOuter = JsonConvert.DeserializeObject(term);
            var jsonOuterPair = (IEnumerable<KeyValuePair<string, JToken>>)jsonOuter;
        
            var jsonTerm = jsonOuterPair.First().Value;
            var jsonTermPair = (IEnumerable<KeyValuePair<string, JToken>>)jsonTerm;

            var convertedTerm = StripRecursion(jsonTermPair);

            return convertedTerm;

        }


        public IExpression StripRecursion(IEnumerable<KeyValuePair<string, JToken>> level)
        {
            var name = level.First().Value.ToString();

            if(name == "Application")
            {
                var t1 = level.ElementAt(1);
                var t1Val = (IEnumerable<KeyValuePair<string, JToken>>)t1.Value;

                

                var t2 = level.ElementAt(2);
                var t2Val = (IEnumerable<KeyValuePair<string, JToken>>)t2.Value;

                return new Application(StripRecursion(t1Val), StripRecursion(t2Val));
            }
            else if (name == "Abstraction")
            {
                var t1Val = level.ElementAt(1).Value.ToString();

                var t2 = level.ElementAt(2);
                var t2Val = (IEnumerable<KeyValuePair<string, JToken>>)t2.Value;

                return new Abstraction(t1Val.First(), StripRecursion(t2Val));
            }
            else if (name == "Variable")
            {
                var t1Val = level.ElementAt(1).Value.ToString();

                return new Variable(t1Val.First());

            }
            else
            {
                Console.WriteLine("Error converting lambda expression into C# structure");
                return null;
            }    
        }

        public static void Main()
        {

            var K = new Krivine();

            var var1 = new Variable('X');
            var var2 = new Variable('Y');
            var var3 = new Variable('X');
            var var4 = new Variable('X');
            var var5 = new Variable('X');

            var app1 = new Application(var1, var2);
            var app2 = new Application(var4, var5);

            var abs1 = new Abstraction('Y',app1);
            var abs2 = new Abstraction('X', abs1);
            var abs3 = new Abstraction('X', var3);
            var abs4 = new Abstraction('X', app2);

            var app5 = new Application(abs2, abs3);
            var app6 = new Application(app5, abs4);

            var output = K.Reduce(app6);

            K.print(output);
        }


        public void print(IExpression output)
        {

            var outputStates = output.states;

            foreach (var state in outputStates)
            {
                state.expression.Print();
                Console.WriteLine();
                Console.Write("Stack: [");
                state.expression.PrintList(state.stack);
                Console.WriteLine("]");
                Console.Write("Environment: [");
                state.expression.PrintList(state.environment);
                Console.WriteLine("]");

                Console.WriteLine();
            }
            output.Print();
            Console.WriteLine();
        }

    }
}
