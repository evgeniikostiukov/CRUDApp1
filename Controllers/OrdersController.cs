using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CRUDApp.Data;
using CRUDApp.Models;

namespace CRUDApp.Controllers
{
    public class OrdersController : Controller
    {
        private readonly OrderContext _context;

        public OrdersController(OrderContext context)
        {
            _context = context;
        }

        // GET: Orders
        public async Task<IActionResult> Index(string sortOrder, string filterNumber = null, string filterDate = null, string filterProvider = null)
        {
            ViewData["IDSortParam"] = String.IsNullOrEmpty(sortOrder) ? "ID_desc" : "";
            ViewData["NumberSortParam"] = sortOrder == "number" ? "number_desc" : "number";
            ViewData["DateSortParam"] = sortOrder == "date" ? "date_desc" : "date";
            //ViewData["CurrentFilter"] = String.IsNullOrEmpty(filterNumber) ? ViewData["CurrentFilter"] : ViewData["CurrentFilter"] + filterNumber;
            //ViewData["CurrentFilter"] = String.IsNullOrEmpty(filterDate) ? ViewData["CurrentFilter"] : ViewData["CurrentFilter"] + filterDate;
            //ViewData["CurrentFilter"] = String.IsNullOrEmpty(filterProvider) ? ViewData["CurrentFilter"] : ViewData["CurrentFilter"] + filterProvider;

            var orders = from s in _context.Orders
                         select s;
            orders = orders.Include(o => o.Provider);

            if (filterNumber != null)
            {
                orders = orders.Where(s => s.Number.Contains(filterNumber));
            }

            if (filterDate != null)
            {
                orders = orders.Where(s => s.Date.ToString().Contains(filterDate));
            }

            if (filterProvider != null)
            {
                orders = orders.Where(s => s.ProviderID.ToString().Contains(filterProvider));
            }

            orders = sortOrder switch
            {
                "ID_desc" => orders.OrderByDescending(s => s.ID),
                "number" => orders.OrderBy(s => s.Number),
                "number_desc" => orders.OrderByDescending(s => s.Number),
                "date" => orders.OrderBy(s => s.Date),
                "date_desc" => orders.OrderByDescending(s => s.Date),
                _ => orders.OrderBy(s => s.ID),
            };
            return View(await orders.AsNoTracking().ToListAsync());
        }

        // GET: Orders/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Provider)
                .Include(s => s.OrderItems)
                .AsNoTracking()
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // GET: Orders/Create
        public IActionResult Create()
        {
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ID", "ID");
            return View();
        }

        // POST: Orders/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Number,Date,ProviderID")] Order order)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    _context.Add(order);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            catch (DbUpdateException /* ex */)
            {
                //Log the error (uncomment ex variable name and write a log.
                ModelState.AddModelError("", "Unable to save changes. " +
                    "Try again, and if the problem persists " +
                    "see your system administrator.");
            }
                return View(order);
        }

        // GET: Orders/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }
            ViewData["ProviderID"] = new SelectList(_context.Providers, "ID", "ID", order.ProviderID);
            return View(order);
        }

        // POST: Orders/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost, ActionName("Edit")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Number,Date,ProviderID")] Order order)
        {
            if (id == null)
            {
                return NotFound();
            }

            var orderToUpdate = await _context.Orders.FirstOrDefaultAsync(s => s.ID == id);
            if (await TryUpdateModelAsync<Order>(
                orderToUpdate,
                "",
                s => s.Number, s => s.Date, s => s.ProviderID))
            {
                try
                {
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
                catch (DbUpdateException ex)
                {
                    //Log the error (uncomment ex variable name and write a log.)
                    ModelState.AddModelError("", "Unable to save changes. " +
                        "Try again, and if the problem persists, " +
                        "see your system administrator.");
                }
            }
            return View(orderToUpdate);
        }

        // GET: Orders/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var order = await _context.Orders
                .Include(o => o.Provider)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }

        // POST: Orders/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OrderExists(int id)
        {
            return _context.Orders.Any(e => e.ID == id);
        }
    }
}
