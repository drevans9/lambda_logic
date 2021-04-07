using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{    
    public interface IExpression
    {
        public string typeOfTerm { get; set; }
        public List<State> states { get; set; }
        public void Print();
        public void PrintList(List<Closure> list);
        public IExpression Evaluate(List<Closure> enviro, List<Closure> stack, List<State> states);

        public IExpression DeepCopy();

        public List<Closure> DeepCopyList(List<Closure> list);
    }
}
