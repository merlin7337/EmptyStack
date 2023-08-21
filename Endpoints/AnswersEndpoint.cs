using EmptyStack.Data;
using Microsoft.EntityFrameworkCore;

namespace EmptyStack.Endpoints;

public static class AnswersEndpoint
{
    public static void MapAnswers(this RouteGroupBuilder app)
    {
        app.MapGet("/", async (EmptyStackDb db) => await db.answers.ToListAsync());

        app.MapGet("/{id}", async (int id, EmptyStackDb db) =>
            await db.answers.FindAsync(id) is { } answer ? Results.Ok(answer) : Results.NotFound()
        );

        app.MapPost("/", async (Answer answer, EmptyStackDb db) =>
        {
            db.answers?.Add(answer);
            await db.SaveChangesAsync();
        });

        app.MapPut("/{id}", async (int id, Answer newAnswer, EmptyStackDb db) =>
        {
            var answer = await db.answers.FindAsync(id);

            answer.answer = newAnswer.answer;
            answer.parentquestionid = newAnswer.parentquestionid;
            answer.ownerid = newAnswer.ownerid;
            answer.score = newAnswer.score;
            answer.createddate = newAnswer.createddate;
            answer.lastmodifieddate = newAnswer.lastmodifieddate;

            await db.SaveChangesAsync();
        });

        app.MapDelete("/{id}", async (int id, EmptyStackDb db) =>
        {
            if (await db.answers.FindAsync(id) is { } answer)
            {
                db.answers.Remove(answer);
                await db.SaveChangesAsync();
            }
        });
    }
}