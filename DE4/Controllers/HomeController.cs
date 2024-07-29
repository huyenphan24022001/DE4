using DE4.Models;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity;
using System.EnterpriseServices.CompensatingResourceManager;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace DE4.Controllers
{
    public class HomeController : Controller
    {
        DE4Entities db = new DE4Entities();

        public ActionResult Index()
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            List<TuyenTruyen> list = db.TuyenTruyens.Where(x => x.IsDelete == false).ToList();
            return View(list);
        }
        public ActionResult LichSu(int ID)
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            List<LichSu> list = db.LichSus.Where(x => x.IDTT == ID).ToList();
            return View(list);
        }
        public ActionResult DuyetBaiViet()
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            List<TuyenTruyen> list = db.TuyenTruyens.Where(x => x.IsDelete == false && x.DaDuyet == false).ToList();
            return View(list);
        }
        public ActionResult TuyenTruyen()
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            DateTime d = DateTime.Now;
            List<TuyenTruyen> list = db.TuyenTruyens.Where(x => x.IsDelete  == false && x.NgayBD.Value.Day <= d.Day && x.NgayBD.Value.Month <= d.Month && x.NgayBD.Value.Year <= d.Year && x.DaDuyet == true && x.NgayKT.Value.Day > d.Day && x.NgayKT.Value.Month == d.Month && x.NgayKT.Value.Year == d.Year).ToList();
            return View(list);
        }
        public ActionResult Duyet(int ID)
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            TuyenTruyen s = db.TuyenTruyens.SingleOrDefault(n => n.ID == ID);
            if (s == null) return RedirectToAction("Error", "Home", new { MaError = "Duyệt thất bại" });
            s.DaDuyet = true;
            bool check1 = Logging(s);
            if (check1)
            {
                db.SaveChanges();
                return Json(new { mess = "success" }, JsonRequestBehavior.AllowGet);
            };
            return Json(new { mess = "fail" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Dung(int ID)
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            TuyenTruyen s = db.TuyenTruyens.SingleOrDefault(n => n.ID == ID);
            if (s == null) return RedirectToAction("Error", "Home", new { MaError = "Dừng thất bại" });
            s.NgayKT = DateTime.Now;
            bool check1 = Logging(s);
            if (check1)
            {
                db.SaveChanges();
                return Json(new { mess = "success" }, JsonRequestBehavior.AllowGet);
            };
            return Json(new { mess = "fail" }, JsonRequestBehavior.AllowGet);
        }
        public ActionResult Login()
        {
            return View();
        }
        public ActionResult LogOut()
        {
            Session["Login"] = null;
            return View("Login");
        }
        public bool checkToken()
        {
            var access_token = Session["access_token"];
            if (access_token == null)
            {
                //return RedirectToAction("Login");
                return false;
            }
            else
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(Convert.ToString(ConfigurationManager.AppSettings["config:JwtKey"]));
                tokenHandler.ValidateToken(access_token.ToString(), new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = false,
                    ValidateAudience = false,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                // Corrected access to the validatedToken
                var jwtToken = (JwtSecurityToken)validatedToken;
                if (jwtToken.ValidTo < DateTime.UtcNow)
                {
                    return false;
                    //return RedirectToAction(Action);
                }


            }
            return true;
            //return RedirectToAction("Login");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Login user)
        {
            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(ConfigurationManager.AppSettings["config:JwtKey"]));
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

            string hashedPassword = HashPassword(user.Password, "12345!#aB");
            User u = db.Users.FirstOrDefault(x => x.UserName == user.UserName && x.Pass == hashedPassword && x.Role == 1); //pass: 12345


            if (u != null)
            {
                var claims = new[]
        { new Claim("ID", u.ID.ToString()),
                    new Claim("UserName", u.UserName),
                    new Claim("Role", u.Role.ToString())
                    // Add more claims if needed
                };

                var accessToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddHours(1), // Token expires in 1 hour
                    signingCredentials: credentials
                );

                var refreshToken = new JwtSecurityToken(
                    claims: claims,
                    expires: DateTime.UtcNow.AddDays(7), // Token expires in 7day
                    signingCredentials: credentials
                );
                var access_token = new JwtSecurityTokenHandler().WriteToken(accessToken);
                var refresh_token = new JwtSecurityTokenHandler().WriteToken(refreshToken);
                Models.Token to = new Models.Token()
                {
                    Users_ID = u.ID,
                    access_token = access_token,
                    refresh_token = refresh_token,
                };
                db.Tokens.Add(to);
                db.SaveChanges();

                Session["access_token"] = access_token;
                //Session["refresh_token"] = refresh_token;
                Session["Login"] = true;
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("", "Login data is incorrect!");
            }
            return View();
        }

        public static string HashPassword(string password, string salt)
        {
            using (var sha256 = SHA256.Create())
            {
                var saltedPassword = password + salt;
                var passwordBytes = Encoding.UTF8.GetBytes(saltedPassword);
                var hashBytes = sha256.ComputeHash(passwordBytes);
                return Convert.ToBase64String(hashBytes);
            }
        }

        public ActionResult Create()
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            return View();
        }

        public ActionResult Register()
        {
            
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(FormCollection collection, User u)
        {
            string pass = u.Pass;
            string rePass = collection["RePassword"];
            if (!pass.Equals(rePass))
            {
                return RedirectToAction("Error", "Home", new { @MaError = "Mật khẩu không trùng khớp!" });

            }
            if (db.Users.SingleOrDefault(x => x.UserName.Equals(u.UserName)) != null)
            {

                return RedirectToAction("Error", "Home", new { @MaError = "Tên Username đã tồn tại!" });


            }
            
            string hashedPassword = HashPassword(pass, "12345!#aB");
            User user = new User()
            {
                UserName = u.UserName,
                Pass = hashedPassword,
                Role = 1,

            };
            db.Users.Add(user);
            db.SaveChanges();
            return RedirectToAction("Success", "Home", new { Success = "Tạo tài khoản thành công" });
        }


        [HttpPost]
        public ActionResult Create(FormCollection collection, TuyenTruyen tt)
        {

            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            try
            {
               
                    tt.IsDelete = false;
              
                

                    db.TuyenTruyens.Add(tt);

                    db.SaveChanges();

                return RedirectToAction("Success", "Home", new { Success = "Thêm thành công" });

            }
            catch
            {
                return View();
            }
        }

        public ActionResult Details(int? id)
        {

            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenTruyen nen = db.TuyenTruyens.Find(id);
            if (nen == null)
            {
                return HttpNotFound();
            }
            return View(nen);
        }
        public bool Logging(TuyenTruyen tt)
        {

            NoiDung hi = new NoiDung()
            {
                 ID = tt.ID,
            TieuDe = tt.TieuDe,
             MoTa = tt.MoTa,
           NgayBD  = tt.NgayBD,

          NgayKT = tt.NgayKT,
            TenDonVi = tt.TenDonVi,
            IsDelete = tt.IsDelete,
            DaDuyet = tt.DaDuyet,
    };
            try
            {
                LichSu ls = new LichSu()
                {
                    NoiDungChinhSua = JsonConvert.SerializeObject(hi),
                    ChinhSuaBy = "Admin",
                    NgayChinhSua = DateTime.Now,
                    IDTT = tt.ID,

                };

                db.LichSus.Add(ls);
                db.SaveChanges();
                return true;
            } catch
            {
                return false;
            }
        }
        public ActionResult Error(string MaError)
        {
            ViewBag.Error = MaError;
            return View();
        }

        public ActionResult Success(string Success)
        {
            ViewBag.Success = Success;
            return View();
        }
        public ActionResult Edit(int id)
        {

            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            TuyenTruyen nen = db.TuyenTruyens.Find(id);
            if (nen == null)
            {
                return HttpNotFound();
            }
            return View(nen);
        }
        [HttpPost]
        public ActionResult Edit(TuyenTruyen nen, FormCollection collection)
        {
            //check token còn thời gian k
            bool check1 = checkToken();
            if (!check1)
            {
                return RedirectToAction("Login");
            }
            try
            {
                if (nen.ID != null)
                {
                    TuyenTruyen check = db.TuyenTruyens.SingleOrDefault(n => n.ID == nen.ID);
                    if (check == null) return RedirectToAction("Error", "Home", new { MaError = "Edit thất bại" });
                    if (nen.NgayKT != null)
                    {
                        check.NgayKT = nen.NgayKT;
                    }
                    if (nen.NgayBD != null)
                    {
                        check.NgayBD = nen.NgayBD;
                    }
                    check.IsDelete = false;
                    check.MoTa = nen.MoTa;
                    check.TenDonVi = nen.TenDonVi;
                    check.TieuDe = nen.TieuDe;

                    db.SaveChanges();
                    bool test = Logging(check);
                    if (test)
                    {
                       
                        return RedirectToAction("Success", "Home", new { Success = "Edit thành công" });
                    }

                }

            }
            catch (Exception e)
            {
                return RedirectToAction("Error", "Home", new { MaError = e.Message });
            }

            return RedirectToAction("Error", "Home", new { MaError = "Lỗi" });
        }

        public ActionResult Delete(int id)
        {
            //check token còn thời gian k
            bool check = checkToken();
            if (!check)
            {
                return RedirectToAction("Login");
            }
            if (id == null)
            {
                return Json(new { mess = "fail" }, JsonRequestBehavior.AllowGet);
            }
            TuyenTruyen nen = db.TuyenTruyens.Find(id);
            if (nen == null)
            {
                return Json(new { mess = "fail" }, JsonRequestBehavior.AllowGet);
            }
            nen.IsDelete = true;
            bool test = Logging(nen);
            if (test)
            {
                db.SaveChanges();
                db.SaveChanges();
                return Json(new { mess = "success" }, JsonRequestBehavior.AllowGet);
            }

            return Json(new { mess = "fail" }, JsonRequestBehavior.AllowGet);
        }

    }
}