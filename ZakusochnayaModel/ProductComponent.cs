﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class ProductComponent
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int ComponentId { get; set; }
        public int Count { get; set; }
    }
}