using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WeddingPlanner.Models;

namespace WeddingPlanner.Controllers
{
  public class WeddingController : Controller
  {
    private MyContext DbContext;
    public WeddingController(MyContext context)
    {
      DbContext = context;
    }


    [HttpGet("weddings/all")]
    public IActionResult All()
    {
      List<Wedding> allWeddings = DbContext.Weddings
        .Include(w => w.Creator)
        .Include(w => w.GuestList)
        .ToList();

      ViewBag.UserId = HttpContext.Session.GetInt32("userId");
      return View(allWeddings);
    }


    [HttpGet("weddings/new")]
    public IActionResult New()
    {
      return View();
    }


    [HttpPost("weddings/create")]
    public IActionResult Create(Wedding newWedding)
    {
      if (ModelState.IsValid)
      {
        var userfromDb = DbContext.Users.FirstOrDefault(user => user.UserId == HttpContext.Session.GetInt32("userId"));
        newWedding.Creator = userfromDb;
        newWedding.UserId = userfromDb.UserId;
        DbContext.Weddings.Add(newWedding);
        DbContext.SaveChanges();
        return RedirectToAction("All");
      }
      else
      {
        return View("New");
      }
    }


    [HttpGet("weddings/{weddingId}")]
    public IActionResult OneWedding(int weddingId)
    {
      var oneWedding = DbContext.Weddings
      .Include(guest => guest.GuestList)
      .ThenInclude(user => user.Guest)
      .FirstOrDefault(wed => wed.WeddingId == weddingId);
      return View(oneWedding);
    }


    [HttpPost("wedding/delete/{weddingId}")]
    public IActionResult Delete(int weddingId)
    {
      var oneWedding = DbContext.Weddings
      .FirstOrDefault(wed => wed.WeddingId == weddingId);
      DbContext.Weddings.Remove(oneWedding);
      DbContext.SaveChanges();
      return RedirectToAction("All");
    }


    [HttpPost("wedding/RSVP/{WeddingId}")]
    public IActionResult Respond(int WeddingId)
    {
      var DidRSVP = DbContext.RSVPs.Where(w => w.WeddingId == WeddingId).FirstOrDefault(u => u.UserId == HttpContext.Session.GetInt32("userId"));
      if (DidRSVP == null)
      {
        RSVP newRSVP = new RSVP();
        newRSVP.UserId = (int)HttpContext.Session.GetInt32("userId");
        newRSVP.WeddingId = WeddingId;
        DbContext.Add(newRSVP);
        DbContext.SaveChanges();
      }
      else
      {
        DbContext.Remove(DidRSVP);
        DbContext.SaveChanges();
      }
      return RedirectToAction("All");
    }


    [HttpGet("logout")]
    public IActionResult logout()
    {
      HttpContext.Session.Clear();
      return RedirectToAction("Index", "Home");
    }
  }

}
