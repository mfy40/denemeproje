using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using kutuphane.database.Context;

using kutuphane.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Book = kutuphane.ViewModels.Book;

namespace kutuphane.Controllers
{
    public class BookController : Controller
    {
        private readonly KutuphaneDbContext _kutuphaneDbContext;

        public BookController(KutuphaneDbContext kutuphaneDbContext)
        {
            _kutuphaneDbContext = kutuphaneDbContext;
        }

        public IActionResult Index()
        {

            var booksList = _kutuphaneDbContext.Books.Select(m => new Book
            {

                EditionNo = m.EditionNo,
                Name = m.Name,
                Id = m.Id,
                Writers = m.BookWriters.Select(n => new Writer { Name = n.Writer.Name, Id = n.Writer.Id }).ToList(),
                Categories = m.BookCategories.Select(n => new Category { Name = n.Category.Name, Id = n.Category.Id }).ToList(),
                ShelfNo = m.Bookshelf.Name,
                SerialNo = m.SerialNo,
                DatePrinting = m.DatePrinting,
                DatePublish = m.DatePublish,
                StockNumber = m.StockNumber,
                Kod= m.Kod

            }).ToList();

            return View(booksList);
        }

        public IActionResult Bookshelfs()
        {

            var shelflist = _kutuphaneDbContext.Bookshelfs.Select(m => new BookShelf
            {

                Id = m.Id,
                Name = m.Name,
                Saloon = new Saloon { Id = m.Saloon.Id, Name = m.Saloon.Name }

            }).ToList();


            return View(shelflist);
        }


        public IActionResult BookCategorys()
        {

            var categorylist = _kutuphaneDbContext.Categories.Select(m => new Category
            {
                Id = m.Id,
                Name = m.Name

            }).ToList();


            return View(categorylist);
        }

        public IActionResult Writers()
        {

            var writerlist = _kutuphaneDbContext.Writers.Select(m => new Writer
            {
                Id = m.Id,
                Name = m.Name


            }).ToList();



            return View(writerlist);
        }



        #region viewbag için getlist methodları

        public List<SelectListItem> getcategories()
        {
            var categorieslist = _kutuphaneDbContext.Categories.ToList();

            List<SelectListItem> categories = new List<SelectListItem>();

            foreach (var item in categorieslist)
            {
                SelectListItem slitem = new SelectListItem();
                slitem.Text = item.Name;
                slitem.Value = item.Id.ToString();

                categories.Add(slitem);

            }

            return categories;


        }


        public List<SelectListItem> getwriters()
        {
            var Bookshelfslist = _kutuphaneDbContext.Writers.ToList();

            List<SelectListItem> shelf = new List<SelectListItem>();

            foreach (var item in Bookshelfslist)
            {
                SelectListItem slitem = new SelectListItem();
                slitem.Text = item.Name;
                slitem.Value = item.Id.ToString();

                shelf.Add(slitem);

            }

            return shelf;

        }




        public List<SelectListItem> getshelf()
        {
            var Bookshelfslist = _kutuphaneDbContext.Bookshelfs.ToList();

            List<SelectListItem> shelf = new List<SelectListItem>();

            foreach (var item in Bookshelfslist)
            {
                SelectListItem slitem = new SelectListItem();
                slitem.Text = item.Name;
                slitem.Value = item.Id.ToString();

                shelf.Add(slitem);

            }

            return shelf;

        }


        public List<SelectListItem> getPublisher()
        {
            var Publisherslist = _kutuphaneDbContext.Publishers.ToList();

            List<SelectListItem> Publisher = new List<SelectListItem>();

            foreach (var item in Publisherslist)
            {
                SelectListItem slitem = new SelectListItem();
                slitem.Text = item.Name;
                slitem.Value = item.Id.ToString();

                Publisher.Add(slitem);

            }

            return Publisher;

        }


        public List<SelectListItem> GetSaloon()
        {
            var Saloonslist = _kutuphaneDbContext.Saloons.ToList();

            List<SelectListItem> Saloons = new List<SelectListItem>();

            foreach (var item in Saloonslist)
            {
                SelectListItem slitem = new SelectListItem();
                slitem.Text = item.Name;
                slitem.Value = item.Id.ToString();

                Saloons.Add(slitem);

            }

            return Saloons;

        }


