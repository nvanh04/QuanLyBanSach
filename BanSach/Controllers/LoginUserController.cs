﻿using BanSach.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BanSach.Controllers
{
    public class LoginUserController : Controller
    {
        QuanLyBanSachEntities db = new QuanLyBanSachEntities();
        // GET: LoginUser
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult Logout()
        {
            if (Session["Idkh"] != null)
            {
                Session["Idkh"] = null;
                Session["IDkh"] = null;
                Session["TenKH"] = null;
                Session["SoDT"] = null;
                return RedirectToAction("ProductList", "SanPhams");
            }
            if(Session["IdQly"] != null)
            {
                Session.Clear();
            }
            return RedirectToAction("ProductList", "SanPhams");
        }
        [HttpGet]
        public ActionResult RegisterCus()
        {
            return View();
        }
        [HttpPost]
        public ActionResult RegisterCus(KhachHang _user)
        {
            var check = db.KhachHangs.FirstOrDefault(s => s.TKhoan == _user.TKhoan);
            var check1 = db.Admins.FirstOrDefault(s => s.TKhoan == _user.TKhoan);
            var check2 = db.KhachHangs.FirstOrDefault(s => s.SoDT == _user.SoDT);
            var check3 = db.KhachHangs.FirstOrDefault(s=>s.Email == _user.Email);
            if (check != null || check1 != null)
            {
                ViewBag.ErrorRegister = "Tài khoản này đã có rồi !!!";
                return View();
            }
            if (check2 != null)
            {
                ViewBag.ErrorRegister = "Số điện thoại đã đăng kí !!!";
                return View();
            }
            if (check3 != null)
            {
                ViewBag.ErrorRegister = "Email đã đăng kí !!!";
                return View();
            }
            if (_user.MKhau != _user.ConfirmPass)
            {
                ViewBag.ErrorRegister = "Mật khẩu nhập lại không đúng!!!";
                return View();
            }
            if (ModelState.IsValid)
            {
                db.Configuration.ValidateOnSaveEnabled = false;
                db.KhachHangs.Add(_user);
                db.SaveChanges();
                return View("SignUpSuccess");
            }
            return View();
        }
            [HttpGet]
        public ActionResult SignUpSuccess()
        {
            return View();
        }
        [HttpGet]
        public ActionResult QuanTri()
        {
            return View();
        }


        [AcceptVerbs(HttpVerbs.Get | HttpVerbs.Post)]
        public ActionResult LoginAccountCus(KhachHang _cus)
        {
            // check là khách hàng cần tìm
            var check = db.KhachHangs.Where(s => s.TKhoan == _cus.TKhoan && s.MKhau == _cus.MKhau).FirstOrDefault();
            var check2 = db.Admins.Where(x => x.TKhoan == _cus.TKhoan && x.MKhau == _cus.MKhau).FirstOrDefault();
            if (_cus.TKhoan == null)
            {
                return View();
            }
            if (check2 != null)
            {
                Session["IDQly"] = check2.ID;
                Session["TenQly"] = check2.HoTen;
                Session["TKQly"] = check2.TKhoan;
                Session["Vaitro"] = check2.VaiTro;
                return RedirectToAction("ProductList", "SanPhams");
            }
            if (check == null)  //không có KH
            {
                ViewBag.ErrorInfo = "Sai tài khoản hoặc mật khẩu";
                return View();
            }              
                db.Configuration.ValidateOnSaveEnabled = false;
                Session["IDkh"] = check.IDkh;
                Session["MKhau"] = check.MKhau;
                Session["TenKH"] = check.TenKH;
                Session["SoDT"] = check.SoDT;
                
                return RedirectToAction("SignInSuccess");
            
        }
        [HttpGet]
        public ActionResult SignInSuccess()
        {
            return View();
        }

        [HttpGet]
        public ActionResult InfoCustomer()
        {
            return RedirectToAction("Details", "KhachHangs", new { id = Session["IDkh"] });
        }
    }


}