using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Bangazon.Data;
using Bangazon.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Http;

namespace BangazonAPI.Controllers
{
    [ProducesAttribute("application/json")]
    [Route("[controller]")]
    public class LineItemsController : Controller
    {
        private BangazonContext context;

        public LineItemsController(BangazonContext ctx)
        {
            context = ctx;
        }

        // GET api/values
        [HttpGet]
        public IActionResult Get()
        {
            IQueryable<object> lineItems = from lineItem in context.LineItem select lineItem;

            if (lineItems == null)
            {
                return NotFound();
            }

            return Ok(lineItems);
        }

        // GET api/values/5
        [HttpGet("{id}", Name = "GetLineItem")]
        public IActionResult Get([FromRoute] int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                LineItem lineItem = context.LineItem.Single(m => m.LineItemId == id);

                if (lineItem == null)
                {
                    return NotFound();
                }

                return Ok(lineItem);
            }
            catch (System.InvalidOperationException ex)
            {
                return NotFound();
            }
        }

        // POST api/values
        [HttpPost]
        public IActionResult Post([FromBody] LineItem lineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            context.LineItem.Add(lineItem);
            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineItem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status409Conflict);
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("GetLineItem", new { id = lineItem.LineItemId }, lineItem);
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public IActionResult Put([FromRoute] int id, [FromBody] LineItem lineItem)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (lineItem.LineItemId != id)
            {
                return BadRequest(lineItem);
            }

            try
            {
                context.LineItem.Update(lineItem);
            }
            catch
            {
                return NotFound();
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineItem.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
                else
                {
                    throw;
                }
            }

            return Ok(lineItem);
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public IActionResult Delete([FromRoute]int id)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            IQueryable<object> lineItems = from lineItem in context.LineItem select lineItem;

            if (lineItems == null)
            {
                return NotFound();
            }

            LineItem lineItemToDelete = null;

            try
            {
                lineItemToDelete = context.LineItem.Single(m => m.LineItemId == id);
                context.LineItem.Remove(lineItemToDelete);
            }
            catch
            {
                return NotFound();
            }

            try
            {
                context.SaveChanges();
            }
            catch (DbUpdateException)
            {
                if (LineItemExists(lineItemToDelete.LineItemId))
                {
                    return new StatusCodeResult(StatusCodes.Status403Forbidden);
                }
                else
                {
                    throw;
                }
            }

            return new NoContentResult();
        }

        private bool LineItemExists(int id)
        {
            return context.LineItem.Count(e => e.LineItemId == id) > 0;
        }
    }
}