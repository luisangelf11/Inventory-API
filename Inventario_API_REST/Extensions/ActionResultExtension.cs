namespace Inventario_API_REST.Extensions
{
    public static class ActionResultExtension
    {
        public static async Task<IActionResult> ToActionResult(this Task<Result> resultTask)
        {
            var result = await resultTask;
            return new ObjectResult(new
            {
                success = result.Success,
                message = result.Message
            })
            { StatusCode = result.StatusCode };
        }
        public static async Task<IActionResult> ToActionResult<T>(this Task<Result<T>> resultTask)
        {
            var result = await resultTask;
            return new ObjectResult(new
            {
                success = result.Success,
                message = result.Message,
                data = result.Data
            })
            { StatusCode = result.StatusCode };
        }
        public static async Task<IActionResult> ToActionResult<T>(this Task<ResultPaginated<T>> resultTask)
        {
            var result = await resultTask;
            return new ObjectResult(new
            {
                success = result.Success,
                message = result.Message,
                data = result.Data,
                currentPage = result.CurrentPage,
                totalPages = result.TotalPages,
                totalCount = result.TotalCount,
                hasNextPage = result.HasNextPage,
                hasPreviousPage = result.HasPreviousPage
            })
            { StatusCode = result.StatusCode };
        }
    }
}