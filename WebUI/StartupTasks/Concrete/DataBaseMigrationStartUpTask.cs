using Autofac;
using DataAccess.Concrete.EntityFramework.Contexts;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebUI.StartupTasks.Concrete
{
    public class DataBaseMigrationStartUpTask : IStartable
    {
        public DataBaseMigrationStartUpTask()
        {

        }
        public void Start()
        {
            EfContext efContext;
            var builder = new ContainerBuilder();
            builder.RegisterType<EfContext>();
            var container = builder.Build();
            using (var scope = container.BeginLifetimeScope())
            {
                efContext = scope.Resolve<EfContext>();
                efContext.Database.Migrate();
            }
        }
    }
}
