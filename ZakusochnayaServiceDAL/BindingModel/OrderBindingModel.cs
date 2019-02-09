﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaServiceDAL.BindingModels
{
    public class OrderBindingModel
    {
        public int Id { get; set; }
        public int ClientId { get; set; }
        public int ProductId { get; set; }
        public int Count { get; set; }
        public decimal Sum { get; set; }
    }
}
