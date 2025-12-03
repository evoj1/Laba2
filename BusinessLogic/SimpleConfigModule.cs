using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataAccess;
using Ninject;
using Ninject.Modules;
using Model;

namespace BusinessLogic
{
    public class SimpleConfigModule : NinjectModule
    {
        public override void Load()
        {
            Bind<IRepository<Student>>().To<EntityRepositoryStudents<Student>>().InSingletonScope();
            Bind<IRepository<Groups>>().To<EntityRepositoryStudents<Groups>>().InSingletonScope();
            //Bind<IRepository<Student>>().To<DapperRepository<Student>>().InSingletonScope();
            //Bind<IRepository<Groups>>().To<DapperRepository<Groups>>().InSingletonScope();
        }
    }
}