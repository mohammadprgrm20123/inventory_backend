using Accounting.Application.Warehouses.Commands;
using ErrorOr;
using Framework.Domain;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Accounting.API.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        [HttpPost]
        public async Task<ErrorOr<string>> AddWarehouse(
            [FromServices] ICommandHandler<AddWarehouseCommand, string> handler,
            [FromBody] AddWarehouseCommand command)
        {
            return await handler.Handle(command);
        }
    }
}
