using Newtonsoft.Json;
using System.Collections.Generic;
using System.Linq;
using System.Web.Hosting;

namespace ClaremontBarAPI.Models
{
    /// <summary>
    /// Stores the data in a json file so that no database is required for this
    /// sample application
    /// </summary>
    public class ProductRepository
    {
        /// <summary>
        /// Creates a new product with default values
        /// </summary>
        /// <returns>Product</returns>
        internal Product Create()
        {
            Product product = new Product
            {

            };
            return product;
        }

        /// <summary>
        /// Retrieves the list of products.
        /// </summary>
        /// <returns>List of Products</returns>
        internal List<Product> Retrieve()
        {
            List<Product> products = new List<Product>();

            using (var fileStream = new System.IO.FileStream(FilePath, System.IO.FileMode.OpenOrCreate,
                                                            System.IO.FileAccess.Read, System.IO.FileShare.ReadWrite))
            {
                using (var textReader = new System.IO.StreamReader(fileStream))
                {
                    string json = textReader.ReadToEnd();
                    products = JsonConvert.DeserializeObject<List<Product>>(json);
                }
            }

            return products;
        }

        /// <summary>
        /// Saves a new product.
        /// </summary>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        internal Product Save(Product product)
        {

            // Read in the existing products
            var products = this.Retrieve();

            // Assign a new Id
            var maxId = products.Max(p => p.Id);
            product.Id = maxId + 1;
            products.Add(product);

            WriteData(products);
            return product;
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <param name="product"></param>
        /// <returns>Product</returns>
        internal Product Save(int id, Product product)
        {
            // Read in the existing products
            var products = this.Retrieve();

            // Locate and replace the item
            var itemIndex = products.FindIndex(p => p.Id == product.Id);

            // The index in the list, starts from 0
            if (itemIndex >= 0)
            {
                products[itemIndex] = product;
            }
            else
            {
                return null;
            }

            WriteData(products);
            return product;
        }

        /// <summary>
        /// Updates an existing product
        /// </summary>
        /// <param name="id"></param>
        /// <returns>bool</returns>
        internal bool Delete(int id)
        {
            var products = this.Retrieve();

            var itemIndex = products.FindIndex(p => p.Id == id);

            if (itemIndex >= 0)
            {
                products.RemoveAt(itemIndex);
                WriteData(products);
                return true;
            }
            else
            {
                // Not found
                return false;
            }
        }

        private string FilePath
        {
            get
            {
                return HostingEnvironment.MapPath(@"~/App_Data/product.json");
            }
        }

        private bool WriteData(List<Product> products)
        {
            // Instead use ADO.NET or an ORM
            // Write out the Json

            var json = JsonConvert.SerializeObject(products, Formatting.Indented);

            // Overwrite the existing data
            using (var fileStream = new System.IO.FileStream(FilePath, System.IO.FileMode.Create,
                                                                System.IO.FileAccess.ReadWrite,
                                                                System.IO.FileShare.Read))
            {
                using (var textWriter = new System.IO.StreamWriter(fileStream))
                {
                    textWriter.Write(json);
                }
            }


            return true;
        }

    }
}