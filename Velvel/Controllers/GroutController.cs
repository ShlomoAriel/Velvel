using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Domain.Tables.Floorings;
using Velvel.Models.Project.Attribute;

namespace Velvel.Controllers
{
    public class GroutController : PricedAttributeController<Grout,GroutViewModel>
    {
        public GroutController(IPricedEntityService<Grout> baseService, IProjectService projectService) : base(baseService,projectService)
        {
        }
    }
}