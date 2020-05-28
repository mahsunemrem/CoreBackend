using System;
using System.Collections.Generic;
using AutoMapper;
using Autofac;
using AutoMapper.Extensions.ExpressionMapping;

namespace Business.DependencyResolvers.Autofac
{
    public class AutoMapperModule : Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            var assemblies = AppDomain.CurrentDomain.GetAssemblies();
            builder.RegisterAssemblyTypes(assemblies)
                .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
                .As<Profile>();

            builder.Register(c => new MapperConfiguration(cfg => {
                cfg.AddExpressionMapping();
                foreach (var profile in c.Resolve<IEnumerable<Profile>>())
                {
                    cfg.AddProfile(profile);
                }
            })).AsSelf().AutoActivate().SingleInstance();

            builder.Register(c => c.Resolve<MapperConfiguration>()
                .CreateMapper(c.Resolve))
                .As<IMapper>()
                .InstancePerLifetimeScope();
        }


        //protected override void Load(ContainerBuilder builder)
        //{
        //    base.Load(builder);

        //    var assemblies = AppDomain.CurrentDomain.GetAssemblies();

        //    builder.RegisterAssemblyTypes(assemblies)
        //        .Where(t => typeof(Profile).IsAssignableFrom(t) && !t.IsAbstract && t.IsPublic)
        //        .As<Profile>();

        //    builder.Register(c => new MapperConfiguration(cfg =>
        //    {
        //        foreach (var profile in c.Resolve<IEnumerable<Profile>>())
        //        {
        //            cfg.AddProfile(profile);
        //        }
        //    }))
        //        .AsSelf()
        //        .AutoActivate()
        //        .SingleInstance();

        //    builder.Register(c =>
        //    {
        //        // these are the changed lines
        //        var scope = c.Resolve<ILifetimeScope>();
        //        return new Mapper(c.Resolve<MapperConfiguration>(), scope.Resolve);
        //    })
        //        .As<IMapper>()
        //        .SingleInstance();
        //}

    }
}
