﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CurrencyConverter.Models
{
    public class CurrencyInfoModel
    {
        public string ccy { get; set; }
        public string base_ccy { get; set; }
        public decimal buy { get; set; }
        public decimal sale { get; set; }
    }
}
