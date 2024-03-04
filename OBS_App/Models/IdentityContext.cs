﻿using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace OBS_App.Models
{
	public class IdentityContext : IdentityDbContext<AppUser, AppRole, string>
	{
        public IdentityContext(DbContextOptions<IdentityContext> options) : base(options)
        {
            
        }
    }
}
