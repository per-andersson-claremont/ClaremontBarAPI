using ClaremontBarAPI.Models;
using System;
using System.Linq;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace ClaremontBarAPI.Controllers
{
    /// <summary>
    /// Claremont Bar Controller. A Web API for manage products in the bar.
    /// </summary>
    [EnableCorsAttribute("*", "*", "*")]
    public class ProductsController : ApiController
    {

        /// <summary>
        /// Returns a list of Products(JSON) on success
        /// </summary>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get()
        {
            try
            {
                var repository = new ProductRepository();
                return Ok(repository.Retrieve());
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Returns a Product(JSON) on success
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        public IHttpActionResult Get(int id)
        {
            Product prod;
            try
            {

                var repository = new ProductRepository();

                if (id > 0)
                {
                    var products = repository.Retrieve();
                    prod = products.FirstOrDefault(p => p.Id == id);
                    if (prod == null)
                    {
                        return NotFound();
                    }
                }
                else
                {
                    prod = repository.Create();
                }

                return Ok(prod);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }



        /// <summary>
        /// Returns the created Product(JSON) on success
        /// </summary>
        /// <param name="prod"></param>
        /// <returns></returns>
        [ResponseType(typeof(Product))]
        public IHttpActionResult Post([FromBody]Product prod)
        {
            if (prod == null)
            {
                return BadRequest("Product can not be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var repository = new ProductRepository();

                var newProduct = repository.Save(prod);
                if (newProduct == null)
                {
                    return Conflict();
                }

                return Created<Product>(Request.RequestUri + newProduct.Id.ToString(), newProduct);
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Returns the updated Product(JSON) on success
        /// </summary>
        /// <returns></returns>
        public IHttpActionResult Put(int id, [FromBody]Product prod)
        {
            if (prod == null)
            {
                return BadRequest("Product can not be null");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            try
            {
                var repository = new ProductRepository();

                var updatedProduct = repository.Save(id, prod);
                if (updatedProduct == null)
                {
                    return NotFound();
                }
                return Ok();
            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }

        /// <summary>
        /// Returns nothing
        /// </summary>
        /// <returns> Returns No content(204) on success</returns>
        public IHttpActionResult Delete(int id)
        {
            try
            {
                var repository = new ProductRepository();

                if (repository.Delete(id))
                {
                    // Return No Content
                    return new System.Web.Http.Results.StatusCodeResult(System.Net.HttpStatusCode.NoContent, this);
                }
                else
                {
                    return NotFound();
                }

            }
            catch (Exception ex)
            {
                return InternalServerError(ex);
            }
        }
    }
}
