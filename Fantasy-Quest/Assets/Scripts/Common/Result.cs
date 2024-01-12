using System;

namespace Common
{
    public abstract class Result
    {
        public bool Success { get; protected set; }
        public bool Failure => !Success;

        protected string Err;

        public string Error =>
            Failure
                ? Err
                : throw new Exception(
                    $"You can't access .{nameof(Error)} when .{nameof(Failure)} is false"
                );
    }

    public class SuccessResult : Result
    {
        public SuccessResult()
        {
            Success = true;
        }
    }

    public class FailResult : Result
    {
        public FailResult(string error)
        {
            Success = false;
            base.Err = error;
        }
    }
}
