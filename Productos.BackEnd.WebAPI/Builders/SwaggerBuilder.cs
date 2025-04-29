namespace Productos.BackEnd.WebAPI.Builders
{
    /// <summary>
    /// Permite el uso de Swagger
    /// </summary>
    public static class SwaggerBuilder
    {
        /// <summary>
        /// Configura Swagger
        /// </summary>
        /// <param name="app"></param>
        /// <returns></returns>
        public static IApplicationBuilder AddSwaggerApp(this IApplicationBuilder app) 
        {
            var enabled = Convert.ToBoolean(Application.Registration.ConfigurationManager.SwaggerEnabled);
            if (((WebApplication)app).Environment.IsDevelopment() && enabled)
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            return app;
        }
    }
}
