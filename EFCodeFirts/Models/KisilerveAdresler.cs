using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace EFCodeFirts.Models
{
    public class KisilerveAdresler
    {
        public List<Kisiler> Kisiler { get; set; }
        public List<Adresler> Adresler { get; set; }
    }
}