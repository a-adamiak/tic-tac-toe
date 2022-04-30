namespace TicTacToe.Api.DI
{
    public static class WebApplicationExtensions
    {
        public static WebApplication UseVersionedSwagger(this WebApplication app)
        {
            app.UseSwagger();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/swagger/v1/swagger.json", "V1");
            });

            return app;
        }
    }
}
