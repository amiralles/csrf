namespace FooBakery.Controllers {
    using AutoMapper;
    using Infrastructure;
    using Models;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Web.Mvc;
    using System.Linq;

    public class StoreController : Controller {
        readonly ProductRepository _products;
        readonly BakeryDbContext _ctx = new BakeryDbContext();

        public StoreController() {
            Mapper.CreateMap<DealViewModel, Order>();
            _products = new ProductRepository();
        }

        [HttpGet]
        public ActionResult Buy(int productId) {
            return View(_products.GetById(productId));
        }

        [Authorize]//Only authenticated users can post orders.
        [HttpPost]
        public ActionResult Buy(DealViewModel deal) {
            return RedirectToAction("Chekout", deal);
        }

        [HttpGet]
        public ActionResult Index() {
            return View(_products.All);
        }

        public ActionResult Chekout(DealViewModel viewModel) {
            if (ModelState.IsValid) {
                using (var ctx= new BakeryDbContext()) {
                    var order = Mapper.Map<DealViewModel, Order>(viewModel);
                    order.User = HttpContext.User.Identity.Name;
                    ctx.Orders.AddOrUpdate(order);
                    ctx.SaveChanges();
                }
            }
            return RedirectToAction("Index");
        }

        [Authorize]
        public ActionResult MyOrders() {
            return View(_ctx.Orders.Where(o => o.User == HttpContext.User.Identity.Name));
        }
    }

    public class ProductRepository {
        public ProductRepository() {
            All = new List<DealViewModel> {
                new DealViewModel {
                    ProductId= 1,
                    ProductName = "6 Pack Cupcakes",
                    Price = 10
                },
                new DealViewModel {
                    ProductId= 2,
                    ProductName = "12 Pack Cupcakes",
                    Price = 18.95
                },
                new DealViewModel {
                    ProductId= 3,
                    ProductName = "Banana Pudding",
                    Price = 22.45
                },
                new DealViewModel {
                    ProductId= 4,
                    ProductName = "6 Pack Muffins",
                    Price = 14.05
                }
            };
        }

        public List<DealViewModel> All { get; set; }

        public object GetById(int productId) {
            return All.SingleOrDefault(p => p.ProductId == productId);
        }
    }
}