using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MVCHelpDesk.Models;
namespace MVCHelpDesk.Helper
{
    public class Helpers
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        public Helpers() {

        }
        public List<Departamento> GetDepartamento() {
            var departamento = db.Departamento.ToList();
            return departamento;
        }
    }
}