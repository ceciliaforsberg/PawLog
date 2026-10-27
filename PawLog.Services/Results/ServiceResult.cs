using System;
using System.Collections.Generic;
using System.Text;

namespace PawLog.Services.Results;

public enum ServiceError 
{
    None,
    NotFound,
    Invalid
}

public class ServiceResult<T>
{
    public T? Value { get; }
    public ServiceError Error { get; }
    public string? ErrorMessage { get; }
    public bool IsSuccess => Error == ServiceError.None;

    private ServiceResult(T? value, ServiceError error, string? errorMessage)
    {
        Value = value;
        Error = error;
        ErrorMessage = errorMessage;
    }

    public static ServiceResult<T> Success(T value) =>
        new(value, ServiceError.None, null);

    public static ServiceResult<T> NotFound(string? message = null) =>
        new(default, ServiceError.NotFound, message);

    public static ServiceResult<T> Invalid(string message) =>
        new(default, ServiceError.Invalid, message);
}
