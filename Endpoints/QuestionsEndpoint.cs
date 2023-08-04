using EmptyStack.Data;
using Microsoft.EntityFrameworkCore;

namespace EmptyStack.Endpoints;

public static class QuestionsEndpoint
{
    public static void MapQuestions(this RouteGroupBuilder app)
    {
        app.MapGet("/", async (EmptyStackDb db) => await db.questions.ToListAsync());
        app.MapPost("/", async (Question question, EmptyStackDb db) =>
        {
            db.questions?.Add(question);
            await db.SaveChangesAsync();
        });
    }
}