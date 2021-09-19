﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ProjectI.WebApp.Core.Entities;
using ProjectI.WebApp.Core.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectI.WebApp.Api.Controller
{
    public class PositionsController : BaseController<Position>
    {
        public PositionsController(IBaseServices<Position> baseService) : base(baseService)
        {
        }
    }
}
