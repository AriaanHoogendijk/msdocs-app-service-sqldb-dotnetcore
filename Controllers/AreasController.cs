using X.PagedList;
using X.PagedList.EntityFramework;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
//using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using DotNetCoreSqlDb.Extensions;
using DotNetCoreSqlDb.Models;
using DotNetCoreSqlDb.Data;

namespace DotNetCoreSqlDb.Controllers
{
    public class AreasController : Controller
    {
        private readonly MyDatabaseContext _context;

        public AreasController(MyDatabaseContext context)
        {
            _context = context;
        }

        // GET: Areas
        public async Task<IActionResult> Index(int? pageNumber, string currentFilter, string searchString
                    ,string Name
                    ,string NameNl
                    ,string NameFr
                    ,string NameGe
                    ,string Description
                    ,string DateFrom
                    ,string DateTo
                    ,string TypeOfArea
            )
        {
            int pageSize = 10; // Aantal items per pagina
            pageNumber = pageNumber ?? 1; // Stel de huidige pagina in op 1 als deze niet is opgegeven
            if (searchString != null)
            {
            pageNumber = 1;
            }
            else
            {
                searchString = currentFilter;
            }
            var items = _context.AreaViews.OrderBy(p => p.Description).AsQueryable();
            if (!String.IsNullOrEmpty(searchString))
            {
                items = items.Where(p => EF.Functions.Collate(p.Description, "Latin1_General_CI_AI").Contains(searchString));
            }
            if (!String.IsNullOrEmpty(Name))
            {
                items = items.Where(p => EF.Functions.Collate(p.Name, "Latin1_General_CI_AI").Contains(Name));
            }
            if (!String.IsNullOrEmpty(NameNl))
            {
                items = items.Where(p => EF.Functions.Collate(p.NameNl, "Latin1_General_CI_AI").Contains(NameNl));
            }
            if (!String.IsNullOrEmpty(NameFr))
            {
                items = items.Where(p => EF.Functions.Collate(p.NameFr, "Latin1_General_CI_AI").Contains(NameFr));
            }
            if (!String.IsNullOrEmpty(NameGe))
            {
                items = items.Where(p => EF.Functions.Collate(p.NameGe, "Latin1_General_CI_AI").Contains(NameGe));
            }
            if (!String.IsNullOrEmpty(Description))
            {
                items = items.Where(p => EF.Functions.Collate(p.Description, "Latin1_General_CI_AI").Contains(Description));
            }
            if (!String.IsNullOrEmpty(DateFrom))
            {
                items = items.Where(p => EF.Functions.Collate(p.DateFrom, "Latin1_General_CI_AI").Contains(DateFrom));
            }
            if (!String.IsNullOrEmpty(DateTo))
            {
                items = items.Where(p => EF.Functions.Collate(p.DateTo, "Latin1_General_CI_AI").Contains(DateTo));
            }
            if (!String.IsNullOrEmpty(TypeOfArea))
            {
                items = items.Where(p => EF.Functions.Collate(p.TypeOfArea, "Latin1_General_CI_AI").Contains(TypeOfArea));
            }
            var archiveContext = _context.AreaViews;
            var pagedList = await items.ToPagedListAsync(pageNumber.Value, pageSize);
            ViewBag.CurrentPage = pageNumber.Value;
            ViewBag.Filter_Name = Name;
            ViewBag.Filter_NameNl = NameNl;
            ViewBag.Filter_NameFr = NameFr;
            ViewBag.Filter_NameGe = NameGe;
            ViewBag.Filter_Description = Description;
            ViewBag.Filter_DateFrom = DateFrom;
            ViewBag.Filter_DateTo = DateTo;
            ViewBag.Filter_TypeOfArea = TypeOfArea;
            ViewBag.TotalPages = pagedList.PageCount;
            ViewBag.PageSize = pageSize;
            ViewBag.CurrentFilter = searchString;
            ViewBag.SetName = "Areas";
            return View(pagedList);
        }

        // GET: Areas/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Areas == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.TypeOfArea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // GET: Areas/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Areas/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,NameNl,NameFr,NameGe,UrlWikipedia,UrlWikipediaNl,UrlWikipediaFr,UrlWikipediaGe,Description,TypeOfAreaId,DateFrom,DateTo")] Area area)
        {
            ModelState.Remove("Description");
            ModelState.Remove("TypeOfArea");
        if (!ValidationExtensions.IsValidDate(area.DateFrom))
        {
            ModelState.AddModelError("DateFrom", "Wrong date format. Provide an exact date or start with 'voor', 'na', 'tussen', or 'circa'. Also, 'm-yyyy' and 'yyyy' are allowed.");
        }
        if (!ValidationExtensions.IsValidDate(area.DateTo))
        {
            ModelState.AddModelError("DateTo", "Wrong date format. Provide an exact date or start with 'voor', 'na', 'tussen', or 'circa'. Also, 'm-yyyy' and 'yyyy' are allowed.");
        }

            if (ModelState.IsValid)
            {
                _context.Add(area);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(area);
        }

        [HttpGet]
        [Route("Areas/GetFilteredTypeOfAreas")]
        public JsonResult GetFilteredTypeOfAreas(string filter)
        {
          var filteredTypeOfAreas = _context.TypeOfAreaViews
              .Where(a => string.IsNullOrEmpty(filter) || EF.Functions.Collate(a.Description, "Latin1_General_CI_AI").Contains(filter))
              .OrderBy(a => a.Description)
              .Select(a => new { a.Id, a.Description })
              .ToList();
        
          return Json(filteredTypeOfAreas);
        }
        [HttpGet]
        [Route("Areas/GetTypeOfAreaDescription")]
        public JsonResult GetTypeOfAreaDescription(int id)
        {
          var TypeOfArea = _context.TypeOfAreaViews
              .Where(a => a.Id == id)
              .Select(a => new { a.Id, a.Description })
              .FirstOrDefault();
          return Json(TypeOfArea);
        }

        // GET: Areas/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Areas == null)
            {
                return NotFound();
            }
            var area = await _context.Areas.FindAsync(id);
            if (area == null)
            {
                return NotFound();
            }
            return View(area);
        }

        // POST: Areas/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,NameNl,NameFr,NameGe,UrlWikipedia,UrlWikipediaNl,UrlWikipediaFr,UrlWikipediaGe,Description,TypeOfAreaId,DateFrom,DateTo")] Area area)
        {
            if (id != area.Id)
            {
                return NotFound();
            }

//            if (ModelState.IsValid)
//            {
                try
                {
                    _context.Update(area);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AreaExists(area.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
//            }
//            return View(area);
        }

        // GET: Areas/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Areas == null)
            {
                return NotFound();
            }

            var area = await _context.Areas
                .Include(a => a.TypeOfArea)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (area == null)
            {
                return NotFound();
            }

            return View(area);
        }

        // POST: Areas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Areas == null)
            {
                return Problem("Entity set 'ArchiveContext.Areas'  is null.");
            }
            var area = await _context.Areas.FindAsync(id);
            if (area != null)
            {
                _context.Areas.Remove(area);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AreaExists(int id)
        {
          return (_context.Areas?.Any(e => e.Id == id)).GetValueOrDefault();
        }
    }
}
