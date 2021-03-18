using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using LambdaApi.Models;
using Lambda_Logic;
using System.Web.Http.Cors;

namespace LambdaApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]    
    public class LambdaExpressionsController : ControllerBase
    {
        // POST: api/LambdaExpressions        
        [HttpPost]
        [EnableCors(origins: "*", headers: "*", methods: "*")]
        public Expression PostLambdaExpression(Object lambdaExpression)
        {
            string stringExpression = lambdaExpression.ToString();

            var k = new Krivine();

            var term = k.Convert(stringExpression);
            var output = k.Reduce(term);
            
            var response = new Expression() {Id = "response", Expr = output };

            return response;
        }

       
    }
}
