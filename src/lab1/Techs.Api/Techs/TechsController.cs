using FluentValidation;
using Marten;
using Microsoft.AspNetCore.Mvc;

namespace Techs.Api.Techs;

public class TechsController(IDocumentSession documentSession) : ControllerBase
{
    [HttpPost("/techs")]
    public async Task<ActionResult> AddATechAsync(
        [FromBody] TechCreateModel request,
        [FromServices] IValidator<TechCreateModel> validator
        )
    {
        if (validator.Validate(request).IsValid == false)
        {
            return BadRequest();
        }

        var response = new TechResponseModel(Guid.NewGuid(), request.FirstName, request.LastName, request.Sub, request.Email, request.Phone);
        var entity = new TechEntity
        {
            Id = response.Id,
            FirstName = response.FirstName,
            LastName = response.LastName,
            Sub = response.Sub,
            Email = response.Email,
            Phone = response.Phone

        };
        documentSession.Store(entity);
        await documentSession.SaveChangesAsync();
        return Created($"/techs/{response.Id}", response);
    }

    [HttpGet("/techs/{id:guid}")]
    public async Task<ActionResult> GetVendorById(Guid id)
    {
        var entity = await documentSession.Query<TechEntity>().SingleOrDefaultAsync(v => v.Id == id);
        if (entity is null)
        {
            return NotFound();
        }
        else
        {
            return Ok(entity);
        }
    }
}