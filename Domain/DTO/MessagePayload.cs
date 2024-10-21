using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.DTO
{
    public class MessagePayload<T>
    {
        public EResponse Response;
        public int Status {  get; set; }
        public T Payload { get; set; }
        public string ErrorCode { get; set; }

        public string Token { get; set; }

    }

   public enum EResponse
    {
        Success,
        Error
    }
}
