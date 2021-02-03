using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{
    public class State
    {
        public IExpression expression { get; set; }
        public List<Closure> environment { get; set; }
        public List<Closure> stack { get; set; }
    }
}
