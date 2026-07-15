using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerce.Application.Common
{
    public class Result
    {
        public bool IsSuccess { get; }
        public IReadOnlyList<Error> Errors { get; }
        protected Result(bool IsSuccess , IReadOnlyList<Error> Errors)
        {
            this.IsSuccess = IsSuccess;
            this.Errors = Errors;
        }
        public static Result Ok() => new(true , Array.Empty<Error>());
        public static Result Fail(Error Error) => new(false , new[] { Error });
        public static Result Fail(IReadOnlyList<Error> Errors) => new(false , Errors);

    }
    public class Result<TValue> : Result

    {
        private readonly TValue? value;

        public TValue? data => IsSuccess ? value : throw new InvalidOperationException("Cannot access Value when the result is a failure.");

        private Result(TValue? value) : base(true, Array.Empty<Error>())
        {
            this.value = value ;
           
        }
        private Result(Error Error) : base(false , new[] { Error })
        {
            value = default!;
        }
        private Result(IReadOnlyList<Error> Errors) : base(false , Errors)
        {
            value = default!;
        }
        public static Result<TValue> Ok(TValue Value) => new Result<TValue>( Value );
        public static Result<TValue> Fail(Error Error) => new Result<TValue>(Error);
        public static  Result<TValue> Fail(IReadOnlyList<Error> Errors) => new Result<TValue>(Errors);
        
        public static implicit operator Result<TValue>(TValue value) => Ok(value);
        public static implicit operator Result<TValue>(Error error) => Fail(error);
    }
}
