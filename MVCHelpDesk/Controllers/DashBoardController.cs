﻿using MVCHelpDesk.Helper;
using MVCHelpDesk.Models;
using MVCHelpDesk.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCHelpDesk.Controllers
{
    [Authorize]
    public class DashBoardController : Controller
    {
        private Helpers helper = new Helpers();
        private UserIdentity userIdentity = new UserIdentity();

        public List<MaestroTaskStatus> ListCantidadTask { get; set; }
        public Helper.FechasDashBoard fechasDashBoard = new Helper.FechasDashBoard();
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: DashBoard
        public ActionResult Index()
        {
            DateTime InicioEnero = fechasDashBoard.InicioEnero();
            DateTime FinEnero = fechasDashBoard.FinEnero();
            DateTime InicioFebrero = fechasDashBoard.InicioFebrero();
            DateTime FinFebrero = fechasDashBoard.FinFebrero();
            DateTime InicioMarzo = fechasDashBoard.InicioMarzo();
            DateTime FinMarzo = fechasDashBoard.FinMarzo();
            DateTime InicioAbril = fechasDashBoard.InicioAbril();
            DateTime FinAbril = fechasDashBoard.FinAbril();
            DateTime InicioMayo = fechasDashBoard.InicioMayo();
            DateTime FinMayo = fechasDashBoard.FinMayo();
            DateTime InicioJunio = fechasDashBoard.FinJunio();
            DateTime FinJunio = fechasDashBoard.FinJunio();
            DateTime InicioJulio = fechasDashBoard.InicioJulio();
            DateTime FinJulio = fechasDashBoard.FinJulio();
            DateTime InicioAgosto = fechasDashBoard.InicioAgosto();
            DateTime FinAgosto = fechasDashBoard.FinAgosto();
            DateTime InicioSeptiembre = fechasDashBoard.InicioSeptiembre();
            DateTime FinSeptiembre = fechasDashBoard.FinSeptiembre();
            DateTime InicioOctubre = fechasDashBoard.InicioOctubre();
            DateTime FinOctubre = fechasDashBoard.FinOctubre();
            DateTime InicioNoviembre = fechasDashBoard.InicioNoviembre();
            DateTime FinNoviembre = fechasDashBoard.FinNoviembre();
            DateTime InicioDiciembre = fechasDashBoard.InicioDiciembre();
            DateTime FinDiciembre = fechasDashBoard.FinDiciembre();

            List<AplicoMensual> viewDashBoard = new List<AplicoMensual>();
            foreach (var item in db.Status.ToList())
            {
                viewDashBoard.Add(new AplicoMensual
                {
                    statusNombre = item.nombre,
                    enero = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioEnero && w.Fecha <= FinEnero && w.StatusID == item.StatusID).Count(),
                    febrero = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioFebrero && w.Fecha <= FinFebrero && w.StatusID == item.StatusID).Count(),
                    marzo = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioMarzo && w.Fecha <= FinMarzo && w.StatusID == item.StatusID).Count(),
                    abril = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioAbril && w.Fecha <= FinAbril && w.StatusID == item.StatusID).Count(),
                    mayo = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioMayo && w.Fecha <= FinMayo && w.StatusID == item.StatusID).Count(),
                    junio = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioJunio && w.Fecha <= FinJunio && w.StatusID == item.StatusID).Count(),
                    julio = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioJulio && w.Fecha <= FinJulio && w.StatusID == item.StatusID).Count(),
                    agosto = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioAgosto && w.Fecha <= FinAgosto && w.StatusID == item.StatusID).Count(),
                    septiembre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioSeptiembre && w.Fecha <= FinSeptiembre && w.StatusID == item.StatusID).Count(),
                    octubre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioOctubre && w.Fecha <= FinOctubre && w.StatusID == item.StatusID).Count(),
                    noviembre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioNoviembre && w.Fecha <= FinNoviembre && w.StatusID == item.StatusID).Count(),
                    diciembre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioDiciembre && w.Fecha <= FinDiciembre && w.StatusID == item.StatusID).Count(),
                    TotalAsignado = db.MaestroTaskStatus.Where(w => w.StatusID == 1 && w.Fecha >= InicioEnero && w.Fecha <= FinDiciembre).Count(),
                    TotalDesarrollo = db.MaestroTaskStatus.Where(w => w.StatusID == 2 && w.Fecha >= InicioEnero && w.Fecha <= FinDiciembre).Count(),
                    TotalRealizado = db.MaestroTaskStatus.Where(w => w.StatusID == 3 && w.Fecha >= InicioEnero && w.Fecha <= FinDiciembre).Count(),
                    TotalRechazado = db.MaestroTaskStatus.Where(w => w.StatusID == 4 && w.Fecha >= InicioEnero && w.Fecha <= FinDiciembre).Count(),


                    //aplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega <= w.FechaFinalizacion).Count(),
                    //noAplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega >= w.FechaFinalizacion).Count(),
                    aplicoEnero = db.Tasks.Where(w => w.FechaEntrega >= InicioEnero && w.FechaEntrega <= FinEnero && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoFebrero = db.Tasks.Where(w => w.FechaEntrega >= InicioFebrero && w.FechaEntrega <= FinFebrero && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoMarzo = db.Tasks.Where(w => w.FechaEntrega >= InicioMarzo && w.FechaEntrega <= FinMarzo && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoAbril = db.Tasks.Where(w => w.FechaEntrega >= InicioAbril && w.FechaEntrega <= FinAbril && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoMayo = db.Tasks.Where(w => w.FechaEntrega >= InicioMayo && w.FechaEntrega <= FinMayo && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoJunio = db.Tasks.Where(w => w.FechaEntrega >= InicioJunio && w.FechaEntrega <= FinJunio && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoJulio = db.Tasks.Where(w => w.FechaEntrega >= InicioJulio && w.FechaEntrega <= FinJulio && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoAgosto = db.Tasks.Where(w => w.FechaEntrega >= InicioAgosto && w.FechaEntrega <= FinAgosto && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoSeptiembre = db.Tasks.Where(w => w.FechaEntrega >= InicioSeptiembre && w.FechaEntrega <= FinSeptiembre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoOctubre = db.Tasks.Where(w => w.FechaEntrega >= InicioOctubre && w.FechaEntrega <= FinOctubre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoNoviembre = db.Tasks.Where(w => w.FechaEntrega >= InicioNoviembre && w.FechaEntrega <= FinNoviembre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoDiciembre = db.Tasks.Where(w => w.FechaEntrega >= InicioDiciembre && w.FechaEntrega <= FinDiciembre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),

                    noAplicoEnero = db.Tasks.Where(w => w.FechaEntrega >= InicioEnero && w.FechaEntrega <= FinEnero && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoFebrero = db.Tasks.Where(w => w.FechaEntrega >= InicioFebrero && w.FechaEntrega <= FinFebrero && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoMarzo = db.Tasks.Where(w => w.FechaEntrega >= InicioMarzo && w.FechaEntrega <= FinMarzo && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoAbril = db.Tasks.Where(w => w.FechaEntrega >= InicioAbril && w.FechaEntrega <= FinAbril && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoMayo = db.Tasks.Where(w => w.FechaEntrega >= InicioMayo && w.FechaEntrega <= FinMayo && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoJunio = db.Tasks.Where(w => w.FechaEntrega >= InicioJunio && w.FechaEntrega <= FinJunio && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoJulio = db.Tasks.Where(w => w.FechaEntrega >= InicioJulio && w.FechaEntrega <= FinJulio && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoAgosto = db.Tasks.Where(w => w.FechaEntrega >= InicioAgosto && w.FechaEntrega <= FinAgosto && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoSeptiembre = db.Tasks.Where(w => w.FechaEntrega >= InicioSeptiembre && w.FechaEntrega <= FinSeptiembre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoOctubre = db.Tasks.Where(w => w.FechaEntrega >= InicioOctubre && w.FechaEntrega <= FinOctubre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoNoviembre = db.Tasks.Where(w => w.FechaEntrega >= InicioNoviembre && w.FechaEntrega <= FinNoviembre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoDiciembre = db.Tasks.Where(w => w.FechaEntrega >= InicioDiciembre && w.FechaEntrega <= FinDiciembre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),

                    aplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega <= w.FechaFinalizacion).Count(),
                    noAplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega >= w.FechaFinalizacion).Count()
                });
            }


            return View(viewDashBoard.ToList());
        }
        [HttpPost]
        public ActionResult Index(string FechaInicio, string fechaFinal)
        {
            DateTime InicioEnero = fechasDashBoard.InicioEnero();
            DateTime FinEnero = fechasDashBoard.FinEnero();
            DateTime InicioFebrero = fechasDashBoard.InicioFebrero();
            DateTime FinFebrero = fechasDashBoard.FinFebrero();
            DateTime InicioMarzo = fechasDashBoard.InicioMarzo();
            DateTime FinMarzo = fechasDashBoard.FinMarzo();
            DateTime InicioAbril = fechasDashBoard.InicioAbril();
            DateTime FinAbril = fechasDashBoard.FinAbril();
            DateTime InicioMayo = fechasDashBoard.InicioMayo();
            DateTime FinMayo = fechasDashBoard.FinMayo();
            DateTime InicioJunio = fechasDashBoard.FinJunio();
            DateTime FinJunio = fechasDashBoard.FinJunio();
            DateTime InicioJulio = fechasDashBoard.InicioJulio();
            DateTime FinJulio = fechasDashBoard.FinJulio();
            DateTime InicioAgosto = fechasDashBoard.InicioAgosto();
            DateTime FinAgosto = fechasDashBoard.FinAgosto();
            DateTime InicioSeptiembre = fechasDashBoard.InicioSeptiembre();
            DateTime FinSeptiembre = fechasDashBoard.FinSeptiembre();
            DateTime InicioOctubre = fechasDashBoard.InicioOctubre();
            DateTime FinOctubre = fechasDashBoard.FinOctubre();
            DateTime InicioNoviembre = fechasDashBoard.InicioNoviembre();
            DateTime FinNoviembre = fechasDashBoard.FinNoviembre();
            DateTime InicioDiciembre = fechasDashBoard.InicioDiciembre();
            DateTime FinDiciembre = fechasDashBoard.FinDiciembre();

            DateTime FInicio = Convert.ToDateTime(FechaInicio).Date;
            DateTime FFin = Convert.ToDateTime(fechaFinal).Date;
            List<AplicoMensual> viewDashBoard = new List<AplicoMensual>();
            foreach (var item in db.Status.ToList())
            {
                viewDashBoard.Add(new AplicoMensual
                {
                    statusNombre = item.nombre,
                    enero = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioEnero && w.Fecha <= FinEnero && w.StatusID == item.StatusID).Count(),
                    febrero = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioFebrero && w.Fecha <= FinFebrero && w.StatusID == item.StatusID).Count(),
                    marzo = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioMarzo && w.Fecha <= FinMarzo && w.StatusID == item.StatusID).Count(),
                    abril = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioAbril && w.Fecha <= FinAbril && w.StatusID == item.StatusID).Count(),
                    mayo = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioMayo && w.Fecha <= FinMayo && w.StatusID == item.StatusID).Count(),
                    junio = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioJunio && w.Fecha <= FinJunio && w.StatusID == item.StatusID).Count(),
                    julio = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioJulio && w.Fecha <= FinJulio && w.StatusID == item.StatusID).Count(),
                    agosto = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioAgosto && w.Fecha <= FinAgosto && w.StatusID == item.StatusID).Count(),
                    septiembre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioSeptiembre && w.Fecha <= FinSeptiembre && w.StatusID == item.StatusID).Count(),
                    octubre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioOctubre && w.Fecha <= FinOctubre && w.StatusID == item.StatusID).Count(),
                    noviembre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioNoviembre && w.Fecha <= FinNoviembre && w.StatusID == item.StatusID).Count(),
                    diciembre = db.MaestroTaskStatus.Where(w => w.Fecha >= InicioDiciembre && w.Fecha <= FinDiciembre && w.StatusID == item.StatusID).Count(),

                    TotalAsignado = db.MaestroTaskStatus.Where(w => w.StatusID == 1 && w.Fecha >= FInicio && w.Fecha <= FFin).Count(),
                    TotalDesarrollo = db.MaestroTaskStatus.Where(w => w.StatusID == 2 && w.Fecha >= FInicio && w.Fecha <= FFin).Count(),
                    TotalRealizado = db.MaestroTaskStatus.Where(w => w.StatusID == 3 && w.Fecha >= FInicio && w.Fecha <= FFin).Count(),
                    TotalRechazado = db.MaestroTaskStatus.Where(w => w.StatusID == 4 && w.Fecha >= FInicio && w.Fecha <= FFin).Count(),

                    aplicoEnero = db.Tasks.Where(w => w.FechaEntrega >= InicioEnero && w.FechaEntrega <= FinEnero && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoFebrero = db.Tasks.Where(w => w.FechaEntrega >= InicioFebrero && w.FechaEntrega <= FinFebrero && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoMarzo = db.Tasks.Where(w => w.FechaEntrega >= InicioMarzo && w.FechaEntrega <= FinMarzo && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoAbril = db.Tasks.Where(w => w.FechaEntrega >= InicioAbril && w.FechaEntrega <= FinAbril && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoMayo = db.Tasks.Where(w => w.FechaEntrega >= InicioMayo && w.FechaEntrega <= FinMayo && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoJunio = db.Tasks.Where(w => w.FechaEntrega >= InicioJunio && w.FechaEntrega <= FinJunio && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoJulio = db.Tasks.Where(w => w.FechaEntrega >= InicioJulio && w.FechaEntrega <= FinJulio && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoAgosto = db.Tasks.Where(w => w.FechaEntrega >= InicioAgosto && w.FechaEntrega <= FinAgosto && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoSeptiembre = db.Tasks.Where(w => w.FechaEntrega >= InicioSeptiembre && w.FechaEntrega <= FinSeptiembre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoOctubre = db.Tasks.Where(w => w.FechaEntrega >= InicioOctubre && w.FechaEntrega <= FinOctubre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoNoviembre = db.Tasks.Where(w => w.FechaEntrega >= InicioNoviembre && w.FechaEntrega <= FinNoviembre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    aplicoDiciembre = db.Tasks.Where(w => w.FechaEntrega >= InicioDiciembre && w.FechaEntrega <= FinDiciembre && w.FechaEntrega <= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),

                    noAplicoEnero = db.Tasks.Where(w => w.FechaEntrega >= InicioEnero && w.FechaEntrega <= FinEnero && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoFebrero = db.Tasks.Where(w => w.FechaEntrega >= InicioFebrero && w.FechaEntrega <= FinFebrero && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoMarzo = db.Tasks.Where(w => w.FechaEntrega >= InicioMarzo && w.FechaEntrega <= FinMarzo && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoAbril = db.Tasks.Where(w => w.FechaEntrega >= InicioAbril && w.FechaEntrega <= FinAbril && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoMayo = db.Tasks.Where(w => w.FechaEntrega >= InicioMayo && w.FechaEntrega <= FinMayo && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoJunio = db.Tasks.Where(w => w.FechaEntrega >= InicioJunio && w.FechaEntrega <= FinJunio && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoJulio = db.Tasks.Where(w => w.FechaEntrega >= InicioJulio && w.FechaEntrega <= FinJulio && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoAgosto = db.Tasks.Where(w => w.FechaEntrega >= InicioAgosto && w.FechaEntrega <= FinAgosto && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoSeptiembre = db.Tasks.Where(w => w.FechaEntrega >= InicioSeptiembre && w.FechaEntrega <= FinSeptiembre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoOctubre = db.Tasks.Where(w => w.FechaEntrega >= InicioOctubre && w.FechaEntrega <= FinOctubre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoNoviembre = db.Tasks.Where(w => w.FechaEntrega >= InicioNoviembre && w.FechaEntrega <= FinNoviembre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),
                    noAplicoDiciembre = db.Tasks.Where(w => w.FechaEntrega >= InicioDiciembre && w.FechaEntrega <= FinDiciembre && w.FechaEntrega >= w.FechaFinalizacion && w.StatusIDActual == 5).Count(),

                    aplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega <= w.FechaFinalizacion && w.FechaEntrega >= FInicio && w.FechaEntrega <= FFin).Count(),
                    noAplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega >= w.FechaFinalizacion && w.FechaEntrega >= FInicio && w.FechaEntrega <= FFin).Count()
                });
            }
            return View(viewDashBoard.ToList());
        }

        public ActionResult DashBoardUsuario()
        {
            var UserID = userIdentity.GetIdUser();
            var idDepartamento = helper.GetDepartByIDUser(UserID);
            var perfil = helper.AllUserByDepart(idDepartamento);

            ViewBag.Perfil = new SelectList(perfil, "UsuarioID", "Nombre");
            List<AplicoMensual> viewDashBoard = new List<AplicoMensual>();
            foreach (var item in db.Status.ToList())
            {
                viewDashBoard.Add(new AplicoMensual
                {
                    enero = 0,
                    febrero = 0,
                    marzo = 0,
                    abril = 0,
                    mayo = 0,
                    junio = 0,
                    julio = 0,
                    agosto = 0,
                    septiembre = 0,
                    octubre = 0,
                    noviembre = 0,
                    diciembre = 0,
                    TotalAsignado = 0,
                    TotalDesarrollo = 0,
                    TotalRealizado = 0,
                    TotalRechazado = 0,
                    aplicoEnero = 0,
                    aplicoFebrero = 0,
                    aplicoMarzo = 0,
                    aplicoAbril = 0,
                    aplicoMayo = 0,
                    aplicoJunio = 0,
                    aplicoJulio = 0,
                    aplicoAgosto = 0,
                    aplicoSeptiembre = 0,
                    aplicoOctubre = 0,
                    aplicoNoviembre = 0,
                    aplicoDiciembre = 0,
                    noAplicoEnero = 0,
                    noAplicoFebrero = 0,
                    noAplicoMarzo = 0,
                    noAplicoAbril = 0,
                    noAplicoMayo = 0,
                    noAplicoJunio = 0,
                    noAplicoJulio = 0,
                    noAplicoAgosto = 0,
                    noAplicoSeptiembre = 0,
                    noAplicoOctubre = 0,
                    noAplicoNoviembre = 0,
                    noAplicoDiciembre = 0,
                    aplico = -1,
                    noAplico = 0,
                    statusNombre = item.nombre,
                });
            }

            return View(viewDashBoard.ToList());
        }
        [HttpPost]
        public ActionResult DashBoardUsuario(string FechaInicio, string fechaFinal, string DropPerfiles)
        {
            var UserID = userIdentity.GetIdUser();
            var idDepartamento = helper.GetDepartByIDUser(UserID);
            var perfil = helper.AllUserByDepart(idDepartamento);
            ViewBag.Perfil = new SelectList(perfil, "UsuarioID", "Nombre");

            DateTime InicioEnero = fechasDashBoard.InicioEnero();
            DateTime FinEnero = fechasDashBoard.FinEnero();
            DateTime InicioFebrero = fechasDashBoard.InicioFebrero();
            DateTime FinFebrero = fechasDashBoard.FinFebrero();
            DateTime InicioMarzo = fechasDashBoard.InicioMarzo();
            DateTime FinMarzo = fechasDashBoard.FinMarzo();
            DateTime InicioAbril = fechasDashBoard.InicioAbril();
            DateTime FinAbril = fechasDashBoard.FinAbril();
            DateTime InicioMayo = fechasDashBoard.InicioMayo();
            DateTime FinMayo = fechasDashBoard.FinMayo();
            DateTime InicioJunio = fechasDashBoard.FinJunio();
            DateTime FinJunio = fechasDashBoard.FinJunio();
            DateTime InicioJulio = fechasDashBoard.InicioJulio();
            DateTime FinJulio = fechasDashBoard.FinJulio();
            DateTime InicioAgosto = fechasDashBoard.InicioAgosto();
            DateTime FinAgosto = fechasDashBoard.FinAgosto();
            DateTime InicioSeptiembre = fechasDashBoard.InicioSeptiembre();
            DateTime FinSeptiembre = fechasDashBoard.FinSeptiembre();
            DateTime InicioOctubre = fechasDashBoard.InicioOctubre();
            DateTime FinOctubre = fechasDashBoard.FinOctubre();
            DateTime InicioNoviembre = fechasDashBoard.InicioNoviembre();
            DateTime FinNoviembre = fechasDashBoard.FinNoviembre();
            DateTime InicioDiciembre = fechasDashBoard.InicioDiciembre();
            DateTime FinDiciembre = fechasDashBoard.FinDiciembre();
            DateTime FInicio = FechaInicio == "" ? InicioEnero : Convert.ToDateTime(FechaInicio).Date;
            DateTime FFin = fechaFinal == "" ? FinDiciembre : Convert.ToDateTime(fechaFinal).Date;
            List<AplicoMensual> viewDashBoard = new List<AplicoMensual>();

            foreach (var item in db.Status.ToList())
            {
                viewDashBoard.Add(new AplicoMensual
                {
                    statusNombre = item.nombre,
                    enero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinEnero && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    febrero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioFebrero && w.maestroTask.Fecha <= FinFebrero && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    marzo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioMarzo && w.maestroTask.Fecha <= FinMarzo && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    abril = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioAbril && w.maestroTask.Fecha <= FinAbril && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    mayo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioMayo && w.maestroTask.Fecha <= FinMayo && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    junio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioJunio && w.maestroTask.Fecha <= FinJunio && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    julio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioJulio && w.maestroTask.Fecha <= FinJulio && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    agosto = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioAgosto && w.maestroTask.Fecha <= FinAgosto && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    septiembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioSeptiembre && w.maestroTask.Fecha <= FinSeptiembre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    octubre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioOctubre && w.maestroTask.Fecha <= FinOctubre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    noviembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioNoviembre && w.maestroTask.Fecha <= FinNoviembre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    diciembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioDiciembre && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),

                    TotalAsignado = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 1 && w.task.AsignadoID == DropPerfiles).Count(),
                    TotalDesarrollo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 2 && w.task.AsignadoID == DropPerfiles).Count(),
                    TotalRealizado = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 3 && w.task.AsignadoID == DropPerfiles).Count(),
                    TotalRechazado = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 4 && w.task.AsignadoID == DropPerfiles).Count(),

                    aplicoEnero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioEnero && w.task.FechaEntrega <= FinEnero && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoFebrero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioFebrero && w.task.FechaEntrega <= FinFebrero && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoMarzo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioMarzo && w.task.FechaEntrega <= FinMarzo && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoAbril = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioAbril && w.task.FechaEntrega <= FinAbril && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoMayo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioMayo && w.task.FechaEntrega <= FinMayo && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoJunio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioJunio && w.task.FechaEntrega <= FinJunio && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoJulio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioJulio && w.task.FechaEntrega <= FinJulio && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoAgosto = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioAgosto && w.task.FechaEntrega <= FinAgosto && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoSeptiembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioSeptiembre && w.task.FechaEntrega <= FinSeptiembre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoOctubre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioOctubre && w.task.FechaEntrega <= FinOctubre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoNoviembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioNoviembre && w.task.FechaEntrega <= FinNoviembre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoDiciembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioDiciembre && w.task.FechaEntrega <= FinDiciembre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),

                    noAplicoEnero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioEnero && w.task.FechaEntrega <= FinEnero && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoFebrero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioFebrero && w.task.FechaEntrega <= FinFebrero && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoMarzo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioMarzo && w.task.FechaEntrega <= FinMarzo && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoAbril = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioAbril && w.task.FechaEntrega <= FinAbril && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoMayo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioMayo && w.task.FechaEntrega <= FinMayo && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoJunio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioJunio && w.task.FechaEntrega <= FinJunio && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoJulio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioJulio && w.task.FechaEntrega <= FinJulio && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoAgosto = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioAgosto && w.task.FechaEntrega <= FinAgosto && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoSeptiembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioSeptiembre && w.task.FechaEntrega <= FinSeptiembre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoOctubre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioOctubre && w.task.FechaEntrega <= FinOctubre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoNoviembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioNoviembre && w.task.FechaEntrega <= FinNoviembre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoDiciembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.FechaEntrega >= InicioDiciembre && w.task.FechaEntrega <= FinDiciembre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),

                    aplico = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.StatusIDActual == 5 && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.FechaEntrega >= FInicio && w.task.FechaEntrega <= FFin && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplico = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                  maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                  taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                  (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                  .Where(w => w.task.StatusIDActual == 5 && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.FechaEntrega >= FInicio && w.task.FechaEntrega <= FFin && w.task.AsignadoID == DropPerfiles).Count(),
                });
            }

            #region PRUEBA
            ViewPrueba pruena = new ViewPrueba();
            pruena.listrololo = new List<trololo>();
            pruena.Estudiante = new List<Estudiante>();
            pruena.Repetidos = new List<Repetidos>();
            foreach (var item in db.Status.ToList())
            {
                pruena.Estudiante.Add(new Estudiante
                {
                    statusNombre = item.nombre,
                    enero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinEnero && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    febrero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioFebrero && w.maestroTask.Fecha <= FinFebrero && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    marzo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioMarzo && w.maestroTask.Fecha <= FinMarzo && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    abril = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioAbril && w.maestroTask.Fecha <= FinAbril && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    mayo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioMayo && w.maestroTask.Fecha <= FinMayo && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    junio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioJunio && w.maestroTask.Fecha <= FinJunio && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    julio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioJulio && w.maestroTask.Fecha <= FinJulio && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    agosto = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioAgosto && w.maestroTask.Fecha <= FinAgosto && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    septiembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioSeptiembre && w.maestroTask.Fecha <= FinSeptiembre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    octubre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioOctubre && w.maestroTask.Fecha <= FinOctubre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    noviembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioNoviembre && w.maestroTask.Fecha <= FinNoviembre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),
                    diciembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioDiciembre && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == item.StatusID && w.task.AsignadoID == DropPerfiles).Count(),

                    TotalAsignado = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 1 && w.task.AsignadoID == DropPerfiles).Count(),
                    TotalDesarrollo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 2 && w.task.AsignadoID == DropPerfiles).Count(),
                    TotalRealizado = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 3 && w.task.AsignadoID == DropPerfiles).Count(),
                    TotalRechazado = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.maestroTask.Fecha >= InicioEnero && w.maestroTask.Fecha <= FinDiciembre && w.maestroTask.StatusID == 4 && w.task.AsignadoID == DropPerfiles).Count(),

                    aplicoEnero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioEnero && w.task.FechaEntrega <= FinEnero && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoFebrero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioFebrero && w.task.FechaEntrega <= FinFebrero && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoMarzo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioMarzo && w.task.FechaEntrega <= FinMarzo && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoAbril = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioAbril && w.task.FechaEntrega <= FinAbril && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoMayo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioMayo && w.task.FechaEntrega <= FinMayo && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoJunio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioJunio && w.task.FechaEntrega <= FinJunio && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoJulio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioJulio && w.task.FechaEntrega <= FinJulio && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoAgosto = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioAgosto && w.task.FechaEntrega <= FinAgosto && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoSeptiembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioSeptiembre && w.task.FechaEntrega <= FinSeptiembre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoOctubre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioOctubre && w.task.FechaEntrega <= FinOctubre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoNoviembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioNoviembre && w.task.FechaEntrega <= FinNoviembre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    aplicoDiciembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioDiciembre && w.task.FechaEntrega <= FinDiciembre && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),

                    noAplicoEnero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioEnero && w.task.FechaEntrega <= FinEnero && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoFebrero = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioFebrero && w.task.FechaEntrega <= FinFebrero && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoMarzo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioMarzo && w.task.FechaEntrega <= FinMarzo && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoAbril = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioAbril && w.task.FechaEntrega <= FinAbril && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoMayo = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioMayo && w.task.FechaEntrega <= FinMayo && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoJunio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioJunio && w.task.FechaEntrega <= FinJunio && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoJulio = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioJulio && w.task.FechaEntrega <= FinJulio && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoAgosto = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioAgosto && w.task.FechaEntrega <= FinAgosto && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoSeptiembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioSeptiembre && w.task.FechaEntrega <= FinSeptiembre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoOctubre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioOctubre && w.task.FechaEntrega <= FinOctubre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoNoviembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioNoviembre && w.task.FechaEntrega <= FinNoviembre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplicoDiciembre = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.FechaEntrega >= InicioDiciembre && w.task.FechaEntrega <= FinDiciembre && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.StatusIDActual == 5 && w.task.AsignadoID == DropPerfiles).Count(),

                    aplico = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.StatusIDActual == 5 && w.task.FechaEntrega <= w.task.FechaFinalizacion && w.task.FechaEntrega >= FInicio && w.task.FechaEntrega <= FFin && w.task.AsignadoID == DropPerfiles).Count(),
                    noAplico = db.MaestroTaskStatus.Join(db.Tasks,//SE SELECCIONA LA TABLA A LA CUAL SE APLICARA EL JOIN
                                                   maestroIDTask => maestroIDTask.TaskID,//SE SELECCIONA LAS LLAVES
                                                   taskID => taskID.TaskID,//SE SELECCIONA LAS LLAVES
                                                   (maestroIDTask, taskID) => new { maestroTask = maestroIDTask, task = taskID })//LOS NOMBRE QUE TENDRAN NUESTRAS TABLAS
                                                   .Where(w => w.task.StatusIDActual == 5 && w.task.FechaEntrega >= w.task.FechaFinalizacion && w.task.FechaEntrega >= FInicio && w.task.FechaEntrega <= FFin && w.task.AsignadoID == DropPerfiles).Count(),
                });

            }
            var Requerimientos = (from maestro in db.MaestroTaskStatus
                                   join status in db.Status on maestro.StatusID equals status.StatusID
                                   join task in db.Tasks on maestro.TaskID equals task.TaskID
                                   join per in db.Perfiles on task.AsignadoID equals per.UsuarioID
                                   where task.AsignadoID == DropPerfiles
                                   select new
                                    {
                                        TaskID = maestro.TaskID,
                                         Nombre = status.nombre,
                                         StatusID = status.StatusID
                                   }).ToList();

                        //FUNCIONA INVESTIGAR https://stackoverflow.com/questions/18547354/c-sharp-linq-find-duplicates-in-list
                                var repetidos = Requerimientos.GroupBy(x => x)
                                      .Where(w => w.Count() > 1)
                                      .Select(s => new { Elemento = s.Key, Cantidad = s.Count() })
                                      .ToList();
            foreach (var item in repetidos)
            {
                pruena.Repetidos.Add(new Repetidos
                {
                    StatusNombre = item.Elemento.Nombre,
                    Cantidad =  item.Cantidad,
                    StatusID = item.Elemento.StatusID
                });
            }

            #endregion










            return View(viewDashBoard);
        }
    }
}