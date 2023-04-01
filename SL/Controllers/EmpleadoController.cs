using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

using System.Net;

namespace SL.Controllers
{
    public class EmpleadoController : Controller
    {
        [EnableCors("API")]
        [HttpGet]
        [Route("api/Empleado/GetAll")]
        public IActionResult GetAll()
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result result = BL.Empleado.GetAll();
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
        [EnableCors("API")]
        [HttpGet]
        [Route("api/Empleado/GetById/{IdEmpleado}")]
        public IActionResult GetById(int IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();

            ML.Result result = BL.Empleado.GetById(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }
        [EnableCors("API")]
        [HttpPost]
        [Route("api/Empleado/Add")]
        public ActionResult Add([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Add(empleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }

        [EnableCors("API")]
        [HttpPost]
        [Route("api/Empleado/Update")]
        public ActionResult Update([FromBody] ML.Empleado empleado)
        {
            ML.Result result = BL.Empleado.Update(empleado);

            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
        [EnableCors("API")]
        [HttpGet]
        [Route("api/Empleado/Delete/{IdEmpleado}")]
        public ActionResult Delete(int IdEmpleado)
        {
            ML.Empleado empleado = new ML.Empleado();
            empleado.IdEmpleado = IdEmpleado;
            ML.Result result = BL.Empleado.Delete(IdEmpleado);
            if (result.Correct)
            {
                return Ok(result);
            }
            else
            {
                return NotFound(result);
            }
        }
    }
}
