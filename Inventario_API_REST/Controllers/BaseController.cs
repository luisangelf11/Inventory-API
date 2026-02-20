namespace Inventario_API_REST.Controllers
{
    public class BaseController : ControllerBase
    {
        protected IActionResult HandleResult(Result result)
        {
            return StatusCode(result.StatusCode, new
            {
                success = result.Success,
                message = result.Message
            });
        }

        protected IActionResult HandleResult<T>(Result<T> result)
        {
            return StatusCode(result.StatusCode, new
            {
                success = result.Success,
                message = result.Message,
                data = result.Data
            });
        }

        protected IActionResult HandleResult<T>(ResultPaginated<T> result)
        {
            return StatusCode(result.StatusCode, new
            {
                success = result.Success,
                message = result.Message,
                data = result.Data,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages,
                totalCount = result.TotalCount,
                hasNextPage = result.HasNextPage,
                hasPreviousPage = result.HasPreviousPage
            });
        }
    }
}