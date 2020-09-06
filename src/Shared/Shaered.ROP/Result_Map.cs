using System;
using System.Runtime.ExceptionServices;
using System.Threading.Tasks;

namespace WebPersonal.Shared.ROP
{
    public static class Result_Map
    {
        public static Result<U> Map<T, U>(this Result<T> r, Func<T, U> mapper)
        {
            try
            {
                return r.Success
                    ? mapper(r.Value).Success()
                    : Result.Failure<U>(r.Errors);
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
        }
        public static async Task<Result<U>> MapAsync<T, U>(this Task<Result<T>> result, Func<T, U> mapper)
        {
            try
            {
                var r = await result;
                return r.Success
                    ? (mapper(r.Value)).Success()
                    : Result.Failure<U>(r.Errors);
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
        }
        public static async Task<Result<U>> MapAsync<T, U>(this Task<Result<T>> result, Func<T, Task<U>> mapper)
        {
            try
            {
                var r = await result;
                return r.Success
                    ? (await mapper(r.Value)).Success()
                    : Result.Failure<U>(r.Errors);
            }
            catch (Exception e)
            {
                ExceptionDispatchInfo.Capture(e).Throw();
                throw;
            }
        }
    }
}
