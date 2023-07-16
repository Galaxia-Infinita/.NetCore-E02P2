using LabWeb06_ConsumirApiCore.Entidad;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LabWeb06_ConsumirApiCore.Controllers
{
    public class ConsultasController : Controller
    {
        public async Task<IActionResult> Index(int? id)
        {   //2 listar genericas
            List<ListarEmpleados> empleados = new List<ListarEmpleados>();
            List<PedidosporEmpleado> pedidosporEmpleados = new List<PedidosporEmpleado>();
            using (var httpCliente = new HttpClient()) 
            {
                using (var resp = await httpCliente.
                    GetAsync("https://localhost:44385/api/VentasIDAT/Empleados")) 
                {   //obteniendo la informacion en JSON(texto)
                    string respApi = await resp.Content.ReadAsStringAsync();
                    empleados = JsonConvert.DeserializeObject<List<ListarEmpleados>>(respApi);
                }
                if(id!=null) //si se ha enviado un codigo de empleado
                {
                    using (var resp = await httpCliente.
                    GetAsync("https://localhost:44385/api/VentasIDAT/PedidosporEmplado/"+id))
                    {   //obteniendo la informacion en JSON(texto)
                        string respApi = await resp.Content.ReadAsStringAsync();
                        pedidosporEmpleados = JsonConvert.DeserializeObject<List<PedidosporEmpleado>>(respApi);
                        ViewBag.nrofilas = pedidosporEmpleados.Count();
                    }
                }
                ViewBag.empleados = new SelectList(empleados, "CodEmp", "empleado", id);
            }
            return View(pedidosporEmpleados);
        }

        //está correcto
        public async Task<IActionResult> CodigoyMes(int? id, int? mes) 
        {
            List<ListarEmpleados> empleados = new List<ListarEmpleados>();
            List<ListarMes> nmes = new List<ListarMes>();
            List<PedidosCodigoyMes> CodigoMes = new List<PedidosCodigoyMes>();
            using (var httpCliente = new HttpClient()) 
            {
                using (var resp = await httpCliente.
                    GetAsync("https://localhost:44385/api/VentasIDAT/Empleados"))
                {   //obteniendo la informacion en JSON(texto)
                    string respApi = await resp.Content.ReadAsStringAsync();
                    empleados = JsonConvert.DeserializeObject<List<ListarEmpleados>>(respApi);
                }
                using (var r = await httpCliente.
                    GetAsync("https://localhost:44385/api/VentasIDAT/Meses"))
                {   //obteniendo la informacion en JSON(texto)
                    string respApi = await r.Content.ReadAsStringAsync();
                    nmes = JsonConvert.DeserializeObject<List<ListarMes>>(respApi);
                }
                if (id != null && mes != null) 
                {
                    using (var r = await httpCliente.
                        GetAsync("https://localhost:44385/api/VentasIDAT/PedidosporEmpladoyMes/" + id + "/" + mes)) 
                    {
                        string rA = await r.Content.ReadAsStringAsync();
                        CodigoMes = JsonConvert.DeserializeObject<List<PedidosCodigoyMes>>(rA);
                        ViewBag.nro = CodigoMes.Count();
                    }
                }
                ViewBag.em = new SelectList(nmes, "numMes", "numMes", mes);
                ViewBag.empleados = new SelectList(empleados, "CodEmp", "empleado", id);
            }
            return View(CodigoMes);
        }
    }
}
