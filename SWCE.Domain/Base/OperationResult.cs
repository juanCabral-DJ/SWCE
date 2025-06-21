﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SWCE.Domain.Base
{
    public class OperationResult
    {  
        public string Message { get; set; } = string.Empty;
        public bool IsSuccess { get; set; }
        public dynamic Data { get; set; }

        public OperationResult()
        {

        }
        public OperationResult(bool isSuccess, string message, dynamic? data = null)
        {
            IsSuccess = isSuccess;
            Message = message;
            Data = data;
        }
      
        public static OperationResult Success(string message, dynamic? data = null)
        {
            return new OperationResult(true, message, data);
        }
        public static OperationResult Failure(string message)
        {
            return new OperationResult(false, message);
        }

        public async Task<OperationResult> failure()
        {
            throw new NotImplementedException();
        }
    }
}

