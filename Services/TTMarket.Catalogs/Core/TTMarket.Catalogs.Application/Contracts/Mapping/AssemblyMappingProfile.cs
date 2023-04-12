using System;
using System.Linq;
using System.Reflection;
using AutoMapper;

namespace TTMarket.Catalogs.Application.Contracts.Mapping
{
    public class AssemblyMappingProfile : Profile
    {
        public AssemblyMappingProfile(Assembly assembly)
            => ApplyMappingFromAssembly(assembly);

        private void ApplyMappingFromAssembly(Assembly assembly)
        {
            var types = assembly.GetExportedTypes()
                                .Where(type => type.GetInterfaces()
                                                   .Any(x => x.IsGenericType && 
                                                        x.GetGenericTypeDefinition() == typeof(IMapWith<>)))
                               .ToList();
            foreach (var type in types)
            {
                var instance = Activator.CreateInstance(type);
                var map = type.GetInterfaceMap(type.GetInterfaces()
                                                   .FirstOrDefault());
                var interfaceMethod = map.TargetMethods
                                          .Where(x => x.Name.Contains("Mapping"))
                                          .FirstOrDefault();
                interfaceMethod?.Invoke(instance, new object[] { this });
            }
        }
    }
}