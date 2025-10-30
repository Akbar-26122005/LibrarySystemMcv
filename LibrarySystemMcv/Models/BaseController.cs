using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LibrarySystemMcv.Models {
    public abstract class BaseController : Controller {
        protected LibraryContext Context { get; }

        protected BaseController() {
            Context = new LibraryContext();
        }

        protected override void Dispose(bool disposing) {
            if (disposing) {
                Context.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}