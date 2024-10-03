﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using BanSach.Models;
using PagedList;

namespace BanSach.Controllers
{
    public class SanPhamsController : Controller
    {
        private QuanLyBanSachEntities db = new QuanLyBanSachEntities();

        // GET: SanPhams
        public ActionResult Index()
        {
            var sanPhams = db.SanPhams.Include(s => s.DanhMuc);
            return View(sanPhams.ToList());
        }
        public ActionResult TrangChu()
        {
            return View();
        }
        public ActionResult ProductList(int? category, int? page, string SearchString)
        {
            var products = db.SanPhams.Include(p => p.DanhMuc);
           
          
            int pageSize = 8;           
            int pageNumber = (page ?? 1);
            if (page == null) page = 1;


            // Tìm kiếm chuỗi truy vấn theo category
            if (category == null)
            {
                products = db.SanPhams.OrderByDescending(x => x.TenSP);
            }
            else
            {
                products = db.SanPhams.OrderByDescending(x => x.TheLoai).Where(x => x.TheLoai == category);
            }

            // Tìm kiếm chuỗi truy vấn theo NamePro (SearchString)
            if (!String.IsNullOrEmpty(SearchString))
            {
                products = db.SanPhams.OrderByDescending(x => x.TheLoai).Where(s => s.TenSP.Contains(SearchString));
            }
          
            // Trả về các product được phân trang theo kích thước và số trang.
            return View(products.ToPagedList(pageNumber, pageSize));

        }

        // Xem SP
        public ActionResult TrangSP(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }
        // GET: SanPhams/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // GET: SanPhams/Create
        public ActionResult Create()
        {
            ViewBag.TheLoai = new SelectList(db.DanhMucs, "ID", "TheLoai");
            return View();
        }

        // POST: SanPhams/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IDsp,TenSP,MoTa,TheLoai,GiaBan,SoLuong,HinhAnh,TacGia,NhaXB,NamXB")] SanPham sanPham)
        {           
            if (ModelState.IsValid)
            {
                db.SanPhams.Add(sanPham);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.TheLoai = new SelectList(db.DanhMucs, "ID", "TheLoai", sanPham.TheLoai);
            return View(sanPham);
        }

        // GET: SanPhams/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham product = db.SanPhams.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.TheLoai = new SelectList(db.DanhMucs, "ID", "TheLoai", product.TheLoai);
            return View(product);
        }

        // POST: SanPhams/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IDsp,TenSP,MoTa,TheLoai,GiaBan,SoLuong,HinhAnh,TacGia,NhaXB,NamXB")] SanPham sanPham)
        {
            if (ModelState.IsValid)
            {                
                db.Entry(sanPham).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.TheLoai = new SelectList(db.DanhMucs, "ID", "TheLoai", sanPham.TheLoai);
            return View(sanPham);

        }

        // GET: SanPhams/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SanPham sanPham = db.SanPhams.Find(id);
            if (sanPham == null)
            {
                return HttpNotFound();
            }
            return View(sanPham);
        }

        // POST: SanPhams/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SanPham sanPham = db.SanPhams.Find(id);
            db.SanPhams.Remove(sanPham);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
