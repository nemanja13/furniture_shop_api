using Application;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Core.FakeActors
{
    public class FakeApiAdmin : IApplicationActor
    {
        public int Id => 2;

        public string Identity => "Fake Api Admin";

        public IEnumerable<int> AllowedUseCases => Enumerable.Range(1, 100);
    }
}
