using System.Reflection;
using Module = Autofac.Module;
using Autofac;
using WebDemoQLDA.Service;
using WebDemoQLDA.IService;

namespace WebDemoQLDA.AutofacModules
{
    public class ValidationModule : Module
    {
        public ValidationModule()
        {
        }

        protected override void Load(ContainerBuilder builder)
        {
            //builder.RegisterType<CategoryMetaValidator>()
            //  .As<IValidator<CategoryMeta>>()
            //  .InstancePerLifetimeScope();
           
            var assembly = Assembly.GetExecutingAssembly();


            builder.RegisterAssemblyTypes(assembly)
                .Where(x => x.Name.EndsWith("Validator"))
                .AsImplementedInterfaces();

            #region Services            
            builder.RegisterAssemblyTypes(assembly)
                .Where(t => t.Name.EndsWith("Service"))
                .AsImplementedInterfaces();
            #endregion                 
        }

    }
}
