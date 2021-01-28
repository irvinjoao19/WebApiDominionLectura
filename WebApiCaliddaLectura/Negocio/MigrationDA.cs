using Entidades;
using Entities;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;

namespace Negocio
{
    public class MigrationDA
    {
        private static readonly string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        //public static string path = "C:/HostingSpaces/admindsige/www.dsige.com/wwwroot/Calidda/Content/foto/foto/";

        public static Mensaje VerificarCorte(string suministro)
        {
            try
            {
                Mensaje mensaje = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_VERIFICAR_CORTE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@Suministro", SqlDbType.VarChar).Value = suministro;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                mensaje = new Mensaje()
                                {
                                    codigo = rd.GetInt32(0),
                                    mensaje = (rd.GetInt32(0) == 0 ? "Enviado" : "No puedes cortar")
                                };
                            }
                        }
                        rd.Close();
                    }
                }
                return mensaje;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Migracion GetMigracion(int operarioId, string version)
        {
            try
            {
                Migracion migracion = new Migracion();

                migracion.migrationId = 1;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    // Version

                    SqlCommand cmdVersion = con.CreateCommand();
                    cmdVersion.CommandTimeout = 0;
                    cmdVersion.CommandType = CommandType.StoredProcedure;
                    cmdVersion.CommandText = "USP_GET_VERSION";
                    cmdVersion.Parameters.Add("@version", SqlDbType.VarChar).Value = version;

                    SqlDataReader drVersion = cmdVersion.ExecuteReader();
                    if (!drVersion.HasRows)
                    {
                        migracion.mensaje = "Actualizar Versión del Aplicativo.";
                    }
                    else
                    {
                        // Servicios
                        SqlCommand cmdServicio = con.CreateCommand();
                        cmdServicio.CommandTimeout = 0;
                        cmdServicio.CommandType = CommandType.StoredProcedure;
                        cmdServicio.CommandText = "USP_SERVICIOS";
                        SqlDataReader drServicio = cmdServicio.ExecuteReader();
                        if (drServicio.HasRows)
                        {
                            List<Servicio> servicio = new List<Servicio>();
                            while (drServicio.Read())
                            {
                                servicio.Add(new Servicio()
                                {
                                    id_servicio = drServicio.GetInt32(0),
                                    nombre_servicio = drServicio.GetString(1),
                                    estado = drServicio.GetInt32(2),
                                    ubicacion = drServicio.GetInt32(3)
                                });
                            }
                            migracion.servicios = servicio;
                        }

                        // Parametro

                        SqlCommand cmdParametro = con.CreateCommand();
                        cmdParametro.CommandTimeout = 0;
                        cmdParametro.CommandType = CommandType.StoredProcedure;
                        cmdParametro.CommandText = "USP_PARAMETROS";
                        SqlDataReader drParametro = cmdParametro.ExecuteReader();
                        if (drParametro.HasRows)
                        {
                            List<Parametro> parametro = new List<Parametro>();
                            while (drParametro.Read())
                            {
                                parametro.Add(new Parametro()
                                {
                                    id_Configuracion = drParametro.GetInt32(0),
                                    nombre_parametro = drParametro.GetString(1),
                                    valor = drParametro.GetInt32(2)
                                });
                            }
                            migracion.parametros = parametro;
                        }

                        // Suministro
                        SqlCommand cmdSuministro = con.CreateCommand();
                        cmdSuministro.CommandTimeout = 0;
                        cmdSuministro.CommandType = CommandType.StoredProcedure;
                        cmdSuministro.CommandText = "USP_LIST_SUMINISTRO";
                        cmdSuministro.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = operarioId;
                        SqlDataReader drSuministro = cmdSuministro.ExecuteReader();
                        if (drSuministro.HasRows)
                        {
                            List<Suministro> suministro = new List<Suministro>();
                            int i = 1;
                            while (drSuministro.Read())
                            {
                                suministro.Add(new Suministro()
                                {
                                    iD_Suministro = drSuministro.GetInt32(0),
                                    suministro_Numero = drSuministro.GetString(1),
                                    suministro_Medidor = drSuministro.GetString(2),
                                    suministro_Cliente = drSuministro.GetString(3),
                                    suministro_Direccion = drSuministro.GetString(4),
                                    suministro_UnidadLectura = drSuministro.GetString(5),
                                    suministro_TipoProceso = drSuministro.GetString(6),
                                    suministro_LecturaMinima = drSuministro.GetString(7),
                                    suministro_LecturaMaxima = drSuministro.GetString(8),
                                    suministro_Fecha_Reg_Movil = drSuministro.GetDateTime(9).ToString("dd/MM/yyyy"),
                                    suministro_UltimoMes = drSuministro.GetString(10),
                                    consumoPromedio = drSuministro.GetDecimal(11),
                                    lecturaAnterior = drSuministro.GetString(12),
                                    suministro_Instalacion = drSuministro.GetString(13),
                                    valida1 = drSuministro.GetInt32(14),
                                    valida2 = drSuministro.GetInt32(15),
                                    valida3 = drSuministro.GetInt32(16),
                                    valida4 = drSuministro.GetInt32(17),
                                    valida5 = drSuministro.GetInt32(18),
                                    valida6 = drSuministro.GetInt32(19),
                                    tipoCliente = drSuministro.GetInt32(20),
                                    estado = drSuministro.GetInt32(21),
                                    suministroOperario_Orden = drSuministro.GetInt32(22),
                                    flagObservada = drSuministro.GetInt32(23),
                                    latitud = drSuministro.GetString(24),
                                    longitud = drSuministro.GetString(25),
                                    telefono = drSuministro.GetString(26),
                                    nota = drSuministro.GetString(27),
                                    fechaAsignacion = drSuministro.GetString(28),
                                    orden = i++,
                                    activo = 1
                                });
                            }
                            migracion.suministroLecturas = suministro;
                        }

                        // SuministroCorte 
                        SqlCommand cmdCortes = con.CreateCommand();
                        cmdCortes.CommandTimeout = 0;
                        cmdCortes.CommandType = CommandType.StoredProcedure;
                        cmdCortes.CommandText = "USP_LIST_SUMINISTRO_CORTES";
                        cmdCortes.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = operarioId;
                        cmdCortes.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = "3";
                        SqlDataReader drCortes = cmdCortes.ExecuteReader();
                        if (drCortes.HasRows)
                        {
                            List<Suministro> suministroCorte = new List<Suministro>();
                            int y = 1;
                            while (drCortes.Read())
                            {
                                suministroCorte.Add(new Suministro()
                                {
                                    iD_Suministro = drCortes.GetInt32(0),
                                    suministro_Numero = drCortes.GetString(1),
                                    suministro_Medidor = drCortes.GetString(2),
                                    suministro_Cliente = drCortes.GetString(3),
                                    suministro_Direccion = drCortes.GetString(4),
                                    suministro_UnidadLectura = drCortes.GetString(5),
                                    suministro_TipoProceso = drCortes.GetString(6),
                                    suministro_LecturaMinima = drCortes.GetString(7),
                                    suministro_LecturaMaxima = drCortes.GetString(8),
                                    suministro_Fecha_Reg_Movil = drCortes.GetDateTime(9).ToString("dd/MM/yyyy"),
                                    suministro_UltimoMes = drCortes.GetString(10),
                                    suministro_NoCortar = drCortes.GetInt32(11),
                                    estado = drCortes.GetInt32(12),
                                    suministroOperario_Orden = drCortes.GetInt32(13),
                                    latitud = drCortes.GetString(14),
                                    longitud = drCortes.GetString(15),
                                    telefono = "",
                                    nota = "",
                                    fechaAsignacion = drCortes.GetString(18),
                                    primeraReconexion = drCortes.GetString(19),
                                    avisoCorte = drCortes.GetString(20),
                                    orden = y++,
                                    activo = 1
                                });
                            }
                            migracion.suministroCortes = suministroCorte;
                        }

                        // SuministroReconexiones
                        SqlCommand cmdReconexiones = con.CreateCommand();
                        cmdReconexiones.CommandTimeout = 0;
                        cmdReconexiones.CommandType = CommandType.StoredProcedure;
                        cmdReconexiones.CommandText = "USP_LIST_SUMINISTRO_CORTES";
                        cmdReconexiones.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = operarioId;
                        cmdReconexiones.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = "4";

                        SqlDataReader drReconexiones = cmdReconexiones.ExecuteReader();
                        if (drReconexiones.HasRows)
                        {
                            List<Suministro> suministroReconexiones = new List<Suministro>();
                            int z = 1;
                            while (drReconexiones.Read())
                            {
                                suministroReconexiones.Add(new Suministro()
                                {
                                    iD_Suministro = drReconexiones.GetInt32(0),
                                    suministro_Numero = drReconexiones.GetString(1),
                                    suministro_Medidor = drReconexiones.GetString(2),
                                    suministro_Cliente = drReconexiones.GetString(3),
                                    suministro_Direccion = drReconexiones.GetString(4),
                                    suministro_UnidadLectura = drReconexiones.GetString(5),
                                    suministro_TipoProceso = drReconexiones.GetString(6),
                                    suministro_LecturaMinima = drReconexiones.GetString(7),
                                    suministro_LecturaMaxima = drReconexiones.GetString(8),
                                    suministro_Fecha_Reg_Movil = drReconexiones.GetDateTime(9).ToString("dd/MM/yyyy"),
                                    suministro_UltimoMes = drReconexiones.GetString(10),
                                    suministro_NoCortar = drReconexiones.GetInt32(11),
                                    estado = drReconexiones.GetInt32(12),
                                    suministroOperario_Orden = drReconexiones.GetInt32(13),
                                    latitud = drReconexiones.GetString(14),
                                    longitud = drReconexiones.GetString(15),
                                    telefono = drReconexiones.GetString(16),
                                    nota = drReconexiones.GetString(17),
                                    fechaAsignacion = drReconexiones.GetString(18),
                                    primeraReconexion = drReconexiones.GetString(19),
                                    avisoCorte = drReconexiones.GetString(20),
                                    orden = z++,
                                    activo = 1
                                });
                            }
                            migracion.suministroReconexiones = suministroReconexiones;
                        }

                        // Tipo Lectura

                        SqlCommand cmdTipo = con.CreateCommand();
                        cmdTipo.CommandTimeout = 0;
                        cmdTipo.CommandType = CommandType.StoredProcedure;
                        cmdTipo.CommandText = "USP_LIST_TIPO_LECTURA";
                        SqlDataReader drTipo = cmdTipo.ExecuteReader();
                        if (drTipo.HasRows)
                        {
                            List<TipoLectura> tipoLectura = new List<TipoLectura>();
                            while (drTipo.Read())
                            {
                                tipoLectura.Add(new TipoLectura()
                                {
                                    iD_TipoLectura = drTipo.GetInt32(0),
                                    tipoLectura_Descripcion = drTipo.GetString(1),
                                    tipoLectura_Abreviatura = drTipo.GetString(2),
                                    tipoLectura_Estado = drTipo.GetString(3)
                                });
                            }
                            migracion.tipoLecturas = tipoLectura;
                        }

                        //Detalle Grupo

                        SqlCommand cmdGrupo = con.CreateCommand();
                        cmdGrupo.CommandTimeout = 0;
                        cmdGrupo.CommandType = CommandType.StoredProcedure;
                        cmdGrupo.CommandText = "USP_LIST_DETALLE_GRUPO";
                        SqlDataReader rdGrupo = cmdGrupo.ExecuteReader();
                        if (rdGrupo.HasRows)
                        {
                            List<DetalleGrupo> detalleGrupo = new List<DetalleGrupo>();
                            int j = 1;
                            while (rdGrupo.Read())
                            {
                                detalleGrupo.Add(new DetalleGrupo()
                                {
                                    id = j++,
                                    iD_DetalleGrupo = rdGrupo.GetInt32(0),
                                    grupo = rdGrupo.GetString(1),
                                    codigo = rdGrupo.GetString(2),
                                    descripcion = rdGrupo.GetString(3),
                                    abreviatura = rdGrupo.GetString(4),
                                    estado = rdGrupo.GetString(5),
                                    descripcionGrupo = rdGrupo.GetString(6),
                                    pideFoto = rdGrupo.GetString(7),
                                    noPideFoto = rdGrupo.GetString(8),
                                    pideLectura = rdGrupo.GetString(9),
                                    id_Servicio = rdGrupo.GetInt32(10),
                                    parentId = rdGrupo.GetInt32(11),
                                    ubicaMedidor = rdGrupo.GetInt32(12)
                                });
                            }
                            migracion.detalleGrupos = detalleGrupo;
                        }
                        // Reparto

                        SqlCommand cmdReparto = con.CreateCommand();
                        cmdReparto.CommandTimeout = 0;
                        cmdReparto.CommandType = CommandType.StoredProcedure;
                        cmdReparto.CommandText = "USP_LIST_REPARTO";
                        cmdReparto.Parameters.AddWithValue("@id_operario_reparto", operarioId);
                        SqlDataReader drReparto = cmdReparto.ExecuteReader();
                        if (drReparto.HasRows)
                        {
                            List<Reparto> reparto = new List<Reparto>();
                            while (drReparto.Read())
                            {
                                reparto.Add(new Reparto()
                                {
                                    id_Reparto = drReparto.GetInt32(0),
                                    id_Operario_Reparto = drReparto.GetInt32(1),
                                    foto_Reparto = drReparto.GetInt32(2),
                                    estado = drReparto.GetInt32(3),
                                    activo = drReparto.GetInt32(4),
                                    Suministro_Medidor_reparto = drReparto.GetString(5),
                                    Suministro_Numero_reparto = drReparto.GetString(6),
                                    Cod_Actividad_Reparto = drReparto.GetString(7),
                                    Cod_Orden_Reparto = drReparto.GetString(8),
                                    Direccion_Reparto = drReparto.GetString(9),
                                    Cliente_Reparto = drReparto.GetString(10),
                                    CodigoBarra = drReparto.GetString(11),
                                    latitud = drReparto.GetString(12),
                                    longitud = drReparto.GetString(13),
                                    telefono = drReparto.GetString(14),
                                    nota = drReparto.GetString(15),
                                    fechaAsignacion = drReparto.GetString(16),
                                });
                            }
                            migracion.repartoLectura = reparto;
                        }

                        // MOTIVO

                        SqlCommand cmdM = con.CreateCommand();
                        cmdM.CommandTimeout = 0;
                        cmdM.CommandType = CommandType.StoredProcedure;
                        cmdM.CommandText = "USP_LIST_MOTIVO";
                        SqlDataReader drM = cmdM.ExecuteReader();
                        if (drM.HasRows)
                        {
                            List<Motivo> m = new List<Motivo>();
                            while (drM.Read())
                            {
                                m.Add(new Motivo()
                                {
                                    motivoId = drM.GetInt32(0),
                                    grupo = drM.GetString(1),
                                    codigo = drM.GetInt32(2),
                                    descripcion = drM.GetString(3)

                                });
                            }
                            migracion.motivos = m;
                        }

                        // FORMATO

                        SqlCommand cmdF = con.CreateCommand();
                        cmdF.CommandTimeout = 0;
                        cmdF.CommandType = CommandType.StoredProcedure;
                        cmdF.CommandText = "Movil_List_Formato_Cargo";
                        SqlDataReader drF = cmdF.ExecuteReader();
                        if (drF.HasRows)
                        {
                            List<FormatoCargo> f = new List<FormatoCargo>();
                            while (drF.Read())
                            {
                                f.Add(new FormatoCargo()
                                {
                                    formatoId = drF.GetInt32(0),
                                    tipo = drF.GetInt32(1),
                                    nombre = drF.GetString(2),
                                    abreviatura = drF.GetString(3),
                                    estado = drF.GetInt32(4)
                                });
                            }
                            migracion.formatos = f;
                        }

                        // CLIENTES

                        SqlCommand cmdC = con.CreateCommand();
                        cmdC.CommandTimeout = 0;
                        cmdC.CommandType = CommandType.StoredProcedure;
                        cmdC.CommandText = "USP_LIST_GRANDES_CLIENTE";
                        cmdC.Parameters.Add("@operarioId", SqlDbType.Int).Value = operarioId;
                        SqlDataReader drC = cmdC.ExecuteReader();
                        if (drC.HasRows)
                        {
                            List<GrandesClientes> c = new List<GrandesClientes>();
                            while (drC.Read())
                            {
                                c.Add(new GrandesClientes()
                                {
                                    clienteId = drC.GetInt32(0),
                                    fechaImportacion = drC.GetDateTime(1).ToString("dd/MM/yyyy HH:mm:ss"),
                                    archivoImportacion = drC.GetString(2),
                                    codigoEMR = drC.GetString(3),
                                    nombreCliente = drC.GetString(4),
                                    direccion = drC.GetString(5),
                                    distrito = drC.GetString(6),
                                    fechaAsignacion = drC.GetDateTime(7).ToString("dd/MM/yyyy"),
                                    fechaEnvioCelular = drC.GetDateTime(8).ToString("dd/MM/yyyy"),
                                    operarioId = drC.GetInt32(9),
                                    ordenLectura = drC.GetInt32(10),
                                    fechaRegistroInicio = drC.GetDateTime(11).ToString("dd/MM/yyyy"),
                                    clientePermiteAcceso = drC.GetString(12),
                                    fotoConstanciaPermiteAcceso = drC.GetString(13),
                                    porMezclaExplosiva = drC.GetString(14),
                                    vManoPresionEntrada = drC.GetString(15),
                                    fotovManoPresionEntrada = drC.GetString(16),
                                    marcaCorrectorId = drC.GetInt32(17),
                                    fotoMarcaCorrector = drC.GetString(18),
                                    vVolumenSCorreUC = drC.GetString(19),
                                    fotovVolumenSCorreUC = drC.GetString(20),
                                    vVolumenSCorreMedidor = drC.GetString(21),
                                    fotovVolumenSCorreMedidor = drC.GetString(22),
                                    vVolumenRegUC = drC.GetString(23),
                                    fotovVolumenRegUC = drC.GetString(24),
                                    vPresionMedicionUC = drC.GetString(25),
                                    fotovPresionMedicionUC = drC.GetString(26),
                                    vTemperaturaMedicionUC = drC.GetString(27),
                                    fotovTemperaturaMedicionUC = drC.GetString(28),
                                    tiempoVidaBateria = drC.GetString(29),
                                    fotoTiempoVidaBateria = drC.GetString(30),
                                    fotoPanomarica = drC.GetString(31),
                                    tieneGabinete = drC.GetString(32),
                                    foroSitieneGabinete = drC.GetString(33),
                                    presenteCliente = drC.GetString(34),
                                    contactoCliente = drC.GetString(35),
                                    latitud = drC.GetString(36),
                                    longitud = drC.GetString(37),
                                    estado = drC.GetInt32(38),
                                    tomaLectura = drC.GetString(39),
                                    fotoTomaLectura = drC.GetString(40),
                                    existeMedidor = drC.GetString(41),
                                    fotoBateriaDescargada = drC.GetString(42),
                                    fotoDisplayMalogrado = drC.GetString(43),
                                    fotoPorMezclaExplosiva = drC.GetString(44),
                                    comentario = drC.GetString(45)
                                });
                            }

                            migracion.clientes = c;
                        }

                        SqlCommand cmdMa = con.CreateCommand();
                        cmdMa.CommandTimeout = 0;
                        cmdMa.CommandType = CommandType.StoredProcedure;
                        cmdMa.CommandText = "USP_LIST_MARCA_MEDIDOR";
                        SqlDataReader drMa = cmdMa.ExecuteReader();
                        if (drMa.HasRows)
                        {
                            List<Marca> m = new List<Marca>();
                            while (drMa.Read())
                            {
                                m.Add(new Marca()
                                {
                                    marcaMedidorId = drMa.GetInt32(0),
                                    nombre = drMa.GetString(1)
                                });
                            }

                            migracion.marcas = m;
                        }


                        migracion.mensaje = "Sincronización Completada.";
                    }
                    con.Close();
                }
                return migracion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Sincronizar
        public static Sincronizar Sincronizar(int operarioId)
        {
            try
            {
                Sincronizar sincronizar = new Sincronizar();
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();

                    sincronizar.sincronizarId = 1;

                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandTimeout = 0;
                    cmd.CommandText = "USP_LIST_SUMINISTRO_CORTES";
                    cmd.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = operarioId;
                    cmd.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = "3";
                    SqlDataReader dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        List<Suministro> suministroCorte = new List<Suministro>();
                        int i = 1;
                        while (dr.Read())
                        {
                            suministroCorte.Add(new Suministro()
                            {
                                iD_Suministro = dr.GetInt32(0),
                                suministro_Numero = dr.GetString(1),
                                suministro_Medidor = dr.GetString(2),
                                suministro_Cliente = dr.GetString(3),
                                suministro_Direccion = dr.GetString(4),
                                suministro_UnidadLectura = dr.GetString(5),
                                suministro_TipoProceso = dr.GetString(6),
                                suministro_LecturaMinima = dr.GetString(7),
                                suministro_LecturaMaxima = dr.GetString(8),
                                suministro_Fecha_Reg_Movil = dr.GetDateTime(9).ToString("dd/MM/yyyy"),
                                suministro_UltimoMes = dr.GetString(10),
                                suministro_NoCortar = dr.GetInt32(11),
                                estado = dr.GetInt32(12),
                                suministroOperario_Orden = dr.GetInt32(13),
                                latitud = dr.GetString(14),
                                longitud = dr.GetString(15),
                                telefono = "",
                                nota = "",
                                fechaAsignacion = dr.GetString(18),
                                primeraReconexion = dr.GetString(19),
                                avisoCorte = dr.GetString(20),
                                orden = i++,
                                activo = 1
                            });
                        }
                        sincronizar.suministrosCortes = suministroCorte;
                    }

                    SqlCommand cmdR = cn.CreateCommand();
                    cmdR.CommandType = CommandType.StoredProcedure;
                    cmdR.CommandTimeout = 0;
                    cmdR.CommandText = "USP_LIST_SUMINISTRO_CORTES";
                    cmdR.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = operarioId;
                    cmdR.Parameters.Add("@Tipo", SqlDbType.VarChar).Value = "4";
                    SqlDataReader drR = cmdR.ExecuteReader();

                    if (dr.HasRows)
                    {
                        List<Suministro> suministroReconexiones = new List<Suministro>();
                        int y = 1;
                        while (dr.Read())
                        {
                            suministroReconexiones.Add(new Suministro()
                            {
                                iD_Suministro = dr.GetInt32(0),
                                suministro_Numero = dr.GetString(1),
                                suministro_Medidor = dr.GetString(2),
                                suministro_Cliente = dr.GetString(3),
                                suministro_Direccion = dr.GetString(4),
                                suministro_UnidadLectura = dr.GetString(5),
                                suministro_TipoProceso = dr.GetString(6),
                                suministro_LecturaMinima = dr.GetString(7),
                                suministro_LecturaMaxima = dr.GetString(8),
                                suministro_Fecha_Reg_Movil = dr.GetDateTime(9).ToString("dd/MM/yyyy"),
                                suministro_UltimoMes = dr.GetString(10),
                                suministro_NoCortar = dr.GetInt32(11),
                                estado = dr.GetInt32(12),
                                suministroOperario_Orden = dr.GetInt32(13),
                                latitud = dr.GetString(14),
                                longitud = dr.GetString(15),
                                telefono = "",
                                nota = "",
                                fechaAsignacion = dr.GetString(18),
                                primeraReconexion = dr.GetString(19),
                                avisoCorte = dr.GetString(20),
                                orden = y++,
                                activo = 1
                            });
                        }
                        sincronizar.suministroReconexion = suministroReconexiones;
                    }
                    cn.Close();

                }
                return sincronizar;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // Observadas 
        public static List<Suministro> GetObservadas(int operarioId)
        {
            try
            {
                List<Suministro> suministro = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmdSuministro = con.CreateCommand();
                    cmdSuministro.CommandTimeout = 0;
                    cmdSuministro.CommandType = CommandType.StoredProcedure;
                    cmdSuministro.CommandText = "USP_LIST_SUMINISTRO";
                    cmdSuministro.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = operarioId;
                    SqlDataReader drSuministro = cmdSuministro.ExecuteReader();
                    if (drSuministro.HasRows)
                    {
                        suministro = new List<Suministro>();
                        int i = 1;
                        while (drSuministro.Read())
                        {
                            suministro.Add(new Suministro()
                            {
                                iD_Suministro = drSuministro.GetInt32(0),
                                suministro_Numero = drSuministro.GetString(1),
                                suministro_Medidor = drSuministro.GetString(2),
                                suministro_Cliente = drSuministro.GetString(3),
                                suministro_Direccion = drSuministro.GetString(4),
                                suministro_UnidadLectura = drSuministro.GetString(5),
                                suministro_TipoProceso = drSuministro.GetString(6),
                                suministro_LecturaMinima = drSuministro.GetString(7),
                                suministro_LecturaMaxima = drSuministro.GetString(8),
                                suministro_Fecha_Reg_Movil = drSuministro.GetDateTime(9).ToString("dd/MM/yyyy"),
                                suministro_UltimoMes = drSuministro.GetString(10),
                                consumoPromedio = drSuministro.GetDecimal(11),
                                lecturaAnterior = drSuministro.GetString(12),
                                suministro_Instalacion = drSuministro.GetString(13),
                                valida1 = drSuministro.GetInt32(14),
                                valida2 = drSuministro.GetInt32(15),
                                valida3 = drSuministro.GetInt32(16),
                                valida4 = drSuministro.GetInt32(17),
                                valida5 = drSuministro.GetInt32(18),
                                valida6 = drSuministro.GetInt32(19),
                                tipoCliente = drSuministro.GetInt32(20),
                                estado = drSuministro.GetInt32(21),
                                suministroOperario_Orden = drSuministro.GetInt32(22),
                                flagObservada = drSuministro.GetInt32(23),
                                latitud = drSuministro.GetString(24),
                                longitud = drSuministro.GetString(25),
                                orden = i++,
                                activo = 1
                            });
                        }
                    }

                    con.Close();
                }

                return suministro;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static List<Operario> GetOperarios()
        {
            try
            {
                List<Operario> operario = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    SqlCommand cmdSuministro = con.CreateCommand();
                    cmdSuministro.CommandTimeout = 0;
                    cmdSuministro.CommandType = CommandType.StoredProcedure;
                    cmdSuministro.CommandText = "USP_LIST_OPERARIOS";
                    SqlDataReader drSuministro = cmdSuministro.ExecuteReader();
                    if (drSuministro.HasRows)
                    {
                        operario = new List<Operario>();
                        int i = 1;
                        while (drSuministro.Read())
                        {
                            operario.Add(new Operario()
                            {
                                operarioId = drSuministro.GetInt32(0),
                                operarioNombre = drSuministro.GetString(1),
                                lecturaManual = drSuministro.GetInt32(2)
                            });
                        }
                    }

                    con.Close();
                }

                return operario;

            }
            catch (Exception e)
            {
                throw e;
            }

        }

        public static Mensaje UpdateOperario(int operarioId, int lecturaManual)
        {
            try
            {

                Mensaje m = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_OPERARIO_MANUAL", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@operarioId", SqlDbType.VarChar).Value = operarioId;
                        cmd.Parameters.Add("@lecturaManual", SqlDbType.VarChar).Value = lecturaManual;

                        int a = cmd.ExecuteNonQuery();

                        if (a == 1)
                        {
                            m = new Mensaje();
                            m.codigo = 1;
                            m.mensaje = "Operario Actualizado";
                        }

                    }
                    con.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw;
            }

        }

        public static Mensaje GetOperarioById(int operarioId)
        {
            try
            {
                Mensaje m = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_GET_OPERARIO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@operarioId", SqlDbType.Int).Value = operarioId;
                        SqlDataReader dr = cmd.ExecuteReader();
                        if (dr.HasRows)
                        {
                            m = new Mensaje();
                            while (dr.Read())
                            {
                                m.codigo = dr.GetInt32(0);
                                m.mensaje = "Recibido";
                            }
                        }
                    }
                    cn.Close();
                }
                return m;
            }
            catch (Exception e)
            {
                throw e;
            }

        }


        // VERSION 5.0.6
        public static Mensaje SaveRegistroRxNew(Registro r)
        {
            try
            {
                int lastId;
                Mensaje m = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    if (r.tipo == 2 || r.tipo == 1 || r.tipo == 9)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_SAVE_REGISTRO_LECTURA", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = r.iD_Registro;
                            cmd.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = r.iD_Operario;
                            cmd.Parameters.Add("@ID_Suministro", SqlDbType.Int).Value = r.iD_Suministro;
                            cmd.Parameters.Add("@ID_TipoLectura", SqlDbType.Int).Value = r.iD_TipoLectura;
                            cmd.Parameters.Add("@Registro_Fecha_SQLITE", SqlDbType.VarChar).Value = r.registro_Fecha_SQLITE;
                            cmd.Parameters.Add("@Registro_Latitud", SqlDbType.VarChar).Value = r.registro_Latitud;
                            cmd.Parameters.Add("@Registro_Longitud", SqlDbType.VarChar).Value = r.registro_Longitud;
                            cmd.Parameters.Add("@Registro_Lectura", SqlDbType.VarChar).Value = r.registro_Lectura;
                            cmd.Parameters.Add("@Registro_Confirmar_Lectura", SqlDbType.VarChar).Value = r.registro_Confirmar_Lectura;
                            cmd.Parameters.Add("@Registro_Observacion", SqlDbType.VarChar).Value = r.registro_Observacion;
                            cmd.Parameters.Add("@Grupo_Incidencia_Codigo", SqlDbType.VarChar).Value = r.grupo_Incidencia_Codigo;
                            cmd.Parameters.Add("@Registro_TieneFoto", SqlDbType.VarChar).Value = r.registro_TieneFoto;
                            cmd.Parameters.Add("@Registro_TipoProceso", SqlDbType.VarChar).Value = r.registro_TipoProceso;
                            cmd.Parameters.Add("@Fecha_Sincronizacion_Android", SqlDbType.VarChar).Value = r.fecha_Sincronizacion_Android;
                            cmd.Parameters.Add("@Registro_Constancia", SqlDbType.VarChar).Value = r.registro_Constancia;
                            cmd.Parameters.Add("@Registro_Desplaza", SqlDbType.VarChar).Value = r.registro_Desplaza;
                            cmd.Parameters.Add("@Suministro_Numero", SqlDbType.Int).Value = r.suministro_Numero;
                            cmd.Parameters.Add("@LecturaManual", SqlDbType.Int).Value = r.lecturaManual;
                            SqlDataReader dr = cmd.ExecuteReader();

                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    lastId = dr.GetInt32(0);
                                    if (lastId != 0)
                                    {
                                        m = new Mensaje();

                                        foreach (var p in r.photos)
                                        {
                                            if (p.estado == 1)
                                            {
                                                SqlCommand cmds = con.CreateCommand();
                                                cmds.CommandType = CommandType.StoredProcedure;
                                                cmds.CommandText = "USP_SAVE_REGISTRO_PHOTO_LECTURA";
                                                cmds.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = lastId;
                                                cmds.Parameters.Add("@RutaFoto", SqlDbType.VarChar).Value = p.rutaFoto;
                                                cmds.Parameters.Add("@latitud", SqlDbType.VarChar).Value = p.latitud;
                                                cmds.Parameters.Add("@longitud", SqlDbType.VarChar).Value = p.longitud;
                                                cmds.ExecuteNonQuery();
                                            }
                                        }

                                        m.mensaje = "Datos Enviados";
                                    }
                                }
                            }
                        }
                    }
                    else if (r.tipo == 3 || r.tipo == 4)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_SAVE_REGISTRO_CORTES_NEW", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = r.iD_Registro;
                            cmd.Parameters.Add("@ID_Operario", SqlDbType.Int).Value = r.iD_Operario;
                            cmd.Parameters.Add("@ID_Suministro", SqlDbType.Int).Value = r.iD_Suministro;
                            cmd.Parameters.Add("@ID_TipoLectura", SqlDbType.Int).Value = r.iD_TipoLectura;
                            cmd.Parameters.Add("@Registro_Fecha_SQLITE", SqlDbType.VarChar).Value = r.registro_Fecha_SQLITE;
                            cmd.Parameters.Add("@Registro_Latitud", SqlDbType.VarChar).Value = r.registro_Latitud;
                            cmd.Parameters.Add("@Registro_Longitud", SqlDbType.VarChar).Value = r.registro_Longitud;
                            cmd.Parameters.Add("@Registro_Lectura", SqlDbType.VarChar).Value = r.registro_Lectura;
                            cmd.Parameters.Add("@Registro_Confirmar_Lectura", SqlDbType.VarChar).Value = r.registro_Confirmar_Lectura;
                            cmd.Parameters.Add("@Registro_Observacion", SqlDbType.VarChar).Value = r.registro_Observacion;
                            cmd.Parameters.Add("@Grupo_Incidencia_Codigo", SqlDbType.VarChar).Value = r.grupo_Incidencia_Codigo;
                            cmd.Parameters.Add("@Registro_TieneFoto", SqlDbType.VarChar).Value = r.registro_TieneFoto;
                            cmd.Parameters.Add("@Registro_TipoProceso", SqlDbType.VarChar).Value = r.registro_TipoProceso;
                            cmd.Parameters.Add("@Fecha_Sincronizacion_Android", SqlDbType.VarChar).Value = r.fecha_Sincronizacion_Android;
                            cmd.Parameters.Add("@Registro_Constancia", SqlDbType.VarChar).Value = r.registro_Constancia;
                            cmd.Parameters.Add("@Registro_Desplaza", SqlDbType.VarChar).Value = r.registro_Desplaza;
                            cmd.Parameters.Add("@Codigo_Resultado", SqlDbType.VarChar).Value = r.codigo_Resultado;
                            cmd.Parameters.Add("@horaActa", SqlDbType.VarChar).Value = r.horaActa;
                            cmd.Parameters.Add("@Suministro_Numero", SqlDbType.Int).Value = r.suministro_Numero;
                            cmd.Parameters.Add("@motivoId", SqlDbType.Int).Value = r.motivoId;
                            cmd.Parameters.Add("@parentId", SqlDbType.Int).Value = r.parentId;
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    lastId = dr.GetInt32(0);
                                    if (lastId != 0)
                                    {
                                        m = new Mensaje();
                                        foreach (var p in r.photos)
                                        {
                                            if (p.estado == 1)
                                            {
                                                SqlCommand cmds = con.CreateCommand();
                                                cmds.CommandType = CommandType.StoredProcedure;
                                                cmds.CommandText = "USP_SAVE_REGISTRO_PHOTO";
                                                cmds.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = lastId;
                                                cmds.Parameters.Add("@RutaFoto", SqlDbType.VarChar).Value = p.rutaFoto;
                                                cmds.Parameters.Add("@latitud", SqlDbType.VarChar).Value = p.latitud;
                                                cmds.Parameters.Add("@longitud", SqlDbType.VarChar).Value = p.longitud;
                                                cmds.ExecuteNonQuery();
                                            }

                                        }
                                        m.mensaje = "Datos Enviados";
                                    }
                                }
                            }
                        }
                    }
                    else if (r.tipo == 5)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_SAVE_REGISTRO_REPARTO", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@id_operario_reparto", SqlDbType.Int).Value = r.iD_Operario;
                            cmd.Parameters.Add("@id_reparto", SqlDbType.Int).Value = r.iD_Suministro;
                            cmd.Parameters.Add("@registro_fecha_sqlite", SqlDbType.VarChar).Value = r.registro_Fecha_SQLITE;
                            cmd.Parameters.Add("@registro_latitud", SqlDbType.VarChar).Value = r.registro_Latitud;
                            cmd.Parameters.Add("@registro_longitud", SqlDbType.VarChar).Value = r.registro_Longitud;
                            cmd.Parameters.Add("@id_Observacion", SqlDbType.VarChar).Value = r.registro_Observacion;
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    lastId = dr.GetInt32(0);
                                    if (lastId != 0)
                                    {
                                        m = new Mensaje();
                                        foreach (var i in r.photos)
                                        {
                                            SqlCommand cmdP = con.CreateCommand();
                                            cmdP.CommandType = CommandType.StoredProcedure;
                                            cmdP.CommandText = "USP_SAVE_REGISTRO_REPARTO_FOTO";
                                            cmdP.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = lastId;
                                            cmdP.Parameters.Add("@RutaFoto", SqlDbType.VarChar).Value = i.rutaFoto;
                                            cmdP.Parameters.Add("@latitud", SqlDbType.VarChar).Value = i.latitud;
                                            cmdP.Parameters.Add("@longitud", SqlDbType.VarChar).Value = i.longitud;
                                            cmdP.ExecuteNonQuery();
                                        }

                                        var rr = r.recibo;
                                        if (rr != null)
                                        {
                                            SqlCommand cmdR = con.CreateCommand();
                                            cmdR.CommandType = CommandType.StoredProcedure;
                                            cmdR.CommandText = "Movil_SaveCargoRecibo";
                                            cmdR.Parameters.Add("@id_registro", SqlDbType.Int).Value = lastId;
                                            cmdR.Parameters.Add("@id_reparto", SqlDbType.Int).Value = rr.repartoId;
                                            cmdR.Parameters.Add("@id_operario_reparto", SqlDbType.Int).Value = rr.operarioId;
                                            cmdR.Parameters.Add("@tiporecibo", SqlDbType.Int).Value = rr.tipo;
                                            cmdR.Parameters.Add("@ciclo_cargorecibo", SqlDbType.VarChar).Value = rr.ciclo;
                                            cmdR.Parameters.Add("@anio_cargorecibo", SqlDbType.Int).Value = rr.year;
                                            cmdR.Parameters.Add("@piso", SqlDbType.Int).Value = rr.piso;
                                            cmdR.Parameters.Add("@id_formatocargo_vivienda", SqlDbType.Int).Value = rr.formatoVivienda;
                                            cmdR.Parameters.Add("@otrosvivienda_cargorecibo", SqlDbType.VarChar).Value = rr.otrosVivienda;
                                            cmdR.Parameters.Add("@id_formatocargo_color", SqlDbType.Int).Value = rr.formatoCargoColor;
                                            cmdR.Parameters.Add("@otroscolor_cargorecibo", SqlDbType.VarChar).Value = rr.otrosCargoColor;
                                            cmdR.Parameters.Add("@id_formatocargo_puerta", SqlDbType.Int).Value = rr.formatoCargoPuerta;
                                            cmdR.Parameters.Add("@otrospuerta_cargorecibo", SqlDbType.VarChar).Value = rr.otrosCargoPuerta;
                                            cmdR.Parameters.Add("@id_formatocargo_colorpuerta", SqlDbType.Int).Value = rr.formatoCargoColorPuerta;
                                            cmdR.Parameters.Add("@otroscolorpuerta_cargorecibo", SqlDbType.VarChar).Value = rr.otrosCargoColorPuerta;
                                            cmdR.Parameters.Add("@id_formatocargo_recibido", SqlDbType.Int).Value = rr.formatoCargoRecibo;
                                            cmdR.Parameters.Add("@dni_cargorecibo", SqlDbType.VarChar).Value = rr.dniCargoRecibo;
                                            cmdR.Parameters.Add("@parentesco_cargorecibo", SqlDbType.VarChar).Value = rr.parentesco;
                                            cmdR.Parameters.Add("@id_formatocargo_devuelto", SqlDbType.Int).Value = rr.formatoCargoDevuelto;
                                            cmdR.Parameters.Add("@fechamax_cargorecibo", SqlDbType.VarChar).Value = rr.fechaMax;
                                            cmdR.Parameters.Add("@fechaentrega_cargorecibo", SqlDbType.VarChar).Value = rr.fechaEntrega;
                                            cmdR.Parameters.Add("@obs_cargorecibo", SqlDbType.VarChar).Value = rr.observacionCargo;
                                            cmdR.Parameters.Add("@firmacliente_cargorecibo", SqlDbType.VarChar).Value = rr.firmaCliente;
                                            cmdR.ExecuteNonQuery();
                                        }

                                        m.mensaje = "Datos Enviados";
                                    }
                                }
                            }
                        }

                    }
                    else if (r.tipo == 6)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_SAVE_SUMINISTRO_ENCONTRADO", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = r.iD_Registro;
                            cmd.Parameters.Add("@id_operario", SqlDbType.Int).Value = r.iD_Operario;
                            cmd.Parameters.Add("@suministro_medidor", SqlDbType.VarChar).Value = r.iD_Suministro;
                            cmd.Parameters.Add("@suministro_contrato", SqlDbType.VarChar).Value = r.registro_Constancia;
                            cmd.Parameters.Add("@suministro_cliente", SqlDbType.VarChar).Value = r.suministroCliente;
                            cmd.Parameters.Add("@suministro_direccion", SqlDbType.VarChar).Value = r.suministroDireccion;
                            cmd.Parameters.Add("@suministro_observacion", SqlDbType.VarChar).Value = r.registro_Observacion;
                            cmd.Parameters.Add("@fecha_movil", SqlDbType.VarChar).Value = r.registro_Fecha_SQLITE;
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    lastId = dr.GetInt32(0);
                                    if (lastId != 0)
                                    {
                                        m = new Mensaje();
                                        foreach (var p in r.photos)
                                        {
                                            if (p.estado == 1)
                                            {
                                                SqlCommand cmds = con.CreateCommand();
                                                cmds.CommandType = CommandType.StoredProcedure;
                                                cmds.CommandText = "USP_SAVE_REGISTRO_PHOTO_LECTURA";
                                                cmds.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = lastId;
                                                cmds.Parameters.Add("@RutaFoto", SqlDbType.VarChar).Value = p.rutaFoto;
                                                cmds.Parameters.Add("@latitud", SqlDbType.VarChar).Value = p.latitud;
                                                cmds.Parameters.Add("@longitud", SqlDbType.VarChar).Value = p.longitud;
                                                cmds.ExecuteNonQuery();
                                            }
                                        }
                                        m.mensaje = "Datos Enviados";
                                    }
                                }
                            }
                        }
                    }
                    else if (r.tipo == 8)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_SAVE_ZONA_PELIGROSA", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = r.iD_Registro;
                            cmd.Parameters.Add("@id_operario", SqlDbType.Int).Value = r.iD_Operario;
                            cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = r.registro_Latitud;
                            cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = r.registro_Longitud;
                            cmd.Parameters.Add("@suministro_observacion", SqlDbType.VarChar).Value = r.registro_Observacion;
                            cmd.Parameters.Add("@fecha_movil", SqlDbType.VarChar).Value = r.registro_Fecha_SQLITE;

                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    lastId = dr.GetInt32(0);
                                    if (lastId != 0)
                                    {
                                        m = new Mensaje();
                                        foreach (var p in r.photos)
                                        {
                                            if (p.estado == 1)
                                            {
                                                SqlCommand cmds = con.CreateCommand();
                                                cmds.CommandType = CommandType.StoredProcedure;
                                                cmds.CommandText = "USP_SAVE_REGISTRO_PHOTO_LECTURA";
                                                cmds.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = lastId;
                                                cmds.Parameters.Add("@RutaFoto", SqlDbType.VarChar).Value = p.rutaFoto;
                                                cmds.Parameters.Add("@latitud", SqlDbType.VarChar).Value = p.latitud;
                                                cmds.Parameters.Add("@longitud", SqlDbType.VarChar).Value = p.longitud;
                                                cmds.ExecuteNonQuery();
                                            }
                                        }
                                        m.mensaje = "Datos Enviados";
                                    }
                                }
                            }
                        }
                    }
                    else if (r.tipo == 11)
                    {
                        using (SqlCommand cmd = new SqlCommand("USP_SAVE_SELFIE_REPARTO", con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = 1;
                            cmd.Parameters.Add("@id_operario_reparto", SqlDbType.Int).Value = r.iD_Operario;
                            cmd.Parameters.Add("@id_reparto", SqlDbType.Int).Value = r.iD_Suministro;
                            cmd.Parameters.Add("@registro_fecha_sqlite", SqlDbType.VarChar).Value = r.registro_Fecha_SQLITE;
                            cmd.Parameters.Add("@registro_latitud", SqlDbType.VarChar).Value = r.registro_Latitud;
                            cmd.Parameters.Add("@registro_longitud", SqlDbType.VarChar).Value = r.registro_Longitud;
                            cmd.Parameters.Add("@id_Observacion", SqlDbType.VarChar).Value = r.registro_Observacion;
                            SqlDataReader dr = cmd.ExecuteReader();
                            if (dr.HasRows)
                            {
                                while (dr.Read())
                                {
                                    lastId = dr.GetInt32(0);
                                    if (lastId != 0)
                                    {
                                        m = new Mensaje();
                                        foreach (var i in r.photos)
                                        {
                                            SqlCommand cmdP = con.CreateCommand();
                                            cmdP.CommandType = CommandType.StoredProcedure;
                                            cmdP.CommandText = "USP_SAVE_REGISTRO_REPARTO_FOTO";
                                            cmdP.Parameters.Add("@ID_Registro", SqlDbType.Int).Value = lastId;
                                            cmdP.Parameters.Add("@RutaFoto", SqlDbType.VarChar).Value = i.rutaFoto;
                                            cmdP.Parameters.Add("@latitud", SqlDbType.VarChar).Value = i.latitud;
                                            cmdP.Parameters.Add("@longitud", SqlDbType.VarChar).Value = i.longitud;
                                            cmdP.ExecuteNonQuery();

                                        }
                                        m.mensaje = "Datos Enviados";
                                    }
                                }
                            }
                        }
                    }
                    con.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        public static Mensaje SaveCliente(GrandesClientes r)
        {
            try
            {
                Mensaje m = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_GRANDES_CLIENTES", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@clienteId", SqlDbType.Int).Value = r.clienteId;
                        cmd.Parameters.Add("@clientePermiteAcceso", SqlDbType.VarChar).Value = r.clientePermiteAcceso;
                        cmd.Parameters.Add("@fotoConstanciaPermiteAcceso", SqlDbType.VarChar).Value = r.fotoConstanciaPermiteAcceso;
                        cmd.Parameters.Add("@porMezclaExplosiva", SqlDbType.VarChar).Value = r.porMezclaExplosiva;
                        cmd.Parameters.Add("@vManoPresionEntrada", SqlDbType.VarChar).Value = r.vManoPresionEntrada;
                        cmd.Parameters.Add("@fotovManoPresionEntrada", SqlDbType.VarChar).Value = r.fotovManoPresionEntrada;
                        cmd.Parameters.Add("@marcaCorrectorId", SqlDbType.Int).Value = r.marcaCorrectorId;
                        cmd.Parameters.Add("@fotoMarcaCorrector", SqlDbType.VarChar).Value = r.fotoMarcaCorrector;
                        cmd.Parameters.Add("@vVolumenSCorreUC", SqlDbType.VarChar).Value = r.vVolumenSCorreUC;
                        cmd.Parameters.Add("@fotovVolumenSCorreUC", SqlDbType.VarChar).Value = r.fotovVolumenSCorreUC;
                        cmd.Parameters.Add("@vVolumenSCorreMedidor", SqlDbType.VarChar).Value = r.vVolumenSCorreMedidor;
                        cmd.Parameters.Add("@fotovVolumenSCorreMedidor", SqlDbType.VarChar).Value = r.fotovVolumenSCorreMedidor;
                        cmd.Parameters.Add("@vVolumenRegUC", SqlDbType.VarChar).Value = r.vVolumenRegUC;
                        cmd.Parameters.Add("@fotovVolumenRegUC", SqlDbType.VarChar).Value = r.fotovVolumenRegUC;
                        cmd.Parameters.Add("@vPresionMedicionUC", SqlDbType.VarChar).Value = r.vPresionMedicionUC;
                        cmd.Parameters.Add("@fotovPresionMedicionUC", SqlDbType.VarChar).Value = r.fotovPresionMedicionUC;
                        cmd.Parameters.Add("@vTemperaturaMedicionUC", SqlDbType.VarChar).Value = r.vTemperaturaMedicionUC;
                        cmd.Parameters.Add("@fotovTemperaturaMedicionUC", SqlDbType.VarChar).Value = r.fotovTemperaturaMedicionUC;
                        cmd.Parameters.Add("@tiempoVidaBateria", SqlDbType.VarChar).Value = r.tiempoVidaBateria;
                        cmd.Parameters.Add("@fotoTiempoVidaBateria", SqlDbType.VarChar).Value = r.fotoTiempoVidaBateria;
                        cmd.Parameters.Add("@fotoPanomarica", SqlDbType.VarChar).Value = r.fotoPanomarica;
                        cmd.Parameters.Add("@tieneGabinete", SqlDbType.VarChar).Value = r.tieneGabinete;
                        cmd.Parameters.Add("@foroSitieneGabinete", SqlDbType.VarChar).Value = r.foroSitieneGabinete;
                        cmd.Parameters.Add("@presenteCliente", SqlDbType.VarChar).Value = r.presenteCliente;
                        cmd.Parameters.Add("@contactoCliente", SqlDbType.VarChar).Value = r.contactoCliente;
                        cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = r.latitud;
                        cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = r.longitud;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = r.estado;
                        cmd.Parameters.Add("@tomaLectura", SqlDbType.VarChar).Value = r.tomaLectura;
                        cmd.Parameters.Add("@fotoTomaLectura", SqlDbType.VarChar).Value = r.fotoTomaLectura;
                        cmd.Parameters.Add("@existeMedidor", SqlDbType.VarChar).Value = r.existeMedidor;
                        cmd.Parameters.Add("@fotoBateriaDescargada", SqlDbType.VarChar).Value = r.fotoBateriaDescargada;
                        cmd.Parameters.Add("@fotoDisplayMalogrado", SqlDbType.VarChar).Value = r.fotoDisplayMalogrado;
                        cmd.Parameters.Add("@fotoPorMezclaExplosiva", SqlDbType.VarChar).Value = r.fotoPorMezclaExplosiva;
                        cmd.Parameters.Add("@comentario", SqlDbType.VarChar).Value = r.comentario;
                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            m = new Mensaje();
                            m.codigo = r.clienteId;
                            m.mensaje = "Enviado";
                        }
                    }

                    con.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje SaveClienteNew(GrandesClientes r)
        {
            try
            {
                Mensaje m = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_UPDATE_GRANDES_CLIENTES_NEW", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@clienteId", SqlDbType.Int).Value = r.clienteId;
                        cmd.Parameters.Add("@clientePermiteAcceso", SqlDbType.VarChar).Value = r.clientePermiteAcceso;
                        cmd.Parameters.Add("@fotoConstanciaPermiteAcceso", SqlDbType.VarChar).Value = r.fotoConstanciaPermiteAcceso;
                        cmd.Parameters.Add("@porMezclaExplosiva", SqlDbType.VarChar).Value = r.porMezclaExplosiva;
                        cmd.Parameters.Add("@vManoPresionEntrada", SqlDbType.VarChar).Value = r.vManoPresionEntrada;
                        cmd.Parameters.Add("@fotovManoPresionEntrada", SqlDbType.VarChar).Value = r.fotovManoPresionEntrada;
                        cmd.Parameters.Add("@marcaCorrectorId", SqlDbType.Int).Value = r.marcaCorrectorId;
                        cmd.Parameters.Add("@fotoMarcaCorrector", SqlDbType.VarChar).Value = r.fotoMarcaCorrector;
                        cmd.Parameters.Add("@vVolumenSCorreUC", SqlDbType.VarChar).Value = r.vVolumenSCorreUC;
                        cmd.Parameters.Add("@fotovVolumenSCorreUC", SqlDbType.VarChar).Value = r.fotovVolumenSCorreUC;
                        cmd.Parameters.Add("@vVolumenSCorreMedidor", SqlDbType.VarChar).Value = r.vVolumenSCorreMedidor;
                        cmd.Parameters.Add("@fotovVolumenSCorreMedidor", SqlDbType.VarChar).Value = r.fotovVolumenSCorreMedidor;
                        cmd.Parameters.Add("@vVolumenRegUC", SqlDbType.VarChar).Value = r.vVolumenRegUC;
                        cmd.Parameters.Add("@fotovVolumenRegUC", SqlDbType.VarChar).Value = r.fotovVolumenRegUC;
                        cmd.Parameters.Add("@vPresionMedicionUC", SqlDbType.VarChar).Value = r.vPresionMedicionUC;
                        cmd.Parameters.Add("@fotovPresionMedicionUC", SqlDbType.VarChar).Value = r.fotovPresionMedicionUC;
                        cmd.Parameters.Add("@vTemperaturaMedicionUC", SqlDbType.VarChar).Value = r.vTemperaturaMedicionUC;
                        cmd.Parameters.Add("@fotovTemperaturaMedicionUC", SqlDbType.VarChar).Value = r.fotovTemperaturaMedicionUC;
                        cmd.Parameters.Add("@tiempoVidaBateria", SqlDbType.VarChar).Value = r.tiempoVidaBateria;
                        cmd.Parameters.Add("@fotoTiempoVidaBateria", SqlDbType.VarChar).Value = r.fotoTiempoVidaBateria;
                        cmd.Parameters.Add("@fotoPanomarica", SqlDbType.VarChar).Value = r.fotoPanomarica;
                        cmd.Parameters.Add("@tieneGabinete", SqlDbType.VarChar).Value = r.tieneGabinete;
                        cmd.Parameters.Add("@foroSitieneGabinete", SqlDbType.VarChar).Value = r.foroSitieneGabinete;
                        cmd.Parameters.Add("@presenteCliente", SqlDbType.VarChar).Value = r.presenteCliente;
                        cmd.Parameters.Add("@contactoCliente", SqlDbType.VarChar).Value = r.contactoCliente;
                        cmd.Parameters.Add("@latitud", SqlDbType.VarChar).Value = r.latitud;
                        cmd.Parameters.Add("@longitud", SqlDbType.VarChar).Value = r.longitud;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = r.estado;
                        cmd.Parameters.Add("@tomaLectura", SqlDbType.VarChar).Value = r.tomaLectura;
                        cmd.Parameters.Add("@fotoTomaLectura", SqlDbType.VarChar).Value = r.fotoTomaLectura;
                        cmd.Parameters.Add("@existeMedidor", SqlDbType.VarChar).Value = r.existeMedidor;
                        cmd.Parameters.Add("@fotoBateriaDescargada", SqlDbType.VarChar).Value = r.fotoBateriaDescargada;
                        cmd.Parameters.Add("@fotoDisplayMalogrado", SqlDbType.VarChar).Value = r.fotoDisplayMalogrado;
                        cmd.Parameters.Add("@fotoPorMezclaExplosiva", SqlDbType.VarChar).Value = r.fotoPorMezclaExplosiva;
                        cmd.Parameters.Add("@comentario", SqlDbType.VarChar).Value = r.comentario;
                        cmd.Parameters.Add("@fechaInicio", SqlDbType.VarChar).Value = r.fechaRegistroInicio;

                        int a = cmd.ExecuteNonQuery();
                        if (a == 1)
                        {
                            m = new Mensaje();
                            m.codigo = r.clienteId;
                            m.mensaje = "Enviado";
                        }
                    }

                    con.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public static Mensaje VerificarCliente(int id)
        {
            try
            {
                Mensaje mensaje = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_VERIFICATE_CLIENTE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                mensaje = new Mensaje()
                                {
                                    codigo = rd.GetInt32(0),
                                    mensaje = (rd.GetInt32(0) == 0 ? "No existen archivos" : "Archivos encontrados : " + rd.GetInt32(0))
                                };
                            }
                        }
                        rd.Close();
                    }
                }
                return mensaje;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static Mensaje VerificarClienteNew(int id,string fecha)
        {
            try
            {
                Mensaje mensaje = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_VERIFICATE_CLIENTE_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id", SqlDbType.Int).Value = id;
                        cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = fecha;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                mensaje = new Mensaje()
                                {
                                    codigo = rd.GetInt32(0),
                                    mensaje = (rd.GetInt32(0) == 0 ? "No existen archivos" : "Archivos encontrados : " + rd.GetInt32(0))
                                };
                            }
                        }
                        rd.Close();
                    }
                }
                return mensaje;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}