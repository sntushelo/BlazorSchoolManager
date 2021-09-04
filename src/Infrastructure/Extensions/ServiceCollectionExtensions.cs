using System;
using System.Linq;
using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using BlazorSchoolManager.Application.Interfaces.Repositories;
using BlazorSchoolManager.Application.Interfaces.Services.Storage;
using BlazorSchoolManager.Application.Interfaces.Services.Storage.Provider;
using BlazorSchoolManager.Application.Interfaces.Serialization.Serializers;
using BlazorSchoolManager.Application.Serialization.JsonConverters;
using BlazorSchoolManager.Infrastructure.Repositories;
using BlazorSchoolManager.Infrastructure.Services.Storage;
using BlazorSchoolManager.Application.Serialization.Options;
using BlazorSchoolManager.Infrastructure.Services.Storage.Provider;
using BlazorSchoolManager.Application.Serialization.Serializers;

namespace BlazorSchoolManager.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddInfrastructureMappings(this IServiceCollection services)
        {
            services.AddAutoMapper(Assembly.GetExecutingAssembly());
        }

        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IRepositoryAsync<,>), typeof(RepositoryAsync<,>))
                .AddTransient<IStudentRepository, StudentRepository>()
                .AddTransient<ITeacherRepository, TeacherRepository>()
                .AddTransient<IAttendanceRepository, AttendanceRepository>()
                .AddTransient<IVenueRepository, VenueRepository>()
                .AddTransient<ILessonRepository, LessonRepository>()
                .AddTransient<IProductRepository, ProductRepository>()
                .AddTransient<IBrandRepository, BrandRepository>()
                .AddTransient<IDocumentRepository, DocumentRepository>()
                .AddTransient<IDocumentTypeRepository, DocumentTypeRepository>()
                .AddTransient(typeof(IUnitOfWork<>), typeof(UnitOfWork<>));
        }

        public static IServiceCollection AddExtendedAttributesUnitOfWork(this IServiceCollection services)
        {
            return services
                .AddTransient(typeof(IExtendedAttributeUnitOfWork<,,>), typeof(ExtendedAttributeUnitOfWork<,,>));
        }

        public static IServiceCollection AddServerStorage(this IServiceCollection services)
            => AddServerStorage(services, null);

        public static IServiceCollection AddServerStorage(this IServiceCollection services, Action<SystemTextJsonOptions> configure)
        {
            return services
                .AddScoped<IJsonSerializer, SystemTextJsonSerializer>()
                .AddScoped<IStorageProvider, ServerStorageProvider>()
                .AddScoped<IServerStorageService, ServerStorageService>()
                .AddScoped<ISyncServerStorageService, ServerStorageService>()
                .Configure<SystemTextJsonOptions>(configureOptions =>
                {
                    configure?.Invoke(configureOptions);
                    if (!configureOptions.JsonSerializerOptions.Converters.Any(c => c.GetType() == typeof(TimespanJsonConverter)))
                        configureOptions.JsonSerializerOptions.Converters.Add(new TimespanJsonConverter());
                });
        }
    }
}