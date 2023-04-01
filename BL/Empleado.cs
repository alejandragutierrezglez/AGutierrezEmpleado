using Microsoft.EntityFrameworkCore;
using ML;

namespace BL
{
    public class Empleado
    {
        public static ML.Result GetAll()
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezEmpleadoContext context = new DL.AgutierrezEmpleadoContext())
                {
                    var query = context.Empleados.FromSqlRaw("EmpleadoGetAll").ToList();

                    if (query != null)
                    {
                        result.Objects = new List<object>();

                        foreach (var obj in query)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = obj.IdEmpleado;
                            empleado.NumeroNomina = obj.NumeroNomina;
                            empleado.Nombre = obj.Nombre;
                            empleado.ApellidoPaterno = obj.ApellidoPaterno;
                            empleado.ApellidoMaterno = obj.ApellidoMaterno;

                            empleado.CatEntidadFederativa = new ML.CatEntidadFederativa();
                            empleado.CatEntidadFederativa.IdEstado = obj.IdEstado.Value;
                            empleado.CatEntidadFederativa.Estado = obj.Estado;


                            result.Objects.Add(empleado);

                        }
                    }
                }
                result.Correct = true;

            }
            catch (Exception ex)
            {
                result.Ex = ex;
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result GetById(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            {
                try
                {
                    using (DL.AgutierrezEmpleadoContext context = new DL.AgutierrezEmpleadoContext())
                    {
                        var query = context.Empleados.FromSqlRaw($"EmpleadoGetById {IdEmpleado}").AsEnumerable().FirstOrDefault();

                        if (query != null)
                        {
                            ML.Empleado empleado = new ML.Empleado();
                            empleado.IdEmpleado = query.IdEmpleado;
                            empleado.NumeroNomina = query.NumeroNomina;
                            empleado.Nombre = query.Nombre;
                            empleado.ApellidoPaterno = query.ApellidoPaterno;
                            empleado.ApellidoMaterno = query.ApellidoMaterno;

                            empleado.CatEntidadFederativa = new ML.CatEntidadFederativa();
                            empleado.CatEntidadFederativa.IdEstado = query.IdEstado.Value;
                            empleado.CatEntidadFederativa.Estado = query.Estado;

                            result.Object = empleado;

                            result.Correct = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                    result.Correct = false;
                    result.Ex = ex;
                    result.ErrorMessage = ex.Message;
                }
                return result;
            }

        }

        public static ML.Result Add(ML.Empleado empleado)
        {
            ML.Result result = new ML.Result();

            try
            {
                using (DL.AgutierrezEmpleadoContext context = new DL.AgutierrezEmpleadoContext())
                {
                    // var query = context.UsuarioAdd(usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.UserName, usuario.Passwordd, DateTime.Parse(usuario.FechaNacimiento), usuario.Rol.IdRol, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoAdd '{empleado.Nombre}', '{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}',{empleado.CatEntidadFederativa.IdEstado}");

                    if (query >= 1)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                        result.ErrorMessage = "No se insertó el registro";
                    }

                    result.Correct = true;

                }
            }
            catch (Exception ex)
            {
                result.Correct = false;
                result.ErrorMessage = ex.Message;
            }
            return result;
        }

        public static ML.Result Update(ML.Empleado empleado)
        {
            using (DL.AgutierrezEmpleadoContext context = new DL.AgutierrezEmpleadoContext())
            {
                Result result = new ML.Result();
                try
                {

                    //var query = context.UsuarioUpdate(usuario.IdUsuario, usuario.Nombre, usuario.ApellidoPaterno, usuario.ApellidoMaterno, usuario.Email, usuario.UserName, usuario.Passwordd, DateTime.Parse(usuario.FechaNacimiento), usuario.Rol.IdRol, usuario.Sexo, usuario.Telefono, usuario.Celular, usuario.CURP, usuario.Imagen, usuario.Direccion.Calle, usuario.Direccion.NumeroInterior, usuario.Direccion.NumeroExterior, usuario.Direccion.Colonia.IdColonia);
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoUpdate {empleado.IdEmpleado}, '{empleado.Nombre}','{empleado.ApellidoPaterno}','{empleado.ApellidoMaterno}',{empleado.CatEntidadFederativa.IdEstado}");
                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
                catch (Exception ex)
                {

                    result.ErrorMessage = ex.Message;
                    result.Ex = ex;
                    result.Correct = false;
                }
                return result;

            }
        }

        public static ML.Result Delete(int IdEmpleado)
        {
            ML.Result result = new ML.Result();
            try
            {
                using (DL.AgutierrezEmpleadoContext context = new DL.AgutierrezEmpleadoContext())
                {
                    var query = context.Database.ExecuteSqlRaw($"EmpleadoDelete {IdEmpleado}");

                    if (query > 0)
                    {
                        result.Correct = true;
                    }
                    else
                    {
                        result.Correct = false;
                    }
                }
            }
            catch (Exception ex)
            {
                result.ErrorMessage = ex.Message;
                result.Ex = ex;
                result.Correct = false;
            }
            return result;
        }
    }
}