        #endregion


 


        [HttpPost]
        public IActionResult EditBook(kutuphane.ViewModels.Book data)
        {

            var model = new kutuphane.database.Models.Book();
            var bcmodels = new List<kutuphane.database.Models.BookCategory>();
            var bwmodels = new List< kutuphane.database.Models.BookWriter>();


            model.DatePrinting = data.DatePrinting;
            model.DatePublish = data.DatePublish;
            model.EditionNo = data.EditionNo;
            model.StockNumber = data.StockNumber;
            model.Kod = data.Kod;
            model.SerialNo = data.SerialNo;
            model.ShelfNo = data.ShelfNo;
            model.Kod = data.Kod;
            model.Name = data.Name;
            model.rlt_Bookshelf_Id = data.BookshelfId;
            model.rlt_Publisher_Id = data.PublisherId;
            model.Bookshelf = _kutuphaneDbContext.Bookshelfs.Where(m => m.Id == data.BookshelfId).SingleOrDefault();
            model.Publisher = _kutuphaneDbContext.Publishers.Where(m => m.Id == data.PublisherId).SingleOrDefault();
            model.Id = data.Id;


            var categories = _kutuphaneDbContext.Categories.Where(m => data.CategoriesIds.Contains(m.Id)).ToList();

            foreach (var item in categories)
            {

                var bcmodel = new kutuphane.database.Models.BookCategory();

                bcmodel.Book = model;
                bcmodel.Category = item;
                bcmodel.rtl_Book_Id = model.Id;
                bcmodel.rtl_Category_Id = item.Id;

                bcmodels.Add(bcmodel);


            }



            var bookwriter = _kutuphaneDbContext.BookWriters.Where(m => m.rlt_Book_Id == model.Id).Select(m => m.rlt_Writer_Id).ToList();


           

            var writers = _kutuphaneDbContext.Writers.Where(m => data.WriterIds.Contains(m.Id) && !bookwriter.Contains(m.Id)).ToList();


           
            foreach (var item in writers)
            {

                var bwmodel = new kutuphane.database.Models.BookWriter();

                bwmodel.Writer = item;
                bwmodel.Book = model;
                bwmodel.rlt_Book_Id = model.Id;
                bwmodel.rlt_Writer_Id = item.Id;

                bwmodels.Add(bwmodel);
            }


            if (model.Id == 0) // ekle
            {

                _kutuphaneDbContext.Books.Add(model);
                _kutuphaneDbContext.BookCategories.AddRange(bcmodels);
                _kutuphaneDbContext.BookWriters.AddRange(bwmodels);

            }
            else // güncelle
            {


                _kutuphaneDbContext.Books.Update(model);
                _kutuphaneDbContext.BookCategories.UpdateRange(bcmodels);
                _kutuphaneDbContext.BookWriters.UpdateRange(bwmodels);


            }

            _kutuphaneDbContext.SaveChanges();

            return RedirectToAction("Index", "Book");

        }

        [HttpPost]
        public IActionResult EditBookshelf(kutuphane.ViewModels.BookShelf data)
        {


            var model = new kutuphane.database.Models.Bookshelf();

            model.Name = data.Name;
            model.rlt_Saloon_Id = data.SelectSalonId;
            model.Id = data.Id;

            if (model.Id == 0)
            {
                _kutuphaneDbContext.Bookshelfs.Add(model);


            }
            else
            {
                _kutuphaneDbContext.Bookshelfs.Update(model);
            }

            _kutuphaneDbContext.SaveChanges();

            return RedirectToAction("Bookshelfs", "Book");
        }

        [HttpPost]
        public IActionResult EditBookCategory(kutuphane.ViewModels.Category data)
        {

            var model = new kutuphane.database.Models.Category();

            model.Name = data.Name;
            model.Id = data.Id;

            if (model.Id == 0)
            {
                _kutuphaneDbContext.Categories.Add(model);



            }
            else
            {
                _kutuphaneDbContext.Categories.Update(model);
            }

            _kutuphaneDbContext.SaveChanges();

            return RedirectToAction("BookCategorys", "Book");
        }


