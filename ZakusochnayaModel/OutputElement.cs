using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ZakusochnayaModel
{
    /// <summary>
    /// Сколько компонентов, требуется при изготовлении изделия
    /// </summary>
    public class OutputElement
    {
        public int OutputElementId { get; set; }
        public int OutputId { get; set; }
        public int ElementId { get; set; }
        public int Number { get; set; }
    }
}
