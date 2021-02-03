using System;
using System.Collections.Generic;
using System.Text;
using Lambda_Logic.Structure;

namespace Lambda_Logic
{
    class Krivine
    {

        
        public static void Main(string[] args)
        {           

            //var aVar1 = new Variable('X');
            //var aVar2 = new Variable('X');
            //var aVar3 = new Variable('X');

            //var aAbs1 = new Abstraction('X', aVar1);
            //var aAbs2 = new Abstraction('X', aVar2);
            //var aAbs3 = new Abstraction('X', aVar3);

            //var aApp1 = new Application(aAbs1, aAbs2);
            //var aApp2 = new Application(aApp1, aAbs3);



            var Var1 = new Variable('X');
            var Var2 = new Variable('X');

            var App2 = new Application(Var1, Var2);

            var Var3 = new Variable('X');

            var Abs1 = new Abstraction('X', App2);
            var Abs2 = new Abstraction('X', Var3);
            var App1 = new Application(Abs1, Abs2);


            var output = App1.Evaluate(new List<Closure>(), new List<Closure>(), new Dictionary<char, int>(), new List<State>());

            var test = output.states;

            foreach(var i in test)
            {
                i.expression.Print();
                Console.WriteLine();
                Console.Write("Stack: [");
                i.expression.PrintList(i.stack);
                Console.WriteLine("]");
                Console.Write("Environment: [");
                i.expression.PrintList(i.environment);
                Console.WriteLine("]");

                Console.WriteLine();
            }            

        }


        public IExpression parseInput()
        {

        }
       



    }
}
