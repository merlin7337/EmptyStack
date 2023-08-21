using EmptyStack.Data;
using Microsoft.EntityFrameworkCore;

namespace EmptyStack.Endpoints;

public static class UsersEndpoint
{
    public static void MapUsers(this RouteGroupBuilder app)
    {
        app.MapGet("/", async (EmptyStackDb db) => await db.users.ToListAsync());

        app.MapGet("/{id}", async (int id, EmptyStackDb db) =>
            await db.users.FindAsync(id) is { } user ? Results.Ok(user) : Results.NotFound()
        );

        app.MapPost("/", async (User user, EmptyStackDb db) =>
        {
            db.users?.Add(user);
            await db.SaveChangesAsync();
        });

        app.MapPut("/{id}", async (int id, User newUser, EmptyStackDb db) =>
        {
            var user = await db.users.FindAsync(id);

            user.username = newUser.username;
            user.password = newUser.password;
            user.role = newUser.role;

            await db.SaveChangesAsync();
        });

        app.MapDelete("/{id}", async (int id, EmptyStackDb db) =>
        {
            if (await db.users.FindAsync(id) is { } user)
            {
                db.users.Remove(user);
                await db.SaveChangesAsync();
            }
        });
    }
}