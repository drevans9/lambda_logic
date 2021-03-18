using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Lambda_Logic.Structure;

namespace LambdaApi.Models
{
    public class Expression
    {

        public String Id { get; set; }

        public IExpression Expr { get; set; }

    }
}
