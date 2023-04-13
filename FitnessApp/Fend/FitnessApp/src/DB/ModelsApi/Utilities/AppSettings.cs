using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ModelsApi.Utilities
{
    public class AppSettings
    {
        public string SecretKey { get; set; }
        public int BcryptWorkfactor { get; set; }
    }
}
