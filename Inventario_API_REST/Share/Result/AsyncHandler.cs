namespace Inventario_API_REST.Share.Result
{
    public static class AsyncHandler
    {
        public static async Task<TResult> TryCatchAsync<TResult>(
         Func<Task<TResult>> action,
         Func<Exception, TResult> onError
            )
        {
            try
            {
                return await action();
            }
            catch (Exception ex)
            {
                return onError(ex);
            }
        }

        public static TResult TryCatchSync<TResult>(
        Func<TResult> action,
        Func<Exception, TResult> onError)
        {
            try
            {
                return action();
            }
            catch (Exception ex)
            {
                return onError(ex);
            }
        }
    }
}
