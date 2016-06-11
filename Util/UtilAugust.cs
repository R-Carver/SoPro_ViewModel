using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1.Util
{
    public class UtilAugust
    {
        /*<!--Oberhauser/-->*/
        //Gets the Departments of Dispatcher depend on the Mandant
        public static IQueryable<ContractUser> GetCoordinatorsFromClient(string client, MyDbContext db)
        {
            var subQuery = from c in db.MU_Relations
                           where c.Mandant.MandantName.Equals(client)
                           select c;
            var query = from u in db.Users
                        from c in subQuery
                        where (
                            u.Id.Equals(c.UserID)
                        )
                        select u;
            return query;
        }

      
        //Gets the Departments of Dispatcher depend on the Mandant
        public static IQueryable<Department> GetDepartmentsFromClient(string client, MyDbContext db)
        {
            var query = from d in db.Departments
                        where d.Mandant.MandantName.Equals(client)
                        select d;
            return query;
        }

        //Gets the Dispatchers depend on the Department
        public static IQueryable<ContractUser> GetDispatchersFromDepartment(string department, MyDbContext db)
        {
            /*
            var subQuery =  from du in db.DU_Relations
                            where du.Department.DepartmentName.Equals(selection)
                            select du;
            var query = from u in db.Users
                        join du in subQuery on u.Id equals du.UserID
                        select u;
            var data = new SelectList(query, "Id", "UserName").ToList();
            return Json(data);
            */
            var subQuery = from dep in db.Departments
                           where dep.DepartmentName.Equals(department)
                           select dep;
            var query = from u in db.Users
                        from dep in subQuery
                        where (
                            u.Id.Equals(dep.DispatcherId)
                            || u.Id.Equals(dep.StvDispatcherId)
                        )
                        select u;
            return query;
        }

        

        //Gets the Departments of UserEmail
        public static IQueryable<Department> GetDepartmentsOfUser(string userName, MyDbContext db)
        {
            var subQuery = from du in db.DU_Relations
                           where du.User.UserName.Equals(userName)
                           select du;
            var query = from dep in db.Departments
                        join du in subQuery on dep.Id equals du.DepartmentID
                        select dep;
            return query;

        }

        //Gets the Client of Department
        public static IQueryable<Mandant> GetClientOfDepartment(string department, MyDbContext db)
        {
            var subQuery = from dep in db.Departments
                           where dep.DepartmentName.Equals(department)
                           select dep;
            var query = from c in db.Mandants
                        join dep in subQuery on c.Id equals dep.Mandant.Id
                        select c;
            return query;
        }

        /*<!--Oberhauser end/-->*/
    }
}