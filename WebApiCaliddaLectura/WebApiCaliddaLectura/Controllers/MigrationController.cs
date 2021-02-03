using Entidades;
using Entities;
using Negocio;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Web;
using System.Web.Http;
namespace WebApiFenosa.Controllers
{
	[RoutePrefix("api/Migration")]
    public class MigrationController : ApiController
    {

        private static string path = ConfigurationManager.AppSettings["uploadFile"];
        private static string pathCliente = ConfigurationManager.AppSettings["verificateFile"];

        [HttpGet]
        [Route("GetLogin")]
        public IHttpActionResult GetLogin(string user, string password, string version, string imei, string token)
        {
            Login login = ServiciosDA.GetOne(user, password, version, imei, token);

            if (login != null)
            {
                return Ok(login);
            }
            else return NotFound();
        }

        [HttpGet]
        [Route("MigracionAll")]
        public IHttpActionResult MigracionAll(int operarioId, string version)
        {
            try
            {
                return Ok(MigrationDA.GetMigracion(operarioId, version));
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        [HttpGet]
        [Route("SincronizarObservadas")]
        public IHttpActionResult MigracionAll(int operarioId)
        {
            List<Suministro> suministro = MigrationDA.GetObservadas(operarioId);
            return Ok(suministro);
        }

        [HttpGet]
        [Route("VerificarCorte")]
        public IHttpActionResult VerificarCorte(string suministro)
        {
            Mensaje mensaje = MigrationDA.VerificarCorte(suministro);
            return Ok(mensaje);
        }


        [HttpGet]
        [Route("SincronizarCorteReconexion")]
        public IHttpActionResult sincronizarCorteReconexion(int operarioId)
        {
            Sincronizar sync = MigrationDA.Sincronizar(operarioId);
            if (sync.suministrosCortes == null & sync.suministroReconexion == null)
            {
                return NotFound();
            }
            else return Ok(sync);
        }

        [HttpGet]
        [Route("GetOperarioById")]
        public IHttpActionResult GetOperarioById(int operarioId)
        {
            Mensaje operarios = MigrationDA.GetOperarioById(operarioId);
            return Ok(operarios);
        }

        [HttpGet]
        [Route("GetOperarios")]
        public IHttpActionResult GetOperarios()
        {
            List<Operario> operarios = MigrationDA.GetOperarios();
            return Ok(operarios);
        }

        [HttpGet]
        [Route("UpdateOperario")]
        public IHttpActionResult UpdateOperario(int operarioId, int lecturaManual)
        {
            Mensaje mensaje = MigrationDA.UpdateOperario(operarioId, lecturaManual);
            return Ok(mensaje);
        }


        [HttpPost]
        [Route("SaveOperarioGps")]
        public IHttpActionResult SaveOperarioGps(EstadoOperario estadoOperario)
        {
            Mensaje mensaje = ServiciosDA.SaveOperarioGps(estadoOperario);
            return Ok(mensaje);
        }

        [HttpPost]
        [Route("SaveEstadoMovil")]
        public IHttpActionResult SaveEstadoMovil(EstadoMovil estadoMovil)
        {
            Mensaje mensaje = ServiciosDA.SaveEstadoMovil(estadoMovil);
            return Ok(mensaje);
        }


        [HttpPost]
        [Route("SaveNew")]
        public IHttpActionResult SaveRegistroMasivoNew()
        {
            try
            {
                //string path = HttpContext.Current.Server.MapPath("~/Imagen/");
                var fotos = HttpContext.Current.Request.Files;
                var json = HttpContext.Current.Request.Form["model"];
                Registro p = JsonConvert.DeserializeObject<Registro>(json);

                Mensaje mensaje = MigrationDA.SaveRegistroRxNew(p);

                if (mensaje != null)
                {
                    for (int i = 0; i < fotos.Count; i++)
                    {
                        string fileName = Path.GetFileName(fotos[i].FileName);
                        fotos[i].SaveAs(path + fileName);
                    }
                }
                else
                {
                    mensaje = new Mensaje();
                    mensaje.mensaje = "Registro repetido";
                }

                return Ok(mensaje);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveRegistroCorteNew")]
        public IHttpActionResult SaveRegistroCorteNew()
        {
            try
            {
                //string path = HttpContext.Current.Server.MapPath("~/Imagen/");
                var fotos = HttpContext.Current.Request.Files;
                var json = HttpContext.Current.Request.Form["model"];
                var suministro = HttpContext.Current.Request.Form["suministro"];

                Mensaje verificar = MigrationDA.VerificarCorte(suministro);

                if (verificar.codigo == 0)
                {
                    Registro p = JsonConvert.DeserializeObject<Registro>(json);

                    Mensaje mensaje = MigrationDA.SaveRegistroRxNew(p);

                    if (mensaje != null)
                    {
                        for (int i = 0; i < fotos.Count; i++)
                        {
                            string fileName = Path.GetFileName(fotos[i].FileName);
                            fotos[i].SaveAs(path + fileName);
                        }
                    }
                    else
                    {
                        mensaje = new Mensaje();
                        mensaje.mensaje = "Registro repetido";
                    }

                    return Ok(mensaje);
                }
                else
                {
                    return Ok(verificar);
                }
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        [Route("SaveCliente")]
        public IHttpActionResult SaveCliente(GrandesClientes c)
        {
			Mensaje m = MigrationDA.SaveCliente(c);
			if (m != null)				 
				return Ok(m);
			else return BadRequest("Error");			
        }

        [HttpPost]
        [Route("SaveClienteNew")]
        public IHttpActionResult SaveClienteNew()
        {
            try
            {
                //string path = HttpContext.Current.Server.MapPath("~/Imagen/");
                var fotos = HttpContext.Current.Request.Files;
                var json = HttpContext.Current.Request.Form["model"];
                GrandesClientes c = JsonConvert.DeserializeObject<GrandesClientes>(json);
                Mensaje mensaje = MigrationDA.SaveClienteNew(c);
                if (mensaje != null)
                {
                    for (int i = 0; i < fotos.Count; i++)
                    {
                        string fileName = Path.GetFileName(fotos[i].FileName);
                        fotos[i].SaveAs(path + fileName);
                    }
                    return Ok(mensaje);
                }
                else
                    return BadRequest("Error");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("VerificateFileCliente")]
        public IHttpActionResult VerificateFileCliente(int id)
        {
            try
            {
                Mensaje verificar = MigrationDA.VerificarCliente(id);
                if (verificar.codigo != 0)
                {
                    return Ok(verificar);
                }
                else
                    return BadRequest("No Existen Archivos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet]
        [Route("VerificateFileClienteNew")]
        public IHttpActionResult VerificateFileCliente(int id, string fecha)
        {
            try
            {
                Mensaje verificar = MigrationDA.VerificarCliente(id);
                if (verificar.codigo != 0)
                {
                    return Ok(verificar);
                }
                else
                    return BadRequest("No Existen Archivos");
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
		
		
		[HttpPost]
        [Route("SavePhotos")]
        public IHttpActionResult SaveInspeccionesPhoto()
        {
            try
            {
                var files = HttpContext.Current.Request.Files;
                for (int i = 0; i < files.Count; i++)
                {
                    string fileName = Path.GetFileName(files[i].FileName);
                    files[i].SaveAs(path + fileName);
                }
                return Ok("Enviado");
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
