﻿namespace Application.Common
{
    public class OperationResult
    {
        public bool IsSucceeded { get; set; }
        public string? Message { get; set; }
        public object? ReturnValue { get; set; }

        public OperationResult()
        {
            IsSucceeded = false;
        }

        public OperationResult Succeeded(string message = "Successfully")
        {
            IsSucceeded = true;
            Message = message;
            return this;
        }

        public OperationResult Succeeded(object returnValue, string message)
        {
            IsSucceeded = true;
            Message = message;
            ReturnValue = returnValue;
            return this;
        }

        public OperationResult Failed(string message)
        {
            IsSucceeded = false;
            Message = message;
            return this;
        }

        //public OperationResult Failed(object passwordsNotMatch)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
