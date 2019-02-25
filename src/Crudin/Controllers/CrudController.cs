using Couchbase;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Couchbase.Core;
using Crudin.Models;
using Couchbase.N1QL;
using Couchbase.Configuration.Client;

namespace Crudin.Controllers
{
  
  
    public class CrudController:Controller
    {
        private IBucket bucket;
        

        public CrudController()
        {
            bucket = ClusterHelper.GetBucket("Test");
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Create()
        {
            return View();
        }
        public IActionResult CreateData(UserModel Item)
        {




            if (Item.Password == Item.Confirmpassword && Item.Password.Length<4||Item.Password.Length>8)
            {
                bucket.Upsert(Item.id, new { Item.id,Item.FirstName, Item.LastName, Item.Age, Item.Password, Item.Confirmpassword, Item.Contact, Item.Address, Item.Country, Item.State, Item.Zipcode });
         
            }
            else
            {
                return Content("Please Check Password");
            }
            return Redirect("/");
         
        }

        public IActionResult Delete()
        {
            return View();
        }
        public IActionResult DeleteData(UserModel Item)
        {
            bucket.Remove(Item.id);



            return Content("Successfully Deleted");

        }

        public IActionResult Update()
        {
            return View();
        }
        public IActionResult UpdateData(UserModel Item)
        {

           
           
                bucket.Replace(Item.id, new { Item.FirstName, Item.LastName, Item.Age, Item.Contact, Item.Address, Item.Country, Item.State, Item.Zipcode });

          


            return Content("Successfully Updated");

        }
        public IActionResult GetData()
        {
            return View();
        }
        public IActionResult GetAllData(UserModel Item)
        {
            UserModel data = bucket.Get<UserModel>(Item.id).Value;
            return View(data);

        }

        //public IActionResult Show()
        //{
        //    return View();
        //}
        public IActionResult Show()// incomplete
        {

            var config = new ClientConfiguration { Servers = new List<Uri> { new Uri("couchbase://127.0.0.1:8091") } };
            Cluster cluster = new Cluster(config);
            cluster.Authenticate("Administrator", "ritesh");
            bucket = cluster.OpenBucket("Test");
            var n1ql = @"select g.* from Test g where country='India'";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = bucket.Query<UserModel>(query);
            return View(result.Rows);

        }
        public IActionResult deleted(UserModel Item)
        {
           
                var config = new ClientConfiguration { Servers = new List<Uri> { new Uri("couchbase://127.0.0.1:8091") } };
            Cluster cluster = new Cluster(config);
            cluster.Authenticate("Administrator", "ritesh");
            bucket = cluster.OpenBucket("Test");
            var n1ql = @"delete from Test where id="+"'"+Item.id+"'";
            var query = QueryRequest.Create(n1ql);
            query.ScanConsistency(ScanConsistency.RequestPlus);
            var result = bucket.Query<UserModel>(query);
            return View(result.Rows);
           

            
        }

    }
}
