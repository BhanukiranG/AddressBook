using AddressBook.Application.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace AddressBook.API.Extensions
{
    /// <summary>
    /// Extension methods for configuring API behavior and response formatting.
    /// </summary>
    public static class ApiBehaviorExtensions
    {
        /// <summary>
        /// Configures a custom API behavior for handling model validation errors.
        /// <para>
        /// This replaces the default ASP.NET Core validation error response with
        /// a standardized <see cref="ApiResponse{T}"/> wrapper.
        /// </para>
        /// </summary>
        /// <param name="services">
        /// The <see cref="IServiceCollection"/> to add the configuration to.
        /// </param>
        /// <returns>
        /// The same <see cref="IServiceCollection"/> instance to allow method chaining.
        /// </returns>
        /// <remarks>
        /// This method is typically used in <c>Program.cs</c>:
        /// <code>
        /// builder.Services.AddCustomApiBehavior();
        /// </code>
        ///
        /// Validation errors (from DataAnnotations or FluentValidation) will be returned
        /// in the following format:
        /// <code>
        /// {
        ///   "successful": false,
        ///   "message": "Validation failed",
        ///   "data": {
        ///     "FieldName": [ "Error message" ]
        ///   }
        /// }
        /// </code>
        /// </remarks>
        public static IServiceCollection AddCustomApiBehavior(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(x => x.Value?.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value!.Errors
                                .Select(e => e.ErrorMessage)
                                .ToArray()
                        );

                    var response = new ApiResponse<object>
                    {
                        Successful = false,
                        Message = "Validation failed",
                        Data = errors
                    };

                    return new BadRequestObjectResult(response);
                };
            });

            return services;
        }
    }
}