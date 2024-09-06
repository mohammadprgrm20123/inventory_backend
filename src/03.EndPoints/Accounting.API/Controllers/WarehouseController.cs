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
        public async Task<ErrorOr<string>> AddWarehouse(
            [FromServices]
            ICommandHandler<AddWarehouseCommand, string> handler,
            [FromBody] AddWarehouseCommand command)
        {
            return await handler.Handle(command);
        }

        [HttpPatch("default")]
        public async Task<ErrorOr<string>> ChangeDefaultWarehouse(
            [FromServices]
            ICommandHandler<ChangeDefaultWarehouseCommand, string> handler,
            [FromBody] ChangeDefaultWarehouseCommand command)
        {
            return await handler.Handle(command);
        }

        [HttpPut]
        public async Task<ErrorOr<string>> Edit(
            [FromServices]
            ICommandHandler<EditWarehouseCommand, string> handler,
            [FromBody] EditWarehouseCommand command)
        {
            return await handler.Handle(command);
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