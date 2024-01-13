using System;

namespace Common
{
    public abstract class ValueResult<T>
    {
        public bool Success { get; protected set; }
        public bool Failure => !Success;

        protected string Err;
        protected T Data_;

        public string Error =>
            Failure
                ? Err
                : throw new Exception(
                    $"You can't access .{nameof(Error)} when .{nameof(Failure)} is false"
                );
        public T Data =>
            Success
                ? Data_
                : throw new Exception(
                    $"You can't access .{nameof(Data)} when .{nameof(Success)} is false"
                );
    }

    public class SuccessValueResult<T> : ValueResult<T>
    {
        public SuccessValueResult(T data)
        {
            Success = true;
            base.Data_ = data;
        }
    }

    public class FailValueResult<T> : ValueResult<T>
    {
        public FailValueResult(string error)
        {
            Success = false;
            base.Err = error;
        }
    }
}
