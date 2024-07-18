using Microsoft.AspNetCore.Mvc;
using Product_MVC.Models.Domains;
using Product_MVC.Models.Services;
using System.IO;

namespace Product_MVC.Controllers
{
    public class ProductController : Controller
    {
        private readonly AppDbContext context;

        //Consructor Injection.
        public ProductController(AppDbContext context)
        {
            this.context = context;
        }

        public IActionResult Index()
        {
            var products = context.Products.ToList();
            return View(products);
        }

        // Creation
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                // Save the image file
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }

                    // Create the Product entity
                    var newProduct = new Product
                    {
                        Name = product.Name,
                        Brand = product.Brand,
                        Category = product.Category,
                        Price = product.Price,
                        Description = product.Description,
                        ImageFileName = uniqueFileName,
                        CreatedAt = DateTime.Now
                    };

                    // Add to the database
                    context.Products.Add(newProduct);
                    context.SaveChanges();

                    return RedirectToAction("Index");
                }
            }

            return View(product);
        }

        // Update
        public IActionResult Edit(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            ViewData["Id"] = product.Id;
            ViewData["ImageFileName"] = product.ImageFileName;
            ViewData["CreatedAt"] = product.CreatedAt;

            var productDto = new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Brand = product.Brand,
                Category = product.Category,
                Price = product.Price,
                Description = product.Description
            };

            return View(productDto);
        }

        [HttpPost]
        public IActionResult Edit(ProductDto product)
        {
            if (ModelState.IsValid)
            {
                var existingProduct = context.Products.Find(product.Id);
                if (existingProduct == null)
                {
                    return RedirectToAction("Index");
                }

                existingProduct.Name = product.Name;
                existingProduct.Brand = product.Brand;
                existingProduct.Category = product.Category;
                existingProduct.Price = product.Price;
                existingProduct.Description = product.Description;

                // Save the image file if provided
                if (product.ImageFile != null && product.ImageFile.Length > 0)
                {
                    string uploadsFolder = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/Products");
                    string uniqueFileName = Guid.NewGuid().ToString() + "_" + product.ImageFile.FileName;
                    string filePath = Path.Combine(uploadsFolder, uniqueFileName);
                    using (var fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        product.ImageFile.CopyTo(fileStream);
                    }

                    existingProduct.ImageFileName = uniqueFileName;
                }

                context.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        // Delete
        public IActionResult Delete(int id)
        {
            var product = context.Products.Find(id);
            if (product == null)
            {
                return RedirectToAction("Index");
            }

            return RedirectToAction("Index");
        }

        [HttpPost, ActionName("DeleteConfirmed")]
        public IActionResult DeleteConfirmed(int id)
        {
            var product = context.Products.Find(id);
            if (product != null)
            {
                context.Products.Remove(product);
                context.SaveChanges();
            }

            return RedirectToAction("Index");
        }
    }
}
