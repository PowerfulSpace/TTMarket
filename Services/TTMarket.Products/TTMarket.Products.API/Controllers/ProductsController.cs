using System.Threading;
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

namespace TTMarket.Products.API.Controllers
{
    [Produces("application/json")]
    public class ProductsController : BaseApiController
    {
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<ProductDto>), StatusCodes.Status200OK)]
        public async Task<IActionResult> Get()
            => Ok(await Mediator.Send(new GetAllProductsQuery()));

        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ProductDetailDto), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Get(Guid id, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(new GetProductByIdQuery(id), cancellationToken));

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<IActionResult> Post(ProductCreateDto product, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(new CreateProductCommand(product), cancellationToken));

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Put(Guid id, ProductUpdateDto product, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(new UpdateProductCommand(id, product), cancellationToken));

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiErrorResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
            => Ok(await Mediator.Send(new DeleteProductCommand(id), cancellationToken));
    }
}