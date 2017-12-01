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
        public int GetDepartByIDUser(string UsuarioID) {
            var departamento = (from depart in db.Departamento
                                join perfil in db.Perfiles on depart.IDDepartamento equals perfil.IDDepartamento
                                where perfil.UsuarioID == UsuarioID
                                select depart.IDDepartamento).SingleOrDefault();
            return departamento;
        }
    }
}