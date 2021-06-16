using Application;
using Application.Logger;
using DataAccess;
using Domain;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;

namespace Implementation.Logging
{
    public class DatabaseUseCaseLogger : IUseCaseLogger
    {
        private readonly ProjekatASPContext _context;

        public DatabaseUseCaseLogger(ProjekatASPContext context)
        {
            _context = context;
        }

        public void Log(IUseCase useCase, IApplicationActor actor, object useCaseData)
        {
            _context.UseCaseLogs.Add(new UseCaseLog
            {
                CreatedAt = DateTime.UtcNow,
                UseCaseName = useCase.Name,
                Data = JsonConvert.SerializeObject(useCaseData),
                Actor = actor.Identity
            });

            _context.SaveChanges();
        }
    }
}
