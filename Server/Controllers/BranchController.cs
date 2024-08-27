﻿using BaseLibrary.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServerLibrary.Repositories.Contracts;

namespace Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BranchController : GenericController<Branch>
    {
  
        public BranchController(IBranchRepository repository) : base(repository)
        {
           
        }


        // Add additional actions specific to GeneralDepartmentController if needed
    }
}