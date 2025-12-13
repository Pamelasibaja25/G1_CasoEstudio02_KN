using CasoEstudio2.Models;
using CasoEstudio2.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace CasoEstudio2.Controllers
{
    public class CasasController : Controller
    {

        [HttpGet]
        public ActionResult Consulta()
        {
            var resultado = ConsultarCasas();
            return View(resultado);
        }

        private List<CasasModel> ConsultarCasas()
        {
            using (var context = new CasoEstudioKNEntities())
            {
                // Ejecutar SP_ConsultarCasas
                var data = context.SP_ConsultarCasas().ToList();

                // Mapear al modelo propio
                var casas = data.Select(c => new CasasModel
                {
                    IdCasa = c.IdCasa,
                    DescripcionCasa = c.DescripcionCasa,
                    PrecioCasa = c.PrecioCasa,
                    UsuarioAlquiler = c.UsuarioAlquiler,
                    Estado = c.Estado,                     
                    FechaAlquilerString = c.FechaAlquiler  
                }).ToList();

                return casas;
            }
        }

        [HttpGet]
        public ActionResult Alquiler()
        {
            CargarCasasDisponibles();
            return View(new CasasModel());
        }


        [HttpPost]
        public ActionResult Alquiler(CasasModel casa)
        {

            if (casa.IdCasa <= 0)
            {
                ViewBag.Mensaje = "Debe seleccionar una casa.";
                CargarCasasDisponibles();
                return View(casa);
            }

            if (string.IsNullOrWhiteSpace(casa.UsuarioAlquiler))
            {
                ViewBag.Mensaje = "Debe ingresar un usuario para alquilar.";
                CargarCasasDisponibles();
                return View(casa);
            }

            using (var context = new CasoEstudioKNEntities())
            {

                var resultado = context.SP_AlquilarCasa(
                    casa.IdCasa,
                    casa.UsuarioAlquiler
                );

                if (resultado > 0)
                {
                    return RedirectToAction("Consulta", "Casas");
                }

                ViewBag.Mensaje = "No fue posible alquilar la casa. Verifique que siga disponible.";
                CargarCasasDisponibles();
                return View(casa);
            }
        }


        private void CargarCasasDisponibles()
        {
            using (var context = new CasoEstudioKNEntities())
            {
                var data = context.SP_CasasDisponibles().ToList();

                var casas = data.Select(c => new SelectListItem
                {
                    Value = c.IdCasa.ToString(),
                    Text = c.DescripcionCasa
                }).ToList();

                casas.Insert(0, new SelectListItem
                {
                    Value = "",
                    Text = "Seleccione una casa"
                });

                ViewBag.ListaCasas = casas;
            }
        }


        [HttpGet]
        public ActionResult ObtenerPrecio(long id)
        {
            using (var context = new CasoEstudioKNEntities())
            {
                var data = context.SP_ObtenerCasaPorId(id).FirstOrDefault();

                if (data != null)
                {
                    return Json(new
                    {
                        Precio = data.PrecioCasa
                    }, JsonRequestBehavior.AllowGet);
                }

                return Json(null, JsonRequestBehavior.AllowGet);
            }
        }
    }
}
