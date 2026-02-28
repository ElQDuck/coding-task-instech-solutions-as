using System.Diagnostics.CodeAnalysis;
using System.Runtime.ExceptionServices;

namespace Claims.BusinessLogic.Entities;


/// <summary>
/// The result of an operation which can be successful or fail.
/// </summary>
/// <remarks>https://github.com/ardalis/Result</remarks>
public class Result: Result<object?>
{
   /// <summary>
   /// Initialize a new instance of <see cref="Result"/>.
   /// </summary>
   private Result() : base((object?)null) { }
   
   /// <summary>
   /// Initialize a new instance of <see cref="Result"/>.
   /// </summary>
   /// <param name="exception"></param>
   private Result(Exception exception) : base(exception) { }

   /// <summary>
   /// Creates a successful result from the operations result.
   /// </summary>
   /// <param name="value"></param>
   /// <typeparam name="T"></typeparam>
   /// <returns>The result with a value.</returns>
   public static Result<T> FromSuccess<T>(T value)
   {
      return new Result<T>(value);
   }
   
   /// <summary>
   /// Creates a successful result.
   /// </summary>
   /// <returns>The result.</returns>
   public static Result FromSuccess()
   {
      return new Result();
   }
   
   /// <summary>
   /// Creates a non successful result from the operations exception.
   /// </summary>
   /// <param name="exception"></param>
   /// <typeparam name="T"></typeparam>
   /// <returns>The exception.</returns>
   public static Result<T> FromException<T>(Exception exception)
   {
      return new Result<T>(exception);
   }

   /// <summary>
   /// Creates a non successful result from an operation.
   /// </summary>
   /// <param name="exception"></param>
   /// <returns></returns>
   public static Result FromException(Exception exception)
   {
      return new Result(exception);
   }
}