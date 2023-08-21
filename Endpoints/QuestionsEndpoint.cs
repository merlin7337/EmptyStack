using EmptyStack.Data;
using Microsoft.EntityFrameworkCore;

namespace EmptyStack.Endpoints;

public static class QuestionsEndpoint
{
    public static void MapQuestions(this RouteGroupBuilder app)
    {
        app.MapGet("/", async (EmptyStackDb db) => await db.questions.ToListAsync());

        app.MapGet("/{id}", async (int id, EmptyStackDb db) =>
            await db.questions.FindAsync(id) is { } question ? Results.Ok(question) : Results.NotFound()
        );

        app.MapGet("/{id}/answers", async (int id, EmptyStackDb db) =>
            await db.questions.FindAsync(id) is { } question
                ? await db.answers
                    .Where(a => a.parentquestionid == question.id)
                    .ToListAsync()
                : null
        );

        app.MapPost("/", async (Question question, EmptyStackDb db) =>
        {
            db.questions?.Add(question);
            await db.SaveChangesAsync();
        });

        app.MapPut("/{id}", async (int id, Question newQuestion, EmptyStackDb db) =>
        {
            var question = await db.questions.FindAsync(id);

            question.title = newQuestion.title;
            question.description = newQuestion.description;
            question.ownerid = newQuestion.ownerid;
            question.score = newQuestion.score;
            question.createddate = newQuestion.createddate;
            question.lastmodifieddate = newQuestion.lastmodifieddate;
            question.tags = newQuestion.tags;

            await db.SaveChangesAsync();
        });

        app.MapDelete("/{id}", async (int id, EmptyStackDb db) =>
        {
            if (await db.questions.FindAsync(id) is { } question)
            {
                db.questions.Remove(question);
                await db.SaveChangesAsync();
            }
        });
    }
}