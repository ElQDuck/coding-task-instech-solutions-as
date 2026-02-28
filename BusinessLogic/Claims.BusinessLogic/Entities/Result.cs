using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace Claims.BusinessLogic.Entities;


/// <summary>
/// The result of an operation which can be successful or fail.
/// </summary>
/// <remarks>https://github.com/ardalis/Result</remarks>
/// <typeparam name="T"></typeparam>
public class Result<T>
{
    private readonly T _value;
    
    /// <summary>
    /// Initialize a new instance of <see cref="Result{T}"/>
    /// </summary>
    /// <param name="value"></param>
    protected internal Result(T value)
    {
        this._value = value;
    }

    /// <summary>
    /// Initialize a new instance of <see cref="Result{T}"/>
    /// </summary>
    /// <param name="exception"></param>
    protected internal Result(Exception exception)
    {
        this.Exception = exception;
        // exception is thrown if value is accessed
        this._value = default !;
    }
    
    /// <summary>
    /// The <see cref="Exception"/>, if the operation is not successful.
    /// </summary>
    public Exception? Exception { get; }

    /// <summary>
    /// <c>true</c> if operation is successful.
    /// </summary>
    [MemberNotNullWhen(false, nameof(Exception))]
    public bool IsSuccess => this.Exception == null;
    
    /// <summary>
    /// The value if the operation is successful.
    /// </summary>
    /// <exception cref="Exception">The operation is not successful.</exception>
    public T Value
    {
        get
        {
            if (this.Exception is not null)
            {
                ExceptionDispatchInfo.Capture(this.Exception).Throw();
            }
            return this._value;
        }
    }
    
    public static implicit operator T(Result<T> result) => result.Value;
    public static implicit operator Result<T>(T value) => new (value);
    
    public void EnsureSuccess()
    {
        if (!this.IsSuccess)
        {
            ExceptionDispatchInfo.Capture(this.Exception).Throw();
        }
    }
}