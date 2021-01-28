using Entidades;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocio
{
    public class ServiciosDA
    {
        private static readonly string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public static Login GetOne(string user, string password, string version, string imei, string token)
        {
            try
            {
                Login item = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_ACCESO_LOGIN", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@User", SqlDbType.VarChar).Value = user;
                        cmd.Parameters.Add("@version", SqlDbType.VarChar).Value = version;
                        cmd.Parameters.Add("@imei", SqlDbType.VarChar).Value = imei;
                        cmd.Parameters.Add("@token", SqlDbType.VarChar).Value = token;
                        SqlDataReader dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            item = new Login();
                            if (password == dr.GetString(2))
                            {
                                item.iD_Operario = Convert.ToInt32(dr["ID_Operario"]);
                                item.operario_Login = dr["Operario_Login"].ToString();
                                item.operario_Contrasenia = dr["Operario_Contrasenia"].ToString();
                                item.operario_Nombre = dr["Operario_Nombre"].ToString();
                                item.operario_EnvioEn_Linea = Convert.ToInt32(dr["Operario_EnvioEn_Linea"]);
                                item.tipoUsuario = dr["TipoUsuario"].ToString();
                                item.estado = dr["estado"].ToString();
                                item.lecturaManual = Convert.ToInt32(dr["LecturaManual"]);
                                item.mensaje = "Go";
                            }
                            else
                            {
                                item.mensaje = "Pass";
                            }
                        }
                    }
                    cn.Close();
                }
                return item;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static List<Servicio> GetServicio()
        {
            try
            {
                List<Servicio> servicio = new List<Servicio>();
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("MOVIL_Servicios", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            foreach (DataRow dr in dt_detalle.Rows)
                            {
                                Servicio rp = new Servicio();
                                rp.id_servicio = Convert.ToInt32(dr["id_servicio"]);
                                rp.estado = Convert.ToInt32(dr["estado"]);
                                rp.nombre_servicio = dr["nombre_servicio"].ToString();
                                servicio.Add(rp);
                            }
                        }
                    }
                }
                return servicio;

            }
            catch (Exception e)
            {
                throw e;
            }
        }
 
        public static Mensaje SaveEstadoMovil(EstadoMovil e)
        {
            try
            {
                Mensaje m = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "USP_SAVE_ESTADOCELULAR";
                    cmd.Parameters.Add("@operarioId", SqlDbType.Int).Value = e.operarioId;
                    cmd.Parameters.Add("@gpsActivo", SqlDbType.Bit).Value = e.gpsActivo;
                    cmd.Parameters.Add("@estadoBateria", SqlDbType.Int).Value = e.estadoBateria;
                    cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = e.fecha;
                    cmd.Parameters.Add("@modoAvion", SqlDbType.Int).Value = e.modoAvion;
                    cmd.Parameters.Add("@planDatos", SqlDbType.Bit).Value = e.planDatos;

                    int a = cmd.ExecuteNonQuery();
                    if (a == 1)
                    {
                        m = new Mensaje();
                        m.codigo = 1;
                        m.mensaje = "Enviado";
                    }

                    cn.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje SaveOperarioGps(EstadoOperario e)
        {
            try
            {
                Mensaje m = new Mensaje();
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SAVE_GPS";
                    cmd.Parameters.Add("@operarioId", SqlDbType.Int).Value = e.operarioId;
                    cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = e.latitud;
                    cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = e.longitud;
                    cmd.Parameters.Add("@fechaGPD", SqlDbType.VarChar).Value = e.fechaGPD;
                    cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = e.fecha;

                    int a = cmd.ExecuteNonQuery();

                    if (a == 1)
                    {
                        m = new Mensaje();
                        m.codigo = 1;
                        m.mensaje = "Enviado";
                    }

                    cn.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
              
    }
}
