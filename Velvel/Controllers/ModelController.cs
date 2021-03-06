﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Velvel.Domain.Data;
using Velvel.Domain.Services;
using Velvel.Domain.Tables.Floorings;
using Velvel.Domain.Tables.Plumbings;
using Velvel.Models.Project.Attribute;

namespace Velvel.Controllers
{
    public class ModelController : PricedAttributeController<Model, ModelViewModel>
    {
        public ModelController(IPricedEntityService<Model> baseService, IProjectService projectService)
            : base(baseService, projectService)
        {
        }
    }
}