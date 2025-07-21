using Microsoft.AspNetCore.Mvc;
using ContactMap.Application.Commands;
using ContactMap.Application.Interfaces;

namespace ContactMap.WebApi.Controllers;

/// <summary>
/// API controller for managing relationships.
/// </summary>
[ApiController]
[Route("api/[controller]")]
public class RelationshipsController(ICommandHandler<ApproveRelationship> approveRelationshipHandler) : ControllerBase
{
    private readonly ICommandHandler<ApproveRelationship> _approveRelationshipHandler = approveRelationshipHandler;

    /// <summary>
    /// Approves a relationship by its ID.
    /// </summary>
    /// <param name="relationshipId">The ID of the relationship to approve.</param>
    /// <returns>An OK result if the relationship is approved.</returns>
    [HttpPost("approve")]
    public async Task<IActionResult> Approve([FromQuery] Guid relationshipId)
    {
        var command = new ApproveRelationship(relationshipId);
        await _approveRelationshipHandler.Handle(command);
        return Ok();
    }
}
