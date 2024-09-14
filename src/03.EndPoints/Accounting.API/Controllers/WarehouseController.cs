using Accounting.Application.Warehouses.Commands;
using Accounting.Domain.Warehouses.Repositories;
using Accounting.Domain.Warehouses.Repositories.ViewModels;
using ErrorOr;
using Framework.Domain;
using Framework.Domain.Data;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        [HttpPost]
        public async Task<IActionResult> AddWarehouse(
            [FromServices]
            ICommandHandler<AddWarehouseCommand, string> handler,
            [FromBody] AddWarehouseCommand command)
        {
            var result = await handler.Handle(command);

            return result.Match<IActionResult>(
                s => Ok(result.Value),
                e => BadRequest(result.FirstError.Description));
        }

        [HttpPatch("default")]
        public async Task<IActionResult> ChangeDefaultWarehouse(
            [FromServices]
            ICommandHandler<ChangeDefaultWarehouseCommand, string> handler,
            [FromBody] ChangeDefaultWarehouseCommand command)
        {
            var result = await handler.Handle(command);

            return result.Match<IActionResult>(
                s => Ok(result.Value),
                e => BadRequest(result.FirstError.Description));
        }

        [HttpPut]
        public async Task<IActionResult> Edit(
            [FromServices]
            ICommandHandler<EditWarehouseCommand, string> handler,
            [FromBody] EditWarehouseCommand command)
        {
            var result = await handler.Handle(command);

            return result.Match<IActionResult>(
                s => Ok(result.Value),
                e => BadRequest(result.FirstError.Description));
        }

        [HttpGet]
        public async Task<IEnumerable<GetAllWarehouseViewModel>> GetAll(
            [FromServices] WarehouseReadRepository repository,
            [FromQuery] Pagination? pagination)
        {
            if (pagination is null)
                pagination = new Pagination();

            return await repository.GetAll(pagination);
        }

        [HttpGet("{id}")]
        public async Task<GetWarehouseByIdViewModel?> GetWarehouseById(
            [FromServices] WarehouseReadRepository repository,
            [FromRoute] string id)
        {
            return await repository.GetWarehouseById(id);
        }
    }
}