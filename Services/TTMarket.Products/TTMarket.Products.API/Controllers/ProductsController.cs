using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TTMarket.Products.Application.Exceptions;
using TTMarket.Products.Application.Features.Commands.Create;
using TTMarket.Products.Application.Features.Commands.Delete;
using TTMarket.Products.Application.Features.Commands.Update;
using TTMarket.Products.Application.Features.Queries.GetAll;
using TTMarket.Products.Application.Features.Queries.GetById;
using TTMarket.Products.Domain;

namespace TTMarket.Products.API.Controllers
{
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Product>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
            => Ok(await Mediator.Send(new GetAllQuery()));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(Product), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id)
            => Ok(await Mediator.Send(new GetByIdQuery(id)));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(Product product)
            => Ok(await Mediator.Send(new CreateCommand(product)));

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Product product)
            => Ok(await Mediator.Send(new UpdateCommand(product)));

        [HttpDelete]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id)
            => Ok(await Mediator.Send(new DeleteCommand(id)));
    }
}