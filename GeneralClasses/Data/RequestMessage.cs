using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GeneralClasses.Data
{
    public class RequestMessage:Message
    {
        public string? TableName { get; set; } = null;

        public int? ObjectId { get; set; } = null;
    }
}
