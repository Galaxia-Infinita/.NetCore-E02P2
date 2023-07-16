using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LabWeb06_ConsumirApiCore.Entidad
{
    public class PedidosporEmpleado
    {
        public int IdPedidos { get; set; }
        public string fechaOrden { get; set; }
        public int Anio { get; set; }
        public int CodEmp { get; set; }
        public string Apellidos { get; set; }
        public string CiudadEnvio { get; set; }
    }
}
