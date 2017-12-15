using System.Collections.Generic;
using System.Linq;
using MVCHelpDesk.Models;
using System;

namespace MVCHelpDesk.Helper
{
    public class Helpers
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Helpers()
        {

        }
        public List<Departamento> GetDepartamento()
        {
            var departamento = db.Departamento.ToList();
            return departamento;
        }
        public int GetDepartByIDUser(string UsuarioID)
        {
            var departamento = (from depart in db.Departamento
                                join perfil in db.Perfiles on depart.IDDepartamento equals perfil.IDDepartamento
                                where perfil.UsuarioID == UsuarioID
                                select depart.IDDepartamento).SingleOrDefault();
            return departamento;
        }
        public bool CantidadArchivos(int TaskID)
        {

            int CantidadArchivo = db.Files.Where(w => w.TasksID == TaskID).Count();
            if (CantidadArchivo >= 3)
            {
                return true;
            }
            return false;
        }

    }
    public class FechasDashBoard
    {
        public DateTime InicioEnero()
        {
            return Convert.ToDateTime("2017/01/01").Date;
        }
        public DateTime FinEnero()
        {
            return Convert.ToDateTime("2017/01/31").Date;
        }
        public DateTime InicioFebrero()
        {
            return Convert.ToDateTime("2017/02/01").Date;
        }
        public DateTime FinFebrero()
        {
            return Convert.ToDateTime("2017/02/28").Date;
        }
        public DateTime InicioMarzo()
        {
            return Convert.ToDateTime("2017/03/01").Date;
        }
        public DateTime FinMarzo()
        {
            return Convert.ToDateTime("2017/03/30").Date;
        }
        public DateTime InicioAbril()
        {
            return Convert.ToDateTime("2017/04/01").Date;
        }
        public DateTime FinAbril()
        {
            return Convert.ToDateTime("2017/04/30").Date;
        }

        public DateTime InicioMayo()
        {
            return Convert.ToDateTime("2017/05/01").Date;
        }
        public DateTime FinMayo()
        {
            return Convert.ToDateTime("2017/05/30").Date;
        }
        public DateTime InicioJunio()
        {
            return Convert.ToDateTime("2017/06/01").Date;
        }
        public DateTime FinJunio()
        {
            return Convert.ToDateTime("2017/06/30").Date;
        }
        public DateTime InicioJulio()
        {
            return Convert.ToDateTime("2017/07/01").Date;
        }
        public DateTime FinJulio()
        {
            return Convert.ToDateTime("2017/07/30").Date;
        }
        public DateTime InicioAgosto()
        {
            return Convert.ToDateTime("2017/08/01").Date;
        }
        public DateTime FinAgosto()
        {
            return Convert.ToDateTime("2017/08/30").Date;
        }
        public DateTime InicioSeptiembre()
        {
            return Convert.ToDateTime("2017/09/01").Date;
        }
        public DateTime FinSeptiembre()
        {
            return Convert.ToDateTime("2017/09/30").Date;
        }
        public DateTime InicioOctubre()
        {
            return Convert.ToDateTime("2017/10/01").Date;
        }
        public DateTime FinOctubre()
        {
            return Convert.ToDateTime("2017/10/30").Date;
        }
        public DateTime InicioNoviembre()
        {
            return Convert.ToDateTime("2017/11/01").Date;
        }
        public DateTime FinNoviembre()
        {
            return Convert.ToDateTime("2017/11/30").Date;
        }
        public DateTime InicioDiciembre()
        {
            return Convert.ToDateTime("2017/12/01").Date;
        }
        public DateTime FinDiciembre()
        {
            return Convert.ToDateTime("2017/12/30").Date;
        }
    }
}