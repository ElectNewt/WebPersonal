using System.Collections.Generic;

namespace WebPersonal.Shared.ROP
{
    public class ResultDto<T>
    {
        public T Value { get; set; }
        public List<ErrorDto> Errors { get; set; }
        public bool Success => Errors.Count > 0;

    }

    public class ErrorDto
    {
        public string Message { get; set; }
        public int? ErrorCode { get; set; }

        public Error ToError()
        {
            return Error.Create(Message, ErrorCode);
        }

        public static ErrorDto FromError(Error error)
        {
            return new ErrorDto()
            {
                Message = error.Message,
                ErrorCode = error.ErrorCode,
            };

        }

    }
}
