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
            });
            }
            viewDashBoard.Add(new AplicoMensual
            {
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
            });
            int aplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega <= w.FechaFinalizacion).Count();
            int noAplico = db.Tasks.Where(w => w.StatusIDActual == 5 && w.FechaEntrega >= w.FechaFinalizacion).Count();

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
            List<ViewDashboard> viewDashBoard = new List<ViewDashboard>();
            foreach (var item in db.Status.ToList())
            {
                viewDashBoard.Add(new ViewDashboard
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
                });
            }
            return View(viewDashBoard.ToList());
        }

        public ActionResult DashBoardUsuario() {
            return View();
        }
        /*PRUEBA*/
        public JsonResult getCantidadTask(string fechaInicio, string fechaFinal)
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

            List<ViewDashboard> viewDashBoard = new List<ViewDashboard>();
            foreach (var item in db.Status.ToList())
            {
                viewDashBoard.Add(new ViewDashboard
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
                });
            }

            List<dynamic> pruena = new List<dynamic>();

            var jsonData = new
            {
                data = viewDashBoard.ToList()
            };
            return Json(jsonData, JsonRequestBehavior.AllowGet);
        }
    }

}