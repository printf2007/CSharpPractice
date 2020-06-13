using DevExtreme.AspNet.Data;
using DevExtreme.AspNet.Mvc;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using DevExtremeAspNetCoreApp2.Models;

namespace DevExtremeAspNetCoreApp2.Controllers
{
    [Route("api/[controller]/[action]")]
    public class RestaurantsController : Controller
    {
        private OdeToFoodContext _context;

        public RestaurantsController(OdeToFoodContext context) {
            _context = context;
        }

        [HttpGet]
        public async Task<IActionResult> Get(DataSourceLoadOptions loadOptions) {
            var restaurants = _context.Restaurants.Select(i => new {
                i.Id,
                i.Name,
                i.Location,
                i.Cuisine
            });

            // If you work with a large amount of data, consider specifying the PaginateViaPrimaryKey and PrimaryKey properties.
            // In this case, keys and data are loaded in separate queries. This can make the SQL execution plan more efficient.
            // Refer to the topic https://github.com/DevExpress/DevExtreme.AspNet.Data/issues/336.
            // loadOptions.PrimaryKey = new[] { "Id" };
            // loadOptions.PaginateViaPrimaryKey = true;

            return Json(await DataSourceLoader.LoadAsync(restaurants, loadOptions));
        }

        [HttpPost]
        public async Task<IActionResult> Post(string values) {
            var model = new Restaurants();
            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            var result = _context.Restaurants.Add(model);
            await _context.SaveChangesAsync();

            return Json(result.Entity.Id);
        }

            
        [HttpPut]
        public async Task<IActionResult> Put(int key, string values) {
            var model = await _context.Restaurants.FirstOrDefaultAsync(item => item.Id == key);
            if(model == null)
                return StatusCode(409, "Object not found");

            var valuesDict = JsonConvert.DeserializeObject<IDictionary>(values);
            PopulateModel(model, valuesDict);

            if(!TryValidateModel(model))
                return BadRequest(GetFullErrorMessage(ModelState));

            await _context.SaveChangesAsync();
            return Ok();
        }

        [HttpDelete]
        public async Task Delete(int key) {
            var model = await _context.Restaurants.FirstOrDefaultAsync(item => item.Id == key);

            _context.Restaurants.Remove(model);
            await _context.SaveChangesAsync();
        }


        private void PopulateModel(Restaurants model, IDictionary values) {
            string ID = nameof(Restaurants.Id);
            string NAME = nameof(Restaurants.Name);
            string LOCATION = nameof(Restaurants.Location);
            string CUISINE = nameof(Restaurants.Cuisine);

            if(values.Contains(ID)) {
                model.Id = Convert.ToInt32(values[ID]);
            }

            if(values.Contains(NAME)) {
                model.Name = Convert.ToString(values[NAME]);
            }

            if(values.Contains(LOCATION)) {
                model.Location = Convert.ToString(values[LOCATION]);
            }

            if(values.Contains(CUISINE)) {
                model.Cuisine = Convert.ToInt32(values[CUISINE]);
            }
        }

        private string GetFullErrorMessage(ModelStateDictionary modelState) {
            var messages = new List<string>();

            foreach(var entry in modelState) {
                foreach(var error in entry.Value.Errors)
                    messages.Add(error.ErrorMessage);
            }

            return String.Join(" ", messages);
        }
    }
}