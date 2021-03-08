using System;
using System.Collections.Generic;
using System.Text;

namespace ApiWithbasicAuthentication.Common
{    public class StudentParameters : QueryStringParameters
    {
        public string StudentName { get; set; }
        public string GradeName { get; set; }

    }
}
