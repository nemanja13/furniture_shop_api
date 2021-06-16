using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.FakeActors
{
    public class UnauthorizedActor : IApplicationActor
    {
        public int Id => 0;
        public string Identity => "Unauthorized Actor";
        public IEnumerable<int> AllowedUseCases => new List<int> { 20, 32, 33 };
    }
}
