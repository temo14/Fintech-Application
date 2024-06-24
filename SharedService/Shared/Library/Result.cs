using Microsoft.AspNetCore.Http;

namespace Shared.Library;

public abstract class ResultBase
{
    public int StatusCode { get; set; }
    public bool IsSuccess { get; set; }
    public List<string> ErrorMessages { get; set; }

    protected ResultBase() { }

    protected ResultBase(int statusCode, bool isSuccess, List<string> errorMessages = null)
    {
        StatusCode = statusCode;
        IsSuccess = isSuccess;
        ErrorMessages = errorMessages;
    }
}

public sealed class Result : ResultBase
{
    public Result() { } 

    public static Result Success()
        => new Result { StatusCode = StatusCodes.Status200OK, IsSuccess = true };

    public static Result Success(int statusCode)
    => new Result { StatusCode = statusCode, IsSuccess = true };


    public static Result Failed(params string[] errorMessages)
        => new Result { StatusCode = StatusCodes.Status500InternalServerError, IsSuccess = false, ErrorMessages = new List<string>(errorMessages) };
    
    public static Result Failed(int statusCode, params string[] errorMessages)
        => new Result { StatusCode = statusCode, IsSuccess = false, ErrorMessages = new List<string>(errorMessages) };
}

public sealed class Result<T> : ResultBase
{
    public T Data { get; set; }

    public Result() { } 

    private Result(int statusCode, bool isSuccess, T data = default, List<string> errorMessages = null)
        : base(statusCode, isSuccess, errorMessages)
    {
        Data = data;
    }
    public static Result<T> Success(T data)
        => new Result<T>(StatusCodes.Status200OK, true, data);
    public static Result<T> Success(T data, int statusCode)
    => new Result<T>(statusCode, true, data);

    public static Result<T> Failed(params string[] errorMessages)
        => new Result<T>(StatusCodes.Status500InternalServerError, false, default, new List<string>(errorMessages));
}