        [HttpPost]
        public IActionResult EditWriter(kutuphane.ViewModels.Writer data)
        {

            var model = new kutuphane.database.Models.Writer();

            model.Name = data.Name;
          
            model.Id = data.Id;


            if (model.Id == 0)
            {
                _kutuphaneDbContext.Writers.Add(model);


            }
            else
            {
                _kutuphaneDbContext.Writers.Update(model);

            }

            _kutuphaneDbContext.SaveChanges();
            return RedirectToAction("Writers", "Book");
        }






        [HttpGet]
        public IActionResult EditBook(int id)
        {

            var booksList = _kutuphaneDbContext.Books.Select(m => new Book
            {

                EditionNo = m.EditionNo,
                Name = m.Name,
                Id = m.Id,
                Writers = m.BookWriters.Select(n => new Writer { Name = n.Writer.Name, Id = n.Writer.Id }).ToList(),
                Categories = m.BookCategories.Select(n => new Category { Name = n.Category.Name, Id = n.Category.Id }).ToList(),
                ShelfNo = m.Bookshelf.Name,
                SerialNo = m.SerialNo,
                DatePrinting = m.DatePrinting,
                DatePublish = m.DatePublish,
                CategoriesIds = m.BookCategories.Select(n => n.rtl_Category_Id).ToList(),
                WriterIds = m.BookWriters.Select(n => n.rlt_Writer_Id).ToList(),
                PublisherId = m.Publisher.Id,
                BookshelfId = m.Bookshelf.Id,
                StockNumber = m.StockNumber,
                Kod = m.Kod

            }).Where(m => m.Id == id).FirstOrDefault();


            ViewBag.writers = getwriters();
            ViewBag.categories = getcategories();
            ViewBag.shelfs = getshelf();
            ViewBag.Publishers = getPublisher();



            return View(booksList);
        }



        [HttpGet]
        public IActionResult EditBookshelf(int id)
        {


            var shelflist = _kutuphaneDbContext.Bookshelfs.Select(m => new BookShelf
            {

                Id = m.Id,
                Name = m.Name,
                Saloon = new Saloon { Id = m.Saloon.Id, Name = m.Saloon.Name }

            }).Where(m => m.Id == id).FirstOrDefault();


            ViewBag.saloons = GetSaloon();


            return View(shelflist);
        }


        [HttpGet]
        public IActionResult EditBookCategory(int id)
        {


            var Categorylist = _kutuphaneDbContext.Categories.Select(m => new Category
            {

                Id = m.Id,
                Name= m.Name,
               
                
              
            }).Where(m => m.Id == id).FirstOrDefault();

            return View(Categorylist);
        }

        [HttpGet]
        public IActionResult EditWriter(int id)
        {


            var Writerlist = _kutuphaneDbContext.Writers.Select(m => new Writer
            {

                Id = m.Id,
                Name = m.Name
             

            }).Where(m => m.Id == id).FirstOrDefault();

            return View(Writerlist);
        }


        public IActionResult DeleteBook(int id)
        {

        
            _kutuphaneDbContext.Books.RemoveRange(_kutuphaneDbContext.Books.Find(id));

            _kutuphaneDbContext.SaveChanges();


            return RedirectToAction("Index", "Book");

        }




        public IActionResult DeleteBookshelf(int id)
        {


            _kutuphaneDbContext.Bookshelfs.RemoveRange(_kutuphaneDbContext.Bookshelfs.Find(id));

            _kutuphaneDbContext.SaveChanges();


            return RedirectToAction("Bookshelfs", "Book");


        }


 
        public IActionResult DeleteBookCategory(int id)
        {


            _kutuphaneDbContext.Categories.RemoveRange(_kutuphaneDbContext.Categories.Find(id));

            _kutuphaneDbContext.SaveChanges();


            return RedirectToAction("BookCategorys", "Book");
        }


        public IActionResult DeleteWriter(int id)
        {


            _kutuphaneDbContext.Writers.RemoveRange(_kutuphaneDbContext.Writers.Find(id));

            _kutuphaneDbContext.SaveChanges();


            return RedirectToAction("Writers", "Book");
        }



    }
}
