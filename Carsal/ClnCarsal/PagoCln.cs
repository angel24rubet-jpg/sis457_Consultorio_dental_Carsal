using System;
using System.Collections.Generic;
using System.Linq;
using CadCarsal;

namespace ClnCarsal
{
    public class PagoCln
    {
        // ================= CONSULTAS =================

        // Lista de precios: servicios activos
        public static List<paServicioListar_Result> ListarServicios()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paServicioListar().ToList();
            }
        }

        // Citas ATENDIDAS del paciente que todavía no se pagaron
        public static List<paPagoCitasPorCobrar_Result> CitasPorCobrar(int idPaciente)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paPagoCitasPorCobrar(idPaciente).ToList();
            }
        }

        // Todos los pagos realizados
        public static List<paPagoListar_Result> Listar()
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paPagoListar().ToList();
            }
        }

        // Servicios cobrados en un pago
        public static List<paDetallePagoListar_Result> ListarDetalle(int idPago)
        {
            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                return contexto.paDetallePagoListar(idPago).ToList();
            }
        }
        // ================= REGISTRAR EL COBRO (maestro-detalle) =================
        public static int Registrar(
            int idCita,
            string metodoPago,
            List<ItemCobro> items,
            int idUsuarioRegistro)
        {
            // 1. REGLAS DE NEGOCIO
            if (idCita <= 0)
                throw new Exception("Seleccione la cita que se va a cobrar.");

            if (metodoPago != "Efectivo" && metodoPago != "QR")
                throw new Exception("Seleccione el método de pago (Efectivo o QR).");

            if (items == null || items.Count == 0)
                throw new Exception("Agregue al menos un servicio al cobro.");

            foreach (var item in items)
            {
                if (item.Cantidad < 1 || item.Cantidad > 10)
                    throw new Exception("La cantidad de '" + item.Servicio + "' debe estar entre 1 y 10.");

                if (item.PrecioUnitario <= 0)
                    throw new Exception("El precio de '" + item.Servicio + "' debe ser mayor a 0.");
            }

            if (items.GroupBy(i => i.IdServicio).Any(g => g.Count() > 1))
                throw new Exception("No se puede repetir el mismo servicio en un cobro.");


            using (var contexto = new LabConsultorioDental_CarsalEntities())
            {
                // 2. LA CITA DEBE ESTAR ATENDIDA Y SIN PAGAR
                var cita = contexto.Cita.FirstOrDefault(c => c.IdCita == idCita);
                if (cita == null || cita.Estado != "Atendida")
                    throw new Exception("Solo se pueden cobrar citas atendidas.");

                bool yaPagada = contexto.Pago.Any(p => p.IdCita == idCita && p.Estado == "Pagado");
                if (yaPagada)
                    throw new Exception("Esta cita ya fue pagada.");

                // 3. ARMAR EL MAESTRO (Pago)
                var pago = new Pago
                {
                    IdCita = idCita,
                    FechaPago = DateTime.Now,
                    MetodoPago = metodoPago,
                    Monto = items.Sum(i => i.Subtotal),
                    Concepto = ArmarConcepto(items),
                    Estado = "Pagado",
                    IdUsuarioRegistro = idUsuarioRegistro
                };

                // 4. AGREGAR EL DETALLE (DetallePago)
                foreach (var item in items)
                {
                    pago.DetallePago.Add(new DetallePago
                    {
                        IdServicio = item.IdServicio,
                        Cantidad = item.Cantidad,
                        PrecioUnitario = item.PrecioUnitario,
                        Subtotal = item.Subtotal
                    });
                }

                // 5. GUARDAR TODO JUNTO (una sola transacción)
                contexto.Pago.Add(pago);
                contexto.SaveChanges();

                return pago.IdPago;
            }
        }

        // Texto resumen para el campo Concepto: "Cobro: Endodoncia x1, Curación con resina x2"
        private static string ArmarConcepto(List<ItemCobro> items)
        {
            string concepto = "Cobro: " + string.Join(", ",
                items.Select(i => i.Servicio + " x" + i.Cantidad));

            return concepto.Length > 250 ? concepto.Substring(0, 247) + "..." : concepto;
        }
    }
    public class ItemCobro
    {
        public int IdServicio { get; set; }
        public string Servicio { get; set; }
        public int Cantidad { get; set; }
        public decimal PrecioUnitario { get; set; }

        // Se calcula solo: no hay que asignarlo
        public decimal Subtotal
        {
            get { return Cantidad * PrecioUnitario; }
        }
    }
}