using Concretar.Entities;
using Concretar.Helper;
using Concretar.Services.Models;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Concretar.Services
{
    public class CuotaService
    {
        private readonly ILogger _logger;
        readonly UnitOfWork _uow = new UnitOfWork();
        private readonly SendNotificacion sender;

        public CuotaService(ILogger logger)
        {
            _logger = logger;
            sender = new SendNotificacion(_logger);
        }

        public List<CuotaViewModel> GetCuotaByVenta(int ventaId)
        {
            var model = _uow.CuotaRepository.FilterIncluding(x => x.VentaId == ventaId, c => c.Venta);
            var cuotas = model.Select(x => new CuotaViewModel()
            {
                Estado = x.Estado,
                CuotaId = x.CuotaId,
                MontoMora = x.MontoMora,
                Mora = x.Mora,
                NumeroCuota = x.NumeroCuota.ToString(),
                Precio = x.Precio,
                SubTotal = x.SubTotal,
                VentaId = x.VentaId,
                Venta = x.Venta,
                FechaVencimiento = x.FechaVencimiento,
                EnablePay = EnablePay(model.ToList(), x.VentaId, x.NumeroCuota)
            }).ToList();
            return cuotas;
        }

        public CuotaViewModel GetCuota (int cuotaId, int ventaId)
        {
            var cuota = _uow.CuotaRepository.Find(x => x.CuotaId == cuotaId && x.VentaId == ventaId);
            return new CuotaViewModel()
            {
                NumeroCuota = cuota.NumeroCuota.ToString(),
                FechaVencimiento = cuota.FechaVencimiento,
                Estado = cuota.Estado,
                Precio = cuota.Precio,
                CuotaId = cuota.CuotaId,
                VentaId = cuota.VentaId
            };
        }

        public CuotaViewModel PagarCuota (int cuotaId, int ventaId)
        {
            var cuota = _uow.CuotaRepository.AllIncluding(v => v.Venta, p => p.Venta.Proyecto, l => l.Venta.Lote, c => c.Venta.Cliente, y => y.Pago).FirstOrDefault(x => x.CuotaId == cuotaId && x.VentaId == x.VentaId);
            var pago = new Pago()
            {
                CuotaId = cuota.CuotaId,
                Fecha = DateTime.Now,
            };
            var pagoInsert = _uow.PagoRepository.Create(pago);
            _uow.PagoRepository.Save();
            if (pagoInsert != null)
            {
                cuota.Estado = EstadosHelper.EstadoCuota.PAGADO.ToString();
                _uow.CuotaRepository.Update(cuota);
                _uow.CuotaRepository.Save();
            }
            try
            {
                sender.SendMailPagoRealizado(cuota, GetProximoVencimiento(cuotaId, ventaId));
                _logger.LogInformation("Email de pago realizado enviado al cliente con extio");
            }
            catch (Exception e)
            {
                _logger.LogError("Ocurrió un error al enviar el email de pago realizado. Error {0}", e);
            }
            return new CuotaViewModel()
            {
                CuotaId = cuota.CuotaId,
                NumeroCuota = cuota.NumeroCuota.ToString()
            };
        }
        public ReciboModel GetCuotaForRecibo (int CuotaId, int VentaId)
        {
            var venta = _uow.VentaRepository.FilterIncluding(x => x.VentaId == VentaId, y => y.Cliente, l => l.Lote, p => p.Proyecto).FirstOrDefault();
            var model = _uow.CuotaRepository.AllIncluding(p => p.Pago).FirstOrDefault(x => x.VentaId == VentaId && x.CuotaId == CuotaId);
            var cuota = new CuotaViewModel() {
                Estado = model.Estado,
                FechaVencimiento = model.FechaVencimiento,
                NumeroCuota = model.NumeroCuota.ToString(),
                Precio = model.Precio,
                SubTotal = model.SubTotal,
                VentaId = model.VentaId,
                Pago = model.Pago,
                Venta = venta
            };
            return new ReciboModel()
            {
                Cuota = cuota,
                NextVencimiento = GetProximoVencimiento(CuotaId, VentaId)
            };
        }
        public DateTime? GetProximoVencimiento (int CuotaId, int VentaId)
        {
            var cuotaPagada = _uow.CuotaRepository.Find(x => x.VentaId == VentaId && x.CuotaId == CuotaId);
            var cuotasXVenta = _uow.CuotaRepository.FilterIncluding(x => x.VentaId == VentaId, p => p.Pago);
            if (GetLastCuota(VentaId) == cuotaPagada.NumeroCuota)
            {
                return null;
            }
            cuotasXVenta = cuotasXVenta.Where(x => x.Estado != EstadosHelper.EstadoCuota.PAGADO.ToString()).OrderBy(x => x.NumeroCuota);
            var nextCuota = cuotasXVenta.FirstOrDefault(x => x.NumeroCuota == (cuotaPagada.NumeroCuota + 1));
            return nextCuota.FechaVencimiento;
        }
        public int GetLastCuota (int VentaId)
        {
            var cuotasXVenta = _uow.CuotaRepository.Filter(x => x.VentaId == VentaId).ToList().Last();
            return cuotasXVenta.NumeroCuota;
        }

        public bool EnablePay(List<Cuota> model, int ventaId, int numeroCuota)
        {
            if (numeroCuota == 1)
            {
                return true;
            }
            else
            {
                var lastCuota = model.Last();
                if (lastCuota.NumeroCuota == numeroCuota)
                {
                    var cuotaPagaAnteriores = model.Where(x => x.Estado == EstadosHelper.EstadoCuota.PENDIENTE.ToString()).Except(model.Where(x => x.CuotaId == lastCuota.CuotaId));
                    if (cuotaPagaAnteriores.Any())
                    {
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                var cuotasAnteriores = new List<Cuota>();
                for (var i = 1; i < numeroCuota; i++)
                {
                    var cuotaByNumero = model.FirstOrDefault(x => x.NumeroCuota == i);
                    cuotasAnteriores.Add(cuotaByNumero);
                }
                if (cuotasAnteriores.Where(x => x.Estado == EstadosHelper.EstadoCuota.PENDIENTE.ToString()).Any())
                {
                    return false;
                }
                else
                {
                    return true;
                }
            }
        }
    }
}
