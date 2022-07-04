using System;
using Microsoft.AspNetCore.Mvc;

namespace Fusap.Common.Model.Presenter.WebApi
{
    public class PresenterOptions
    {
        public Func<Error, ActionResult?>? ErrorRenderer { get; set; }
    }
}