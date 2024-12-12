using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using GameNet.Data;
using GameNet.Models.Platform;

namespace GameNet.Controllers
{
    public class LobbiesController : Controller
    {
        private readonly GameNetDbContext _context;

        public LobbiesController(GameNetDbContext context)
        {
            _context = context;
        }

        // GET: Lobbies
        public async Task<IActionResult> Index()
        {
            return View(await _context.Lobbies.OrderByDescending(lobby => lobby.Create).ToListAsync());
        }

        // GET: Lobbies/Launch/5
        public async Task<IActionResult> Launch(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lobby = await _context.Lobbies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lobby == null)
            {
                return NotFound();
            }

            return View(lobby);
        }

        // GET: Lobbies/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lobby = await _context.Lobbies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lobby == null)
            {
                return NotFound();
            }

            return View(lobby);
        }

        // GET: Lobbies/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Lobbies/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name, IsPrivate, SelectedBoardGame")] Lobby lobby)
        {
            if (ModelState.IsValid)
            {   
                lobby.Name = lobby.Name;
                lobby.SelectedBoardGame = lobby.SelectedBoardGame;
                lobby.Initialize(lobby.IsPrivate);
                _context.Add(lobby);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(lobby);
        }

        // // GET: Lobbies/Edit/5
        // public async Task<IActionResult> Edit(string id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     var lobby = await _context.Lobbies.FindAsync(id);
        //     if (lobby == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(lobby);
        // }

        // // POST: Lobbies/Edit/5
        // // To protect from overposting attacks, enable the specific properties you want to bind to.
        // // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public async Task<IActionResult> Edit(string id, [Bind("ID, Create, Link, Pass, IsPrivate")] Lobby lobby)
        // {
        //     if (id != lobby.ID)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         try
        //         {
        //             lobby.Initialize(lobby.IsPrivate);
        //             _context.Update(lobby);
        //             await _context.SaveChangesAsync();
        //         }
        //         catch (DbUpdateConcurrencyException)
        //         {
        //             if (!LobbyExists(lobby.ID))
        //             {
        //                 return NotFound();
        //             }
        //             else
        //             {
        //                 throw;
        //             }
        //         }
        //         return RedirectToAction(nameof(Index));
        //     }
        //     return View(lobby);
        // }

        // GET: Lobbies/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var lobby = await _context.Lobbies
                .FirstOrDefaultAsync(m => m.ID == id);
            if (lobby == null)
            {
                return NotFound();
            }

            return View(lobby);
        }

        // POST: Lobbies/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var lobby = await _context.Lobbies.FindAsync(id);
            if (lobby != null)
            {
                _context.Lobbies.Remove(lobby);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LobbyExists(string id)
        {
            return _context.Lobbies.Any(e => e.ID == id);
        }
    }
}
