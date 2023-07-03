using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.CreateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.DeleteLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Commands.UpdateLeaveAllocation;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocationDetails;
using HR.LeaveManagement.Application.Features.LeaveAllocations.Queries.GetLeaveAllocations;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace HR.LeaveManagement.Api.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LeaveAllocationsController : ControllerBase
{
    private readonly IMediator _mediator;

    public LeaveAllocationsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    // GET: api/LeaveAllocations
    [HttpGet]
    public async Task<ActionResult<List<LeaveAllocationDto>>> Get(bool isLoggedInUser = false)
    {
        var leaveAllocations = await _mediator.Send(new GetLeaveAllocationsQuery());
        return Ok(leaveAllocations);
    }

    // GET: api/LeaveAllocations/5
    [HttpGet("{id:int}", Name = "Get")]
    public async Task<ActionResult<LeaveAllocationDetailsDto>> Get(int id)
    {
        var leaveAllocation = await _mediator.Send(new GetLeaveAllocationDetailsQuery(id));
        return Ok(leaveAllocation);
    }

    // POST: api/LeaveAllocations
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult> Post([FromBody] CreateLeaveAllocationCommand command)
    {
        var response = await _mediator.Send(command);
        return CreatedAtAction(nameof(Get), new { id = response });
    }

    // PUT: api/LeaveAllocations/5
    [HttpPut]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Put([FromBody] UpdateLeaveAllocationCommand command)
    {
        await _mediator.Send(command);
        return NoContent();
    }

    // DELETE: api/LeaveAllocations/5
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status204NoContent)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult> Delete(int id)
    {
        await _mediator.Send(new DeleteLeaveAllocationCommand(id));
        return NoContent();
    }
}