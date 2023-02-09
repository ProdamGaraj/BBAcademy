namespace Backend.Middlewares;

public class AuthRequiredMiddleware
{
    private readonly RequestDelegate _next;

    public AuthRequiredMiddleware(RequestDelegate next)
    {
        _next = next;
    }
    
    public async Task InvokeAsync(HttpContext context /* other dependencies allowed */)
    {
        if (!context.User.Identity.IsAuthenticated)
        {
            if (!context.Request.Path.Equals("/Account/Register") && !context.Request.Path.Equals("/Account/Login") && !context.Request.Path.Equals("/"))
            {
                context.Response.Redirect("/Account/Login");
            }
        }
        else
        {
            //context.Response.Redirect("/Account");
        }

        await _next.Invoke(context);
    }
}