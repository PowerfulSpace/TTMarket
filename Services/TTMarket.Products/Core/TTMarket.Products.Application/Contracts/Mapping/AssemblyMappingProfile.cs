using System;
using System.Reflection;
using System.Linq;
using AutoMapper;

namespace TTMarket.Products.Application.Contracts.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
            => ApplyMappingFromAssembly(assembly);

        void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                                .Where(type => type.GetInterfaces()
                                                   .Any(x => x.IsGenericType && 
                                                        x.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                               .ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                InterfaceMapping map = type.GetInterfaceMap(type.GetInterfaces()
                                                                .FirstOrDefault());
                MethodInfo interfaceMethod = map.TargetMethods
                                                .Where(x => x.Name.Contains("Mapping"))
                                                .FirstOrDefault();
                interfaceMethod?.Invoke(instance, new object[] { this });
            }
        }
    }
}