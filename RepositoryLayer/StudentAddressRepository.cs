﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryLayer
{
    public class StudentAddressRepository : BaseRepository<StudentAddress>
    {
        public StudentAddressRepository(SchoolContext dbContext) : base(dbContext)
        {
        }
    }
}
