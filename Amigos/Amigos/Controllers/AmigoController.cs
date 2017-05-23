using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Amigos.Models;
using Amigos.DataAccessLayer;
using DistancesLibrary;
using System.Device.Location;

namespace Amigos.Controllers
{
    public class AmigoController : Controller
    {
        private AmigoDBContext db = new AmigoDBContext();
        private List<Amigo> Filtrados = new List<Amigo>();

        public ViewResult Index(string Latitude, string Longitude, string Distance)
        {
            
            if (!String.IsNullOrEmpty(Latitude) && !String.IsNullOrEmpty(Longitude) && !String.IsNullOrEmpty(Distance))
            {
                var amigo = from s in db.Amigos select s;

                double Lat = double.Parse(Latitude);
                double Lon = double.Parse(Longitude);
                double DisKm = double.Parse(Distance);
                double DisMeters = DisKm*1000;

                var sCoord = new GeoCoordinate(Lat, Lon);
                foreach (Amigo a in amigo)
                {
                    var eCoord = new GeoCoordinate(double.Parse(a.lati), double.Parse(a.longi));
                    
                    if (eCoord.GetDistanceTo(sCoord) < DisMeters)
                    {
                        Filtrados.Add(a);
                    }
                }

                return View(Filtrados);
            }

            else
            {
                return View(db.Amigos.ToList());
            }
        }

        // GET: /Amigo/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigos.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }
            return View(amigo);
        }

        public ActionResult DetailsJSON(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigos.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }
            return View(amigo);
        }

        // GET: /Amigo/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: /Amigo/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include="ID,name,longi,lati")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                db.Amigos.Add(amigo);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(amigo);
        }

        // GET: /Amigo/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigos.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }
            return View(amigo);
        }

        // POST: /Amigo/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que desea enlazarse. Para obtener 
        // más información vea http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include="ID,name,longi,lati")] Amigo amigo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(amigo).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(amigo);
        }

        // GET: /Amigo/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Amigo amigo = db.Amigos.Find(id);
            if (amigo == null)
            {
                return HttpNotFound();
            }
            return View(amigo);
        }

        // POST: /Amigo/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Amigo amigo = db.Amigos.Find(id);
            db.Amigos.Remove(amigo);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
