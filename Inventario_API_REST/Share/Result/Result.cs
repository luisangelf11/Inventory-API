namespace Inventario_API_REST.Share.Result;
public class Result
{
    public bool Success { get; }
    public string? Message { get; }
    public int StatusCode { get; }

    protected Result(bool success, string? message, int statusCode)
    {
        Success = success;
        Message = message;
        StatusCode = statusCode;
    }

    public static Result Ok(string message = "Success", int statusCode = 200)
        => new(true, message, statusCode);

    public static Result Failure(string message, int statusCode = 400)
        => new(false, message, statusCode);
}

public class Result<T> : Result
{
    public T? Data { get; }

    private Result(bool success, T? data, string? message, int statusCode)
        : base(success, message, statusCode)
    {
        Data = data;
    }

    public static Result<T> Ok(T data, int statusCode = 200)
        => new(true, data, null, statusCode);

    public static Result<T> Ok(T data, string message, int statusCode = 200)
        => new(true, data, message, statusCode);
    public new static Result<T> Failure(string message, int statusCode = 400)
        => new(false, default, message, statusCode);
}