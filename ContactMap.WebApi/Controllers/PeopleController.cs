using Microsoft.AspNetCore.Mvc;
using ContactMap.Application.Commands;
using ContactMap.Application.UseCases;
using ContactMap.Application.Interfaces;

namespace ContactMap.WebApi.Controllers;

/// <summary>
/// API controller for managing people-related operations.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class PeopleController(
    ISearchPeopleUseCase searchPeopleUseCase,
    ICommandHandler<RequestRelationship> requestRelationshipHandler) : ControllerBase
{
    private readonly ISearchPeopleUseCase _searchPeopleUseCase = searchPeopleUseCase;
    private readonly ICommandHandler<RequestRelationship> _requestRelationshipHandler = requestRelationshipHandler;

    /// <summary>
    /// Searches for people by name and/or community.
    /// </summary>
    /// <param name="name">The name to search for.</param>
    /// <param name="community">The community to search for.</param>
    /// <returns>A list of matching people.</returns>
    [HttpGet("search")]
    public async Task<IActionResult> Search([FromQuery] string? name, [FromQuery] string? community)
    {
        IEnumerable<Domain.Entities.Person> people = await _searchPeopleUseCase.SearchPeopleAsync(name, community);
        return Ok(people);
    }

    /// <summary>
    /// Requests a relationship between two people.
    /// </summary>
    /// <param name="requesterId">The ID of the requester.</param>
    /// <param name="addresseeId">The ID of the addressee.</param>
    /// <returns>An accepted result if the request is processed.</returns>
    [HttpPost("request-relationship")]
    public async Task<IActionResult> RequestRelationship([FromQuery] Guid requesterId, [FromQuery] Guid addresseeId)
    {
        var command = new RequestRelationship(requesterId, addresseeId);
        await _requestRelationshipHandler.Handle(command);
        return Accepted();
    }
}
