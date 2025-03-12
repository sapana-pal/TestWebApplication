using Microsoft.AspNetCore.Mvc;
using System.Data.Entity;
using TestWebApplication.Models;

namespace TestWebApplication.Controllers
{
    public class PostController : Controller
    {
        private DemoEntities _db = new DemoEntities();
        public ActionResult Index()
        {
            var data = (from s in _db.Posts select s).ToList();
            ViewBag.posts = data;
            return View();
        }


        public ActionResult Store()
        {
            return View();
        }


        [HttpPost]
        public ActionResult Store(Post _post)
        {
            if (ModelState.IsValid)
            {
                var title = _post.Title;
                var count = _db.Posts.Where(s => s.Title.Contains(title)).Count();
                if (count > 0)
                {
                    ViewBag.message = "Title already exists";
                    return View();
                }
                _post.Created_at = DateTime.Now;
                _post.Updated_at = DateTime.Now;
                _db.Posts.Add(_post);
                _db.SaveChanges();
                return RedirectToAction("Index");

                //return Json(_post);
            }
            ViewBag.message = "Insert failed!";
            return View();

        }

        public ActionResult Edit(int id)
        {
            var data = _db.Posts.Where(s => s.Id == id).FirstOrDefault();
            return View(data);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind("Id,Title,Body,Updated_at")] Post _post)
        {
            if (ModelState.IsValid)
            {
                var data = _db.Posts.Find(_post.Id);
                data.Title = _post.Title;
                data.Body = _post.Body;
                data.Updated_at = DateTime.Now;
                _db.Entry(data).State = EntityState.Modified;
                _db.SaveChanges();
                return RedirectToAction("Index");
            }
            var dataEdit = _db.Posts.Where(s => s.Id == _post.Id).FirstOrDefault();
            return View(dataEdit);
        }

        [HttpGet]
        public ActionResult Delete(int? id)
        {
            var product = _db.Posts.Where(s => s.Id == id).First();
            _db.Posts.Remove(product);
            _db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}
