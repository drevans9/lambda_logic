using System;
using System.Collections.Generic;
using System.Text;

namespace Lambda_Logic.Structure
{
    public class Closure
    {
        public char variable { get; set; }
        public IExpression expression { get; set; }
        public List<Closure> environment { get; set; }
        
    }
}
