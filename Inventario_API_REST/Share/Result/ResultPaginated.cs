namespace Inventario_API_REST.Share.Result
{
    public class ResultPaginated<T> : Result
    {
        public IEnumerable<T>? Data { get; }
        public int CurrentPage { get; }
        public int TotalPages { get; }
        public int TotalCount { get; }
        public bool HasNextPage => CurrentPage < TotalPages;
        public bool HasPreviousPage => CurrentPage > 1;

        private ResultPaginated(IEnumerable<T> data, int count, int pageNumber, int pageSize)
            : base(true, null, 200)
        {
            Data = data;
            TotalCount = count;
            CurrentPage = pageNumber;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
        }

        private ResultPaginated(string message, int statusCode)
        : base(false, message, statusCode)
        {
            Data = default;
        }

        public static ResultPaginated<T> Ok(IEnumerable<T> data, int count, int pageNumber, int pageSize)
            => new(data, count, pageNumber, pageSize);

        public static new ResultPaginated<T> Failure(string message, int statusCode = 400)
            => new(message, statusCode);
    }
}
