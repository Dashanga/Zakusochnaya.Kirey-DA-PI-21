﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Изделие, изготавливаемое в магазине
    /// </summary>
    public class Output
    {
        public int Id { get; set; }
        public string OutputName { get; set; }
        public decimal Cost { get; set; }
    }
}
